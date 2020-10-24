using System;
using System.Windows.Forms;

// ReSharper disable LocalizableElement

namespace TT_Games_Explorer.Formats.GHG.Structure
{
    /// <summary>
    /// Represents the data structure of a GHG file and can load the full structure from a byte array
    /// </summary>
    public class GhgFile
    {
        /// <summary>
        /// Stores the raw byte data of all texture files contained inside a GHG
        /// </summary>
        public GhgDdsFile[] Textures { get; set; }

        /// <summary>
        /// Stores the raw byte data of all OBJ model files contained inside a GHG
        /// </summary>
        public GhgModelFile[] Models { get; set; }

        /// <summary>
        /// Stores necessary file metadata; please ensure this is always loaded correctly.
        /// </summary>
        public GhgMeta Metadata { get; set; } = new GhgMeta();

        /// <summary>
        /// Read a GHG/GSC file from its raw data
        /// </summary>
        /// <param name="ghgData"></param>
        /// <param name="fileName"></param>
        /// <param name="silent"></param>
        /// <returns></returns>
        public static GhgFile FromRawBytes(byte[] ghgData, string fileName, bool silent = false)
        {
            try
            {
                //validate data
                if (ghgData != null)
                    if (ghgData.Length > 0)
                    {
                    }
                    else
                    {
                        if (!silent)
                            MessageBox.Show("GHG File load error:\n\nData length of zero is invalid", @"", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                    }
                else
                    if (!silent)
                    MessageBox.Show("GHG File load error:\n\nNull bytes are invalid", @"", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                if (!silent)
                    MessageBox.Show($"GHG File load error:\n\n{ex}", @"", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }

            //default
            return null;
        }
    }
}