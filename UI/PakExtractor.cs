using System;
using System.IO;
using System.Windows.Forms;
using TT_Games_Explorer.Structs.PakFormat;

namespace TT_Games_Explorer.UI
{
    public partial class PakExtractor : Form
    {
        public string PakFilePath { get; }

        public PakExtractor(string pakFile)
        {
            //apply global
            PakFilePath = pakFile;

            InitializeComponent();
        }

        private byte[] PakFile => File.Exists(PakFilePath)
                                    ? File.ReadAllBytes(PakFilePath)
                                    : null;

        private PakFileEntry[] PakFileNames()
        {
            try
            {
            }
            catch (Exception)
            {
                //ignore
            }

            //default
            return null;
        }

        private void ItmQuit_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void ItmExtractAll_Click(object sender, System.EventArgs e)
        {
        }

        private void PakExtractor_Load(object sender, System.EventArgs e)
        {
        }
    }
}