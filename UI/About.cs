using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using TT_Games_Explorer.Common;

// ReSharper disable LocalizableElement

namespace TT_Games_Explorer.UI
{
    internal partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            Text = $@"About {AssemblyTitle}";
            lblProductName.Text = AssemblyProduct;
            lblVersion.Text = $@"Version {AssemblyVersion}";
            lblCopyright.Text = AssemblyCopyright;
            lblCompanyName.Text = AssemblyCompany;
            txtDescription.Lines = CreditsString;
        }

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        public string[] CreditsString
        {
            get
            {
                var credits = new string[Globals.Credits.Length];
                for (var i = 0; i < Globals.Credits.Length; i++)
                {
                    var c = Globals.Credits[i];
                    credits[i] = $"{c[0]} {c[1]}";
                }

                return credits;
            }
        }

        public string AssemblyTitle
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length <= 0)
                    return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
                var titleAttribute = (AssemblyTitleAttribute)attributes[0];
                return titleAttribute.Title != ""
                    ? titleAttribute.Title
                    : Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public string AssemblyDescription
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                return attributes.Length == 0
                    ? ""
                    : ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                return attributes.Length == 0
                    ? ""
                    : ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                return attributes.Length == 0
                    ? ""
                    : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                return attributes.Length == 0
                    ? ""
                    : ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        private void TxtDescription_TextChanged(object sender, EventArgs e)
        {
        }

        private void About_Load(object sender, EventArgs e)
        {
        }
    }
}