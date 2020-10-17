using Lego_Pak_Explorer.Renderer;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Lego_Pak_Explorer.UI
{
    public partial class TexturePreview : Form
    {
        private readonly string _filepath;
        private readonly Image _previewImage;
        private readonly int _previewWidth;
        private readonly int _previewHeight;
        private int _zoomVal = 100;

        public TexturePreview(string fullPath, string format)
        {
            InitializeComponent();
            _filepath = fullPath;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
            switch (format)
            {
                case "dds":
                    {
                        var fileStream = File.OpenRead(fullPath);
                        var numArray = new byte[fileStream.Length];
                        fileStream.Read(numArray, 0, Convert.ToInt32(fileStream.Length));
                        var ddsImage = new DDSImage(numArray);
                        _toolStripStatusLabel1.Text =
                            $@"{Path.GetFileName(fullPath)} | H: {ddsImage.images[0].Height}px | W: {ddsImage.images[0].Width}px";
                        _previewImage = ddsImage.images[0];
                        _previewWidth = ddsImage.images[0].Width;
                        _previewHeight = ddsImage.images[0].Height;
                        _pictureBox1.Image = ddsImage.images[0];
                        break;
                    }
                case "png":
                    _previewImage = Image.FromFile(fullPath);
                    _previewWidth = _previewImage.Width;
                    _previewHeight = _previewImage.Height;
                    _pictureBox1.Image = _previewImage;
                    break;
            }
            _toolStripStatusLabel3.Text = $@"{_zoomVal}%";
            _trackBar1.TickFrequency = 10;
            _trackBar1.Maximum = 200;
            _trackBar1.Size = new Size(137, 40);
            _trackBar1.Value = 100;
            _trackBar1.TickStyle = TickStyle.Both;
            _trackBar1.Scroll += trackBar1_Scroll;
            _statusStrip1.Items.Add(new ToolStripControlHost(_trackBar1));
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (_trackBar1.Value <= 0)
                return;
            _zoomVal = _trackBar1.Value;
            _toolStripStatusLabel3.Text = $@"{_zoomVal}%";
            _pictureBox1.Image = PictureBoxZoom(_previewImage, new Size(_previewHeight * _zoomVal / 100, _previewWidth * _zoomVal / 100));
        }

        public Image PictureBoxZoom(Image img, Size size)
        {
            var bitmap = new Bitmap(img, size.Width, size.Height);
            Graphics.FromImage(bitmap).InterpolationMode = InterpolationMode.HighQualityBicubic;
            return bitmap;
        }

        private void ExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _saveFileDialog1.FileName = $"{Path.GetFileNameWithoutExtension(_filepath)}.png";
            if (_saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            _pictureBox1.Image.Save(_saveFileDialog1.FileName, ImageFormat.Png);
        }

        private static void HowModifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                  @"For now, I'm not able to convert image files in texture files, if you want modify them, rename the *.TEX file to *.DDS and open them through the NVIDIA DDS Plugin and Photoshop!");
        }
    }
}