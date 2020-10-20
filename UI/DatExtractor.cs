using NAudio.Vorbis;
using NAudio.Wave;
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
using TT_Games_Explorer.Common;
using TT_Games_Explorer.ListViewSorter;
using TT_Games_Explorer.Renderer.Textures;
using TT_Games_Explorer.Structs.DatFormat;

// ReSharper disable LocalizableElement

// ReSharper disable UnusedParameter.Local
// ReSharper disable AccessToModifiedClosure
// ReSharper disable UnusedMember.Local
// ReSharper disable NotAccessedField.Local
// ReSharper disable UnusedVariable

namespace TT_Games_Explorer.UI
{
    public partial class DatExtractor : Form
    {
        private FileStream _hFs;
        private BinaryReader _extractInReader;
        private BinaryWriter _extractOutWriter;
        private DatFileEntry[] _filesArray = new DatFileEntry[0];
        private DatPkgInfo _pkgInfo;
        private int[] _crcArray = new int[0];
        private ListViewColumnSorter _lvs;

        [DllImport("TTGames.UnComp.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Un_LZ2K(IntPtr @in, IntPtr @out, int inSz, int outSz);

        [DllImport("TTGames.UnComp.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Un_DFLT(IntPtr @in, IntPtr @out, int inSz, int outSz);

        public DatExtractor(string filePath)
        {
            //designer specific stuff
            InitializeComponent();

            //assign file name
            _pkgInfo.FilePath = filePath;
        }

        private void SystemSetup()
        {
            //UI setup
            lstMain.Items.Clear();
            trvMain.Nodes.Clear();
            trvMain.Sort();
            itmExtractAll.Enabled = false;

            //parsing
            var path = _pkgInfo.FilePath;
            if (File.Exists(Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + ".HDR"))
            {
                path = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + ".HDR";
                _pkgInfo.Version = 2;
            }
            else
                _pkgInfo.Version = 1;

            //attempt file read
            try
            {
                _hFs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
                _extractInReader = new BinaryReader(_hFs);
                if (_pkgInfo.Version == 2)
                {
                    _pkgInfo.InfoFilesOffset = 0U;
                    _pkgInfo.InfoFilesSize = Methods.ReverseBytes(_extractInReader.ReadUInt32());
                }
                else
                {
                    _pkgInfo.InfoFilesOffset = _extractInReader.ReadUInt32();
                    _pkgInfo.InfoFilesSize = _extractInReader.ReadUInt32();
                    _hFs.Seek(_pkgInfo.InfoFilesOffset, SeekOrigin.Begin);
                }
                _pkgInfo.TypeUnk = _extractInReader.ReadUInt32();
                _pkgInfo.FilesNumber = _extractInReader.ReadUInt32();
                _pkgInfo.InfoFilesOffset = (uint)_hFs.Position;
                _pkgInfo.NamesNumberOffset = (uint)_hFs.Position + _pkgInfo.FilesNumber * 16U;
                _hFs.Seek(_pkgInfo.NamesNumberOffset, SeekOrigin.Begin);
                _pkgInfo.NamesNumber = _extractInReader.ReadUInt32();
                _pkgInfo.NameFieldSize = 8U;
                if (_pkgInfo.TypeUnk <= 4294967291U)
                    _pkgInfo.NameFieldSize = 12U;
                _pkgInfo.NamesCrcOffset = (uint)_hFs.Position + _pkgInfo.NamesNumber * _pkgInfo.NameFieldSize;
                _hFs.Seek(_pkgInfo.NamesCrcOffset, SeekOrigin.Begin);
                _pkgInfo.NamesCrcOffset += _extractInReader.ReadUInt32() + 4U;
                _pkgInfo.NamesOffset = (uint)_hFs.Position;
                Array.Resize(ref _crcArray, (int)_pkgInfo.FilesNumber);
                for (var index = 0; index < (int)_pkgInfo.FilesNumber; ++index)
                {
                    _hFs.Seek(_pkgInfo.NamesCrcOffset + index * 4, SeekOrigin.Begin);
                    _crcArray[index] = Methods.IntReverseBytes(_extractInReader.ReadInt32());
                }
                _pkgInfo.DirNumber = _pkgInfo.NamesNumber - _pkgInfo.FilesNumber;
                _toolStripStatusLabel1.Text =
                    $@"{Path.GetFileName(path)} | {_pkgInfo.DirNumber} dir(s) | {_pkgInfo.FilesNumber} file(s)";
                txtFileInfo.Text = $@"0x{_pkgInfo.InfoFilesOffset:X4}";
                txtNameInfo.Text = $@"0x{(_pkgInfo.NamesNumberOffset + 4U):X4}";
                txtNameCrc.Text = $@"0x{_pkgInfo.NamesCrcOffset:X4}";
                txtName.Text = $@"0x{_pkgInfo.NamesOffset:X4}";
                if (pbMain.ProgressBar != null)
                    pbMain.ProgressBar.Maximum = (int)_pkgInfo.FilesNumber;
                new Thread(ReadFileNameInfo)
                {
                    IsBackground = true
                }.Start();
            }
            catch (Exception)
            {
                MessageBox.Show(@"Error whilst trying to read file information. The program will now exit.");
                Application.Exit();
            }
        }

        private string ReadFileName(uint offset)
        {
            if (offset > _pkgInfo.NamesCrcOffset)
                return "";
            _hFs.Seek(offset, SeekOrigin.Begin);
            if (_extractInReader.ReadByte() > 240)
                return "";
            _hFs.Seek(offset, SeekOrigin.Begin);
            var array = new byte[200];
            var flag = true;
            var newSize = 0;
            while (flag)
            {
                var num = _extractInReader.ReadByte();
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

        private void ReadFileNameInfo()
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
                    namesArray[i].Next = _extractInReader.ReadUInt16();
                    namesArray[i].Prev = _extractInReader.ReadUInt16();
                    namesArray[i].Offset = _extractInReader.ReadUInt32();
                    if (_pkgInfo.TypeUnk <= 4294967291U)
                    {
                        var num2 = (int)_extractInReader.ReadUInt32();
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
                                var blah2 = LocateNode("/", trvMain.Nodes);
                                trvMain.Invoke((MethodInvoker)delegate { blah2.Nodes.Add(namesArray[i].Name, namesArray[i].Name); });
                            }
                            else
                            {
                                var blah2 = LocateNode("/\\" + str1.Remove(str1.Length - 1), trvMain.Nodes);
                                trvMain.Invoke((MethodInvoker)delegate { blah2.Nodes.Add(namesArray[i].Name, namesArray[i].Name); });
                            }
                            str1 = str1 + namesArray[i].Name + Path.DirectorySeparatorChar;
                        }
                        else
                            trvMain.Invoke((MethodInvoker)delegate { trvMain.Nodes.Add("/", "/"); });
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
                _filesArray[i].Crc = Methods.IntReverseBytes(val);
                _hFs.Seek(_pkgInfo.InfoFilesOffset + Array.IndexOf(_crcArray, _filesArray[i].Crc) * 16, SeekOrigin.Begin);
                _filesArray[i].Offset = _extractInReader.ReadUInt32();
                _filesArray[i].Offset <<= 8;
                _filesArray[i].Size = _extractInReader.ReadUInt32();
                _filesArray[i].SizeUnComp = _extractInReader.ReadUInt32();
                _filesArray[i].Pack = _extractInReader.ReadByte();
                _filesArray[i].Pack += _extractInReader.ReadByte();
                _filesArray[i].Pack += _extractInReader.ReadByte();
                _filesArray[i].Offset2 = _extractInReader.ReadByte();
                _filesArray[i].Offset += _filesArray[i].Offset2;
                pbMain.ProgressBar?.Invoke((MethodInvoker)delegate
                {
                    pbMain.ProgressBar.Value = i;
                    var num2 = (int)(pbMain.ProgressBar.Value / (double)pbMain.ProgressBar.Maximum * 100.0);
                    pbMain.ProgressBar.CreateGraphics().DrawString(num2 + "%", new Font("Arial", 8.25f, FontStyle.Regular), Brushes.Gray, new PointF(pbMain.ProgressBar.Width / 2 - 10, pbMain.ProgressBar.Height / 2 - 7));
                });
            }
            lstMain.Invoke((MethodInvoker)delegate
           {
               lstMain.Enabled = true;
           });
            trvMain.Invoke((MethodInvoker)delegate
            {
                trvMain.Enabled = true;
                trvMain.Sort();
                trvMain.Nodes[0].Expand();
                trvMain.SelectedNode = trvMain.Nodes[0];
                itmExtractAll.Enabled = true;
                trvMain.Focus();
            });

            //release file handle
            _hFs.Close();
        }

        private byte[] ExtractFile(int fileId)
        {
            try
            {
                //open the .DAT file up as a stream
                var inFileStream = new FileStream(_pkgInfo.FilePath, FileMode.Open, FileAccess.Read);

                //create a binary reader from the .DAT file stream
                _extractInReader = new BinaryReader(inFileStream);

                //seek to the file's offset that we're trying to extract
                inFileStream.Seek(_filesArray[fileId].Offset, SeekOrigin.Begin);

                //assign a new memory space for the file
                var rawFileBytes = new byte[_filesArray[fileId].Size];

                //use the binary reader from above to read in the entire compressed file's size
                //NOTE: the file stream has already offset itself to the correct location, so this reads on from there.
                _extractInReader.Read(rawFileBytes, 0, (int)_filesArray[fileId].Size);

                //first 4 bytes of the raw file dictates its compression function
                var algorithm = Encoding.UTF8.GetString(rawFileBytes, 0, 4);

                //test whether a valid compression function has been applied
                if (algorithm == @"DFLT" || algorithm == @"LZ2K")
                {
                    //the bytes of the unprocessed file above are loaded into a memory stream
                    var memoryStream = new MemoryStream(rawFileBytes);

                    //then, they're put into a binary reader for seeking
                    var binaryReader = new BinaryReader(memoryStream);

                    //seek past the string id (we already checked for that above; "DFLT" and "LZ2K")
                    var seekTo = 4;

                    //we're at index 0 of the file byte array to start off with
                    var fileOffset = 0;

                    //assign a new buffer for the uncompressed file
                    var buffer = new byte[_filesArray[fileId].SizeUnComp];

                    //counter; counts until all bytes in the compressed file have been accounted for
                    var totalProcessed = 0;

                    //let's go!
                    while (totalProcessed < (int)_filesArray[fileId].Size)
                    {
                        memoryStream.Seek(seekTo, SeekOrigin.Begin);

                        //for some weird reason, the algorithm will only accept two different variables of the same value
                        var outSize = binaryReader.ReadUInt32();
                        var inSize = binaryReader.ReadUInt32();

                        //the two process buffers
                        var finalBuffer = new byte[inSize];
                        var sourceBuffer = new byte[outSize];

                        //fill the output array
                        binaryReader.Read(finalBuffer, 0, (int)inSize);

                        //the pointer that will allocate an unmanaged memory space for the initial process buffer
                        var inBytes = Marshal.AllocHGlobal(Marshal.SizeOf(finalBuffer[0]) * finalBuffer.Length);

                        //allocate the finalBuffer bytes to the new pointer above
                        Marshal.Copy(finalBuffer, 0, inBytes, finalBuffer.Length);

                        //the pointer that will allocate an unmanaged memory space for the final process buffer
                        var outBytes = Marshal.AllocHGlobal(Marshal.SizeOf(sourceBuffer[0]) * sourceBuffer.Length);

                        //allocate the sourceBuffer bytes to the new pointer above
                        Marshal.Copy(sourceBuffer, 0, outBytes, sourceBuffer.Length);

                        //detect proper decompression function
                        switch (algorithm)
                        {
                            //Deflate algorithm
                            case @"DFLT":
                                Un_DFLT(inBytes, outBytes, (int)inSize, (int)outSize);
                                break;

                            //TT Games weird algorithm
                            case @"LZ2K":
                                Un_LZ2K(inBytes, outBytes, (int)inSize, (int)outSize);
                                break;
                        }

                        //allocate a new array for the processed result
                        var destination = new byte[(int)outSize];

                        //copy the buffer from the unmanaged code to the new array above
                        Marshal.Copy(outBytes, destination, 0, (int)outSize);

                        //apply it to the processed buffer
                        Array.Copy(destination, 0L, buffer, fileOffset, outSize);

                        //move the file offsets along so as to process the next entry
                        fileOffset += (int)outSize;
                        seekTo += (int)inSize + 12;
                        totalProcessed += +12 + (int)inSize;

                        //free up the global pointers allocated earlier to the unmanaged code
                        Marshal.FreeHGlobal(inBytes);
                        Marshal.FreeHGlobal(outBytes);
                    }

                    return buffer;
                }

                //release the existing streams
                inFileStream.Close();

                //default to here for no compression algorithm (return as-is)
                return rawFileBytes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"File extraction error:\n\n{ex}");
            }

            //default to here if an error occurs (errors will skip 'return rawFileBytes')
            return null;
        }

        private bool ExtractFile(int fileId, string extractFolder)
        {
            try
            {
                var outFileStream = new FileStream(extractFolder + _filesArray[fileId].Name, FileMode.Create, FileAccess.Write);
                _extractOutWriter = new BinaryWriter(outFileStream);

                //fetch the file bytes and decompress if necessary
                var bytes = ExtractFile(fileId);

                //flush bytes to file (if valid)
                if (bytes != null)
                    _extractOutWriter.Write(bytes);

                //release binary handle
                _extractOutWriter.Close();

                //release file handle
                outFileStream.Close();

                //report success
                return true;
            }
            catch (Exception)
            {
                //ignore
            }

            //default
            return false;
        }

        private void TrvMain_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lstMain.Items.Clear();
            _lvs = new ListViewColumnSorter();
            _lvs.Initialize(lstMain, "num,text,text,text,num,num,text", null);
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
                    lstMain.Items.Add(index.ToString()).SubItems.AddRange(items);
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
                    lstMain.Items.Add(index.ToString()).SubItems.AddRange(items);
                }
            }
            _lvs.lv_ColumnClick(this, new ColumnClickEventArgs(1));
            lstMain.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lstMain.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void ExtractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fbdExtractFolder.SelectedPath = Path.GetDirectoryName(_pkgInfo.FilePath);
            if (fbdExtractFolder.ShowDialog() != DialogResult.OK)
                return;
            foreach (ListViewItem selectedItem in lstMain.SelectedItems)
            {
                MessageBox.Show(
                    ExtractFile(int.Parse(selectedItem.SubItems[0].Text), fbdExtractFolder.SelectedPath + "\\")
                        ? $@"{selectedItem.SubItems[1].Text} extracted!"
                        : @"Error during file extracting!");
            }
        }

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (lstMain.SelectedItems.Count > 0)
            {
                //grab file name and file extension for verification
                var fileName = lstMain.SelectedItems[0].SubItems[1].Text;
                var ext = Path.GetExtension(fileName).ToLower();

                //is it an image?
                if (ext == @".png" ||
                    ext == @".tex" ||
                    ext == @".dds" ||
                    ext == @".jpg" ||
                    ext == @".jpeg" ||
                    ext == @".bmp" ||
                    ext == @".gif")
                {
                    //yes, enable the view option in the context menu
                    if (!cxtLstExtract.Items.Contains(itmCxtPreviewTexture))
                        cxtLstExtract.Items.Add(itmCxtPreviewTexture);
                }
                else
                    //no, disable the view option in the context menu
                    if (cxtLstExtract.Items.Contains(itmCxtPreviewTexture))
                    cxtLstExtract.Items.Remove(itmCxtPreviewTexture);
            }
            else
                e.Cancel = true;
        }

        private void ExtractAllMenuItem_Click(object sender, EventArgs e)
        {
            fbdExtractFolder.SelectedPath = Path.GetDirectoryName(_pkgInfo.FilePath);
            if (fbdExtractFolder.ShowDialog() != DialogResult.OK)
                return;
            for (var fileId = 0; fileId < (int)_pkgInfo.FilesNumber; ++fileId)
            {
                var strArray = Path.GetDirectoryName(_filesArray[fileId].Parent + _filesArray[fileId].Name)?.Split('\\');
                var str1 = "";
                if (strArray != null)
                    foreach (var str2 in strArray)
                    {
                        str1 = str1 + str2 + "\\";
                        if (!Directory.Exists(fbdExtractFolder.SelectedPath + "\\" + str1))
                            Directory.CreateDirectory(fbdExtractFolder.SelectedPath + "\\" + str1);
                    }

                ExtractFile(fileId, fbdExtractFolder.SelectedPath + "\\" + _filesArray[fileId].Parent);
            }
            MessageBox.Show(@"Extract Finish!");
        }

        private void SoundPlay()
        {
            try
            {
                if (lstMain.SelectedItems.Count > 0)
                {
                    //parse out file extension from list view
                    var fileName = lstMain.SelectedItems[0].SubItems[1].Text;
                    var fileId = Convert.ToInt32(lstMain.SelectedItems[0].SubItems[0].Text);
                    var ext = Path.GetExtension(fileName);

                    //read raw sound bytes from DAT
                    var buffer = ExtractFile(fileId);

                    //stores sound bytes for playback
                    var memStream = new MemoryStream(buffer, true);

                    switch (ext)
                    {
                        case @".wav":

                            new SoundPlayer(memStream).Play();
                            break;

                        case @".ogg":
                            using (var vorbisStream = new VorbisWaveReader(memStream))
                            using (var waveOut = new WaveOutEvent())
                            {
                                waveOut.Init(vorbisStream);
                                waveOut.Play();

                                while (waveOut.PlaybackState == PlaybackState.Playing)
                                {
                                    //wait
                                }
                            }
                            break;
                    }

                    memStream.Close();
                }
                else
                    MessageBox.Show(@"Nothing selected!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error whilst trying to load sound content\n\n{ex}");
            }
        }

        private void ItmPlaySound_Click(object sender, EventArgs e)
        {
            SoundPlay();
        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e) => Close();

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

        private void DatExtractor_Load(object sender, EventArgs e)
        {
            //setup read operations
            SystemSetup();
        }

        private void TextureOpen()
        {
            try
            {
                if (lstMain.SelectedItems.Count > 0)
                {
                    //grab file name
                    var fileName = lstMain.SelectedItems[0].SubItems[1].Text;
                    var fileId = Convert.ToInt32(lstMain.SelectedItems[0].SubItems[0].Text);

                    //grab raw file bytes
                    var buffer = ExtractFile(fileId);

                    //verify extracted bytes
                    if (buffer != null)
                    {
                        //texture handler
                        var handler = new TexTrend(buffer, fileName);

                        //display texture previewer
                        var frm = new TexturePreview(handler);
                        if (!frm.IsDisposed)
                            frm.ShowDialog();
                    }
                    else
                        MessageBox.Show(@"Extracted bytes were null; texture previewer cannot be opened.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error whilst loading in-memory texture:\n\n{ex}");
            }
        }

        private void ItmViewTexture_Click(object sender, EventArgs e)
        {
            TextureOpen();
        }

        private void ItmCxtPreviewTexture_Click(object sender, EventArgs e)
        {
            TextureOpen();
        }
    }
}