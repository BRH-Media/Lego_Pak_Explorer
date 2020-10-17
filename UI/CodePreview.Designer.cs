using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using Lego_Pak_Explorer.Properties;

namespace Lego_Pak_Explorer.UI
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
            components = new Container();
            resources = new ComponentResourceManager(typeof(CodePreview));

            _menuStrip1 = new MenuStrip();
            _fileToolStripMenuItem = new ToolStripMenuItem();
            _saveToolStripMenuItem = new ToolStripMenuItem();
            _saveAsToolStripMenuItem = new ToolStripMenuItem();
            _toolStripSeparator1 = new ToolStripSeparator();
            _quitToolStripMenuItem = new ToolStripMenuItem();
            _fastColoredTextBox1 = new FastColoredTextBox();
            _saveFileDialog1 = new SaveFileDialog();
            _menuStrip1.SuspendLayout();
            ((ISupportInitialize)_fastColoredTextBox1).BeginInit();
            SuspendLayout();
            _menuStrip1.Items.AddRange(new ToolStripItem[1]
            {
        _fileToolStripMenuItem
            });
            _menuStrip1.Location = new Point(0, 0);
            _menuStrip1.Name = @"_menuStrip1";
            _menuStrip1.Size = new Size(711, 24);
            _menuStrip1.TabIndex = 1;
            _menuStrip1.Text = @"menuStrip1";
            _fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[4]
            {
        _saveToolStripMenuItem,
        _saveAsToolStripMenuItem,
        _toolStripSeparator1,
        _quitToolStripMenuItem
            });
            _fileToolStripMenuItem.Image = Resources.brick_go;
            _fileToolStripMenuItem.Name = @"_fileToolStripMenuItem";
            _fileToolStripMenuItem.Size = new Size(53, 20);
            _fileToolStripMenuItem.Text = @"File";
            _saveToolStripMenuItem.Image = Resources.disk;
            _saveToolStripMenuItem.Name = @"_saveToolStripMenuItem";
            _saveToolStripMenuItem.ShortcutKeys = Keys.S | Keys.Control;
            _saveToolStripMenuItem.Size = new Size(152, 22);
            _saveToolStripMenuItem.Text = @"Save";
            _saveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
            _saveAsToolStripMenuItem.Image = Resources.disk;
            _saveAsToolStripMenuItem.Name = @"_saveAsToolStripMenuItem";
            _saveAsToolStripMenuItem.Size = new Size(152, 22);
            _saveAsToolStripMenuItem.Text = @"Save as...";
            _saveAsToolStripMenuItem.Click += SaveAsToolStripMenuItem_Click;
            _toolStripSeparator1.Name = @"_toolStripSeparator1";
            _toolStripSeparator1.Size = new Size(149, 6);
            _quitToolStripMenuItem.Image = (Image)resources.GetObject(@"_quitToolStripMenuItem.Image");
            _quitToolStripMenuItem.Name = @"_quitToolStripMenuItem";
            _quitToolStripMenuItem.ShortcutKeys = Keys.Q | Keys.Control;
            _quitToolStripMenuItem.Size = new Size(152, 22);
            _quitToolStripMenuItem.Text = @"Quit";
            _quitToolStripMenuItem.Click += QuitToolStripMenuItem_Click;
            _fastColoredTextBox1.AutoCompleteBracketsList = new char[10]
            {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '"',
        '"',
        '\'',
        '\''
            };
            _fastColoredTextBox1.AutoScrollMinSize = new Size(27, 14);
            _fastColoredTextBox1.BackBrush = null;
            _fastColoredTextBox1.CharHeight = 14;
            _fastColoredTextBox1.CharWidth = 8;
            _fastColoredTextBox1.Cursor = Cursors.IBeam;
            _fastColoredTextBox1.DisabledColor = Color.FromArgb(100, 180, 180, 180);
            _fastColoredTextBox1.Dock = DockStyle.Fill;
            _fastColoredTextBox1.IsReplaceMode = false;
            _fastColoredTextBox1.Location = new Point(0, 24);
            _fastColoredTextBox1.Name = @"_fastColoredTextBox1";
            _fastColoredTextBox1.Paddings = new Padding(0);
            _fastColoredTextBox1.SelectionColor = Color.FromArgb(60, 0, 0, byte.MaxValue);
            _fastColoredTextBox1.Size = new Size(711, 339);
            _fastColoredTextBox1.TabIndex = 2;
            _fastColoredTextBox1.Zoom = 100;
            _fastColoredTextBox1.TextChanged += FastColoredTextBox1_TextChanged;
            _saveFileDialog1.Filter = @"All Files|*.*";
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(711, 363);
            Controls.Add(_fastColoredTextBox1);
            Controls.Add(_menuStrip1);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            MainMenuStrip = _menuStrip1;
            Name = @"CodePreview";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = @"Text / Code Preview";
            _menuStrip1.ResumeLayout(false);
            _menuStrip1.PerformLayout();
            ((ISupportInitialize)_fastColoredTextBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private MenuStrip _menuStrip1;
        private ToolStripMenuItem _fileToolStripMenuItem;
        private ToolStripMenuItem _saveToolStripMenuItem;
        private ToolStripMenuItem _saveAsToolStripMenuItem;
        private ToolStripSeparator _toolStripSeparator1;
        private ToolStripMenuItem _quitToolStripMenuItem;
        private FastColoredTextBox _fastColoredTextBox1;
        private SaveFileDialog _saveFileDialog1;
    }
}
