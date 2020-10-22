using System.ComponentModel;
using System.Windows.Forms;

namespace TT_Games_Explorer.UI
{
    partial class GhgManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GhgManager));
            this.containerMain = new System.Windows.Forms.SplitContainer();
            this.lstMain = new System.Windows.Forms.ListView();
            this.colId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imgMain = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.containerMain)).BeginInit();
            this.containerMain.Panel1.SuspendLayout();
            this.containerMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // containerMain
            // 
            this.containerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerMain.Location = new System.Drawing.Point(0, 0);
            this.containerMain.Name = "containerMain";
            // 
            // containerMain.Panel1
            // 
            this.containerMain.Panel1.Controls.Add(this.lstMain);
            this.containerMain.Size = new System.Drawing.Size(800, 450);
            this.containerMain.SplitterDistance = 266;
            this.containerMain.TabIndex = 0;
            // 
            // lstMain
            // 
            this.lstMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colId,
            this.colFileName,
            this.colType,
            this.colSize});
            this.lstMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstMain.Enabled = false;
            this.lstMain.FullRowSelect = true;
            this.lstMain.GridLines = true;
            this.lstMain.HideSelection = false;
            this.lstMain.Location = new System.Drawing.Point(0, 0);
            this.lstMain.Name = "lstMain";
            this.lstMain.Size = new System.Drawing.Size(266, 450);
            this.lstMain.TabIndex = 14;
            this.lstMain.UseCompatibleStateImageBehavior = false;
            this.lstMain.View = System.Windows.Forms.View.Details;
            // 
            // colId
            // 
            this.colId.Text = "ID";
            // 
            // colFileName
            // 
            this.colFileName.Text = "Name";
            // 
            // colType
            // 
            this.colType.Text = "Type";
            // 
            // colSize
            // 
            this.colSize.Text = "Size";
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
            // GhgManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.containerMain);
            this.Name = "GhgManager";
            this.Text = "Complex Model Manager";
            this.Load += new System.EventHandler(this.GhgManager_Load);
            this.containerMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.containerMain)).EndInit();
            this.containerMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SplitContainer containerMain;
        private ListView lstMain;
        private ColumnHeader colFileName;
        private ColumnHeader colType;
        private ColumnHeader colSize;
        private ColumnHeader colId;
        private ImageList imgMain;
    }
}