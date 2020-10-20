using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TT_Games_Explorer.Common;
using TT_Games_Explorer.Renderer.Enums;

namespace TT_Games_Explorer.Renderer.Textures
{
    /// <summary>
    /// Simplifies DDSImage operation and manipulation
    /// </summary>
    public class TexTrend
    {
        /// <summary>
        /// All texture mipmaps/subimages are stored here, with index 0 being the default image.
        /// </summary>
        public Image[] Images { get; }

        /// <summary>
        /// If you're loading directly from a byte array,
        /// this should be the file name of the texture file and not a path.
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// Takes the file name component out of 'FilePath'
        /// </summary>
        public string FileName => string.IsNullOrEmpty(FilePath)
            ? @""
            : Path.GetFileName(FilePath);

        /// <summary>
        /// Takes the file name component out of 'FilePath' and strips the extension
        /// </summary>
        public string BareFileName => string.IsNullOrEmpty(FilePath)
            ? @""
            : Path.GetFileNameWithoutExtension(FilePath);

        /// <summary>
        /// Stores static information regarding the format, thereby avoiding another switch/case for the file extension<br />
        /// NOTE: Default is TextureFormat.Unknown
        /// </summary>
        public TextureFormat ImageFormat { get; } = TextureFormat.Unknown;

        public TexTrend(byte[] textureData, string textureFilePath)
        {
            //A QUICK NOTE:
            //textureFilePath doesn't have to be a valid path in this routine; it's kept for record only.
            //This is useful if you need to store the file's name (e.g. for labeled viewing later on)

            try
            {
                //validations; this guys have to be valid for this to work
                if (textureData == null || string.IsNullOrEmpty(textureFilePath)) return;

                //final global image array
                var imageArray = new List<Image>();

                //make sure there's data to work on!
                if (textureData.Length > 0)
                {
                    //extension is used to check the format of the image
                    var ext = Path.GetExtension(textureFilePath).ToLower();

                    //try signature verification first; foolproof checks
                    const uint dds = 1145328416;
                    const uint png = 2303741511;

                    //read signature of the bytes
                    var readCc = Methods.ReadFourCc(textureData);

                    //verify
                    switch (readCc)
                    {
                        //change extension variable to .dds to trip the checks below
                        case dds:
                            ext = @".dds";
                            break;

                        //change extension variable to .png to trip the checks below
                        case png:
                            ext = @".png";
                            break;
                    }

                    //validate the extension so we know how to use the data
                    switch (ext)
                    {
                        //DirectDraw textures are handled via managed DirectX 9
                        //NOTE: TT Games just renames *.dds files to *.tex
                        case @".tex":
                        case @".dds":

                            //load the raw bytes directly into the DDS handler
                            var newImages = new DDS_IMAGE(textureData);

                            //append the new images to the image collection (if valid)
                            if (newImages.images.Length > 0)
                                imageArray.AddRange(newImages.images);
                            break;

                        //'Normal' images are handled natively by .NET
                        case @".jpeg":
                        case @".jpg":
                        case @".raw":
                        case @".bmp":
                        case @".png":
                        case @".gif":
                        case @".giff":
                        case @".tif":
                        case @".tiff":
                            //load image directly from a memory stream (don't load the bytes from the file again!)
                            var newImage = Image.FromStream(new MemoryStream(textureData));

                            //append the new image to the image collection (if valid)
                            if (newImage.Height > 0 && newImage.Width > 0)
                                imageArray.Add(newImage);
                            break;

                        //Targa (TruVision) images get handled by a library
                        case @".tga":
                            break;

                        //Anything else isn't supported and can't be processed
                        default:
                            MessageBox.Show(@"Texture format was invalid; unrecognised file extension. Please consult the supported file-types list.");
                            return;
                    }
                }

                //if any images ended up getting processed, apply all of them to the global array
                if (imageArray.Count > 0)
                    Images = imageArray.ToArray();

                //apply the other globals needed
                FilePath = textureFilePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Texture byte parse error:\n\n{ex}");
                //ignore all errors
            }
        }

        public TexTrend(string textureFilePath) : this(LoadInBytes(textureFilePath), textureFilePath)
        {
            //proxied routine; review above
        }

        private static byte[] LoadInBytes(string textureFilePath)
        {
            try
            {
                //if the file doesn't exist, we can't operate on it
                if (File.Exists(textureFilePath))
                {
                    //read it all in using a byte reader; no streams necessary yet
                    var fileBytes = File.ReadAllBytes(textureFilePath);

                    //return the raw file bytes
                    return fileBytes;
                }
            }
            catch (Exception)
            {
                //ignore all errors
            }

            //default
            return null;
        }
    }
}