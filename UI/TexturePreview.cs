using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using TT_Games_Explorer.Renderer;

// ReSharper disable LocalizableElement

namespace TT_Games_Explorer.UI
{
    public partial class TexturePreview : Form
    {
        private readonly string _filepath;
        private Image _previewImage;
        private int _previewWidth;
        private int _previewHeight;
        private readonly Bitmap[] _mipmaps;
        private int _zoomVal = 100;

        public TexturePreview(string fullPath, string format)
        {
            //designer stuff
            InitializeComponent();

            //set global
            _filepath = fullPath;

            //double-buffering disabled
            SetStyle(ControlStyles.OptimizedDoubleBuffer, false);

            //find format
            switch (format)
            {
                //directX texturing format
                case "dds":
                    {
                        //read all bytes to a stream
                        var fileStream = File.OpenRead(fullPath);
                        var numArray = new byte[fileStream.Length];

                        //read the entire image byte array into memory
                        fileStream.Read(numArray, 0, Convert.ToInt32(fileStream.Length));

                        //construct texture
                        var ddsImage = new DDSImage(numArray);

                        //apply global mipmaps
                        _mipmaps = ddsImage.images;

                        //setup the interface with the first mipmap as the default
                        SetupUi(_mipmaps[0], fullPath);
                        break;
                    }
                //normal image
                case "png":
                    SetupUi(Image.FromFile(fullPath), fullPath);
                    break;
            }

            statusMain.Items.Add(new ToolStripControlHost(_trackBar1));
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            if (_trackBar1.Value <= 0)
                return;
            _zoomVal = _trackBar1.Value;
            _toolStripStatusLabel3.Text = $@"{_zoomVal}%";
            picMain.Image = PictureBoxZoom(_previewImage, new Size(_previewHeight * _zoomVal / 100, _previewWidth * _zoomVal / 100));
        }

        private void SetupUi(Image image, string fullPath)
        {
            //apply globals
            _zoomVal = 100;

            //apply UI
            _toolStripStatusLabel1.Text =
                $@"{Path.GetFileName(fullPath)} | H: {image.Height}px | W: {image.Width}px";
            _previewImage = image;
            _previewWidth = image.Width;
            _previewHeight = image.Height;

            //display image
            picMain.Image = image;

            //setup trackbar for zoom
            _toolStripStatusLabel3.Text = $@"{_zoomVal}%";
            _trackBar1.TickFrequency = 10;
            _trackBar1.Maximum = 200;
            _trackBar1.Size = new Size(137, 40);
            _trackBar1.Value = 100;
            _trackBar1.TickStyle = TickStyle.Both;
            _trackBar1.Scroll += TrackBar1_Scroll;

            //setup mipmap menu
            MipmapMenuFill();
        }

        private void MipmapMenuFill()
        {
            try
            {
                //get rid of existing mipmaps
                itmMipmap.DropDownItems.Clear();

                //load all from mipmap array
                if (_mipmaps != null)
                    if (_mipmaps.Length > 0)
                        for (var index = 0; index < _mipmaps.Length; index++)
                        {
                            //construct new menu item
                            var newItem = new ToolStripMenuItem()
                            {
                                Enabled = true,
                                Text = $"Image {index + 1}",
                                Name = $"Mipmap{index + 1}"
                            };

                            //click event for loading the mipmap
                            newItem.Click += MipmapHandler;

                            //add new menu item
                            itmMipmap.DropDownItems.Add(newItem);
                        }
                    else
                        itmMipmap.DropDownItems.Add(BlankMipmapMenuItem());
                else
                    itmMipmap.DropDownItems.Add(BlankMipmapMenuItem());

                //force repaint
                itmMipmap.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Mipmap menu fill error:\n\n{ex}");
            }
        }

        private void MipmapHandler(object sender, EventArgs e)
        {
            //validate sender
            if (sender.GetType() == typeof(ToolStripMenuItem))
            {
                //recast the sender
                var obj = (ToolStripMenuItem)sender;

                //menu items correlate with the mipmap array
                var index = itmMipmap.DropDownItems.IndexOf(obj);

                //validate index
                if (index > -1)
                {
                    //menu item indexes correlate with the mipmap indexes
                    var selectedMipmap = _mipmaps[index];

                    //reset UI
                    SetupUi(selectedMipmap, _filepath);
                }
            }
        }

        private static ToolStripItem BlankMipmapMenuItem()
        {
            return new ToolStripMenuItem()
            {
                Enabled = false,
                Text = @"None",
                Name = @"itmNoMipmaps"
            };
        }

        public Image PictureBoxZoom(Image img, Size size)
        {
            var bitmap = new Bitmap(img, size.Width, size.Height);
            Graphics.FromImage(bitmap).InterpolationMode = InterpolationMode.HighQualityBicubic;
            return bitmap;
        }

        private void ItmExport_Click(object sender, EventArgs e)
        {
            sfdExport.FileName = $"{Path.GetFileNameWithoutExtension(_filepath)}.png";
            if (sfdExport.ShowDialog() != DialogResult.OK)
                return;
            picMain.Image.Save(sfdExport.FileName, ImageFormat.Png);
        }

        private void ItmModify_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                @"For now, I'm not able to convert image files in texture files, if you want modify them, rename the *.TEX file to *.DDS and open them through the NVIDIA DDS Plugin and Photoshop!");
        }

        private void TexturePreview_Load(object sender, EventArgs e)
        {
        }

        private void ItmMipmap_Click(object sender, EventArgs e)
        {
        }
    }
}