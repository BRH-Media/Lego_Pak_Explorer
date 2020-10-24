using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TT_Games_Explorer.Properties;

namespace TT_Games_Explorer.UI
{
    public partial class DatExtractor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatExtractor));
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.itmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.itmExtractAll = new System.Windows.Forms.ToolStripMenuItem();
            this._toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.itmQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.itmOperation = new System.Windows.Forms.ToolStripMenuItem();
            this.itmPlaySound = new System.Windows.Forms.ToolStripMenuItem();
            this.itmViewTexture = new System.Windows.Forms.ToolStripMenuItem();
            this.itmViewModel = new System.Windows.Forms.ToolStripMenuItem();
            this.itmViewCode = new System.Windows.Forms.ToolStripMenuItem();
            this.ofdOpenDatFile = new System.Windows.Forms.OpenFileDialog();
            this.cxtLstExtract = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._extractToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itmCxtPreviewTexture = new System.Windows.Forms.ToolStripMenuItem();
            this.itmCxtViewCode = new System.Windows.Forms.ToolStripMenuItem();
            this.statusMain = new System.Windows.Forms.StatusStrip();
            this._toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbMain = new System.Windows.Forms.ToolStripProgressBar();
            this.fbdExtractFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.containerMain = new System.Windows.Forms.SplitContainer();
            this._splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lblArchiveOffset = new System.Windows.Forms.Label();
            this.txtNameInfo = new System.Windows.Forms.TextBox();
            this.lblFileInfo = new System.Windows.Forms.Label();
            this.txtNameCrc = new System.Windows.Forms.TextBox();
            this.txtFileInfo = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblNameInfo = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblNameCRC = new System.Windows.Forms.Label();
            this.trvMain = new System.Windows.Forms.TreeView();
            this.imgMain = new System.Windows.Forms.ImageList(this.components);
            this.lstMain = new System.Windows.Forms.ListView();
            this.colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCrc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOffset = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSizeUnComp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPack = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sfdExtractFile = new System.Windows.Forms.SaveFileDialog();
            this.colIcon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuMain.SuspendLayout();
            this.cxtLstExtract.SuspendLayout();
            this.statusMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.containerMain)).BeginInit();
            this.containerMain.Panel1.SuspendLayout();
            this.containerMain.Panel2.SuspendLayout();
            this.containerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer2)).BeginInit();
            this._splitContainer2.Panel1.SuspendLayout();
            this._splitContainer2.Panel2.SuspendLayout();
            this._splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.BackColor = System.Drawing.SystemColors.Control;
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmFile,
            this.itmOperation});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(814, 24);
            this.menuMain.TabIndex = 0;
            this.menuMain.Text = "menuStrip1";
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
            // itmExtractAll
            // 
            this.itmExtractAll.Image = global::TT_Games_Explorer.Properties.Resources.disk_multiple;
            this.itmExtractAll.Name = "itmExtractAll";
            this.itmExtractAll.Size = new System.Drawing.Size(127, 22);
            this.itmExtractAll.Text = "Extract All";
            this.itmExtractAll.Click += new System.EventHandler(this.ExtractAllMenuItem_Click);
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
            this.itmQuit.Click += new System.EventHandler(this.QuitToolStripMenuItem_Click);
            // 
            // itmOperation
            // 
            this.itmOperation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmPlaySound,
            this.itmViewTexture,
            this.itmViewModel,
            this.itmViewCode});
            this.itmOperation.Name = "itmOperation";
            this.itmOperation.Size = new System.Drawing.Size(72, 20);
            this.itmOperation.Text = "Operation";
            // 
            // itmPlaySound
            // 
            this.itmPlaySound.Name = "itmPlaySound";
            this.itmPlaySound.Size = new System.Drawing.Size(140, 22);
            this.itmPlaySound.Text = "Play Sound";
            this.itmPlaySound.Click += new System.EventHandler(this.ItmPlaySound_Click);
            // 
            // itmViewTexture
            // 
            this.itmViewTexture.Name = "itmViewTexture";
            this.itmViewTexture.Size = new System.Drawing.Size(140, 22);
            this.itmViewTexture.Text = "View Texture";
            this.itmViewTexture.Click += new System.EventHandler(this.ItmViewTexture_Click);
            // 
            // itmViewModel
            // 
            this.itmViewModel.Name = "itmViewModel";
            this.itmViewModel.Size = new System.Drawing.Size(140, 22);
            this.itmViewModel.Text = "View Model";
            this.itmViewModel.Click += new System.EventHandler(this.ItmViewModel_Click);
            // 
            // itmViewCode
            // 
            this.itmViewCode.Name = "itmViewCode";
            this.itmViewCode.Size = new System.Drawing.Size(140, 22);
            this.itmViewCode.Text = "View Code";
            this.itmViewCode.Click += new System.EventHandler(this.ItmViewCode_Click);
            // 
            // ofdOpenDatFile
            // 
            this.ofdOpenDatFile.Filter = "Lego Dat Files|*.dat|All Files|*.*";
            // 
            // cxtLstExtract
            // 
            this.cxtLstExtract.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._extractToolStripMenuItem,
            this.itmCxtPreviewTexture,
            this.itmCxtViewCode});
            this.cxtLstExtract.Name = "_contextMenuStrip1";
            this.cxtLstExtract.Size = new System.Drawing.Size(157, 70);
            this.cxtLstExtract.Opening += new System.ComponentModel.CancelEventHandler(this.CxtLstExtract_Opening);
            // 
            // _extractToolStripMenuItem
            // 
            this._extractToolStripMenuItem.Image = global::TT_Games_Explorer.Properties.Resources.disk;
            this._extractToolStripMenuItem.Name = "_extractToolStripMenuItem";
            this._extractToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this._extractToolStripMenuItem.Text = "Extract";
            this._extractToolStripMenuItem.Click += new System.EventHandler(this.ExtractToolStripMenuItem_Click);
            // 
            // itmCxtPreviewTexture
            // 
            this.itmCxtPreviewTexture.Name = "itmCxtPreviewTexture";
            this.itmCxtPreviewTexture.Size = new System.Drawing.Size(156, 22);
            this.itmCxtPreviewTexture.Text = "Preview Texture";
            this.itmCxtPreviewTexture.Click += new System.EventHandler(this.ItmCxtPreviewTexture_Click);
            // 
            // itmCxtViewCode
            // 
            this.itmCxtViewCode.Name = "itmCxtViewCode";
            this.itmCxtViewCode.Size = new System.Drawing.Size(156, 22);
            this.itmCxtViewCode.Text = "View Code";
            this.itmCxtViewCode.Click += new System.EventHandler(this.ItmCxtViewCode_Click);
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
            this.statusMain.TabIndex = 2;
            this.statusMain.Text = "statusStrip1";
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
            // fbdExtractFolder
            // 
            this.fbdExtractFolder.Description = "Choose Extract Folder";
            this.fbdExtractFolder.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // containerMain
            // 
            this.containerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerMain.Location = new System.Drawing.Point(0, 24);
            this.containerMain.Name = "containerMain";
            // 
            // containerMain.Panel1
            // 
            this.containerMain.Panel1.Controls.Add(this._splitContainer2);
            this.containerMain.Panel1MinSize = 206;
            // 
            // containerMain.Panel2
            // 
            this.containerMain.Panel2.Controls.Add(this.lstMain);
            this.containerMain.Size = new System.Drawing.Size(814, 405);
            this.containerMain.SplitterDistance = 206;
            this.containerMain.TabIndex = 3;
            // 
            // _splitContainer2
            // 
            this._splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this._splitContainer2.Location = new System.Drawing.Point(0, 0);
            this._splitContainer2.Name = "_splitContainer2";
            this._splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _splitContainer2.Panel1
            // 
            this._splitContainer2.Panel1.Controls.Add(this.lblArchiveOffset);
            this._splitContainer2.Panel1.Controls.Add(this.txtNameInfo);
            this._splitContainer2.Panel1.Controls.Add(this.lblFileInfo);
            this._splitContainer2.Panel1.Controls.Add(this.txtNameCrc);
            this._splitContainer2.Panel1.Controls.Add(this.txtFileInfo);
            this._splitContainer2.Panel1.Controls.Add(this.lblName);
            this._splitContainer2.Panel1.Controls.Add(this.lblNameInfo);
            this._splitContainer2.Panel1.Controls.Add(this.txtName);
            this._splitContainer2.Panel1.Controls.Add(this.lblNameCRC);
            this._splitContainer2.Panel1MinSize = 139;
            // 
            // _splitContainer2.Panel2
            // 
            this._splitContainer2.Panel2.Controls.Add(this.trvMain);
            this._splitContainer2.Size = new System.Drawing.Size(206, 405);
            this._splitContainer2.SplitterDistance = 139;
            this._splitContainer2.TabIndex = 0;
            // 
            // lblArchiveOffset
            // 
            this.lblArchiveOffset.AutoSize = true;
            this.lblArchiveOffset.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblArchiveOffset.Location = new System.Drawing.Point(12, 9);
            this.lblArchiveOffset.Name = "lblArchiveOffset";
            this.lblArchiveOffset.Size = new System.Drawing.Size(83, 13);
            this.lblArchiveOffset.TabIndex = 23;
            this.lblArchiveOffset.Text = "Archive Offset:";
            // 
            // txtNameInfo
            // 
            this.txtNameInfo.Location = new System.Drawing.Point(81, 55);
            this.txtNameInfo.Name = "txtNameInfo";
            this.txtNameInfo.ReadOnly = true;
            this.txtNameInfo.Size = new System.Drawing.Size(108, 20);
            this.txtNameInfo.TabIndex = 20;
            // 
            // lblFileInfo
            // 
            this.lblFileInfo.AutoSize = true;
            this.lblFileInfo.Location = new System.Drawing.Point(12, 32);
            this.lblFileInfo.Name = "lblFileInfo";
            this.lblFileInfo.Size = new System.Drawing.Size(47, 13);
            this.lblFileInfo.TabIndex = 15;
            this.lblFileInfo.Text = "File Info:";
            // 
            // txtNameCrc
            // 
            this.txtNameCrc.Location = new System.Drawing.Point(81, 81);
            this.txtNameCrc.Name = "txtNameCrc";
            this.txtNameCrc.ReadOnly = true;
            this.txtNameCrc.Size = new System.Drawing.Size(108, 20);
            this.txtNameCrc.TabIndex = 21;
            // 
            // txtFileInfo
            // 
            this.txtFileInfo.Location = new System.Drawing.Point(81, 29);
            this.txtFileInfo.Name = "txtFileInfo";
            this.txtFileInfo.ReadOnly = true;
            this.txtFileInfo.Size = new System.Drawing.Size(108, 20);
            this.txtFileInfo.TabIndex = 16;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 110);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 19;
            this.lblName.Text = "Name:";
            // 
            // lblNameInfo
            // 
            this.lblNameInfo.AutoSize = true;
            this.lblNameInfo.Location = new System.Drawing.Point(12, 58);
            this.lblNameInfo.Name = "lblNameInfo";
            this.lblNameInfo.Size = new System.Drawing.Size(59, 13);
            this.lblNameInfo.TabIndex = 17;
            this.lblNameInfo.Text = "Name Info:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(81, 107);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(108, 20);
            this.txtName.TabIndex = 22;
            // 
            // lblNameCRC
            // 
            this.lblNameCRC.AutoSize = true;
            this.lblNameCRC.Location = new System.Drawing.Point(12, 84);
            this.lblNameCRC.Name = "lblNameCRC";
            this.lblNameCRC.Size = new System.Drawing.Size(63, 13);
            this.lblNameCRC.TabIndex = 18;
            this.lblNameCRC.Text = "Name CRC:";
            // 
            // trvMain
            // 
            this.trvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvMain.Enabled = false;
            this.trvMain.ImageIndex = 0;
            this.trvMain.ImageList = this.imgMain;
            this.trvMain.Location = new System.Drawing.Point(0, 0);
            this.trvMain.Name = "trvMain";
            this.trvMain.SelectedImageIndex = 0;
            this.trvMain.Size = new System.Drawing.Size(206, 262);
            this.trvMain.TabIndex = 14;
            this.trvMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TrvMain_AfterSelect);
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
            this.colIcon,
            this.colID,
            this.colName,
            this.colType,
            this.colCrc,
            this.colOffset,
            this.colSizeUnComp,
            this.colSize,
            this.colPack});
            this.lstMain.ContextMenuStrip = this.cxtLstExtract;
            this.lstMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstMain.Enabled = false;
            this.lstMain.FullRowSelect = true;
            this.lstMain.GridLines = true;
            this.lstMain.HideSelection = false;
            this.lstMain.Location = new System.Drawing.Point(0, 0);
            this.lstMain.Name = "lstMain";
            this.lstMain.Size = new System.Drawing.Size(604, 405);
            this.lstMain.SmallImageList = this.imgMain;
            this.lstMain.TabIndex = 13;
            this.lstMain.UseCompatibleStateImageBehavior = false;
            this.lstMain.View = System.Windows.Forms.View.Details;
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
            // colCrc
            // 
            this.colCrc.Text = "CRC Hash";
            // 
            // colOffset
            // 
            this.colOffset.Text = "Offset";
            // 
            // colSizeUnComp
            // 
            this.colSizeUnComp.Text = "SizeUnComp";
            // 
            // colSize
            // 
            this.colSize.Text = "Size";
            // 
            // colPack
            // 
            this.colPack.Text = "Pack";
            // 
            // sfdExtractFile
            // 
            this.sfdExtractFile.Filter = "All Files|*.*";
            // 
            // colIcon
            // 
            this.colIcon.Text = "";
            // 
            // DatExtractor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 451);
            this.Controls.Add(this.containerMain);
            this.Controls.Add(this.statusMain);
            this.Controls.Add(this.menuMain);
            this.MainMenuStrip = this.menuMain;
            this.MinimizeBox = false;
            this.Name = "DatExtractor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dat Extractor";
            this.Load += new System.EventHandler(this.DatExtractor_Load);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.cxtLstExtract.ResumeLayout(false);
            this.statusMain.ResumeLayout(false);
            this.statusMain.PerformLayout();
            this.containerMain.Panel1.ResumeLayout(false);
            this.containerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.containerMain)).EndInit();
            this.containerMain.ResumeLayout(false);
            this._splitContainer2.Panel1.ResumeLayout(false);
            this._splitContainer2.Panel1.PerformLayout();
            this._splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer2)).EndInit();
            this._splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private MenuStrip menuMain;
        private OpenFileDialog ofdOpenDatFile;
        private StatusStrip statusMain;
        private ToolStripStatusLabel _toolStripStatusLabel1;
        private ContextMenuStrip cxtLstExtract;
        private ToolStripMenuItem _extractToolStripMenuItem;
        private ToolStripStatusLabel lblStatus;
        private FolderBrowserDialog fbdExtractFolder;
        private ToolStripMenuItem itmFile;
        private ToolStripMenuItem itmExtractAll;
        private ToolStripSeparator _toolStripSeparator1;
        private ToolStripMenuItem itmQuit;
        private SplitContainer containerMain;
        private SplitContainer _splitContainer2;
        private Label lblArchiveOffset;
        private TextBox txtNameInfo;
        private Label lblFileInfo;
        private TextBox txtNameCrc;
        private TextBox txtFileInfo;
        private Label lblName;
        private Label lblNameInfo;
        private TextBox txtName;
        private Label lblNameCRC;
        private TreeView trvMain;
        private ListView lstMain;
        private ColumnHeader colID;
        private ColumnHeader colName;
        private ColumnHeader colCrc;
        private ColumnHeader colOffset;
        private ColumnHeader colSizeUnComp;
        private ColumnHeader colSize;
        private ColumnHeader colPack;
        private ToolStripProgressBar pbMain;
        private SaveFileDialog sfdExtractFile;
        private ToolStripMenuItem itmOperation;
        private ToolStripMenuItem itmPlaySound;
        private ToolStripMenuItem itmViewTexture;
        private ToolStripMenuItem itmCxtPreviewTexture;
        private ToolStripMenuItem itmViewModel;
        private ToolStripMenuItem itmViewCode;
        private ToolStripMenuItem itmCxtViewCode;
        private ImageList imgMain;
        private ColumnHeader colType;
        private ColumnHeader colIcon;
    }
}
