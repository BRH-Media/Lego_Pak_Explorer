using Lego_Pak_Explorer.UI;
using System;
using System.Windows.Forms;

namespace Lego_Pak_Explorer.Properties
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Home());
        }
    }
}