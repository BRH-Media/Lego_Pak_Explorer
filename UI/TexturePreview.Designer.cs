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
            _pictureBox1 = new PictureBox();
            _menuStrip1 = new MenuStrip();
            _exportToolStripMenuItem = new ToolStripMenuItem();
            _howModifyToolStripMenuItem = new ToolStripMenuItem();
            _saveFileDialog1 = new SaveFileDialog();
            _statusStrip1 = new StatusStrip();
            _toolStripStatusLabel1 = new ToolStripStatusLabel();
            _toolStripStatusLabel2 = new ToolStripStatusLabel();
            _toolStripStatusLabel3 = new ToolStripStatusLabel();
            _panel1 = new Panel();
            ((ISupportInitialize)_pictureBox1).BeginInit();
            _menuStrip1.SuspendLayout();
            _statusStrip1.SuspendLayout();
            _panel1.SuspendLayout();
            SuspendLayout();
            _pictureBox1.BackColor = Color.Transparent;
            _pictureBox1.Location = new Point(0, 0);
            _pictureBox1.Name = "_pictureBox1";
            _pictureBox1.Size = new Size(116, 116);
            _pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            _pictureBox1.TabIndex = 0;
            _pictureBox1.TabStop = false;
            _menuStrip1.Items.AddRange(new ToolStripItem[]
            {
        _exportToolStripMenuItem,
        _howModifyToolStripMenuItem
            });
            _menuStrip1.Location = new Point(0, 0);
            _menuStrip1.Name = "_menuStrip1";
            _menuStrip1.Size = new Size(537, 24);
            _menuStrip1.TabIndex = 1;
            _menuStrip1.Text = @"menuStrip1";
            _exportToolStripMenuItem.Image = Resources.disk;
            _exportToolStripMenuItem.Name = "_exportToolStripMenuItem";
            _exportToolStripMenuItem.Size = new Size(69, 20);
            _exportToolStripMenuItem.Text = @"Export";
            _exportToolStripMenuItem.Click += ExportToolStripMenuItem_Click;
            _howModifyToolStripMenuItem.Image = Resources.help;
            _howModifyToolStripMenuItem.Name = "_howModifyToolStripMenuItem";
            _howModifyToolStripMenuItem.Size = new Size(106, 20);
            _howModifyToolStripMenuItem.Text = @"How modify?";
            _howModifyToolStripMenuItem.Click += HowModifyToolStripMenuItem_Click;
            _saveFileDialog1.Filter = @"PNG Files|*.png";
            _statusStrip1.Items.AddRange(new ToolStripItem[]
            {
        _toolStripStatusLabel1,
        _toolStripStatusLabel2,
        _toolStripStatusLabel3
            });
            _statusStrip1.Location = new Point(0, 344);
            _statusStrip1.Name = @"_statusStrip1";
            _statusStrip1.Size = new Size(537, 22);
            _statusStrip1.TabIndex = 2;
            _statusStrip1.Text = @"statusStrip1";
            _toolStripStatusLabel1.Name = @"_toolStripStatusLabel1";
            _toolStripStatusLabel1.Size = new Size(0, 17);
            _toolStripStatusLabel2.Name = @"_toolStripStatusLabel2";
            _toolStripStatusLabel2.Size = new Size(522, 17);
            _toolStripStatusLabel2.Spring = true;
            _toolStripStatusLabel3.Name = @"_toolStripStatusLabel3";
            _toolStripStatusLabel3.Size = new Size(0, 17);
            _panel1.AutoScroll = true;
            _panel1.BackColor = Color.Transparent;
            _panel1.Controls.Add(_pictureBox1);
            _panel1.Dock = DockStyle.Fill;
            _panel1.Location = new Point(0, 24);
            _panel1.Name = @"_panel1";
            _panel1.Size = new Size(537, 320);
            _panel1.TabIndex = 3;
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackgroundImage = Resources.transpback1;
            ClientSize = new Size(537, 366);
            Controls.Add(_panel1);
            Controls.Add(_menuStrip1);
            Controls.Add(_statusStrip1);
            MainMenuStrip = _menuStrip1;
            Name = @"Form4";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = @"Texture / Image Preview";
            ((ISupportInitialize)_pictureBox1).EndInit();
            _menuStrip1.ResumeLayout(false);
            _menuStrip1.PerformLayout();
            _statusStrip1.ResumeLayout(false);
            _statusStrip1.PerformLayout();
            _panel1.ResumeLayout(false);
            _panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private PictureBox _pictureBox1;
        private MenuStrip _menuStrip1;
        private ToolStripMenuItem _exportToolStripMenuItem;
        private SaveFileDialog _saveFileDialog1;
        private StatusStrip _statusStrip1;
        private ToolStripStatusLabel _toolStripStatusLabel1;
        private Panel _panel1;
        private ToolStripStatusLabel _toolStripStatusLabel2;
        private ToolStripStatusLabel _toolStripStatusLabel3;
        private ToolStripMenuItem _howModifyToolStripMenuItem;
        private readonly NoFocusTrackBar _trackBar1 = new NoFocusTrackBar();
    }
}
