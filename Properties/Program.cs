using System;
using System.Windows.Forms;
using TT_Games_Explorer.UI;

namespace TT_Games_Explorer.Properties
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