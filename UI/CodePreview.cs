using FastColoredTextBoxNS;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

// ReSharper disable LocalizableElement
// ReSharper disable UnusedMember.Local

namespace TT_Games_Explorer.UI
{
    public partial class CodePreview : Form
    {
        //styling information for syntax highlighting
        private readonly TextStyle _blueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);

        private readonly TextStyle _boldStyle = new TextStyle(null, null, FontStyle.Bold | FontStyle.Underline);
        private readonly TextStyle _grayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
        private readonly TextStyle _blueVioletStyle = new TextStyle(Brushes.BlueViolet, null, FontStyle.Regular);
        private readonly TextStyle _greenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        private readonly TextStyle _brownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
        private readonly TextStyle _maroonStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);

        //this is where the file name/path is stored
        private readonly string _txtPath = @"";

        /// <summary>
        /// Load the code window from an existing file
        /// </summary>
        /// <param name="filePath">Path to the file that is to be loaded; the code will fail if it doesn't exist.</param>
        public CodePreview(string filePath)
        {
            try
            {
                //designer and UI setup
                InitializeComponent();

                //validation
                if (!File.Exists(filePath))
                    Close();
                else
                {
                    //setup the code viewer
                    txtCodePreview.Text = File.ReadAllText(filePath);
                    _txtPath = filePath;

                    //enable 'Save' since we loaded from a file and not memory
                    itmSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                //alert the user and kill the form
                MessageBox.Show($"Code previewer failed to load because of an error. Stack trace:\n\n{ex}");
                Close();
            }
        }

        /// <summary>
        /// Load the code window from memory (byte array)
        /// </summary>
        /// <param name="fileData">The byte data to load; will be converted to a string.</param>
        /// <param name="fileName">This file doesn't have to exist; default is string.Empty. This serves to provide a file name for the GUI.</param>
        public CodePreview(byte[] fileData, string fileName = @"")
        {
            try
            {
                //designer and UI setup
                InitializeComponent();

                //validation of byte array
                if (fileData != null)
                {
                    if (fileData.Length > 0)
                    {
                        //convert bytes to string
                        var fileString = Encoding.Default.GetString(fileData);

                        //validate converted string
                        if (!string.IsNullOrEmpty(fileString))
                        {
                            //setup the code viewer
                            txtCodePreview.Text = fileString;
                            _txtPath = fileName;

                            //disable 'Save' since we loaded from memory and not a file
                            itmSave.Enabled = false;
                        }
                        else
                            Close();
                    }
                    else
                        Close();
                }
                else
                    Close();
            }
            catch (Exception ex)
            {
                //alert the user and kill the form
                MessageBox.Show($"Code previewer failed to load because of an error. Stack trace:\n\n{ex}");
                Close();
            }
        }

        private void ItmQuit_Click(object sender, EventArgs e) => Close();

        private void TxtCodePreview_TextChanged(object sender, TextChangedEventArgs e)
        {
            //styling updaters (syntax highlighting)
            e.ChangedRange.SetStyle(_brownStyle, "\"\"|@\"\"|''|@\".*?\"|(?<!@)(?<range>\".*?[^\\\\]\")|'.*?[^\\\\]'");
            e.ChangedRange.SetStyle(_greenStyle, "//.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(_greenStyle, "(/\\*.*?\\*/)|(/\\*.*)", RegexOptions.Singleline);
            e.ChangedRange.SetStyle(_greenStyle, "(/\\*.*?\\*/)|(.*\\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft);
            e.ChangedRange.SetStyle(_brownStyle, "\\b\\d+[\\.]?\\d*([eE]\\-?\\d+)?[lLdDfF]?\\b|\\b0x[a-fA-F\\d]+\\b");
            e.ChangedRange.SetStyle(_grayStyle, "^\\s*(?<range>\\[.+?\\])\\s*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(_blueVioletStyle, "\\b(dir|file|minikit|level)");
            e.ChangedRange.SetStyle(_blueStyle, "\\b(area_start|area_end|level_start|level_end)");
            e.ChangedRange.ClearFoldingMarkers();
            e.ChangedRange.SetFoldingMarkers("area_start", "area_end");
            e.ChangedRange.SetFoldingMarkers("level_start", "level_end");
            e.ChangedRange.SetFoldingMarkers("{", "}");
        }

        private void ItmSave_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(_txtPath, txtCodePreview.Text);
            }
            catch (Exception ex)
            {
                //alert the user
                MessageBox.Show($"Save failed due to an error. Stack trace:\n\n{ex}");
            }
        }

        private void ItmSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                //file name is name of the file originally loaded
                sfdExport.FileName = Path.GetFileName(_txtPath);

                //filter is set to the existing file type of the loaded file data
                var ext = Path.GetExtension(_txtPath);

                //*.txt is the default if no extension exists
                var filter = @"TXT Files|*.txt";

                //validate filter information,
                if (!string.IsNullOrEmpty(ext))
                    //trim the start of a period and display the relevant information
                    filter = $"{ext.TrimStart('.').ToUpper()} Files|*{ext}";

                //apply file filter to dialog
                sfdExport.Filter = filter;

                //prompt for a save location
                if (sfdExport.ShowDialog() != DialogResult.OK)
                    return;

                //write all edited data to the file (existing or not)
                File.WriteAllText(sfdExport.FileName, txtCodePreview.Text);
            }
            catch (Exception ex)
            {
                //alert the user
                MessageBox.Show($"Save failed due to an error. Stack trace:\n\n{ex}");
            }
        }

        private void CodePreview_Load(object sender, EventArgs e)
        {
            //nothing just yet
        }
    }
}