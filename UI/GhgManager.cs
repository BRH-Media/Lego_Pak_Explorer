using System;
using System.IO;
using System.Windows.Forms;
using TT_Games_Explorer.Formats.GHG.Structure;

// ReSharper disable LocalizableElement

namespace TT_Games_Explorer.UI
{
    public partial class GhgManager : Form
    {
        public GhgFile ComplexModel { get; }

        public GhgManager(string filePath)
        {
            if (File.Exists(filePath))
            {
                //read in the entire GHG file
                var fileData = File.ReadAllBytes(filePath);

                //validate it
                if (fileData.Length > 0)
                {
                    //attempt to load the GHG modeling format
                    var ghgModel = GhgFile.FromRawBytes(fileData, filePath);

                    //validate it
                    if (ghgModel != null)
                        ComplexModel = ghgModel;
                }
            }
        }

        public GhgManager(byte[] ghgData, string filePath)
        {
            //validate it
            if (ghgData != null)
                if (ghgData.Length > 0)
                {
                    //attempt to load the GHG modeling format
                    var ghgModel = GhgFile.FromRawBytes(ghgData, filePath);

                    //validate it
                    if (ghgModel != null)
                        ComplexModel = ghgModel;
                }
        }

        public GhgManager()
        {
            InitializeComponent();

            //blank initialiser; will not load GHG model
        }

        private void FillFileList()
        {
            try
            {
                //validate data
                if (ComplexModel != null)
                {
                    //clear existing files out of the list view
                    lstMain.Clear();

                    //ID number incrementer
                    var id = 0;

                    //add all DDS textures
                    foreach (var d in ComplexModel.Textures)
                    {
                        //entry to add to list
                        var entry = new ListViewItem(id.ToString(), 4);

                        //add entry sub-items
                        var entries = new[]
                        {
                            new ListViewItem.ListViewSubItem(entry, d.FileName),
                            new ListViewItem.ListViewSubItem(entry, @"Texture"),
                            new ListViewItem.ListViewSubItem(entry, d.TextureData.Length.ToString())
                        };

                        //apply sub-items
                        entry.SubItems.AddRange(entries);

                        //apply the new entry
                        lstMain.Items.Add(entry);

                        //increment file list ID
                        id++;
                    }

                    //add all models
                    foreach (var m in ComplexModel.Models)
                    {
                        //entry to add to list
                        var entry = new ListViewItem(id.ToString(), 3);

                        //add entry sub-items
                        var entries = new[]
                        {
                            new ListViewItem.ListViewSubItem(entry, m.FileName),
                            new ListViewItem.ListViewSubItem(entry, @"Model"),
                            new ListViewItem.ListViewSubItem(entry, m.ModelData.Length.ToString())
                        };

                        //apply sub-items
                        entry.SubItems.AddRange(entries);

                        //apply the new entry
                        lstMain.Items.Add(entry);

                        //increment file list ID
                        id++;
                    }

                    //resize the list columns to fit the render area
                    lstMain.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    lstMain.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"List load error:\n\n{ex}");
            }
        }

        private void GhgManager_Load(object sender, EventArgs e)
        {
            //refresh list view
            FillFileList();
        }
    }
}