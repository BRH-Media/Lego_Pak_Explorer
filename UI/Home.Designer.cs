using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using TT_Games_Explorer.Properties;

namespace TT_Games_Explorer.UI
{
    public partial class Home
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                components?.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Home));
            this.containerMain = new SplitContainer();
            this.trvMain = new TreeView();
            this.imgMain = new ImageList(this.components);
            this.lstMain = new ListView();
            this._columnHeader1 = ((ColumnHeader)(new ColumnHeader()));
            this._columnHeader2 = ((ColumnHeader)(new ColumnHeader()));
            this._columnHeader3 = ((ColumnHeader)(new ColumnHeader()));
            this._columnHeader4 = ((ColumnHeader)(new ColumnHeader()));
            this.cxtListView = new ContextMenuStrip(this.components);
            this.itmCxtOpen = new ToolStripMenuItem();
            this.menuMain = new MenuStrip();
            this._fileToolStripMenuItem = new ToolStripMenuItem();
            this.itmLoadGame = new ToolStripMenuItem();
            this.itmRecentGame = new ToolStripMenuItem();
            this.sepMain = new ToolStripSeparator();
            this.itmQuit = new ToolStripMenuItem();
            this.itmAbout = new ToolStripMenuItem();
            this.itmActions = new ToolStripMenuItem();
            this.itmGameModReady = new ToolStripMenuItem();
            this.itmOptions = new ToolStripMenuItem();
            this.itmOptionDoubleClick = new ToolStripMenuItem();
            this.itmOptionRightClick = new ToolStripMenuItem();
            this.itmRefresh = new ToolStripMenuItem();
            this.fbdOpenGameFolder = new FolderBrowserDialog();
            this.ofdOpenExecutable = new OpenFileDialog();
            this.statusMain = new StatusStrip();
            this._toolStripStatusLabel1 = new ToolStripStatusLabel();
            this.lblStatus = new ToolStripStatusLabel();
            this.pbMain = new ToolStripProgressBar();
            ((ISupportInitialize)(this.containerMain)).BeginInit();
            this.containerMain.Panel1.SuspendLayout();
            this.containerMain.Panel2.SuspendLayout();
            this.containerMain.SuspendLayout();
            this.cxtListView.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.statusMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // containerMain
            // 
            this.containerMain.Dock = DockStyle.Fill;
            this.containerMain.Enabled = false;
            this.containerMain.Location = new Point(0, 24);
            this.containerMain.Margin = new Padding(3, 3, 3, 22);
            this.containerMain.Name = "containerMain";
            // 
            // containerMain.Panel1
            // 
            this.containerMain.Panel1.Controls.Add(this.trvMain);
            // 
            // containerMain.Panel2
            // 
            this.containerMain.Panel2.Controls.Add(this.lstMain);
            this.containerMain.Size = new Size(683, 326);
            this.containerMain.SplitterDistance = 202;
            this.containerMain.TabIndex = 0;
            this.containerMain.MouseDown += new MouseEventHandler(this.ContainerMain_MouseDown);
            this.containerMain.MouseUp += new MouseEventHandler(this.ContainerMain_MouseUp);
            // 
            // trvMain
            // 
            this.trvMain.BackColor = SystemColors.MenuBar;
            this.trvMain.Dock = DockStyle.Fill;
            this.trvMain.Enabled = false;
            this.trvMain.ImageIndex = 0;
            this.trvMain.ImageList = this.imgMain;
            this.trvMain.Location = new Point(0, 0);
            this.trvMain.Name = "trvMain";
            this.trvMain.SelectedImageIndex = 0;
            this.trvMain.Size = new Size(202, 326);
            this.trvMain.TabIndex = 0;
            this.trvMain.AfterExpand += new TreeViewEventHandler(this.TrvMain_AfterExpand);
            this.trvMain.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.TrvMain_NodeMouseClick);
            // 
            // imgMain
            // 
            this.imgMain.ImageStream = ((ImageListStreamer)(resources.GetObject("imgMain.ImageStream")));
            this.imgMain.TransparentColor = Color.Transparent;
            this.imgMain.Images.SetKeyName(0, "folder.png");
            this.imgMain.Images.SetKeyName(1, "page.png");
            this.imgMain.Images.SetKeyName(2, "page_code.png");
            this.imgMain.Images.SetKeyName(3, "brick.png");
            this.imgMain.Images.SetKeyName(4, "image.png");
            this.imgMain.Images.SetKeyName(5, "application.png");
            // 
            // lstMain
            // 
            this.lstMain.Columns.AddRange(new ColumnHeader[] {
            this._columnHeader1,
            this._columnHeader2,
            this._columnHeader3,
            this._columnHeader4});
            this.lstMain.ContextMenuStrip = this.cxtListView;
            this.lstMain.Dock = DockStyle.Fill;
            this.lstMain.Enabled = false;
            this.lstMain.FullRowSelect = true;
            this.lstMain.GridLines = true;
            this.lstMain.HideSelection = false;
            this.lstMain.Location = new Point(0, 0);
            this.lstMain.Name = "lstMain";
            this.lstMain.Size = new Size(477, 326);
            this.lstMain.SmallImageList = this.imgMain;
            this.lstMain.TabIndex = 0;
            this.lstMain.UseCompatibleStateImageBehavior = false;
            this.lstMain.View = View.Details;
            this.lstMain.MouseDoubleClick += new MouseEventHandler(this.LstMain_MouseDoubleClick);
            // 
            // _columnHeader1
            // 
            this._columnHeader1.Text = "Name";
            // 
            // _columnHeader2
            // 
            this._columnHeader2.Text = "Type";
            // 
            // _columnHeader3
            // 
            this._columnHeader3.Text = "Size";
            this._columnHeader3.Width = 97;
            // 
            // _columnHeader4
            // 
            this._columnHeader4.Text = "Last Modified";
            // 
            // cxtListView
            // 
            this.cxtListView.Items.AddRange(new ToolStripItem[] {
            this.itmCxtOpen});
            this.cxtListView.Name = "cxtListView";
            this.cxtListView.Size = new Size(181, 48);
            this.cxtListView.Opening += new CancelEventHandler(this.CxtListView_Opening);
            // 
            // itmCxtOpen
            // 
            this.itmCxtOpen.Image = Resources.magnifier;
            this.itmCxtOpen.Name = "itmCxtOpen";
            this.itmCxtOpen.Size = new Size(180, 22);
            this.itmCxtOpen.Text = "Open";
            this.itmCxtOpen.Click += new EventHandler(this.ItmCxtOpen_Click);
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new ToolStripItem[] {
            this._fileToolStripMenuItem,
            this.itmAbout,
            this.itmActions,
            this.itmOptions,
            this.itmRefresh});
            this.menuMain.Location = new Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new Size(683, 24);
            this.menuMain.TabIndex = 1;
            this.menuMain.Text = "menuMain";
            // 
            // _fileToolStripMenuItem
            // 
            this._fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            this.itmLoadGame,
            this.itmRecentGame,
            this.sepMain,
            this.itmQuit});
            this._fileToolStripMenuItem.Image = Resources.brick_go;
            this._fileToolStripMenuItem.Name = "_fileToolStripMenuItem";
            this._fileToolStripMenuItem.Size = new Size(53, 20);
            this._fileToolStripMenuItem.Text = "File";
            // 
            // itmLoadGame
            // 
            this.itmLoadGame.Image = Resources.folder_brick;
            this.itmLoadGame.Name = "itmLoadGame";
            this.itmLoadGame.ShortcutKeys = ((Keys)((Keys.Control | Keys.L)));
            this.itmLoadGame.Size = new Size(183, 22);
            this.itmLoadGame.Text = "Load Game...";
            this.itmLoadGame.Click += new EventHandler(this.ItmLoadGame_Click);
            // 
            // itmRecentGame
            // 
            this.itmRecentGame.Image = Resources.folder_go;
            this.itmRecentGame.Name = "itmRecentGame";
            this.itmRecentGame.Size = new Size(183, 22);
            this.itmRecentGame.Text = "Recent Game...";
            // 
            // sepMain
            // 
            this.sepMain.Name = "sepMain";
            this.sepMain.Size = new Size(180, 6);
            // 
            // itmQuit
            // 
            this.itmQuit.Image = ((Image)(resources.GetObject("itmQuit.Image")));
            this.itmQuit.Name = "itmQuit";
            this.itmQuit.ShortcutKeys = ((Keys)((Keys.Alt | Keys.F4)));
            this.itmQuit.Size = new Size(183, 22);
            this.itmQuit.Text = "Quit";
            this.itmQuit.Click += new EventHandler(this.ItmQuit_Click);
            // 
            // itmAbout
            // 
            this.itmAbout.Alignment = ToolStripItemAlignment.Right;
            this.itmAbout.Image = Resources.BlackFigure;
            this.itmAbout.Name = "itmAbout";
            this.itmAbout.Size = new Size(68, 20);
            this.itmAbout.Text = "About";
            this.itmAbout.Click += new EventHandler(this.ItmAbout_Click);
            // 
            // itmActions
            // 
            this.itmActions.DropDownItems.AddRange(new ToolStripItem[] {
            this.itmGameModReady});
            this.itmActions.Enabled = false;
            this.itmActions.Image = Resources.cog;
            this.itmActions.Name = "itmActions";
            this.itmActions.Size = new Size(75, 20);
            this.itmActions.Text = "Actions";
            // 
            // itmGameModReady
            // 
            this.itmGameModReady.Name = "itmGameModReady";
            this.itmGameModReady.Size = new Size(196, 22);
            this.itmGameModReady.Text = "Do Game \"Mod Ready\"";
            this.itmGameModReady.Click += new EventHandler(this.ItmGameModReady_Click);
            // 
            // itmOptions
            // 
            this.itmOptions.DropDownItems.AddRange(new ToolStripItem[] {
            this.itmOptionDoubleClick,
            this.itmOptionRightClick});
            this.itmOptions.Enabled = false;
            this.itmOptions.Image = Resources.wrench;
            this.itmOptions.Name = "itmOptions";
            this.itmOptions.Size = new Size(77, 20);
            this.itmOptions.Text = "Options";
            // 
            // itmOptionDoubleClick
            // 
            this.itmOptionDoubleClick.Checked = true;
            this.itmOptionDoubleClick.CheckOnClick = true;
            this.itmOptionDoubleClick.CheckState = CheckState.Checked;
            this.itmOptionDoubleClick.Name = "itmOptionDoubleClick";
            this.itmOptionDoubleClick.Size = new Size(234, 22);
            this.itmOptionDoubleClick.Text = "Open File by Double Click";
            // 
            // itmOptionRightClick
            // 
            this.itmOptionRightClick.Checked = true;
            this.itmOptionRightClick.CheckOnClick = true;
            this.itmOptionRightClick.CheckState = CheckState.Checked;
            this.itmOptionRightClick.Name = "itmOptionRightClick";
            this.itmOptionRightClick.Size = new Size(234, 22);
            this.itmOptionRightClick.Text = "Open File by Right Click Menu";
            // 
            // itmRefresh
            // 
            this.itmRefresh.Enabled = false;
            this.itmRefresh.Image = Resources.arrow_refresh;
            this.itmRefresh.Name = "itmRefresh";
            this.itmRefresh.ShortcutKeys = ((Keys)((Keys.Control | Keys.R)));
            this.itmRefresh.ShowShortcutKeys = false;
            this.itmRefresh.Size = new Size(74, 20);
            this.itmRefresh.Text = "Refresh";
            this.itmRefresh.Click += new EventHandler(this.ItmRefresh_Click);
            // 
            // ofdOpenExecutable
            // 
            this.ofdOpenExecutable.Filter = "Lego Games Executables|*.exe";
            // 
            // statusMain
            // 
            this.statusMain.AllowMerge = false;
            this.statusMain.Items.AddRange(new ToolStripItem[] {
            this._toolStripStatusLabel1,
            this.lblStatus,
            this.pbMain});
            this.statusMain.Location = new Point(0, 350);
            this.statusMain.Name = "statusMain";
            this.statusMain.Size = new Size(683, 22);
            this.statusMain.TabIndex = 2;
            this.statusMain.Text = "statusMain";
            // 
            // _toolStripStatusLabel1
            // 
            this._toolStripStatusLabel1.Name = "_toolStripStatusLabel1";
            this._toolStripStatusLabel1.Size = new Size(0, 17);
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(566, 17);
            this.lblStatus.Spring = true;
            // 
            // pbMain
            // 
            this.pbMain.Maximum = 0;
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new Size(100, 16);
            this.pbMain.Step = 1;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(683, 372);
            this.Controls.Add(this.containerMain);
            this.Controls.Add(this.menuMain);
            this.Controls.Add(this.statusMain);
            this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuMain;
            this.Name = "Home";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "TT Games Explorer [BETA]";
            this.Load += new EventHandler(this.Home_Load);
            this.containerMain.Panel1.ResumeLayout(false);
            this.containerMain.Panel2.ResumeLayout(false);
            ((ISupportInitialize)(this.containerMain)).EndInit();
            this.containerMain.ResumeLayout(false);
            this.cxtListView.ResumeLayout(false);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.statusMain.ResumeLayout(false);
            this.statusMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private string _legoGameFolder;
        private string _listviewFileSelected;
        private ThreadStart _readDir;
        private Thread _thReadDir;
        private TreeNode _newSelected;
        private Control _focused;
        private SplitContainer containerMain;
        private TreeView trvMain;
        private ImageList imgMain;
        private ListView lstMain;
        private ColumnHeader _columnHeader1;
        private ColumnHeader _columnHeader2;
        private ColumnHeader _columnHeader3;
        private MenuStrip menuMain;
        private ToolStripMenuItem _fileToolStripMenuItem;
        private ToolStripMenuItem itmLoadGame;
        private FolderBrowserDialog fbdOpenGameFolder;
        private ToolStripSeparator sepMain;
        private ToolStripMenuItem itmQuit;
        private ToolStripMenuItem itmAbout;
        private ToolStripMenuItem itmActions;
        private ToolStripMenuItem itmGameModReady;
        private ToolStripMenuItem itmOptions;
        private ColumnHeader _columnHeader4;
        private ContextMenuStrip cxtListView;
        private ToolStripMenuItem itmCxtOpen;
        private OpenFileDialog ofdOpenExecutable;
        private ToolStripMenuItem itmRecentGame;
        private StatusStrip statusMain;
        private ToolStripStatusLabel _toolStripStatusLabel1;
        private ToolStripMenuItem itmOptionDoubleClick;
        private ToolStripMenuItem itmOptionRightClick;
        private ToolStripStatusLabel lblStatus;
        private ToolStripProgressBar pbMain;
        private ToolStripMenuItem itmRefresh;
    }
}
