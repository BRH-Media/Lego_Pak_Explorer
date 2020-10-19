namespace TT_Games_Explorer.UI
{
    partial class PakExtractor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.lblArchiveOffset = new System.Windows.Forms.Label();
            this.lblFileInfo = new System.Windows.Forms.Label();
            this.txtNameCrc = new System.Windows.Forms.TextBox();
            this.txtFileInfo = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblNameInfo = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblNameCRC = new System.Windows.Forms.Label();
            this.imgMain = new System.Windows.Forms.ImageList(this.components);
            this.lstMain = new System.Windows.Forms.ListView();
            this._columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cxtLstExtract = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._extractToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtNameInfo = new System.Windows.Forms.TextBox();
            this.sfdExtractFile = new System.Windows.Forms.SaveFileDialog();
            this.containerOffset = new System.Windows.Forms.SplitContainer();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.itmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.itmExtractAll = new System.Windows.Forms.ToolStripMenuItem();
            this._toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.itmQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.itmOperation = new System.Windows.Forms.ToolStripMenuItem();
            this.itmPlaySound = new System.Windows.Forms.ToolStripMenuItem();
            this.itmViewTexture = new System.Windows.Forms.ToolStripMenuItem();
            this.ofdOpenPakFile = new System.Windows.Forms.OpenFileDialog();
            this.statusMain = new System.Windows.Forms.StatusStrip();
            this._toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbMain = new System.Windows.Forms.ToolStripProgressBar();
            this.fbdExtractFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.containerMain = new System.Windows.Forms.SplitContainer();
            this.cxtLstExtract.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.containerOffset)).BeginInit();
            this.containerOffset.Panel1.SuspendLayout();
            this.containerOffset.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.statusMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.containerMain)).BeginInit();
            this.containerMain.Panel1.SuspendLayout();
            this.containerMain.Panel2.SuspendLayout();
            this.containerMain.SuspendLayout();
            this.SuspendLayout();
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
            // imgMain
            // 
            this.imgMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgMain.ImageStream")));
            this.imgMain.TransparentColor = System.Drawing.Color.Transparent;
            this.imgMain.Images.SetKeyName(0, "folder.png");
            // 
            // lstMain
            // 
            this.lstMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._columnHeader1,
            this._columnHeader3,
            this._columnHeader2,
            this._columnHeader4,
            this._columnHeader5,
            this._columnHeader6,
            this._columnHeader7});
            this.lstMain.ContextMenuStrip = this.cxtLstExtract;
            this.lstMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstMain.Enabled = false;
            this.lstMain.FullRowSelect = true;
            this.lstMain.GridLines = true;
            this.lstMain.HideSelection = false;
            this.lstMain.Location = new System.Drawing.Point(0, 0);
            this.lstMain.Name = "lstMain";
            this.lstMain.Size = new System.Drawing.Size(604, 405);
            this.lstMain.TabIndex = 13;
            this.lstMain.UseCompatibleStateImageBehavior = false;
            this.lstMain.View = System.Windows.Forms.View.Details;
            // 
            // _columnHeader1
            // 
            this._columnHeader1.Text = "ID";
            this._columnHeader1.Width = 31;
            // 
            // _columnHeader3
            // 
            this._columnHeader3.Text = "Name";
            this._columnHeader3.Width = 190;
            // 
            // _columnHeader2
            // 
            this._columnHeader2.Text = "CRC Hash";
            // 
            // _columnHeader4
            // 
            this._columnHeader4.Text = "Offset";
            // 
            // _columnHeader5
            // 
            this._columnHeader5.Text = "SizeUnComp";
            // 
            // _columnHeader6
            // 
            this._columnHeader6.Text = "Size";
            // 
            // _columnHeader7
            // 
            this._columnHeader7.Text = "Pack";
            // 
            // cxtLstExtract
            // 
            this.cxtLstExtract.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._extractToolStripMenuItem});
            this.cxtLstExtract.Name = "_contextMenuStrip1";
            this.cxtLstExtract.Size = new System.Drawing.Size(111, 26);
            // 
            // _extractToolStripMenuItem
            // 
            this._extractToolStripMenuItem.Image = global::TT_Games_Explorer.Properties.Resources.disk;
            this._extractToolStripMenuItem.Name = "_extractToolStripMenuItem";
            this._extractToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this._extractToolStripMenuItem.Text = "Extract";
            // 
            // txtNameInfo
            // 
            this.txtNameInfo.Location = new System.Drawing.Point(81, 55);
            this.txtNameInfo.Name = "txtNameInfo";
            this.txtNameInfo.ReadOnly = true;
            this.txtNameInfo.Size = new System.Drawing.Size(108, 20);
            this.txtNameInfo.TabIndex = 20;
            // 
            // sfdExtractFile
            // 
            this.sfdExtractFile.Filter = "All Files|*.*";
            // 
            // containerOffset
            // 
            this.containerOffset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerOffset.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.containerOffset.Location = new System.Drawing.Point(0, 0);
            this.containerOffset.Name = "containerOffset";
            this.containerOffset.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // containerOffset.Panel1
            // 
            this.containerOffset.Panel1.Controls.Add(this.lblArchiveOffset);
            this.containerOffset.Panel1.Controls.Add(this.txtNameInfo);
            this.containerOffset.Panel1.Controls.Add(this.lblFileInfo);
            this.containerOffset.Panel1.Controls.Add(this.txtNameCrc);
            this.containerOffset.Panel1.Controls.Add(this.txtFileInfo);
            this.containerOffset.Panel1.Controls.Add(this.lblName);
            this.containerOffset.Panel1.Controls.Add(this.lblNameInfo);
            this.containerOffset.Panel1.Controls.Add(this.txtName);
            this.containerOffset.Panel1.Controls.Add(this.lblNameCRC);
            this.containerOffset.Panel1MinSize = 139;
            this.containerOffset.Size = new System.Drawing.Size(206, 405);
            this.containerOffset.SplitterDistance = 139;
            this.containerOffset.TabIndex = 0;
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
            this.menuMain.TabIndex = 4;
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
            this.itmExtractAll.Size = new System.Drawing.Size(180, 22);
            this.itmExtractAll.Text = "Extract All";
            this.itmExtractAll.Click += new System.EventHandler(this.ItmExtractAll_Click);
            // 
            // _toolStripSeparator1
            // 
            this._toolStripSeparator1.Name = "_toolStripSeparator1";
            this._toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // itmQuit
            // 
            this.itmQuit.Image = ((System.Drawing.Image)(resources.GetObject("itmQuit.Image")));
            this.itmQuit.Name = "itmQuit";
            this.itmQuit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.itmQuit.Size = new System.Drawing.Size(180, 22);
            this.itmQuit.Text = "Quit";
            this.itmQuit.Click += new System.EventHandler(this.ItmQuit_Click);
            // 
            // itmOperation
            // 
            this.itmOperation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmPlaySound,
            this.itmViewTexture});
            this.itmOperation.Name = "itmOperation";
            this.itmOperation.Size = new System.Drawing.Size(72, 20);
            this.itmOperation.Text = "Operation";
            // 
            // itmPlaySound
            // 
            this.itmPlaySound.Name = "itmPlaySound";
            this.itmPlaySound.Size = new System.Drawing.Size(140, 22);
            this.itmPlaySound.Text = "Play Sound";
            // 
            // itmViewTexture
            // 
            this.itmViewTexture.Name = "itmViewTexture";
            this.itmViewTexture.Size = new System.Drawing.Size(140, 22);
            this.itmViewTexture.Text = "View Texture";
            // 
            // ofdOpenPakFile
            // 
            this.ofdOpenPakFile.Filter = "Lego Pak Files|*.pak|All Files|*.*";
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
            this.lblStatus.Size = new System.Drawing.Size(683, 17);
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
            this.containerMain.Panel1.Controls.Add(this.containerOffset);
            this.containerMain.Panel1MinSize = 206;
            // 
            // containerMain.Panel2
            // 
            this.containerMain.Panel2.Controls.Add(this.lstMain);
            this.containerMain.Size = new System.Drawing.Size(814, 405);
            this.containerMain.SplitterDistance = 206;
            this.containerMain.TabIndex = 6;
            // 
            // PakExtractor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 451);
            this.Controls.Add(this.containerMain);
            this.Controls.Add(this.menuMain);
            this.Controls.Add(this.statusMain);
            this.MinimizeBox = false;
            this.Name = "PakExtractor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pak Extractor";
            this.Load += new System.EventHandler(this.PakExtractor_Load);
            this.cxtLstExtract.ResumeLayout(false);
            this.containerOffset.Panel1.ResumeLayout(false);
            this.containerOffset.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.containerOffset)).EndInit();
            this.containerOffset.ResumeLayout(false);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.statusMain.ResumeLayout(false);
            this.statusMain.PerformLayout();
            this.containerMain.Panel1.ResumeLayout(false);
            this.containerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.containerMain)).EndInit();
            this.containerMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblArchiveOffset;
        private System.Windows.Forms.Label lblFileInfo;
        private System.Windows.Forms.TextBox txtNameCrc;
        private System.Windows.Forms.TextBox txtFileInfo;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblNameInfo;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblNameCRC;
        private System.Windows.Forms.ImageList imgMain;
        private System.Windows.Forms.ListView lstMain;
        private System.Windows.Forms.ColumnHeader _columnHeader1;
        private System.Windows.Forms.ColumnHeader _columnHeader3;
        private System.Windows.Forms.ColumnHeader _columnHeader2;
        private System.Windows.Forms.ColumnHeader _columnHeader4;
        private System.Windows.Forms.ColumnHeader _columnHeader5;
        private System.Windows.Forms.ColumnHeader _columnHeader6;
        private System.Windows.Forms.ColumnHeader _columnHeader7;
        private System.Windows.Forms.ContextMenuStrip cxtLstExtract;
        private System.Windows.Forms.ToolStripMenuItem _extractToolStripMenuItem;
        private System.Windows.Forms.TextBox txtNameInfo;
        private System.Windows.Forms.SaveFileDialog sfdExtractFile;
        private System.Windows.Forms.SplitContainer containerOffset;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem itmFile;
        private System.Windows.Forms.ToolStripMenuItem itmExtractAll;
        private System.Windows.Forms.ToolStripSeparator _toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem itmQuit;
        private System.Windows.Forms.ToolStripMenuItem itmOperation;
        private System.Windows.Forms.ToolStripMenuItem itmPlaySound;
        private System.Windows.Forms.ToolStripMenuItem itmViewTexture;
        private System.Windows.Forms.OpenFileDialog ofdOpenPakFile;
        private System.Windows.Forms.StatusStrip statusMain;
        private System.Windows.Forms.ToolStripStatusLabel _toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripProgressBar pbMain;
        private System.Windows.Forms.FolderBrowserDialog fbdExtractFolder;
        private System.Windows.Forms.SplitContainer containerMain;
    }
}