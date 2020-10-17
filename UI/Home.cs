using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;

// ReSharper disable AccessToModifiedClosure

namespace Lego_Pak_Explorer.UI
{
    public partial class Home : Form
    {
        private MruManager _mruManager;

        private readonly Dictionary<string, string> _legoFileType = new Dictionary<string, string>
        {
      {
        ".cfg",
        "Configuration"
      },
      {
        ".csv",
        "Comma Separated Values"
      },
      {
        ".dat",
        "Data Archive"
      },
      {
        ".dds",
        "Direct Draw Surface"
      },
      {
        ".exe",
        "Executable"
      },
      {
        ".fmv",
        "Full Motion Video"
      },
      {
        ".hdr",
        "Header Data Archive"
      },
      {
        ".ogg",
        "OGG Vorbis Sound"
      },
      {
        ".png",
        "Portable Network Graphics"
      },
      {
        ".sf",
        "Script File"
      },
      {
        ".tex",
        "Texture"
      },
      {
        ".txt",
        "Text"
      },
      {
        ".wav",
        "Waveform Audio"
      },
      {
        ".xma",
        "Xbox360 Media Audio"
      }
    };

        public Home() => InitializeComponent();

        private string Get_LEGO_File_Type(string ext)
        {
            string str;
            try
            {
                str = _legoFileType[ext.ToLower()];
            }
            catch (Exception)
            {
                str = "Unknown";
            }
            return str + " File";
        }

        protected string FormatSize(long lSize)
        {
            var numberFormatInfo = new NumberFormatInfo();
            return (lSize >= 1024L ? (lSize / 1024L).ToString("n", numberFormatInfo).Replace(".00", "") : (lSize != 0L ? "1" : "0")) + " KB";
        }

        private void PopulateTreeView(string rootPath)
        {
            var directoryInfo = new DirectoryInfo(rootPath);
            if (!directoryInfo.Exists)
                return;
            try
            {
                var rootNode = new TreeNode(directoryInfo.Name)
                {
                    Tag = directoryInfo
                };
                GetDirectories(directoryInfo.GetDirectories(), rootNode);
                _treeView1.BeginInvoke((MethodInvoker)delegate { _treeView1.Nodes.Add(rootNode); });
            }
            catch (UnauthorizedAccessException)
            {
            }
            catch (Exception)
            {
                //nothing
            }
        }

        private void GetDirectories(ICollection<DirectoryInfo> subDirs, TreeNode nodeToAddTo)
        {
            _treeView1.Invoke((MethodInvoker)delegate
            {
                if (_toolStripProgressBar1.ProgressBar != null)
                    _toolStripProgressBar1.ProgressBar.Maximum += subDirs.Count;
            });
            foreach (var subDir in subDirs)
            {
                try
                {
                    var aNode = new TreeNode(subDir.Name, 0, 0)
                    {
                        Tag = subDir,
                        ImageKey = @"folder"
                    };
                    var directories = subDir.GetDirectories();
                    if (directories.Length != 0)
                        GetDirectories(directories, aNode);
                    _treeView1.Invoke((MethodInvoker)delegate
                    {
                        nodeToAddTo.Nodes.Add(aNode);
                        if (_toolStripProgressBar1.ProgressBar == null) return;

                        ++_toolStripProgressBar1.ProgressBar.Value;
                        var num = (int)(_toolStripProgressBar1.ProgressBar.Value /
                            (double)_toolStripProgressBar1.ProgressBar.Maximum * 100.0);
                        _toolStripProgressBar1.ProgressBar.CreateGraphics().DrawString($"{num}%",
                            new Font("Arial", 8.25f, FontStyle.Regular), Brushes.Gray,
                            new PointF(_toolStripProgressBar1.ProgressBar.Width / 2 - 10,
                                _toolStripProgressBar1.ProgressBar.Height / 2 - 7));
                    });
                }
                catch (UnauthorizedAccessException)
                {
                }
                catch (Exception)
                {
                    //ignore
                }
            }
        }

        private void Fill_Listview()
        {
            _listView1.Invoke((MethodInvoker)delegate
            {
                _listView1.Items.Clear();
                _listView1.Enabled = false;
            });
            var nodeDirInfo = (DirectoryInfo)_newSelected.Tag;
            ListViewItem item;
            _listView1.Invoke((MethodInvoker)delegate
            {
                if (_toolStripProgressBar1.ProgressBar == null) return;

                _toolStripProgressBar1.ProgressBar.Maximum = nodeDirInfo.GetFiles().Length;
                _toolStripProgressBar1.ProgressBar.Value = 0;
            });
            foreach (var file in nodeDirInfo.GetFiles())
            {
                var imageIndex = 1;
                switch (Path.GetExtension(file.Name).ToLower())
                {
                    case ".txt":
                    case ".csv":
                    case ".sub":
                    case ".bms":
                    case ".sf":
                        imageIndex = 2;
                        break;

                    case ".dat":
                    case ".hdr":
                        imageIndex = 3;
                        break;

                    case ".tex":
                    case ".dds":
                    case ".png":
                        imageIndex = 4;
                        break;

                    case ".exe":
                    case ".dll":
                        imageIndex = 5;
                        break;
                }
                item = new ListViewItem(file.Name, imageIndex);
                var items = new[]
                {
          new ListViewItem.ListViewSubItem(item, Get_LEGO_File_Type(file.Extension)),
          new ListViewItem.ListViewSubItem(item, FormatSize(file.Length)),
          new ListViewItem.ListViewSubItem(item, file.LastAccessTime.ToShortDateString())
                };
                item.SubItems.AddRange(items);
                _listView1.Invoke((MethodInvoker)delegate
                {
                    _listView1.Items.Add(item);
                    if (_toolStripProgressBar1.ProgressBar == null) return;

                    ++_toolStripProgressBar1.ProgressBar.Value;
                    var num = (int)(_toolStripProgressBar1.ProgressBar.Value /
                        (double)_toolStripProgressBar1.ProgressBar.Maximum * 100.0);
                    _toolStripProgressBar1.ProgressBar.CreateGraphics().DrawString($"{num}%",
                        new Font("Arial", 8.25f, FontStyle.Regular), Brushes.Gray,
                        new PointF(_toolStripProgressBar1.ProgressBar.Width / 2 - 10,
                            _toolStripProgressBar1.ProgressBar.Height / 2 - 7));
                });
            }
            _listView1.Invoke((MethodInvoker)delegate
            {
                _listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                _listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                _listView1.Enabled = true;
            });
        }

        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            _thReadDir?.Abort();
            _newSelected = e.Node;
            _readDir = Fill_Listview;
            _thReadDir = new Thread(_readDir)
            {
                IsBackground = true
            };
            _thReadDir.Start();
        }

        private static void QuitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (_listView1.SelectedItems.Count <= 0)
            {
                e.Cancel = true;
                _listviewFileSelected = "";
            }
            else
            {
                if (_listView1.SelectedItems.Count != 1)
                    return;
                _listviewFileSelected = _legoGameFolder.Substring(0,
                    _legoGameFolder.LastIndexOf("\\",
                        StringComparison.Ordinal)) + "\\" + _treeView1.SelectedNode.FullPath + "\\" + _listView1.SelectedItems[0].SubItems[0].Text;
                if (new FileInfo(_listviewFileSelected).Length != 0L)
                    return;
                e.Cancel = true;
                _listviewFileSelected = "";
            }
        }

        private void Open_LEGO_Game_Env()
        {
            _listView1.Invoke((MethodInvoker)delegate
            {
                Enabled = false;
                _listView1.Items.Clear();
                _treeView1.Nodes.Clear();
            });
            PopulateTreeView(_legoGameFolder);
            _listView1.Invoke((MethodInvoker)delegate
            {
                _toolStripStatusLabel1.Text = _legoGameFolder;
                _treeView1.BackColor = SystemColors.Window;
                _treeView1.Enabled = true;
                _splitContainer1.Enabled = true;
                _treeView1.SelectedNode = _treeView1.Nodes[0];
                _treeView1.Nodes[0].Expand();
                _treeView1.SelectedNode.EnsureVisible();
                _newSelected = _treeView1.Nodes[0];
                Fill_Listview();
                Enabled = true;
                _optionsToolStripMenuItem.Enabled = true;
                _refreshToolStripMenuItem.Enabled = true;
            });
        }

        private void Home_Load(object sender, EventArgs e) =>
            _mruManager = new MruManager(_recentGameToolStripMenuItem, "LEGOPakExplorer", MyOwnRecentFileGotClicked_handler, MyOwnRecentFilesGotCleared_handler);

        private void MyOwnRecentFileGotClicked_handler(object obj, EventArgs evt)
        {
            var text = (obj as ToolStripItem)?.Text;
            if (!Directory.Exists(text))
            {
                if (MessageBox.Show($@"{text} doesn't exist. Remove from recent workspaces?", @"File not found", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
                _mruManager.RemoveRecentFile(text);
            }
            else
            {
                _legoGameFolder = text;
                _readDir = Open_LEGO_Game_Env;
                _thReadDir = new Thread(_readDir)
                {
                    IsBackground = true
                };
                _thReadDir.Start();
            }
        }

        private static void MyOwnRecentFilesGotCleared_handler(object obj, EventArgs evt)
        {
            MessageBox.Show(@"You just cleared all recent files.");
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _folderBrowserDialog1.Description = @"Choose your LEGO Game folder";
            _folderBrowserDialog1.SelectedPath = Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\");
            if (_folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                return;
            _mruManager.AddRecentFile(_folderBrowserDialog1.SelectedPath);
            _legoGameFolder = _folderBrowserDialog1.SelectedPath;
            new Thread(Open_LEGO_Game_Env).Start();
        }

        private void DoGameModReadyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            MessageBox.Show(_openFileDialog1.FileName);
        }

        private void Preview_File()
        {
            switch (Path.GetExtension(_listviewFileSelected).ToLower())
            {
                case ".txt":
                case ".csv":
                case ".sub":
                case ".sf":
                case ".bms":
                    new CodePreview(_listviewFileSelected).ShowDialog();
                    break;

                case ".dat":
                    new DatExtractor(_listviewFileSelected).ShowDialog();
                    break;

                case ".hdr":
                    MessageBox.Show(@"Please, Open the *.DAT instead of the *.HDR");
                    break;

                case ".tex":
                case ".dds":
                case ".png":
                    var format = "";
                    switch (ReadFourCc(_listviewFileSelected))
                    {
                        case 1145328416:
                            format = "dds";
                            break;

                        case 2303741511:
                            format = "png";
                            break;
                    }
                    if (format != "")
                    {
                        new TexturePreview(_listviewFileSelected, format).ShowDialog();
                        break;
                    }
                    MessageBox.Show(
                        $@"{Path.GetFileName(_listviewFileSelected)} - Invalid file, probably not really a texture!");
                    break;

                default:
                    MessageBox.Show(
                        $@"*{Path.GetExtension(_listviewFileSelected)?.ToLower()} files are not supported yet!");
                    break;
            }
        }

        private static uint ReverseBytes(uint value) => (uint)(((int)value & byte.MaxValue) << 24 | ((int)value & 65280) << 8) | (value & 16711680U) >> 8 | (value & 4278190080U) >> 24;

        private void OpenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!_openFileByRightClickMenuToolStripMenuItem.Checked)
                return;
            Preview_File();
        }

        private static void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }

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

        private void SplitContainer1_MouseUp(object sender, MouseEventArgs e)
        {
            if (_focused == null)
                return;
            _focused.Focus();
            _focused = null;
        }

        private void TreeView1_AfterExpand(object sender, TreeViewEventArgs e) => _treeView1.SelectedNode = e.Node;

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e) => new Thread(Open_LEGO_Game_Env).Start();

        private static uint ReadFourCc(string filepath)
        {
            var fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            var num = new BinaryReader(fileStream).ReadUInt32();
            fileStream.Close();
            return ReverseBytes(num);
        }

        private void ListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || !_openFileByDoubleClickToolStripMenuItem.Checked)
                return;
            if (_listView1.SelectedItems.Count <= 0)
            {
                _listviewFileSelected = "";
            }
            else
            {
                if (_listView1.SelectedItems.Count != 1)
                    return;
                _listviewFileSelected =
                    _legoGameFolder.Substring(0, _legoGameFolder.LastIndexOf("\\", StringComparison.Ordinal)) + "\\" +
                    _treeView1.SelectedNode.FullPath + "\\" + _listView1.SelectedItems[0].SubItems[0].Text;
                if (new FileInfo(_listviewFileSelected).Length != 0L)
                    Preview_File();
                else
                    _listviewFileSelected = "";
            }
        }
    }
}