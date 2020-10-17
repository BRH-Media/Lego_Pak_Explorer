using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Lego_Pak_Explorer.Properties;

namespace Lego_Pak_Explorer.UI
{
    public partial class DatExtractor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;
        private ComponentResourceManager resources = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                components?.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            resources = new ComponentResourceManager(typeof(DatExtractor));

            _menuStrip1 = new MenuStrip();
            _openFileDialog1 = new OpenFileDialog();
            _contextMenuStrip1 = new ContextMenuStrip(components);
            _statusStrip1 = new StatusStrip();
            _toolStripStatusLabel1 = new ToolStripStatusLabel();
            _toolStripStatusLabel4 = new ToolStripStatusLabel();
            _imageList1 = new ImageList(components);
            _folderBrowserDialog1 = new FolderBrowserDialog();
            _splitContainer1 = new SplitContainer();
            _splitContainer2 = new SplitContainer();
            _label5 = new Label();
            _treeView1 = new TreeView();
            _label1 = new Label();
            _textBox1 = new TextBox();
            _label2 = new Label();
            _listView1 = new ListView();
            _columnHeader1 = new ColumnHeader();
            _columnHeader3 = new ColumnHeader();
            _columnHeader2 = new ColumnHeader();
            _columnHeader4 = new ColumnHeader();
            _columnHeader5 = new ColumnHeader();
            _columnHeader6 = new ColumnHeader();
            _columnHeader7 = new ColumnHeader();
            _label3 = new Label();
            _textBox4 = new TextBox();
            _label4 = new Label();
            _textBox3 = new TextBox();
            _textBox2 = new TextBox();
            _toolStripProgressBar1 = new ToolStripProgressBar();
            _extractToolStripMenuItem = new ToolStripMenuItem();
            _fileToolStripMenuItem = new ToolStripMenuItem();
            _extractAllToolStripMenuItem = new ToolStripMenuItem();
            _toolStripSeparator1 = new ToolStripSeparator();
            _quitToolStripMenuItem = new ToolStripMenuItem();
            _saveFileDialog1 = new SaveFileDialog();
            _menuStrip1.SuspendLayout();
            _contextMenuStrip1.SuspendLayout();
            _statusStrip1.SuspendLayout();
            _splitContainer1.BeginInit();
            _splitContainer1.Panel1.SuspendLayout();
            _splitContainer1.Panel2.SuspendLayout();
            _splitContainer1.SuspendLayout();
            _splitContainer2.BeginInit();
            _splitContainer2.Panel1.SuspendLayout();
            _splitContainer2.Panel2.SuspendLayout();
            _splitContainer2.SuspendLayout();
            SuspendLayout();
            _menuStrip1.BackColor = SystemColors.Control;
            _menuStrip1.Items.AddRange(new ToolStripItem[]
            {
        _fileToolStripMenuItem
            });
            _menuStrip1.Location = new Point(0, 0);
            _menuStrip1.Name = "_menuStrip1";
            _menuStrip1.Size = new Size(812, 24);
            _menuStrip1.TabIndex = 0;
            _menuStrip1.Text = @"menuStrip1";
            _openFileDialog1.Filter = @"Lego Pak Files|*.dat|All Files|*.*";
            _contextMenuStrip1.Items.AddRange(new ToolStripItem[]
            {
        _extractToolStripMenuItem
            });
            _contextMenuStrip1.Name = "_contextMenuStrip1";
            _contextMenuStrip1.Size = new Size(111, 26);
            _contextMenuStrip1.Opening += ContextMenuStrip1_Opening;
            _statusStrip1.Items.AddRange(new ToolStripItem[]
            {
        _toolStripStatusLabel1,
        _toolStripStatusLabel4,
        _toolStripProgressBar1
            });
            _statusStrip1.Location = new Point(0, 428);
            _statusStrip1.Name = "_statusStrip1";
            _statusStrip1.Size = new Size(812, 22);
            _statusStrip1.TabIndex = 2;
            _statusStrip1.Text = @"statusStrip1";
            _toolStripStatusLabel1.Name = "_toolStripStatusLabel1";
            _toolStripStatusLabel1.Size = new Size(0, 17);
            _toolStripStatusLabel4.Name = "_toolStripStatusLabel4";
            _toolStripStatusLabel4.Size = new Size(695, 17);
            _toolStripStatusLabel4.Spring = true;
            _imageList1.ImageStream = (ImageListStreamer)resources.GetObject("_imageList1.ImageStream");
            _imageList1.TransparentColor = Color.Transparent;
            _imageList1.Images.SetKeyName(0, "folder.png");
            _folderBrowserDialog1.Description = @"Choose Extract Folder";
            _folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
            _splitContainer1.Dock = DockStyle.Fill;
            _splitContainer1.Location = new Point(0, 24);
            _splitContainer1.Name = "_splitContainer1";
            _splitContainer1.Panel1.Controls.Add(_splitContainer2);
            _splitContainer1.Panel1MinSize = 206;
            _splitContainer1.Panel2.Controls.Add(_listView1);
            _splitContainer1.Size = new Size(812, 404);
            _splitContainer1.SplitterDistance = 206;
            _splitContainer1.TabIndex = 3;
            _splitContainer2.Dock = DockStyle.Fill;
            _splitContainer2.FixedPanel = FixedPanel.Panel1;
            _splitContainer2.Location = new Point(0, 0);
            _splitContainer2.Name = "_splitContainer2";
            _splitContainer2.Orientation = Orientation.Horizontal;
            _splitContainer2.Panel1.Controls.Add(_label5);
            _splitContainer2.Panel1.Controls.Add(_textBox2);
            _splitContainer2.Panel1.Controls.Add(_label1);
            _splitContainer2.Panel1.Controls.Add(_textBox3);
            _splitContainer2.Panel1.Controls.Add(_textBox1);
            _splitContainer2.Panel1.Controls.Add(_label4);
            _splitContainer2.Panel1.Controls.Add(_label2);
            _splitContainer2.Panel1.Controls.Add(_textBox4);
            _splitContainer2.Panel1.Controls.Add(_label3);
            _splitContainer2.Panel1MinSize = 139;
            _splitContainer2.Panel2.Controls.Add(_treeView1);
            _splitContainer2.Size = new Size(206, 404);
            _splitContainer2.SplitterDistance = 139;
            _splitContainer2.TabIndex = 0;
            _label5.AutoSize = true;
            _label5.Font = new Font("Segoe UI", 8.25f, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            _label5.Location = new Point(12, 9);
            _label5.Name = "_label5";
            _label5.Size = new Size(83, 13);
            _label5.TabIndex = 23;
            _label5.Text = @"Archive Offset:";
            _treeView1.Dock = DockStyle.Fill;
            _treeView1.Enabled = false;
            _treeView1.ImageIndex = 0;
            _treeView1.ImageList = _imageList1;
            _treeView1.Location = new Point(0, 0);
            _treeView1.Name = "_treeView1";
            _treeView1.SelectedImageIndex = 0;
            _treeView1.Size = new Size(206, 261);
            _treeView1.TabIndex = 14;
            _treeView1.AfterSelect += TreeView1_AfterSelect;
            _label1.AutoSize = true;
            _label1.Location = new Point(12, 32);
            _label1.Name = "_label1";
            _label1.Size = new Size(52, 13);
            _label1.TabIndex = 15;
            _label1.Text = @"Files Info:";
            _textBox1.Location = new Point(81, 29);
            _textBox1.Name = "_textBox1";
            _textBox1.ReadOnly = true;
            _textBox1.Size = new Size(108, 20);
            _textBox1.TabIndex = 16;
            _label2.AutoSize = true;
            _label2.Location = new Point(12, 58);
            _label2.Name = "_label2";
            _label2.Size = new Size(64, 13);
            _label2.TabIndex = 17;
            _label2.Text = @"Names Info:";
            _listView1.Columns.AddRange(new ColumnHeader[]
            {
        _columnHeader1,
        _columnHeader3,
        _columnHeader2,
        _columnHeader4,
        _columnHeader5,
        _columnHeader6,
        _columnHeader7
            });
            _listView1.ContextMenuStrip = _contextMenuStrip1;
            _listView1.Dock = DockStyle.Fill;
            _listView1.Enabled = false;
            _listView1.FullRowSelect = true;
            _listView1.GridLines = true;
            _listView1.HideSelection = false;
            _listView1.Location = new Point(0, 0);
            _listView1.Name = "_listView1";
            _listView1.Size = new Size(602, 404);
            _listView1.TabIndex = 13;
            _listView1.UseCompatibleStateImageBehavior = false;
            _listView1.View = View.Details;
            _columnHeader1.Text = @"ID";
            _columnHeader1.Width = 31;
            _columnHeader3.Text = @"Name";
            _columnHeader3.Width = 190;
            _columnHeader2.Text = @"CRC Hash";
            _columnHeader4.Text = @"Offset";
            _columnHeader5.Text = @"SizeUnComp";
            _columnHeader6.Text = @"Size";
            _columnHeader7.Text = @"Pack";
            _label3.AutoSize = true;
            _label3.Location = new Point(12, 84);
            _label3.Name = "_label3";
            _label3.Size = new Size(68, 13);
            _label3.TabIndex = 18;
            _label3.Text = @"Names CRC:";
            _textBox4.Location = new Point(81, 107);
            _textBox4.Name = "_textBox4";
            _textBox4.ReadOnly = true;
            _textBox4.Size = new Size(108, 20);
            _textBox4.TabIndex = 22;
            _label4.AutoSize = true;
            _label4.Location = new Point(12, 110);
            _label4.Name = "_label4";
            _label4.Size = new Size(43, 13);
            _label4.TabIndex = 19;
            _label4.Text = @"Names:";
            _textBox3.Location = new Point(81, 81);
            _textBox3.Name = "_textBox3";
            _textBox3.ReadOnly = true;
            _textBox3.Size = new Size(108, 20);
            _textBox3.TabIndex = 21;
            _textBox2.Location = new Point(81, 55);
            _textBox2.Name = "_textBox2";
            _textBox2.ReadOnly = true;
            _textBox2.Size = new Size(108, 20);
            _textBox2.TabIndex = 20;
            _toolStripProgressBar1.Name = "_toolStripProgressBar1";
            _toolStripProgressBar1.Size = new Size(100, 16);
            _extractToolStripMenuItem.Image = Resources.disk;
            _extractToolStripMenuItem.Name = "_extractToolStripMenuItem";
            _extractToolStripMenuItem.Size = new Size(152, 22);
            _extractToolStripMenuItem.Text = @"Extract";
            _extractToolStripMenuItem.Click += ExtractToolStripMenuItem_Click;
            _fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
        _extractAllToolStripMenuItem,
        _toolStripSeparator1,
        _quitToolStripMenuItem
            });
            _fileToolStripMenuItem.Image = Resources.brick_go;
            _fileToolStripMenuItem.Name = "_fileToolStripMenuItem";
            _fileToolStripMenuItem.Size = new Size(53, 20);
            _fileToolStripMenuItem.Text = @"File";
            _extractAllToolStripMenuItem.Image = Resources.disk_multiple;
            _extractAllToolStripMenuItem.Name = "_extractAllToolStripMenuItem";
            _extractAllToolStripMenuItem.Size = new Size(sbyte.MaxValue, 22);
            _extractAllToolStripMenuItem.Text = @"Extract All";
            _extractAllToolStripMenuItem.Click += ToolStripMenuItem1_Click;
            _toolStripSeparator1.Name = "_toolStripSeparator1";
            _toolStripSeparator1.Size = new Size(124, 6);
            _quitToolStripMenuItem.Image = (Image)resources.GetObject("_quitToolStripMenuItem.Image");
            _quitToolStripMenuItem.Name = "_quitToolStripMenuItem";
            _quitToolStripMenuItem.RightToLeft = RightToLeft.No;
            _quitToolStripMenuItem.Size = new Size(152, 22);
            _quitToolStripMenuItem.Text = @"Quit";
            _quitToolStripMenuItem.Click += QuitToolStripMenuItem_Click;
            _saveFileDialog1.Filter = @"All Files|*.*";
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(812, 450);
            Controls.Add(_splitContainer1);
            Controls.Add(_statusStrip1);
            Controls.Add(_menuStrip1);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            MainMenuStrip = _menuStrip1;
            Name = @"DatExtractor";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = @"Dat Extractor";
            _menuStrip1.ResumeLayout(false);
            _menuStrip1.PerformLayout();
            _contextMenuStrip1.ResumeLayout(false);
            _statusStrip1.ResumeLayout(false);
            _statusStrip1.PerformLayout();
            _splitContainer1.Panel1.ResumeLayout(false);
            _splitContainer1.Panel2.ResumeLayout(false);
            _splitContainer1.EndInit();
            _splitContainer1.ResumeLayout(false);
            _splitContainer2.Panel1.ResumeLayout(false);
            _splitContainer2.Panel1.PerformLayout();
            _splitContainer2.Panel2.ResumeLayout(false);
            _splitContainer2.EndInit();
            _splitContainer2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private MenuStrip _menuStrip1;
        private OpenFileDialog _openFileDialog1;
        private StatusStrip _statusStrip1;
        private ToolStripStatusLabel _toolStripStatusLabel1;
        private ImageList _imageList1;
        private ContextMenuStrip _contextMenuStrip1;
        private ToolStripMenuItem _extractToolStripMenuItem;
        private ToolStripStatusLabel _toolStripStatusLabel4;
        private FolderBrowserDialog _folderBrowserDialog1;
        private ToolStripMenuItem _fileToolStripMenuItem;
        private ToolStripMenuItem _extractAllToolStripMenuItem;
        private ToolStripSeparator _toolStripSeparator1;
        private ToolStripMenuItem _quitToolStripMenuItem;
        private SplitContainer _splitContainer1;
        private SplitContainer _splitContainer2;
        private Label _label5;
        private TextBox _textBox2;
        private Label _label1;
        private TextBox _textBox3;
        private TextBox _textBox1;
        private Label _label4;
        private Label _label2;
        private TextBox _textBox4;
        private Label _label3;
        private TreeView _treeView1;
        private ListView _listView1;
        private ColumnHeader _columnHeader1;
        private ColumnHeader _columnHeader3;
        private ColumnHeader _columnHeader2;
        private ColumnHeader _columnHeader4;
        private ColumnHeader _columnHeader5;
        private ColumnHeader _columnHeader6;
        private ColumnHeader _columnHeader7;
        private ToolStripProgressBar _toolStripProgressBar1;
        private SaveFileDialog _saveFileDialog1;
    }
}
