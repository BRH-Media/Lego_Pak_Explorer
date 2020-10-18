using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

// ReSharper disable AccessToModifiedClosure

namespace TT_Games_Explorer.UI
{
    public partial class Home : Form
    {
        public Home() => InitializeComponent();

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
                trvMain.BeginInvoke((MethodInvoker)delegate { trvMain.Nodes.Add(rootNode); });
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
            trvMain.Invoke((MethodInvoker)delegate
            {
                if (pbMain.ProgressBar != null)
                    pbMain.ProgressBar.Maximum += subDirs.Count;
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
                    trvMain.Invoke((MethodInvoker)delegate
                    {
                        nodeToAddTo.Nodes.Add(aNode);
                        if (pbMain.ProgressBar == null) return;

                        ++pbMain.ProgressBar.Value;
                        var num = (int)(pbMain.ProgressBar.Value /
                            (double)pbMain.ProgressBar.Maximum * 100.0);
                        pbMain.ProgressBar.CreateGraphics().DrawString($"{num}%",
                            new Font("Arial", 8.25f, FontStyle.Regular), Brushes.Gray,
                            new PointF(pbMain.ProgressBar.Width / 2 - 10,
                                pbMain.ProgressBar.Height / 2 - 7));
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

        private void PopulateListView()
        {
            lstMain.Invoke((MethodInvoker)delegate
            {
                lstMain.Items.Clear();
                lstMain.Enabled = false;
            });
            var nodeDirInfo = (DirectoryInfo)_newSelected.Tag;
            ListViewItem item;
            lstMain.Invoke((MethodInvoker)delegate
            {
                if (pbMain.ProgressBar == null) return;

                pbMain.ProgressBar.Maximum = nodeDirInfo.GetFiles().Length;
                pbMain.ProgressBar.Value = 0;
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
          new ListViewItem.ListViewSubItem(item, Common.GetLegoFileType(file.Extension)),
          new ListViewItem.ListViewSubItem(item, Common.FormatSize(file.Length, true)),
          new ListViewItem.ListViewSubItem(item, file.LastAccessTime.ToShortDateString())
                };
                item.SubItems.AddRange(items);
                lstMain.Invoke((MethodInvoker)delegate
                {
                    lstMain.Items.Add(item);
                    if (pbMain.ProgressBar == null) return;

                    ++pbMain.ProgressBar.Value;
                    var num = (int)(pbMain.ProgressBar.Value /
                        (double)pbMain.ProgressBar.Maximum * 100.0);
                    pbMain.ProgressBar.CreateGraphics().DrawString($"{num}%",
                        new Font("Arial", 8.25f, FontStyle.Regular), Brushes.Gray,
                        new PointF(pbMain.ProgressBar.Width / 2 - 10,
                            pbMain.ProgressBar.Height / 2 - 7));
                });
            }
            lstMain.Invoke((MethodInvoker)delegate
            {
                lstMain.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                lstMain.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                lstMain.Enabled = true;
            });
        }

        private void TrvMain_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            _thReadDir?.Abort();
            _newSelected = e.Node;
            _readDir = PopulateListView;
            _thReadDir = new Thread(_readDir)
            {
                IsBackground = true
            };
            _thReadDir.Start();
        }

        private void ItmQuit_Click(object sender, EventArgs e) => Application.Exit();

        private void CxtListView_Opening(object sender, CancelEventArgs e)
        {
            if (lstMain.SelectedItems.Count <= 0)
            {
                e.Cancel = true;
                _listviewFileSelected = "";
            }
            else
            {
                if (lstMain.SelectedItems.Count != 1)
                    return;
                _listviewFileSelected = _legoGameFolder.Substring(0,
                    _legoGameFolder.LastIndexOf("\\",
                        StringComparison.Ordinal)) + "\\" + trvMain.SelectedNode.FullPath + "\\" + lstMain.SelectedItems[0].SubItems[0].Text;
                if (new FileInfo(_listviewFileSelected).Length != 0L)
                    return;
                e.Cancel = true;
                _listviewFileSelected = "";
            }
        }

        private void OpenLegoGame()
        {
            lstMain.Invoke((MethodInvoker)delegate
            {
                Enabled = false;
                lstMain.Items.Clear();
                trvMain.Nodes.Clear();
            });
            PopulateTreeView(_legoGameFolder);
            lstMain.Invoke((MethodInvoker)delegate
            {
                _toolStripStatusLabel1.Text = _legoGameFolder;
                trvMain.BackColor = SystemColors.Window;
                trvMain.Enabled = true;
                containerMain.Enabled = true;
                trvMain.SelectedNode = trvMain.Nodes[0];
                trvMain.Nodes[0].Expand();
                trvMain.SelectedNode.EnsureVisible();
                _newSelected = trvMain.Nodes[0];
                PopulateListView();
                Enabled = true;
                itmOptions.Enabled = true;
                itmRefresh.Enabled = true;
            });
        }

        private void Home_Load(object sender, EventArgs e) =>
            Common.MruManager = new MruManager(itmRecentGame, "LEGOPakExplorer", OnRecentFileClick, OnRecentFilesCleared);

        private void OnRecentFileClick(object sender, EventArgs e)
        {
            var text = (sender as ToolStripItem)?.Text;
            if (!Directory.Exists(text))
            {
                if (MessageBox.Show($@"{text} doesn't exist. Remove from recent workspaces?", @"File not found", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
                Common.MruManager.RemoveRecentFile(text);
            }
            else
            {
                _legoGameFolder = text;
                _readDir = OpenLegoGame;
                _thReadDir = new Thread(_readDir)
                {
                    IsBackground = true
                };
                _thReadDir.Start();
            }
        }

        private static void OnRecentFilesCleared(object sender, EventArgs e)
        {
            MessageBox.Show(@"All recent files were successfully cleared");
        }

        private void ItmLoadGame_Click(object sender, EventArgs e)
        {
            fbdOpenGameFolder.Description = @"Choose your LEGO Game folder";
            fbdOpenGameFolder.SelectedPath = Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\");
            if (fbdOpenGameFolder.ShowDialog() != DialogResult.OK)
                return;
            Common.MruManager.AddRecentFile(fbdOpenGameFolder.SelectedPath);
            _legoGameFolder = fbdOpenGameFolder.SelectedPath;
            new Thread(OpenLegoGame).Start();
        }

        private void ItmGameModReady_Click(object sender, EventArgs e)
        {
            if (ofdOpenExecutable.ShowDialog() != DialogResult.OK)
                return;
            MessageBox.Show(ofdOpenExecutable.FileName);
        }

        private void Preview_File()
        {
            switch (Path.GetExtension(_listviewFileSelected).ToLower())
            {
                case ".txt":
                case ".csv":
                case ".sub":
                case ".sf":
                case ".scp":
                case ".cfg":
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
                    if (!string.IsNullOrEmpty(format))
                        new TexturePreview(_listviewFileSelected, format).ShowDialog();
                    else
                        MessageBox.Show(
                            $@"{Path.GetFileName(_listviewFileSelected)} - Invalid texture file; probably an incorrect format.");
                    break;

                default:
                    MessageBox.Show(
                        $@"*{Path.GetExtension(_listviewFileSelected)?.ToLower()} files are not currently supported");
                    break;
            }
        }

        private static uint ReverseBytes(uint value) => (uint)(((int)value & byte.MaxValue) << 24 | ((int)value & 65280) << 8) | (value & 16711680U) >> 8 | (value & 4278190080U) >> 24;

        private void ItmCxtOpen_Click(object sender, EventArgs e)
        {
            if (!itmOptionRightClick.Checked)
                return;
            Preview_File();
        }

        private void ItmAbout_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }

        private void ContainerMain_MouseDown(object sender, MouseEventArgs e) => _focused = GetFocused(Controls);

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

        private void ContainerMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (_focused == null)
                return;
            _focused.Focus();
            _focused = null;
        }

        private void TrvMain_AfterExpand(object sender, TreeViewEventArgs e) => trvMain.SelectedNode = e.Node;

        private void ItmRefresh_Click(object sender, EventArgs e) => new Thread(OpenLegoGame).Start();

        private static uint ReadFourCc(string filepath)
        {
            var fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            var num = new BinaryReader(fileStream).ReadUInt32();
            fileStream.Close();
            return ReverseBytes(num);
        }

        private void LstMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || !itmOptionDoubleClick.Checked)
                return;
            if (lstMain.SelectedItems.Count <= 0)
            {
                _listviewFileSelected = "";
            }
            else
            {
                if (lstMain.SelectedItems.Count != 1)
                    return;
                _listviewFileSelected =
                    _legoGameFolder.Substring(0, _legoGameFolder.LastIndexOf("\\", StringComparison.Ordinal)) + "\\" +
                    trvMain.SelectedNode.FullPath + "\\" + lstMain.SelectedItems[0].SubItems[0].Text;
                if (new FileInfo(_listviewFileSelected).Length != 0L)
                    Preview_File();
                else
                    _listviewFileSelected = "";
            }
        }
    }
}