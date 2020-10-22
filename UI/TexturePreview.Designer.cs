using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TT_Games_Explorer.Components;
using TT_Games_Explorer.Properties;

namespace TT_Games_Explorer.UI
{
    public partial class TexturePreview
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
            this.picMain = new PictureBox();
            this.menuMain = new MenuStrip();
            this.itmExport = new ToolStripMenuItem();
            this.itmMipmap = new ToolStripMenuItem();
            this.itmModify = new ToolStripMenuItem();
            this.sfdExport = new SaveFileDialog();
            this.statusMain = new StatusStrip();
            this.lblFileDimensions = new ToolStripStatusLabel();
            this.lblStatus = new ToolStripStatusLabel();
            this.lblZoomPercentage = new ToolStripStatusLabel();
            this.pnlMain = new Panel();
            ((ISupportInitialize)(this.picMain)).BeginInit();
            this.menuMain.SuspendLayout();
            this.statusMain.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // picMain
            // 
            this.picMain.BackColor = Color.Transparent;
            this.picMain.Location = new Point(0, 0);
            this.picMain.Name = "picMain";
            this.picMain.Size = new Size(116, 116);
            this.picMain.SizeMode = PictureBoxSizeMode.AutoSize;
            this.picMain.TabIndex = 0;
            this.picMain.TabStop = false;
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new ToolStripItem[] {
            this.itmExport,
            this.itmMipmap,
            this.itmModify});
            this.menuMain.Location = new Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new Size(537, 24);
            this.menuMain.TabIndex = 1;
            this.menuMain.Text = "menuStrip1";
            // 
            // itmExport
            // 
            this.itmExport.Image = Resources.disk;
            this.itmExport.Name = "itmExport";
            this.itmExport.Size = new Size(69, 20);
            this.itmExport.Text = "Export";
            this.itmExport.Click += new EventHandler(this.ItmExport_Click);
            // 
            // itmMipmap
            // 
            this.itmMipmap.Image = Resources.cog;
            this.itmMipmap.Name = "itmMipmap";
            this.itmMipmap.Size = new Size(80, 20);
            this.itmMipmap.Text = "Mipmap";
            this.itmMipmap.Click += new EventHandler(this.ItmMipmap_Click);
            // 
            // itmModify
            // 
            this.itmModify.Image = Resources.help;
            this.itmModify.Name = "itmModify";
            this.itmModify.Size = new Size(151, 20);
            this.itmModify.Text = "How do I modify this?";
            this.itmModify.Click += new EventHandler(this.ItmModify_Click);
            // 
            // sfdExport
            // 
            this.sfdExport.Filter = "PNG File|*.png";
            // 
            // statusMain
            // 
            this.statusMain.Items.AddRange(new ToolStripItem[] {
            this.lblFileDimensions,
            this.lblStatus,
            this.lblZoomPercentage});
            this.statusMain.Location = new Point(0, 344);
            this.statusMain.Name = "statusMain";
            this.statusMain.Size = new Size(537, 22);
            this.statusMain.TabIndex = 2;
            this.statusMain.Text = "statusMain";
            // 
            // lblFileDimensions
            // 
            this.lblFileDimensions.Name = "lblFileDimensions";
            this.lblFileDimensions.Size = new Size(0, 17);
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(522, 17);
            this.lblStatus.Spring = true;
            // 
            // lblZoomPercentage
            // 
            this.lblZoomPercentage.Name = "lblZoomPercentage";
            this.lblZoomPercentage.Size = new Size(0, 17);
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.BackColor = Color.Transparent;
            this.pnlMain.Controls.Add(this.picMain);
            this.pnlMain.Dock = DockStyle.Fill;
            this.pnlMain.Location = new Point(0, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new Size(537, 320);
            this.pnlMain.TabIndex = 3;
            // 
            // TexturePreview
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackgroundImage = Resources.transpback1;
            this.ClientSize = new Size(537, 366);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.menuMain);
            this.Controls.Add(this.statusMain);
            this.MainMenuStrip = this.menuMain;
            this.Name = "TexturePreview";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Texture / Image Preview";
            this.Load += new EventHandler(this.TexturePreview_Load);
            ((ISupportInitialize)(this.picMain)).EndInit();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.statusMain.ResumeLayout(false);
            this.statusMain.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private PictureBox picMain;
        private MenuStrip menuMain;
        private ToolStripMenuItem itmExport;
        private SaveFileDialog sfdExport;
        private StatusStrip statusMain;
        private ToolStripStatusLabel lblFileDimensions;
        private Panel pnlMain;
        private ToolStripStatusLabel lblStatus;
        private ToolStripStatusLabel lblZoomPercentage;
        private ToolStripMenuItem itmModify;
        private readonly NoFocusTrackBar trkZoomTexture = new NoFocusTrackBar();
        private ToolStripMenuItem itmMipmap;
    }
}
