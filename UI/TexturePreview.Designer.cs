using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Lego_Pak_Explorer.Components;
using Lego_Pak_Explorer.Properties;

namespace Lego_Pak_Explorer.UI
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
            this.picMain = new System.Windows.Forms.PictureBox();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.itmExport = new System.Windows.Forms.ToolStripMenuItem();
            this.itmMipmap = new System.Windows.Forms.ToolStripMenuItem();
            this.itmModify = new System.Windows.Forms.ToolStripMenuItem();
            this.sfdExport = new System.Windows.Forms.SaveFileDialog();
            this.statusMain = new System.Windows.Forms.StatusStrip();
            this._toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this._toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlMain = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).BeginInit();
            this.menuMain.SuspendLayout();
            this.statusMain.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // picMain
            // 
            this.picMain.BackColor = System.Drawing.Color.Transparent;
            this.picMain.Location = new System.Drawing.Point(0, 0);
            this.picMain.Name = "picMain";
            this.picMain.Size = new System.Drawing.Size(116, 116);
            this.picMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picMain.TabIndex = 0;
            this.picMain.TabStop = false;
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmExport,
            this.itmMipmap,
            this.itmModify});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(537, 24);
            this.menuMain.TabIndex = 1;
            this.menuMain.Text = "menuStrip1";
            // 
            // itmExport
            // 
            this.itmExport.Image = global::Lego_Pak_Explorer.Properties.Resources.disk;
            this.itmExport.Name = "itmExport";
            this.itmExport.Size = new System.Drawing.Size(69, 20);
            this.itmExport.Text = "Export";
            this.itmExport.Click += new System.EventHandler(this.ItmExport_Click);
            // 
            // itmMipmap
            // 
            this.itmMipmap.Image = global::Lego_Pak_Explorer.Properties.Resources.cog;
            this.itmMipmap.Name = "itmMipmap";
            this.itmMipmap.Size = new System.Drawing.Size(80, 20);
            this.itmMipmap.Text = "Mipmap";
            this.itmMipmap.Click += new System.EventHandler(this.ItmMipmap_Click);
            // 
            // itmModify
            // 
            this.itmModify.Image = global::Lego_Pak_Explorer.Properties.Resources.help;
            this.itmModify.Name = "itmModify";
            this.itmModify.Size = new System.Drawing.Size(151, 20);
            this.itmModify.Text = "How do I modify this?";
            this.itmModify.Click += new System.EventHandler(this.ItmModify_Click);
            // 
            // sfdExport
            // 
            this.sfdExport.Filter = "PNG File|*.png";
            // 
            // statusMain
            // 
            this.statusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolStripStatusLabel1,
            this.lblStatus,
            this._toolStripStatusLabel3});
            this.statusMain.Location = new System.Drawing.Point(0, 344);
            this.statusMain.Name = "statusMain";
            this.statusMain.Size = new System.Drawing.Size(537, 22);
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
            this.lblStatus.Size = new System.Drawing.Size(522, 17);
            this.lblStatus.Spring = true;
            // 
            // _toolStripStatusLabel3
            // 
            this._toolStripStatusLabel3.Name = "_toolStripStatusLabel3";
            this._toolStripStatusLabel3.Size = new System.Drawing.Size(0, 17);
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.Controls.Add(this.picMain);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(537, 320);
            this.pnlMain.TabIndex = 3;
            // 
            // TexturePreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackgroundImage = global::Lego_Pak_Explorer.Properties.Resources.transpback1;
            this.ClientSize = new System.Drawing.Size(537, 366);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.menuMain);
            this.Controls.Add(this.statusMain);
            this.MainMenuStrip = this.menuMain;
            this.Name = "TexturePreview";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Texture / Image Preview";
            this.Load += new System.EventHandler(this.TexturePreview_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).EndInit();
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
        private ToolStripStatusLabel _toolStripStatusLabel1;
        private Panel pnlMain;
        private ToolStripStatusLabel lblStatus;
        private ToolStripStatusLabel _toolStripStatusLabel3;
        private ToolStripMenuItem itmModify;
        private readonly NoFocusTrackBar _trackBar1 = new NoFocusTrackBar();
        private ToolStripMenuItem itmMipmap;
    }
}
