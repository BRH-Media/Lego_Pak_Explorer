using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Lego_Pak_Explorer.Properties;

namespace Lego_Pak_Explorer.UI
{
    public partial class Home
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;
        private ComponentResourceManager resources = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                components?.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            resources = new ComponentResourceManager(typeof(Home));

            _splitContainer1 = new SplitContainer();
            _treeView1 = new TreeView();
            _imageList1 = new ImageList(components);
            _listView1 = new ListView();
            _columnHeader1 = new ColumnHeader();
            _columnHeader2 = new ColumnHeader();
            _columnHeader3 = new ColumnHeader();
            _columnHeader4 = new ColumnHeader();
            _contextMenuStrip1 = new ContextMenuStrip(components);
            _menuStrip1 = new MenuStrip();
            _folderBrowserDialog1 = new FolderBrowserDialog();
            _openFileDialog1 = new OpenFileDialog();
            _statusStrip1 = new StatusStrip();
            _toolStripStatusLabel1 = new ToolStripStatusLabel();
            _toolStripStatusLabel2 = new ToolStripStatusLabel();
            _toolStripProgressBar1 = new ToolStripProgressBar();
            _openToolStripMenuItem1 = new ToolStripMenuItem();
            _fileToolStripMenuItem = new ToolStripMenuItem();
            _openToolStripMenuItem = new ToolStripMenuItem();
            _recentGameToolStripMenuItem = new ToolStripMenuItem();
            _toolStripSeparator1 = new ToolStripSeparator();
            _quitToolStripMenuItem = new ToolStripMenuItem();
            _aboutToolStripMenuItem = new ToolStripMenuItem();
            _actionToolStripMenuItem = new ToolStripMenuItem();
            _doGameModReadyToolStripMenuItem = new ToolStripMenuItem();
            _optionsToolStripMenuItem = new ToolStripMenuItem();
            _openFileByDoubleClickToolStripMenuItem = new ToolStripMenuItem();
            _openFileByRightClickMenuToolStripMenuItem = new ToolStripMenuItem();
            _refreshToolStripMenuItem = new ToolStripMenuItem();
            _splitContainer1.BeginInit();
            _splitContainer1.Panel1.SuspendLayout();
            _splitContainer1.Panel2.SuspendLayout();
            _splitContainer1.SuspendLayout();
            _contextMenuStrip1.SuspendLayout();
            _menuStrip1.SuspendLayout();
            _statusStrip1.SuspendLayout();
            SuspendLayout();
            _splitContainer1.Dock = DockStyle.Fill;
            _splitContainer1.Enabled = false;
            _splitContainer1.Location = new Point(0, 24);
            _splitContainer1.Margin = new Padding(3, 3, 3, 22);
            _splitContainer1.Name = "_splitContainer1";
            _splitContainer1.Panel1.Controls.Add(_treeView1);
            _splitContainer1.Panel2.Controls.Add(_listView1);
            _splitContainer1.Size = new Size(683, 326);
            _splitContainer1.SplitterDistance = 202;
            _splitContainer1.TabIndex = 0;
            _splitContainer1.MouseDown += SplitContainer1_MouseDown;
            _splitContainer1.MouseUp += SplitContainer1_MouseUp;
            _treeView1.BackColor = SystemColors.MenuBar;
            _treeView1.Dock = DockStyle.Fill;
            _treeView1.Enabled = false;
            _treeView1.ImageIndex = 0;
            _treeView1.ImageList = _imageList1;
            _treeView1.Location = new Point(0, 0);
            _treeView1.Name = "_treeView1";
            _treeView1.SelectedImageIndex = 0;
            _treeView1.Size = new Size(202, 326);
            _treeView1.TabIndex = 0;
            _treeView1.AfterExpand += TreeView1_AfterExpand;
            _treeView1.NodeMouseClick += TreeView1_NodeMouseClick;
            _imageList1.ImageStream = (ImageListStreamer)resources.GetObject("_imageList1.ImageStream");
            _imageList1.TransparentColor = Color.Transparent;
            _imageList1.Images.SetKeyName(0, "folder.png");
            _imageList1.Images.SetKeyName(1, "page.png");
            _imageList1.Images.SetKeyName(2, "page_code.png");
            _imageList1.Images.SetKeyName(3, "brick.png");
            _imageList1.Images.SetKeyName(4, "image.png");
            _imageList1.Images.SetKeyName(5, "application.png");
            _listView1.Columns.AddRange(new ColumnHeader[]
            {
        _columnHeader1,
        _columnHeader2,
        _columnHeader3,
        _columnHeader4
            });
            _listView1.ContextMenuStrip = _contextMenuStrip1;
            _listView1.Dock = DockStyle.Fill;
            _listView1.Enabled = false;
            _listView1.FullRowSelect = true;
            _listView1.GridLines = true;
            _listView1.Location = new Point(0, 0);
            _listView1.Name = "_listView1";
            _listView1.Size = new Size(477, 326);
            _listView1.SmallImageList = _imageList1;
            _listView1.TabIndex = 0;
            _listView1.UseCompatibleStateImageBehavior = false;
            _listView1.View = View.Details;
            _listView1.MouseDoubleClick += ListView1_MouseDoubleClick;
            _columnHeader1.Text = @"Name";
            _columnHeader2.Text = @"Type";
            _columnHeader3.Text = @"Size";
            _columnHeader3.Width = 97;
            _columnHeader4.Text = @"Last Modified";
            _contextMenuStrip1.Items.AddRange(new ToolStripItem[]
            {
        _openToolStripMenuItem1
            });
            _contextMenuStrip1.Name = "_contextMenuStrip1";
            _contextMenuStrip1.Size = new Size(104, 26);
            _contextMenuStrip1.Opening += ContextMenuStrip1_Opening;
            _menuStrip1.Items.AddRange(new ToolStripItem[]
            {
        _fileToolStripMenuItem,
        _aboutToolStripMenuItem,
        _actionToolStripMenuItem,
        _optionsToolStripMenuItem,
        _refreshToolStripMenuItem
            });
            _menuStrip1.Location = new Point(0, 0);
            _menuStrip1.Name = "_menuStrip1";
            _menuStrip1.Size = new Size(683, 24);
            _menuStrip1.TabIndex = 1;
            _menuStrip1.Text = @"menuStrip1";
            _openFileDialog1.Filter = @"Lego Games Executables|*.exe";
            _statusStrip1.AllowMerge = false;
            _statusStrip1.Items.AddRange(new ToolStripItem[]
            {
        _toolStripStatusLabel1,
        _toolStripStatusLabel2,
        _toolStripProgressBar1
            });
            _statusStrip1.Location = new Point(0, 350);
            _statusStrip1.Name = "_statusStrip1";
            _statusStrip1.Size = new Size(683, 22);
            _statusStrip1.TabIndex = 2;
            _statusStrip1.Text = @"statusStrip1";
            _toolStripStatusLabel1.Name = "_toolStripStatusLabel1";
            _toolStripStatusLabel1.Size = new Size(0, 17);
            _toolStripStatusLabel2.Name = "_toolStripStatusLabel2";
            _toolStripStatusLabel2.Size = new Size(566, 17);
            _toolStripStatusLabel2.Spring = true;
            _toolStripProgressBar1.Maximum = 0;
            _toolStripProgressBar1.Name = "_toolStripProgressBar1";
            _toolStripProgressBar1.Size = new Size(100, 16);
            _toolStripProgressBar1.Step = 1;
            _openToolStripMenuItem1.Image = Resources.magnifier;
            _openToolStripMenuItem1.Name = "_openToolStripMenuItem1";
            _openToolStripMenuItem1.Size = new Size(103, 22);
            _openToolStripMenuItem1.Text = @"Open";
            _openToolStripMenuItem1.Click += OpenToolStripMenuItem1_Click;
            _fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
        _openToolStripMenuItem,
        _recentGameToolStripMenuItem,
        _toolStripSeparator1,
        _quitToolStripMenuItem
            });
            _fileToolStripMenuItem.Image = Resources.brick_go;
            _fileToolStripMenuItem.Name = "_fileToolStripMenuItem";
            _fileToolStripMenuItem.Size = new Size(53, 20);
            _fileToolStripMenuItem.Text = @"File";
            _openToolStripMenuItem.Image = Resources.folder_brick;
            _openToolStripMenuItem.Name = "_openToolStripMenuItem";
            _openToolStripMenuItem.ShortcutKeys = Keys.L | Keys.Control;
            _openToolStripMenuItem.Size = new Size(182, 22);
            _openToolStripMenuItem.Text = @"Load Game...";
            _openToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
            _recentGameToolStripMenuItem.Image = Resources.folder_go;
            _recentGameToolStripMenuItem.Name = "_recentGameToolStripMenuItem";
            _recentGameToolStripMenuItem.Size = new Size(182, 22);
            _recentGameToolStripMenuItem.Text = @"Recent Game...";
            _toolStripSeparator1.Name = "_toolStripSeparator1";
            _toolStripSeparator1.Size = new Size(179, 6);
            _quitToolStripMenuItem.Image = (Image)resources.GetObject("_quitToolStripMenuItem.Image");
            _quitToolStripMenuItem.Name = "_quitToolStripMenuItem";
            _quitToolStripMenuItem.ShortcutKeys = Keys.F4 | Keys.Alt;
            _quitToolStripMenuItem.Size = new Size(182, 22);
            _quitToolStripMenuItem.Text = @"Quit";
            _quitToolStripMenuItem.Click += QuitToolStripMenuItem_Click;
            _aboutToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            _aboutToolStripMenuItem.Image = Resources.BlackFigure;
            _aboutToolStripMenuItem.Name = "_aboutToolStripMenuItem";
            _aboutToolStripMenuItem.Size = new Size(68, 20);
            _aboutToolStripMenuItem.Text = @"About";
            _aboutToolStripMenuItem.Click += AboutToolStripMenuItem_Click;
            _actionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
        _doGameModReadyToolStripMenuItem
            });
            _actionToolStripMenuItem.Enabled = false;
            _actionToolStripMenuItem.Image = Resources.cog;
            _actionToolStripMenuItem.Name = "_actionToolStripMenuItem";
            _actionToolStripMenuItem.Size = new Size(75, 20);
            _actionToolStripMenuItem.Text = @"Actions";
            _doGameModReadyToolStripMenuItem.Name = "_doGameModReadyToolStripMenuItem";
            _doGameModReadyToolStripMenuItem.Size = new Size(197, 22);
            _doGameModReadyToolStripMenuItem.Text = @"Do Game ""Mod Ready""";
            _doGameModReadyToolStripMenuItem.Click += DoGameModReadyToolStripMenuItem_Click;
            _optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
        _openFileByDoubleClickToolStripMenuItem,
        _openFileByRightClickMenuToolStripMenuItem
            });
            _optionsToolStripMenuItem.Enabled = false;
            _optionsToolStripMenuItem.Image = Resources.wrench;
            _optionsToolStripMenuItem.Name = "_optionsToolStripMenuItem";
            _optionsToolStripMenuItem.Size = new Size(77, 20);
            _optionsToolStripMenuItem.Text = @"Options";
            _openFileByDoubleClickToolStripMenuItem.Checked = true;
            _openFileByDoubleClickToolStripMenuItem.CheckOnClick = true;
            _openFileByDoubleClickToolStripMenuItem.CheckState = CheckState.Checked;
            _openFileByDoubleClickToolStripMenuItem.Name = "_openFileByDoubleClickToolStripMenuItem";
            _openFileByDoubleClickToolStripMenuItem.Size = new Size(233, 22);
            _openFileByDoubleClickToolStripMenuItem.Text = @"Open File by Double Click";
            _openFileByRightClickMenuToolStripMenuItem.Checked = true;
            _openFileByRightClickMenuToolStripMenuItem.CheckOnClick = true;
            _openFileByRightClickMenuToolStripMenuItem.CheckState = CheckState.Checked;
            _openFileByRightClickMenuToolStripMenuItem.Name = "_openFileByRightClickMenuToolStripMenuItem";
            _openFileByRightClickMenuToolStripMenuItem.Size = new Size(233, 22);
            _openFileByRightClickMenuToolStripMenuItem.Text = @"Open File by Right Click Menu";
            _refreshToolStripMenuItem.Enabled = false;
            _refreshToolStripMenuItem.Image = Resources.arrow_refresh;
            _refreshToolStripMenuItem.Name = "_refreshToolStripMenuItem";
            _refreshToolStripMenuItem.ShortcutKeys = Keys.R | Keys.Control;
            _refreshToolStripMenuItem.ShowShortcutKeys = false;
            _refreshToolStripMenuItem.Size = new Size(74, 20);
            _refreshToolStripMenuItem.Text = @"Refresh";
            _refreshToolStripMenuItem.Click += RefreshToolStripMenuItem_Click;
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(683, 372);
            Controls.Add(_splitContainer1);
            Controls.Add(_menuStrip1);
            Controls.Add(_statusStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = _menuStrip1;
            Name = @"Home";
            StartPosition = FormStartPosition.CenterScreen;
            Text = @"TT Games Explorer [BETA]";
            Load += Home_Load;
            _splitContainer1.Panel1.ResumeLayout(false);
            _splitContainer1.Panel2.ResumeLayout(false);
            _splitContainer1.EndInit();
            _splitContainer1.ResumeLayout(false);
            _contextMenuStrip1.ResumeLayout(false);
            _menuStrip1.ResumeLayout(false);
            _menuStrip1.PerformLayout();
            _statusStrip1.ResumeLayout(false);
            _statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private string _legoGameFolder;
        private string _listviewFileSelected;
        private ThreadStart _readDir;
        private Thread _thReadDir;
        private TreeNode _newSelected;
        private Control _focused;
        private SplitContainer _splitContainer1;
        private TreeView _treeView1;
        private ImageList _imageList1;
        private ListView _listView1;
        private ColumnHeader _columnHeader1;
        private ColumnHeader _columnHeader2;
        private ColumnHeader _columnHeader3;
        private MenuStrip _menuStrip1;
        private ToolStripMenuItem _fileToolStripMenuItem;
        private ToolStripMenuItem _openToolStripMenuItem;
        private FolderBrowserDialog _folderBrowserDialog1;
        private ToolStripSeparator _toolStripSeparator1;
        private ToolStripMenuItem _quitToolStripMenuItem;
        private ToolStripMenuItem _aboutToolStripMenuItem;
        private ToolStripMenuItem _actionToolStripMenuItem;
        private ToolStripMenuItem _doGameModReadyToolStripMenuItem;
        private ToolStripMenuItem _optionsToolStripMenuItem;
        private ColumnHeader _columnHeader4;
        private ContextMenuStrip _contextMenuStrip1;
        private ToolStripMenuItem _openToolStripMenuItem1;
        private OpenFileDialog _openFileDialog1;
        private ToolStripMenuItem _recentGameToolStripMenuItem;
        private StatusStrip _statusStrip1;
        private ToolStripStatusLabel _toolStripStatusLabel1;
        private ToolStripMenuItem _openFileByDoubleClickToolStripMenuItem;
        private ToolStripMenuItem _openFileByRightClickMenuToolStripMenuItem;
        private ToolStripStatusLabel _toolStripStatusLabel2;
        private ToolStripProgressBar _toolStripProgressBar1;
        private ToolStripMenuItem _refreshToolStripMenuItem;
    }
}
