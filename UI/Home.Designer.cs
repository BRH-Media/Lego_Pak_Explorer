using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.containerMain = new System.Windows.Forms.SplitContainer();
            this.trvMain = new System.Windows.Forms.TreeView();
            this.imgMain = new System.Windows.Forms.ImageList(this.components);
            this.lstMain = new System.Windows.Forms.ListView();
            this._columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cxtListView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itmCxtOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this._fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itmLoadGame = new System.Windows.Forms.ToolStripMenuItem();
            this.itmRecentGame = new System.Windows.Forms.ToolStripMenuItem();
            this.sepMain = new System.Windows.Forms.ToolStripSeparator();
            this.itmQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.itmAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.itmActions = new System.Windows.Forms.ToolStripMenuItem();
            this.itmGameModReady = new System.Windows.Forms.ToolStripMenuItem();
            this.itmOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.itmOptionDoubleClick = new System.Windows.Forms.ToolStripMenuItem();
            this.itmOptionRightClick = new System.Windows.Forms.ToolStripMenuItem();
            this.itmRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.fbdOpenGameFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.ofdOpenExecutable = new System.Windows.Forms.OpenFileDialog();
            this.statusMain = new System.Windows.Forms.StatusStrip();
            this._toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbMain = new System.Windows.Forms.ToolStripProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.containerMain)).BeginInit();
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
            this.containerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerMain.Enabled = false;
            this.containerMain.Location = new System.Drawing.Point(0, 24);
            this.containerMain.Margin = new System.Windows.Forms.Padding(3, 3, 3, 22);
            this.containerMain.Name = "containerMain";
            // 
            // containerMain.Panel1
            // 
            this.containerMain.Panel1.Controls.Add(this.trvMain);
            // 
            // containerMain.Panel2
            // 
            this.containerMain.Panel2.Controls.Add(this.lstMain);
            this.containerMain.Size = new System.Drawing.Size(683, 326);
            this.containerMain.SplitterDistance = 202;
            this.containerMain.TabIndex = 0;
            this.containerMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ContainerMain_MouseDown);
            this.containerMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ContainerMain_MouseUp);
            // 
            // trvMain
            // 
            this.trvMain.BackColor = System.Drawing.SystemColors.MenuBar;
            this.trvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvMain.Enabled = false;
            this.trvMain.ImageIndex = 0;
            this.trvMain.ImageList = this.imgMain;
            this.trvMain.Location = new System.Drawing.Point(0, 0);
            this.trvMain.Name = "trvMain";
            this.trvMain.SelectedImageIndex = 0;
            this.trvMain.Size = new System.Drawing.Size(202, 326);
            this.trvMain.TabIndex = 0;
            this.trvMain.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.TrvMain_AfterExpand);
            this.trvMain.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TrvMain_NodeMouseClick);
            // 
            // imgMain
            // 
            this.imgMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgMain.ImageStream")));
            this.imgMain.TransparentColor = System.Drawing.Color.Transparent;
            this.imgMain.Images.SetKeyName(0, "folder.png");
            this.imgMain.Images.SetKeyName(1, "page.png");
            this.imgMain.Images.SetKeyName(2, "page_code.png");
            this.imgMain.Images.SetKeyName(3, "brick.png");
            this.imgMain.Images.SetKeyName(4, "image.png");
            this.imgMain.Images.SetKeyName(5, "application.png");
            // 
            // lstMain
            // 
            this.lstMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._columnHeader1,
            this._columnHeader2,
            this._columnHeader3,
            this._columnHeader4});
            this.lstMain.ContextMenuStrip = this.cxtListView;
            this.lstMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstMain.Enabled = false;
            this.lstMain.FullRowSelect = true;
            this.lstMain.GridLines = true;
            this.lstMain.HideSelection = false;
            this.lstMain.Location = new System.Drawing.Point(0, 0);
            this.lstMain.Name = "lstMain";
            this.lstMain.Size = new System.Drawing.Size(477, 326);
            this.lstMain.SmallImageList = this.imgMain;
            this.lstMain.TabIndex = 0;
            this.lstMain.UseCompatibleStateImageBehavior = false;
            this.lstMain.View = System.Windows.Forms.View.Details;
            this.lstMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LstMain_MouseDoubleClick);
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
            this.cxtListView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmCxtOpen});
            this.cxtListView.Name = "cxtListView";
            this.cxtListView.Size = new System.Drawing.Size(181, 48);
            this.cxtListView.Opening += new System.ComponentModel.CancelEventHandler(this.CxtListView_Opening);
            // 
            // itmCxtOpen
            // 
            this.itmCxtOpen.Image = global::TT_Games_Explorer.Properties.Resources.magnifier;
            this.itmCxtOpen.Name = "itmCxtOpen";
            this.itmCxtOpen.Size = new System.Drawing.Size(180, 22);
            this.itmCxtOpen.Text = "Open";
            this.itmCxtOpen.Click += new System.EventHandler(this.ItmCxtOpen_Click);
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._fileToolStripMenuItem,
            this.itmAbout,
            this.itmActions,
            this.itmOptions,
            this.itmRefresh});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(683, 24);
            this.menuMain.TabIndex = 1;
            this.menuMain.Text = "menuMain";
            // 
            // _fileToolStripMenuItem
            // 
            this._fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmLoadGame,
            this.itmRecentGame,
            this.sepMain,
            this.itmQuit});
            this._fileToolStripMenuItem.Image = global::TT_Games_Explorer.Properties.Resources.brick_go;
            this._fileToolStripMenuItem.Name = "_fileToolStripMenuItem";
            this._fileToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this._fileToolStripMenuItem.Text = "File";
            // 
            // itmLoadGame
            // 
            this.itmLoadGame.Image = global::TT_Games_Explorer.Properties.Resources.folder_brick;
            this.itmLoadGame.Name = "itmLoadGame";
            this.itmLoadGame.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.itmLoadGame.Size = new System.Drawing.Size(183, 22);
            this.itmLoadGame.Text = "Load Game...";
            this.itmLoadGame.Click += new System.EventHandler(this.ItmLoadGame_Click);
            // 
            // itmRecentGame
            // 
            this.itmRecentGame.Image = global::TT_Games_Explorer.Properties.Resources.folder_go;
            this.itmRecentGame.Name = "itmRecentGame";
            this.itmRecentGame.Size = new System.Drawing.Size(183, 22);
            this.itmRecentGame.Text = "Recent Game...";
            // 
            // sepMain
            // 
            this.sepMain.Name = "sepMain";
            this.sepMain.Size = new System.Drawing.Size(180, 6);
            // 
            // itmQuit
            // 
            this.itmQuit.Image = ((System.Drawing.Image)(resources.GetObject("itmQuit.Image")));
            this.itmQuit.Name = "itmQuit";
            this.itmQuit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.itmQuit.Size = new System.Drawing.Size(183, 22);
            this.itmQuit.Text = "Quit";
            this.itmQuit.Click += new System.EventHandler(this.ItmQuit_Click);
            // 
            // itmAbout
            // 
            this.itmAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.itmAbout.Image = global::TT_Games_Explorer.Properties.Resources.BlackFigure;
            this.itmAbout.Name = "itmAbout";
            this.itmAbout.Size = new System.Drawing.Size(68, 20);
            this.itmAbout.Text = "About";
            this.itmAbout.Click += new System.EventHandler(this.ItmAbout_Click);
            // 
            // itmActions
            // 
            this.itmActions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmGameModReady});
            this.itmActions.Enabled = false;
            this.itmActions.Image = global::TT_Games_Explorer.Properties.Resources.cog;
            this.itmActions.Name = "itmActions";
            this.itmActions.Size = new System.Drawing.Size(75, 20);
            this.itmActions.Text = "Actions";
            // 
            // itmGameModReady
            // 
            this.itmGameModReady.Name = "itmGameModReady";
            this.itmGameModReady.Size = new System.Drawing.Size(196, 22);
            this.itmGameModReady.Text = "Do Game \"Mod Ready\"";
            this.itmGameModReady.Click += new System.EventHandler(this.ItmGameModReady_Click);
            // 
            // itmOptions
            // 
            this.itmOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmOptionDoubleClick,
            this.itmOptionRightClick});
            this.itmOptions.Enabled = false;
            this.itmOptions.Image = global::TT_Games_Explorer.Properties.Resources.wrench;
            this.itmOptions.Name = "itmOptions";
            this.itmOptions.Size = new System.Drawing.Size(77, 20);
            this.itmOptions.Text = "Options";
            // 
            // itmOptionDoubleClick
            // 
            this.itmOptionDoubleClick.Checked = true;
            this.itmOptionDoubleClick.CheckOnClick = true;
            this.itmOptionDoubleClick.CheckState = System.Windows.Forms.CheckState.Checked;
            this.itmOptionDoubleClick.Name = "itmOptionDoubleClick";
            this.itmOptionDoubleClick.Size = new System.Drawing.Size(234, 22);
            this.itmOptionDoubleClick.Text = "Open File by Double Click";
            // 
            // itmOptionRightClick
            // 
            this.itmOptionRightClick.Checked = true;
            this.itmOptionRightClick.CheckOnClick = true;
            this.itmOptionRightClick.CheckState = System.Windows.Forms.CheckState.Checked;
            this.itmOptionRightClick.Name = "itmOptionRightClick";
            this.itmOptionRightClick.Size = new System.Drawing.Size(234, 22);
            this.itmOptionRightClick.Text = "Open File by Right Click Menu";
            // 
            // itmRefresh
            // 
            this.itmRefresh.Enabled = false;
            this.itmRefresh.Image = global::TT_Games_Explorer.Properties.Resources.arrow_refresh;
            this.itmRefresh.Name = "itmRefresh";
            this.itmRefresh.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.itmRefresh.ShowShortcutKeys = false;
            this.itmRefresh.Size = new System.Drawing.Size(74, 20);
            this.itmRefresh.Text = "Refresh";
            this.itmRefresh.Click += new System.EventHandler(this.ItmRefresh_Click);
            // 
            // ofdOpenExecutable
            // 
            this.ofdOpenExecutable.Filter = "Lego Games Executables|*.exe";
            // 
            // statusMain
            // 
            this.statusMain.AllowMerge = false;
            this.statusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolStripStatusLabel1,
            this.lblStatus,
            this.pbMain});
            this.statusMain.Location = new System.Drawing.Point(0, 350);
            this.statusMain.Name = "statusMain";
            this.statusMain.Size = new System.Drawing.Size(683, 22);
            this.statusMain.TabIndex = 2;
            this.statusMain.Text = "statusMain";
            // 
            // _toolStripStatusLabel1
            // 
            this._toolStripStatusLabel1.Name = "_toolStripStatusLabel1";
            this._toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(566, 17);
            this.lblStatus.Spring = true;
            // 
            // pbMain
            // 
            this.pbMain.Maximum = 0;
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(100, 16);
            this.pbMain.Step = 1;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 372);
            this.Controls.Add(this.containerMain);
            this.Controls.Add(this.menuMain);
            this.Controls.Add(this.statusMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuMain;
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TT Games Explorer [BETA]";
            this.Load += new System.EventHandler(this.Home_Load);
            this.containerMain.Panel1.ResumeLayout(false);
            this.containerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.containerMain)).EndInit();
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
