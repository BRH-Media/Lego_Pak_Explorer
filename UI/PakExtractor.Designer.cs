using System.ComponentModel;
using System.Windows.Forms;

namespace TT_Games_Explorer.UI
{
    partial class PakExtractor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PakExtractor));
            this.imgMain = new System.Windows.Forms.ImageList(this.components);
            this.cxtLstExtract = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itmExtract = new System.Windows.Forms.ToolStripMenuItem();
            this.itmCxtPreview = new System.Windows.Forms.ToolStripMenuItem();
            this._toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbMain = new System.Windows.Forms.ToolStripProgressBar();
            this.statusMain = new System.Windows.Forms.StatusStrip();
            this.fbdExtractFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.ofdOpenDatFile = new System.Windows.Forms.OpenFileDialog();
            this.itmExtractAll = new System.Windows.Forms.ToolStripMenuItem();
            this._toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.itmQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.itmOperation = new System.Windows.Forms.ToolStripMenuItem();
            this.itmPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.itmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.sfdExtractFile = new System.Windows.Forms.SaveFileDialog();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.itmOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.itmOptionDoubleClick = new System.Windows.Forms.ToolStripMenuItem();
            this.itmOptionRightClick = new System.Windows.Forms.ToolStripMenuItem();
            this.lstMain = new System.Windows.Forms.ListView();
            this.colIcon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOffset = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cxtLstExtract.SuspendLayout();
            this.statusMain.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.SuspendLayout();
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
            // cxtLstExtract
            // 
            this.cxtLstExtract.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmExtract,
            this.itmCxtPreview});
            this.cxtLstExtract.Name = "cxtLstExtract";
            this.cxtLstExtract.Size = new System.Drawing.Size(116, 48);
            this.cxtLstExtract.Opening += new System.ComponentModel.CancelEventHandler(this.CxtLstExtract_Opening);
            // 
            // itmExtract
            // 
            this.itmExtract.Image = global::TT_Games_Explorer.Properties.Resources.disk;
            this.itmExtract.Name = "itmExtract";
            this.itmExtract.Size = new System.Drawing.Size(115, 22);
            this.itmExtract.Text = "Extract";
            this.itmExtract.Click += new System.EventHandler(this.ItmExtract_Click);
            // 
            // itmCxtPreview
            // 
            this.itmCxtPreview.Name = "itmCxtPreview";
            this.itmCxtPreview.Size = new System.Drawing.Size(115, 22);
            this.itmCxtPreview.Text = "Preview";
            this.itmCxtPreview.Click += new System.EventHandler(this.ItmCxtPreview_Click);
            // 
            // _toolStripStatusLabel1
            // 
            this._toolStripStatusLabel1.Name = "_toolStripStatusLabel1";
            this._toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(697, 17);
            this.lblStatus.Spring = true;
            // 
            // pbMain
            // 
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(100, 16);
            // 
            // statusMain
            // 
            this.statusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolStripStatusLabel1,
            this.lblStatus,
            this.pbMain});
            this.statusMain.Location = new System.Drawing.Point(0, 429);
            this.statusMain.Name = "statusMain";
            this.statusMain.Size = new System.Drawing.Size(814, 22);
            this.statusMain.TabIndex = 5;
            this.statusMain.Text = "statusMain";
            // 
            // fbdExtractFolder
            // 
            this.fbdExtractFolder.Description = "Choose Extract Folder";
            this.fbdExtractFolder.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // ofdOpenDatFile
            // 
            this.ofdOpenDatFile.Filter = "Lego Dat Files|*.dat|All Files|*.*";
            // 
            // itmExtractAll
            // 
            this.itmExtractAll.Image = global::TT_Games_Explorer.Properties.Resources.disk_multiple;
            this.itmExtractAll.Name = "itmExtractAll";
            this.itmExtractAll.Size = new System.Drawing.Size(127, 22);
            this.itmExtractAll.Text = "Extract All";
            this.itmExtractAll.Click += new System.EventHandler(this.ItmExtractAll_Click);
            // 
            // _toolStripSeparator1
            // 
            this._toolStripSeparator1.Name = "_toolStripSeparator1";
            this._toolStripSeparator1.Size = new System.Drawing.Size(124, 6);
            // 
            // itmQuit
            // 
            this.itmQuit.Image = ((System.Drawing.Image)(resources.GetObject("itmQuit.Image")));
            this.itmQuit.Name = "itmQuit";
            this.itmQuit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.itmQuit.Size = new System.Drawing.Size(127, 22);
            this.itmQuit.Text = "Quit";
            this.itmQuit.Click += new System.EventHandler(this.ItmQuit_Click);
            // 
            // itmOperation
            // 
            this.itmOperation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmPreview});
            this.itmOperation.Enabled = false;
            this.itmOperation.Image = global::TT_Games_Explorer.Properties.Resources.cog;
            this.itmOperation.Name = "itmOperation";
            this.itmOperation.Size = new System.Drawing.Size(88, 20);
            this.itmOperation.Text = "Operation";
            // 
            // itmPreview
            // 
            this.itmPreview.Name = "itmPreview";
            this.itmPreview.Size = new System.Drawing.Size(180, 22);
            this.itmPreview.Text = "Preview";
            this.itmPreview.Click += new System.EventHandler(this.ItmPreview_Click);
            // 
            // itmFile
            // 
            this.itmFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmExtractAll,
            this._toolStripSeparator1,
            this.itmQuit});
            this.itmFile.Image = global::TT_Games_Explorer.Properties.Resources.brick_go;
            this.itmFile.Name = "itmFile";
            this.itmFile.Size = new System.Drawing.Size(53, 20);
            this.itmFile.Text = "File";
            // 
            // sfdExtractFile
            // 
            this.sfdExtractFile.Filter = "All Files|*.*";
            // 
            // menuMain
            // 
            this.menuMain.BackColor = System.Drawing.SystemColors.Control;
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmFile,
            this.itmOptions,
            this.itmOperation});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(814, 24);
            this.menuMain.TabIndex = 4;
            this.menuMain.Text = "menuStrip1";
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
            // lstMain
            // 
            this.lstMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colIcon,
            this.colID,
            this.colName,
            this.colType,
            this.colOffset,
            this.colSize});
            this.lstMain.ContextMenuStrip = this.cxtLstExtract;
            this.lstMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstMain.Enabled = false;
            this.lstMain.FullRowSelect = true;
            this.lstMain.GridLines = true;
            this.lstMain.HideSelection = false;
            this.lstMain.Location = new System.Drawing.Point(0, 24);
            this.lstMain.Name = "lstMain";
            this.lstMain.Size = new System.Drawing.Size(814, 405);
            this.lstMain.SmallImageList = this.imgMain;
            this.lstMain.TabIndex = 14;
            this.lstMain.UseCompatibleStateImageBehavior = false;
            this.lstMain.View = System.Windows.Forms.View.Details;
            this.lstMain.SelectedIndexChanged += new System.EventHandler(this.LstMain_SelectedIndexChanged);
            this.lstMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LstMain_MouseDoubleClick);
            // 
            // colIcon
            // 
            this.colIcon.Text = "";
            // 
            // colID
            // 
            this.colID.Text = "ID";
            this.colID.Width = 31;
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 190;
            // 
            // colType
            // 
            this.colType.Text = "Type";
            // 
            // colOffset
            // 
            this.colOffset.Text = "Offset";
            // 
            // colSize
            // 
            this.colSize.Text = "Size";
            // 
            // PakExtractor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 451);
            this.Controls.Add(this.lstMain);
            this.Controls.Add(this.statusMain);
            this.Controls.Add(this.menuMain);
            this.MinimizeBox = false;
            this.Name = "PakExtractor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pak Extractor";
            this.Load += new System.EventHandler(this.PakExtractor_Load);
            this.cxtLstExtract.ResumeLayout(false);
            this.statusMain.ResumeLayout(false);
            this.statusMain.PerformLayout();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ImageList imgMain;
        private ContextMenuStrip cxtLstExtract;
        private ToolStripMenuItem itmExtract;
        private ToolStripMenuItem itmCxtPreview;
        private ToolStripStatusLabel _toolStripStatusLabel1;
        private ToolStripStatusLabel lblStatus;
        private ToolStripProgressBar pbMain;
        private StatusStrip statusMain;
        private FolderBrowserDialog fbdExtractFolder;
        private OpenFileDialog ofdOpenDatFile;
        private ToolStripMenuItem itmExtractAll;
        private ToolStripSeparator _toolStripSeparator1;
        private ToolStripMenuItem itmQuit;
        private ToolStripMenuItem itmOperation;
        private ToolStripMenuItem itmPreview;
        private ToolStripMenuItem itmFile;
        private SaveFileDialog sfdExtractFile;
        private MenuStrip menuMain;
        private ListView lstMain;
        private ColumnHeader colIcon;
        private ColumnHeader colID;
        private ColumnHeader colName;
        private ColumnHeader colType;
        private ColumnHeader colOffset;
        private ColumnHeader colSize;
        private ToolStripMenuItem itmOptions;
        private ToolStripMenuItem itmOptionDoubleClick;
        private ToolStripMenuItem itmOptionRightClick;
    }
}