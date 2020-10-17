using Lego_Pak_Explorer.ListViewSorter;
using Lego_Pak_Explorer.Structs;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

// ReSharper disable UnusedParameter.Local
// ReSharper disable AccessToModifiedClosure
// ReSharper disable UnusedMember.Local
// ReSharper disable NotAccessedField.Local
// ReSharper disable UnusedVariable

namespace Lego_Pak_Explorer.UI
{
    public partial class DatExtractor : Form
    {
        private readonly FileStream _hFs;
        private BinaryReader _hBr;
        private BinaryWriter _hBw;
        private DatFileEntry[] _filesArray = new DatFileEntry[0];
        private readonly PkgInfo _pkgInfo;
        private readonly int[] _crcArray = new int[0];
        private ListViewColumnSorter _lvs;
        private Control _focused;

        [DllImport("TTGames.UnComp.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Un_LZ2K(IntPtr @in, IntPtr @out, int inSz, int outSz);

        [DllImport("TTGames.UnComp.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Un_DFLT(IntPtr @in, IntPtr @out, int inSz, int outSz);

        public DatExtractor(string filePath)
        {
            InitializeComponent();
            _listView1.Items.Clear();
            _treeView1.Nodes.Clear();
            _treeView1.Sort();
            _extractAllToolStripMenuItem.Enabled = false;
            var path = _pkgInfo.FilePath = filePath;
            if (File.Exists(Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + ".HDR"))
            {
                path = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + ".HDR";
                _pkgInfo.Version = 2;
            }
            else
                _pkgInfo.Version = 1;
            try
            {
                _hFs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
                _hBr = new BinaryReader(_hFs);
                if (_pkgInfo.Version == 2)
                {
                    _pkgInfo.InfoFilesOffset = 0U;
                    _pkgInfo.InfoFilesSize = ReverseBytes(_hBr.ReadUInt32());
                }
                else
                {
                    _pkgInfo.InfoFilesOffset = _hBr.ReadUInt32();
                    _pkgInfo.InfoFilesSize = _hBr.ReadUInt32();
                    _hFs.Seek(_pkgInfo.InfoFilesOffset, SeekOrigin.Begin);
                }
                _pkgInfo.TypeUnk = _hBr.ReadUInt32();
                _pkgInfo.FilesNumber = _hBr.ReadUInt32();
                _pkgInfo.InfoFilesOffset = (uint)_hFs.Position;
                _pkgInfo.NamesNumberOffset = (uint)_hFs.Position + _pkgInfo.FilesNumber * 16U;
                _hFs.Seek(_pkgInfo.NamesNumberOffset, SeekOrigin.Begin);
                _pkgInfo.NamesNumber = _hBr.ReadUInt32();
                _pkgInfo.NameFieldSize = 8U;
                if (_pkgInfo.TypeUnk <= 4294967291U)
                    _pkgInfo.NameFieldSize = 12U;
                _pkgInfo.NamesCrcOffset = (uint)_hFs.Position + _pkgInfo.NamesNumber * _pkgInfo.NameFieldSize;
                _hFs.Seek(_pkgInfo.NamesCrcOffset, SeekOrigin.Begin);
                _pkgInfo.NamesCrcOffset += _hBr.ReadUInt32() + 4U;
                _pkgInfo.NamesOffset = (uint)_hFs.Position;
                Array.Resize(ref _crcArray, (int)_pkgInfo.FilesNumber);
                for (var index = 0; index < (int)_pkgInfo.FilesNumber; ++index)
                {
                    _hFs.Seek(_pkgInfo.NamesCrcOffset + index * 4, SeekOrigin.Begin);
                    _crcArray[index] = IntReverseBytes(_hBr.ReadInt32());
                }
                _pkgInfo.DirNumber = _pkgInfo.NamesNumber - _pkgInfo.FilesNumber;
                _toolStripStatusLabel1.Text =
                    $@"{Path.GetFileName(filePath)} | {_pkgInfo.DirNumber} dir(s) | {_pkgInfo.FilesNumber} file(s)";
                _textBox1.Text = $@"0x{_pkgInfo.InfoFilesOffset:X4}";
                _textBox2.Text = $@"0x{(_pkgInfo.NamesNumberOffset + 4U):X4}";
                _textBox3.Text = $@"0x{_pkgInfo.NamesCrcOffset:X4}";
                _textBox4.Text = $@"0x{_pkgInfo.NamesOffset:X4}";
                if (_toolStripProgressBar1.ProgressBar != null)
                    _toolStripProgressBar1.ProgressBar.Maximum = (int)_pkgInfo.FilesNumber;
                new Thread(ReadFileNameInfos)
                {
                    IsBackground = true
                }.Start();
            }
            catch (Exception)
            {
                MessageBox.Show(@"Error when trying to read file infos! Program will exit!");
                Application.Exit();
            }
        }

        private string ReadFileName(uint offset)
        {
            if (offset > _pkgInfo.NamesCrcOffset)
                return "";
            _hFs.Seek(offset, SeekOrigin.Begin);
            if (_hBr.ReadByte() > 240)
                return "";
            _hFs.Seek(offset, SeekOrigin.Begin);
            var array = new byte[200];
            var flag = true;
            var newSize = 0;
            while (flag)
            {
                var num = _hBr.ReadByte();
                if (num == 0)
                {
                    Array.Resize(ref array, newSize);
                    flag = false;
                }
                else
                {
                    array[newSize] = num;
                    ++newSize;
                }
            }
            return Encoding.UTF8.GetString(array, 0, array.Length);
        }

        private static TreeNode LocateNode(string path, TreeNodeCollection treeCol)
        {
            var strArray = path.Split('\\');
            for (var index = 0; index < treeCol.Count; ++index)
            {
                if (treeCol[index].Text == strArray[0])
                    return treeCol[index].Nodes.Count == 0 || strArray.Length == 1 ? treeCol[index] : LocateNode(path.Remove(0, strArray[0].Length + 1), treeCol[index].Nodes);
            }
            return null;
        }

        private void ReadFileNameInfos()
        {
            var num1 = _pkgInfo.NamesNumberOffset + 4U;
            var str1 = "";
            var index = 0;
            var namesArray = new DatFileName[(int)_pkgInfo.FilesNumber];
            Array.Resize(ref _filesArray, (int)_pkgInfo.FilesNumber);
            var strArray = new string[(_pkgInfo.FilesNumber * 16U)];
            for (var i = 0; i < (int)_pkgInfo.FilesNumber; ++i)
            {
                do
                {
                    _hFs.Seek(num1, SeekOrigin.Begin);
                    namesArray[i].Next = _hBr.ReadUInt16();
                    namesArray[i].Prev = _hBr.ReadUInt16();
                    namesArray[i].Offset = _hBr.ReadUInt32();
                    if (_pkgInfo.TypeUnk <= 4294967291U)
                    {
                        var num2 = (int)_hBr.ReadUInt32();
                    }
                    num1 = (uint)_hFs.Position;
                    namesArray[i].Offset += _pkgInfo.NamesOffset;
                    namesArray[i].Name = ReadFileName(namesArray[i].Offset);
                    if ((namesArray[i].Next & 32768) > 0)
                        namesArray[i].Next |= -65536;
                    if (namesArray[i].Prev != 0U)
                        str1 = strArray[namesArray[i].Prev];
                    strArray[index] = str1;
                    if (namesArray[i].Next > 0)
                    {
                        var str2 = strArray[namesArray[i].Prev];
                        if (str2 != "")
                        {
                            var _ = str1.Insert(0, Path.DirectorySeparatorChar + str2 + Path.DirectorySeparatorChar);
                        }

                        if (namesArray[i].Name != "")
                        {
                            if (str1 == "")
                            {
                                var blah2 = LocateNode("/", _treeView1.Nodes);
                                _treeView1.Invoke((MethodInvoker)delegate { blah2.Nodes.Add(namesArray[i].Name, namesArray[i].Name); });
                            }
                            else
                            {
                                var blah2 = LocateNode("/\\" + str1.Remove(str1.Length - 1), _treeView1.Nodes);
                                _treeView1.Invoke((MethodInvoker)delegate { blah2.Nodes.Add(namesArray[i].Name, namesArray[i].Name); });
                            }
                            str1 = str1 + namesArray[i].Name + Path.DirectorySeparatorChar;
                        }
                        else
                            _treeView1.Invoke((MethodInvoker)delegate { _treeView1.Nodes.Add("/", "/"); });
                    }
                    ++index;
                }
                while (namesArray[i].Next > 0);
                var str3 = str1 + namesArray[i].Name;
                _filesArray[i].Parent = str1;
                _filesArray[i].Name = namesArray[i].Name;
                var num3 = -2128831035;
                foreach (var ch in str3)
                {
                    int num2 = Convert.ToByte(ch);
                    if (num2 >= 97 && num2 <= 122)
                        num2 -= 32;
                    num3 = (num3 ^ num2) * 1677619;
                }
                var val = num3 & -1;
                _filesArray[i].Crc = IntReverseBytes(val);
                _hFs.Seek(_pkgInfo.InfoFilesOffset + Array.IndexOf(_crcArray, _filesArray[i].Crc) * 16, SeekOrigin.Begin);
                _filesArray[i].Offset = _hBr.ReadUInt32();
                _filesArray[i].Offset <<= 8;
                _filesArray[i].Size = _hBr.ReadUInt32();
                _filesArray[i].SizeUnComp = _hBr.ReadUInt32();
                _filesArray[i].Pack = _hBr.ReadByte();
                _filesArray[i].Pack += _hBr.ReadByte();
                _filesArray[i].Pack += _hBr.ReadByte();
                _filesArray[i].Offset2 = _hBr.ReadByte();
                _filesArray[i].Offset += _filesArray[i].Offset2;
                _toolStripProgressBar1.ProgressBar?.Invoke((MethodInvoker)delegate
                {
                    _toolStripProgressBar1.ProgressBar.Value = i;
                    var num2 = (int)(_toolStripProgressBar1.ProgressBar.Value / (double)_toolStripProgressBar1.ProgressBar.Maximum * 100.0);
                    _toolStripProgressBar1.ProgressBar.CreateGraphics().DrawString(num2 + "%", new Font("Arial", 8.25f, FontStyle.Regular), Brushes.Gray, new PointF(_toolStripProgressBar1.ProgressBar.Width / 2 - 10, _toolStripProgressBar1.ProgressBar.Height / 2 - 7));
                });
            }
            _listView1.Invoke((MethodInvoker)delegate { _listView1.Enabled = true; });
            _treeView1.Invoke((MethodInvoker)delegate
            {
                _treeView1.Enabled = true;
                _treeView1.Sort();
                _treeView1.Nodes[0].Expand();
                _treeView1.SelectedNode = _treeView1.Nodes[0];
                _extractAllToolStripMenuItem.Enabled = true;
                _treeView1.Focus();
            });
            _hFs.Close();
        }

        private bool ExtractFile(int fileId, string extractFolder)
        {
            try
            {
                var fileStream1 = new FileStream(_pkgInfo.FilePath, FileMode.Open, FileAccess.Read);
                _hBr = new BinaryReader(fileStream1);
                var fileStream2 = new FileStream(extractFolder + _filesArray[fileId].Name, FileMode.Create, FileAccess.Write);
                _hBw = new BinaryWriter(fileStream2);
                fileStream1.Seek(_filesArray[fileId].Offset, SeekOrigin.Begin);
                var numArray1 = new byte[_filesArray[fileId].Size];
                _hBr.Read(numArray1, 0, (int)_filesArray[fileId].Size);
                var str = Encoding.UTF8.GetString(numArray1, 0, 4);
                if (str == "LZ2K")
                {
                    var memoryStream = new MemoryStream(numArray1);
                    var binaryReader = new BinaryReader(memoryStream);
                    var num1 = 4;
                    var num2 = 0;
                    var buffer = new byte[_filesArray[fileId].SizeUnComp];
                    var num3 = 0;
                    while (num3 < (int)_filesArray[fileId].Size)
                    {
                        memoryStream.Seek(num1, SeekOrigin.Begin);
                        var num4 = binaryReader.ReadUInt32();
                        var num5 = binaryReader.ReadUInt32();
                        var numArray2 = new byte[num5];
                        var source = new byte[num4];
                        binaryReader.Read(numArray2, 0, (int)num5);
                        var num6 = Marshal.AllocHGlobal(Marshal.SizeOf(numArray2[0]) * numArray2.Length);
                        Marshal.Copy(numArray2, 0, num6, numArray2.Length);
                        var num7 = Marshal.AllocHGlobal(Marshal.SizeOf(source[0]) * source.Length);
                        Marshal.Copy(source, 0, num7, source.Length);
                        Un_LZ2K(num6, num7, (int)num5, (int)num4);
                        var destination = new byte[(int)num4];
                        Marshal.Copy(num7, destination, 0, (int)num4);
                        Array.Copy(destination, 0L, buffer, num2, num4);
                        num2 += (int)num4;
                        num1 += (int)num5 + 12;
                        num3 = num3 + 12 + (int)num5;
                        Marshal.FreeHGlobal(num6);
                        Marshal.FreeHGlobal(num7);
                    }
                    _hBw.Write(buffer);
                }
                else if (str == "DFLT")
                {
                    var memoryStream = new MemoryStream(numArray1);
                    var binaryReader = new BinaryReader(memoryStream);
                    var num1 = 4;
                    var num2 = 0;
                    var buffer = new byte[_filesArray[fileId].SizeUnComp];
                    var num3 = 0;
                    while (num3 < (int)_filesArray[fileId].Size)
                    {
                        memoryStream.Seek(num1, SeekOrigin.Begin);
                        var num4 = binaryReader.ReadUInt32();
                        var num5 = binaryReader.ReadUInt32();
                        var numArray2 = new byte[num4];
                        var source = new byte[num5];
                        binaryReader.Read(numArray2, 0, (int)num4);
                        var num6 = Marshal.AllocHGlobal(Marshal.SizeOf(numArray2[0]) * numArray2.Length);
                        Marshal.Copy(numArray2, 0, num6, numArray2.Length);
                        var num7 = Marshal.AllocHGlobal(Marshal.SizeOf(source[0]) * source.Length);
                        Marshal.Copy(source, 0, num7, source.Length);
                        Un_DFLT(num6, num7, (int)num4, (int)num5);
                        var destination = new byte[(int)num5];
                        Marshal.Copy(num7, destination, 0, (int)num5);
                        Array.Copy(destination, 0L, buffer, num2, num5);
                        num2 += (int)num5;
                        num1 += (int)num4 + 12;
                        num3 = num3 + 12 + (int)num4;
                        Marshal.FreeHGlobal(num6);
                        Marshal.FreeHGlobal(num7);
                    }
                    _hBw.Write(buffer);
                }
                else
                    _hBw.Write(numArray1);
                fileStream1.Close();
                fileStream2.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static uint ReverseBytes(uint value) => (uint)(((int)value & byte.MaxValue) << 24 | ((int)value & 65280) << 8) | (value & 16711680U) >> 8 | (value & 4278190080U) >> 24;

        private static int IntReverseBytes(int val)
        {
            var bytes = BitConverter.GetBytes(val);
            Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _listView1.Items.Clear();
            _lvs = new ListViewColumnSorter();
            _lvs.Initialize(_listView1, "num,text,text,text,num,num,text", null);
            for (var index = 0; index < _filesArray.Length; ++index)
            {
                if (e.Node.FullPath.Length > 2)
                {
                    if (_filesArray[index].Parent != e.Node.FullPath.Remove(0, 2) + "\\") continue;

                    var items = new[]
                    {
                        _filesArray[index].Name,
                        $"0x{_filesArray[index].Crc:X8}",
                        $"0x{_filesArray[index].Offset:X8}",
                        _filesArray[index].SizeUnComp.ToString(),
                        _filesArray[index].Size.ToString(),
                        $"0x{_filesArray[index].Pack:X3}"
                    };
                    _listView1.Items.Add(index.ToString()).SubItems.AddRange(items);
                }
                else if (_filesArray[index].Parent == "")
                {
                    var items = new[]
                    {
            _filesArray[index].Name,
            $"0x{_filesArray[index].Crc:X8}",
            $"0x{_filesArray[index].Offset:X8}",
            _filesArray[index].SizeUnComp.ToString(),
            _filesArray[index].Size.ToString(),
            $"0x{_filesArray[index].Pack:X3}"
                    };
                    _listView1.Items.Add(index.ToString()).SubItems.AddRange(items);
                }
            }
            _lvs.lv_ColumnClick(this, new ColumnClickEventArgs(1));
            _listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            _listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void ExtractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _folderBrowserDialog1.SelectedPath = Path.GetDirectoryName(_pkgInfo.FilePath);
            if (_folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                return;
            foreach (ListViewItem selectedItem in _listView1.SelectedItems)
            {
                MessageBox.Show(
                    ExtractFile(int.Parse(selectedItem.SubItems[0].Text), _folderBrowserDialog1.SelectedPath + "\\")
                        ? $@"{selectedItem.SubItems[1].Text} extracted!"
                        : @"Error during file extracting!");
            }
        }

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (_listView1.SelectedItems.Count > 0)
                return;
            e.Cancel = true;
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _folderBrowserDialog1.SelectedPath = Path.GetDirectoryName(_pkgInfo.FilePath);
            if (_folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                return;
            for (var fileId = 0; fileId < (int)_pkgInfo.FilesNumber; ++fileId)
            {
                var strArray = Path.GetDirectoryName(_filesArray[fileId].Parent + _filesArray[fileId].Name)?.Split('\\');
                var str1 = "";
                if (strArray != null)
                    foreach (var str2 in strArray)
                    {
                        str1 = str1 + str2 + "\\";
                        if (!Directory.Exists(_folderBrowserDialog1.SelectedPath + "\\" + str1))
                            Directory.CreateDirectory(_folderBrowserDialog1.SelectedPath + "\\" + str1);
                    }

                ExtractFile(fileId, _folderBrowserDialog1.SelectedPath + "\\" + _filesArray[fileId].Parent);
            }
            MessageBox.Show(@"Extract Finish!");
        }

        private void PlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileStream = new FileStream(_pkgInfo.FilePath, FileMode.Open, FileAccess.ReadWrite);
            _hBr = new BinaryReader(fileStream);
            var int32 = Convert.ToInt32(_listView1.SelectedItems[0].SubItems[0].Text);
            fileStream.Seek(_filesArray[int32].Offset, SeekOrigin.Begin);
            var buffer = new byte[_filesArray[int32].Size];
            _hBr.Read(buffer, 0, (int)_filesArray[int32].Size);
            fileStream.Close();
            new SoundPlayer(new MemoryStream(buffer, true)).Play();
        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e) => Close();

        private void SplitContainer1_MouseDown(object sender, MouseEventArgs e) => _focused = GetFocused(Controls);

        private static Control GetFocused(IEnumerable controls)
        {
            foreach (Control control in controls)
            {
                if (control.Focused)
                    return control;
                if (control.ContainsFocus)
                    return GetFocused(control.Controls);
            }
            return null;
        }
    }
}