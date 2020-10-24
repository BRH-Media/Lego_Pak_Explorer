using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using TT_Games_Explorer.Properties;

namespace TT_Games_Explorer.UI
{
    public partial class CodePreview
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;
        private ComponentResourceManager resources = new ComponentResourceManager(typeof(CodePreview));

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                components?.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodePreview));
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.itmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.itmSave = new System.Windows.Forms.ToolStripMenuItem();
            this.itmSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this._toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.itmQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.txtCodePreview = new FastColoredTextBoxNS.FastColoredTextBox();
            this.sfdExport = new System.Windows.Forms.SaveFileDialog();
            this.menuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodePreview)).BeginInit();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmFile});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(711, 24);
            this.menuMain.TabIndex = 1;
            this.menuMain.Text = "menuStrip1";
            // 
            // itmFile
            // 
            this.itmFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmSave,
            this.itmSaveAs,
            this._toolStripSeparator1,
            this.itmQuit});
            this.itmFile.Image = global::TT_Games_Explorer.Properties.Resources.brick_go;
            this.itmFile.Name = "itmFile";
            this.itmFile.Size = new System.Drawing.Size(53, 20);
            this.itmFile.Text = "File";
            // 
            // itmSave
            // 
            this.itmSave.Enabled = false;
            this.itmSave.Image = global::TT_Games_Explorer.Properties.Resources.disk;
            this.itmSave.Name = "itmSave";
            this.itmSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.itmSave.Size = new System.Drawing.Size(180, 22);
            this.itmSave.Text = "Save";
            this.itmSave.Click += new System.EventHandler(this.ItmSave_Click);
            // 
            // itmSaveAs
            // 
            this.itmSaveAs.Image = global::TT_Games_Explorer.Properties.Resources.disk;
            this.itmSaveAs.Name = "itmSaveAs";
            this.itmSaveAs.Size = new System.Drawing.Size(180, 22);
            this.itmSaveAs.Text = "Save as...";
            this.itmSaveAs.Click += new System.EventHandler(this.ItmSaveAs_Click);
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
            this.itmQuit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.itmQuit.Size = new System.Drawing.Size(180, 22);
            this.itmQuit.Text = "Quit";
            this.itmQuit.Click += new System.EventHandler(this.ItmQuit_Click);
            // 
            // txtCodePreview
            // 
            this.txtCodePreview.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.txtCodePreview.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.txtCodePreview.BackBrush = null;
            this.txtCodePreview.CharHeight = 14;
            this.txtCodePreview.CharWidth = 8;
            this.txtCodePreview.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCodePreview.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtCodePreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCodePreview.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.txtCodePreview.IsReplaceMode = false;
            this.txtCodePreview.Location = new System.Drawing.Point(0, 24);
            this.txtCodePreview.Name = "txtCodePreview";
            this.txtCodePreview.Paddings = new System.Windows.Forms.Padding(0);
            this.txtCodePreview.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtCodePreview.Size = new System.Drawing.Size(711, 339);
            this.txtCodePreview.TabIndex = 2;
            this.txtCodePreview.Zoom = 100;
            this.txtCodePreview.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.TxtCodePreview_TextChanged);
            // 
            // sfdExport
            // 
            this.sfdExport.Filter = "All Files|*.*";
            // 
            // CodePreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 363);
            this.Controls.Add(this.txtCodePreview);
            this.Controls.Add(this.menuMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuMain;
            this.MinimizeBox = false;
            this.Name = "CodePreview";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Text / Code Preview";
            this.Load += new System.EventHandler(this.CodePreview_Load);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodePreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private MenuStrip menuMain;
        private ToolStripMenuItem itmFile;
        private ToolStripMenuItem itmSave;
        private ToolStripMenuItem itmSaveAs;
        private ToolStripSeparator _toolStripSeparator1;
        private ToolStripMenuItem itmQuit;
        private FastColoredTextBox txtCodePreview;
        private SaveFileDialog sfdExport;
    }
}
