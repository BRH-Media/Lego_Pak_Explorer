using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using TT_Games_Explorer.Common;
using TT_Games_Explorer.Renderer.Textures;

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
            //clear the list of any items then disable it to prevent user input while working
            lstMain.Invoke((MethodInvoker)delegate
            {
                lstMain.Items.Clear();
                lstMain.Enabled = false;
            });

            var nodeDirInfo = (DirectoryInfo)_newSelected.Tag;
            ListViewItem item;

            //reset progress bar
            lstMain.Invoke((MethodInvoker)delegate
            {
                if (pbMain.ProgressBar == null) return;

                pbMain.ProgressBar.Maximum = nodeDirInfo.GetFiles().Length;
                pbMain.ProgressBar.Value = 0;
            });

            foreach (var file in nodeDirInfo.GetFiles())
            {
                //index 1 is the 'Unknown File' icon
                var imageIndex = 1;

                //go through and attempt icon assignations
                switch (Path.GetExtension(file.Name).ToLower())
                {
                    //code files
                    case ".txt":
                    case ".csv":
                    case ".sub":
                    case ".bms":
                    case ".sf":
                    case ".scp":
                    case ".cfg":
                    case ".ini":
                    case ".inf":
                    case ".vdf":
                    case ".gip":
                    case ".gix":
                    case ".giz":
                    case ".gin":
                    case ".ats":
                        imageIndex = 2;
                        break;

                    //archive files
                    case ".dat":
                    case ".hdr":
                    case ".pak":
                        imageIndex = 3;
                        break;

                    //image files
                    case ".tex":
                    case ".dds":
                    case ".png":
                    case ".bmp":
                    case ".raw":
                    case ".tga":
                    case ".jpg":
                    case ".jpeg":
                    case ".gif":
                    case ".giff":
                    case ".tif":
                    case ".tiff":
                        imageIndex = 4;
                        break;

                    //executables
                    case ".exe":
                    case ".dll":
                    case ".bat":
                    case ".com":
                    case ".cmd":
                    case ".sh":
                    case ".so":
                        imageIndex = 5;
                        break;
                }

                //the new item to add
                item = new ListViewItem(file.Name, imageIndex);

                //the cells that belong to the new item
                var items = new[]
                {
                    new ListViewItem.ListViewSubItem(item, Methods.GetLegoFileType(file.Extension)),
                    new ListViewItem.ListViewSubItem(item, Methods.FormatSize(file.Length, true)),
                    new ListViewItem.ListViewSubItem(item, file.LastAccessTime.ToShortDateString())
                };

                //add the new sub items
                item.SubItems.AddRange(items);

                //invoke and begin adding
                lstMain.Invoke((MethodInvoker)delegate
                {
                    //new entry is added
                    lstMain.Items.Add(item);

                    //the method is exited if the progress bar is undefined
                    if (pbMain.ProgressBar == null) return;

                    //the progress bar is incremented
                    ++pbMain.ProgressBar.Value;

                    //percentage completion calculation
                    var percComplete = (int)(pbMain.ProgressBar.Value /
                        (double)pbMain.ProgressBar.Maximum * 100.0);

                    //draws the % completion inside of the progressbar (on-the-fly)
                    pbMain.ProgressBar.CreateGraphics().DrawString($"{percComplete}%",
                        new Font("Arial", 8.25f, FontStyle.Regular), Brushes.Gray,
                        new PointF(pbMain.ProgressBar.Width / 2 - 10,
                            pbMain.ProgressBar.Height / 2 - 7));
                });
            }

            //resize the columns to fit the window
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
                _listviewFileSelected =
                    $"{_legoGameFolder.Substring(0, _legoGameFolder.LastIndexOf("\\", StringComparison.Ordinal))}\\{trvMain.SelectedNode.FullPath}\\{lstMain.SelectedItems[0].SubItems[0].Text}";
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
            Globals.MruManager = new MruManager(itmRecentGame, "LEGOPakExplorer", OnRecentFileClick, OnRecentFilesCleared);

        private void OnRecentFileClick(object sender, EventArgs e)
        {
            var text = (sender as ToolStripItem)?.Text;
            if (!Directory.Exists(text))
            {
                if (MessageBox.Show($@"{text} doesn't exist. Remove from recent workspaces?", @"File not found", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
                Globals.MruManager.RemoveRecentFile(text);
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
            fbdOpenGameFolder.SelectedPath = Path.GetDirectoryName(
                $"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\\");
            if (fbdOpenGameFolder.ShowDialog() != DialogResult.OK)
                return;
            Globals.MruManager.AddRecentFile(fbdOpenGameFolder.SelectedPath);
            _legoGameFolder = fbdOpenGameFolder.SelectedPath;
            new Thread(OpenLegoGame).Start();
        }

        private void ItmGameModReady_Click(object sender, EventArgs e)
        {
            if (ofdOpenExecutable.ShowDialog() != DialogResult.OK)
                return;
            MessageBox.Show(ofdOpenExecutable.FileName);
        }

        private void PreviewFile()
        {
            switch (Path.GetExtension(_listviewFileSelected).ToLower())
            {
                //executables
                case ".exe":
                case ".dll":
                case ".bat":
                case ".com":
                case ".cmd":
                case ".sh":
                case ".so":
                    var ask = MessageBox.Show(
                        @"Are you sure? You're about to open an executable file which will run code on your PC; this may have unforeseen adverse effects.",
                            @"Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (ask == DialogResult.Yes)
                        if (!string.IsNullOrEmpty(_listviewFileSelected))
                        {
                            var dir = Path.GetDirectoryName(Path.GetFullPath(_listviewFileSelected));
                            var p = new Process
                            {
                                StartInfo =
                                {
                                    FileName = _listviewFileSelected,
                                    CreateNoWindow = false,
                                    WorkingDirectory = !string.IsNullOrEmpty(dir) ? dir : @""
                                }
                            };
                            p.Start();
                        }

                    break;

                //code files
                case ".txt":
                case ".csv":
                case ".sub":
                case ".bms":
                case ".sf":
                case ".scp":
                case ".cfg":
                case ".ini":
                case ".inf":
                case ".vdf":
                case ".gip":
                case ".gix":
                case ".giz":
                case ".gin":
                case ".ats":
                    new CodePreview(_listviewFileSelected).ShowDialog();
                    break;

                //archive files
                case ".dat":
                    new DatExtractor(_listviewFileSelected).ShowDialog();
                    break;

                case ".hdr":
                    MessageBox.Show(@"Please open the *.DAT instead of the *.HDR");
                    break;

                case ".pak":
                    new PakExtractor(_listviewFileSelected).ShowDialog();
                    break;

                //image files
                case ".tex":
                case ".dds":
                case ".png":
                case ".bmp":
                case ".raw":
                case ".tga":
                case ".jpg":
                case ".jpeg":
                case ".gif":
                case ".giff":
                case ".tif":
                case ".tiff":
                    //new texture handler
                    var texHandler = new TexTrend(_listviewFileSelected);

                    //run preview window
                    new TexturePreview(texHandler).ShowDialog();
                    break;

                //anything else is unsupported
                default:
                    MessageBox.Show(
                        $@"*{Path.GetExtension(_listviewFileSelected)?.ToLower()} files are not currently supported");
                    break;
            }
        }

        private void ItmCxtOpen_Click(object sender, EventArgs e)
        {
            if (!itmOptionRightClick.Checked)
                return;
            PreviewFile();
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
                    $"{_legoGameFolder.Substring(0, _legoGameFolder.LastIndexOf("\\", StringComparison.Ordinal))}\\{trvMain.SelectedNode.FullPath}\\{lstMain.SelectedItems[0].SubItems[0].Text}";
                if (new FileInfo(_listviewFileSelected).Length != 0L)
                    PreviewFile();
                else
                    _listviewFileSelected = "";
            }
        }
    }
}