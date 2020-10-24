using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using TT_Games_Explorer.Common;
using TT_Games_Explorer.Formats.PAK;
using TT_Games_Explorer.ListViewSorter;
using TT_Games_Explorer.Renderer.Textures;

// ReSharper disable LocalizableElement

namespace TT_Games_Explorer.UI
{
    public partial class PakExtractor : Form
    {
        public string PakFilePath { get; }
        private ListViewColumnSorter _lvs;
        public BinaryReader PakStreamReader { get; set; }
        private byte[] PakFile { get; }
        private PakFileEntry[] PakEntries { get; }

        public PakExtractor(string pakFile)
        {
            //designer and UI
            InitializeComponent();

            //validate
            if (File.Exists(pakFile))
            {
                //apply globals
                PakFilePath = pakFile;
                PakFile = File.ReadAllBytes(pakFile);

                //apply entries
                PakEntries = PakFileNames();
            }
            else
                Close();
        }

        public PakExtractor(byte[] pakData, string pakFileName = @"")
        {
            //designer and UI
            InitializeComponent();

            //validate
            if (pakData != null)
            {
                if (pakData.Length > 0)
                {
                    //apply globals
                    PakFilePath = pakFileName;
                    PakFile = pakData;

                    //apply entries
                    PakEntries = PakFileNames();
                }
                else
                    Close();
            }
            else
                Close();
        }

        private void PakExtractor_Load(object sender, EventArgs e)
        {
            PopulateListView();
        }

        private void PreviewFile()
        {
            //more validation
            if (lstMain.SelectedItems.Count > 0)
            {
                //parse out file ID from list
                var fileId = Convert.ToInt32(lstMain.SelectedItems[0].SubItems[1].Text);

                //read information
                var readOffset = PakEntries[fileId].Offset;
                var readSize = PakEntries[fileId].Offset;
                var fileName = PakEntries[fileId].Name;
                var ext = Path.GetExtension(fileName).ToLower();

                //grab needed data from archive via extraction
                var buffer = ReadFileData(readOffset, readSize);

                //validation
                if (buffer != null)
                    switch (ext)
                    {
                        //executables
                        case ".exe":
                        case ".dll":
                        case ".bat":
                        case ".com":
                        case ".cmd":
                        case ".sh":
                        case ".so":
                            MessageBox.Show(
                                $"You cannot run executable files of type '*{ext}' from within an archive, as this could lead to arbitrary code execution.",
                                @"Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            break;

                        //code files
                        case ".txt":
                        case ".csv":
                        case ".sub":
                        case ".bms":
                        case ".sf":
                        case ".scp":
                        case ".cfg":
                        case ".ini":
                        case ".inf":
                        case ".vdf":
                        case ".gip":
                        case ".gix":
                        case ".giz":
                        case ".gin":
                        case ".ats":
                            new CodePreview(buffer, fileName).ShowDialog();
                            break;

                        //archive files
                        case ".dat":
                            MessageBox.Show(@"You cannot open *.DAT files from within a *.DAT file");
                            break;

                        case ".hdr":
                            MessageBox.Show(@"You cannot open *.HDR files from within a *.DAT file");
                            break;

                        case ".pak":
                            new PakExtractor(buffer, fileName).ShowDialog();
                            break;

                        //image files
                        case ".tex":
                        case ".dds":
                        case ".png":
                        case ".bmp":
                        case ".raw":
                        case ".tga":
                        case ".jpg":
                        case ".jpeg":
                        case ".gif":
                        case ".giff":
                        case ".tif":
                        case ".tiff":
                            //new texture handler
                            var texHandler = new TexTrend(buffer, fileName);

                            //run preview window
                            new TexturePreview(texHandler).ShowDialog();
                            break;

                        //anything else is unsupported
                        default:
                            MessageBox.Show(
                                $@"*{ext} files are not currently supported");
                            break;
                    }
                else
                    MessageBox.Show(@"Null extracted data; could not open file.");
            }
        }

        private byte[] ReadFileData(uint dataOffset, uint dataSize)
        {
            try
            {
                //current position to seek back to when done
                var getBack = PakStreamReader.BaseStream.Position;

                //seek to file data location
                PakStreamReader.BaseStream.Seek(dataOffset, SeekOrigin.Begin);

                //extract data from specified information
                var data = PakStreamReader.ReadBytes(Convert.ToInt32(dataSize));

                //seek back to original location
                PakStreamReader.BaseStream.Seek(getBack, SeekOrigin.Begin);

                //return result
                return data;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error whilst attempting PAK extraction:\n\n{ex}");
            }

            //default
            return null;
        }

        private string ReadFileName(uint nameOffset)
        {
            //current position to seek back to when done
            var getBack = PakStreamReader.BaseStream.Position;

            //debugging
            //MessageBox.Show(getBack.ToString());
            //MessageBox.Show(offset.ToString());

            //seek to the name offset
            PakStreamReader.BaseStream.Seek(nameOffset, SeekOrigin.Begin);

            //verify UTF-8
            if (PakStreamReader.ReadByte() > 240)
                return "";

            //seek back to the name offset
            PakStreamReader.BaseStream.Seek(nameOffset, SeekOrigin.Begin);

            //where the name is stored
            var array = new byte[200];

            //the loop kill switch
            var flag = true;

            //size of name data array
            var newSize = 0;

            while (flag)
            {
                var num = PakStreamReader.ReadByte();

                //is is null?
                if (num == 0)
                {
                    Array.Resize(ref array, newSize);
                    flag = false;
                }
                else
                {
                    array[newSize] = num;
                    ++newSize;
                }
            }

            //go back to the previous position
            PakStreamReader.BaseStream.Seek(getBack, SeekOrigin.Begin);

            //return UTF-8 encoded string
            return Encoding.UTF8.GetString(array, 0, array.Length);
        }

        private PakFileEntry[] PakFileNames()
        {
            try
            {
                //validate
                if (PakFile != null)
                {
                    if (PakFile.Length > 0)
                    {
                        //load whole PAK file into memory
                        var pakMemoryStream = new MemoryStream(PakFile);
                        PakStreamReader = new BinaryReader(pakMemoryStream);

                        //header information
                        var pakMagic = PakStreamReader.ReadUInt32().ToString(@"X8");

                        //verify magic
                        if (pakMagic == @"1234567A")
                        {
                            //total files contained
                            var pakFileCount = PakStreamReader.ReadUInt32();
                            var pakArchiveSize = PakStreamReader.ReadUInt32();

                            //entries to return
                            var pakEntries = new PakFileEntry[pakFileCount];

                            //seek past useless information
                            PakStreamReader.BaseStream.Seek(24, SeekOrigin.Begin);

                            //go through the file counter
                            for (var i = 0; i < pakFileCount; i++)
                            {
                                //needed info
                                var nameOffset = PakStreamReader.ReadUInt32();
                                var fileOffset = PakStreamReader.ReadUInt32();
                                var fileSize = PakStreamReader.ReadUInt32();

                                //get file name
                                var fileName = ReadFileName(nameOffset);

                                //seek forward by 16 to skip useless header information
                                PakStreamReader.BaseStream.Seek(16, SeekOrigin.Current);

                                //append to return list
                                pakEntries[i] = new PakFileEntry
                                {
                                    Name = fileName,
                                    Offset = fileOffset,
                                    Size = fileSize
                                };
                            }

                            //return the final list
                            return pakEntries;
                        }
                        else
                            MessageBox.Show(@"Not a PAK file!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            //default
            return null;
        }

        private ListViewItem GetItem(int index)
        {
            //information needed for parsing
            var fileName = PakEntries[index].Name;
            var fileType = Methods.GetLegoFileType(fileName);

            //icon assignation (default is the 'Unknown File' icon)
            var imageIndex = 1;

            //go through and attempt icon assignations
            switch (Path.GetExtension(fileName).ToLower())
            {
                //code files
                case ".txt":
                case ".csv":
                case ".sub":
                case ".bms":
                case ".sf":
                case ".scp":
                case ".cfg":
                case ".ini":
                case ".inf":
                case ".vdf":
                case ".gip":
                case ".gix":
                case ".giz":
                case ".gin":
                case ".ats":
                    imageIndex = 2;
                    break;

                //archive files
                case ".dat":
                case ".hdr":
                case ".pak":
                    imageIndex = 3;
                    break;

                //image files
                case ".tex":
                case ".dds":
                case ".png":
                case ".bmp":
                case ".raw":
                case ".tga":
                case ".jpg":
                case ".jpeg":
                case ".gif":
                case ".giff":
                case ".tif":
                case ".tiff":
                    imageIndex = 4;
                    break;

                //executables
                case ".exe":
                case ".dll":
                case ".bat":
                case ".com":
                case ".cmd":
                case ".sh":
                case ".so":
                    imageIndex = 5;
                    break;
            }

            //item to add to the list view
            var newItem = new ListViewItem(@"", imageIndex);

            //construct sub-items
            var items = new[]
            {
                new ListViewItem.ListViewSubItem(newItem, index.ToString()),
                new ListViewItem.ListViewSubItem(newItem, fileName),
                new ListViewItem.ListViewSubItem(newItem, fileType),
                new ListViewItem.ListViewSubItem(newItem, $"0x{PakEntries[index].Offset:X8}"),
                new ListViewItem.ListViewSubItem(newItem, PakEntries[index].Size.ToString()),
            };

            //add sub-items
            newItem.SubItems.AddRange(items);

            //return the final item
            return newItem;
        }

        private void PopulateListView()
        {
            try
            {
                if (PakEntries != null)
                {
                    if (PakEntries.Length > 0)
                    {
                        //disable list while populating
                        lstMain.Enabled = false;
                        itmExtract.Enabled = false;
                        itmExtractAll.Enabled = false;
                        itmOptions.Enabled = false;
                        itmOperation.Enabled = false;

                        //sorting information
                        lstMain.Items.Clear();
                        _lvs = new ListViewColumnSorter();
                        _lvs.Initialize(lstMain, "text,num,text,text,text,num", null);

                        //add entries
                        for (var i = 0; i < PakEntries.Length; i++)
                        {
                            lstMain.Items.Add(GetItem(i));
                        }

                        //resize columns
                        _lvs.lv_ColumnClick(this, new ColumnClickEventArgs(1));
                        lstMain.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                        lstMain.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                        //reenable list after populating
                        lstMain.Enabled = true;
                        itmExtract.Enabled = true;
                        itmExtractAll.Enabled = true;
                        itmOptions.Enabled = true;
                        itmOperation.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error whilst attempting to fill list view:\n\n{ex}");
            }
        }

        private void ItmQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ExtractFile(int fileId, string folder)
        {
            //directory information
            var directoryName = Path.GetDirectoryName(PakEntries[fileId].Name);
            var fullDirectory = $"{folder}\\{directoryName}";

            //split up each directory level
            var strArray = directoryName?.Split('\\');
            var directoryBuilder = @"";

            //validate and ensure each directory level exists
            if (strArray != null)
                foreach (var dir in strArray)
                {
                    directoryBuilder += $"{dir}\\";
                    if (!Directory.Exists($"{folder}\\{directoryBuilder}"))
                        Directory.CreateDirectory($"{folder}\\{directoryBuilder}");
                }

            //read in data
            var readOffset = PakEntries[fileId].Offset;
            var readSize = PakEntries[fileId].Size;
            var fileName = Path.GetFileName(PakEntries[fileId].Name);
            var readData = ReadFileData(readOffset, readSize);

            //build full path
            var fullPath = $"{fullDirectory}\\{fileName}";

            //verify and write
            if (readData != null)
                File.WriteAllBytes(fullPath, readData);
        }

        private void ItmExtractAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (fbdExtractFolder.ShowDialog() != DialogResult.OK)
                    return;

                var dir = $"{fbdExtractFolder.SelectedPath}\\{Path.GetFileNameWithoutExtension(PakFilePath)}";

                //ensure we are writing to a valid folder
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                for (var fileId = 0; fileId < PakEntries.Length; fileId++)
                    ExtractFile(fileId, dir);

                MessageBox.Show(@"Extract Finish!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Extract error. Stack trace:\n\n{ex}");
            }
        }

        private void ItmExtract_Click(object sender, EventArgs e)
        {
            try
            {
                if (fbdExtractFolder.ShowDialog() != DialogResult.OK)
                    return;

                var dir = $"{fbdExtractFolder.SelectedPath}\\{Path.GetFileNameWithoutExtension(PakFilePath)}";

                //ensure we are writing to a valid folder
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                foreach (ListViewItem selectedItem in lstMain.SelectedItems)
                {
                    var fileId = Convert.ToInt32(selectedItem.SubItems[1].Text);
                    ExtractFile(fileId, dir);
                }

                MessageBox.Show(@"Successfully extracted files!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Extract error. Stack trace:\n\n{ex}");
            }
        }

        private void ItmPreview_Click(object sender, EventArgs e)
        {
            PreviewFile();
        }

        private void ItmCxtPreview_Click(object sender, EventArgs e)
        {
            PreviewFile();
        }

        private void CxtLstExtract_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!itmOptionRightClick.Checked) e.Cancel = true;
        }

        private void LstMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || !itmOptionDoubleClick.Checked)
                return;
            if (lstMain.SelectedItems.Count > 0)
                PreviewFile();
        }

        private void LstMain_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}