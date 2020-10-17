using FastColoredTextBoxNS;
using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

// ReSharper disable UnusedMember.Local

namespace Lego_Pak_Explorer.UI
{
    public partial class CodePreview : Form
    {
        private readonly TextStyle _blueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        private TextStyle _boldStyle = new TextStyle(null, null, FontStyle.Bold | FontStyle.Underline);
        private readonly TextStyle _grayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
        private readonly TextStyle _blueVioletStyle = new TextStyle(Brushes.BlueViolet, null, FontStyle.Regular);
        private readonly TextStyle _greenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        private readonly TextStyle _brownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
        private TextStyle _maroonStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);
        private readonly string _txtPath = "";

        public CodePreview(string filePath)
        {
            InitializeComponent();
            if (!File.Exists(filePath))
                return;
            _fastColoredTextBox1.Text = File.ReadAllText(filePath);
            _txtPath = filePath;
        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e) => Close();

        private void FastColoredTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
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

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e) => File.WriteAllText(_txtPath, _fastColoredTextBox1.Text);

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _saveFileDialog1.FileName = Path.GetFileName(_txtPath);
            if (_saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            File.WriteAllText(_saveFileDialog1.FileName, _fastColoredTextBox1.Text);
        }
    }
}