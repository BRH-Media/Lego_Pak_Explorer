using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TT_Games_Explorer.Components
{
    internal class NoFocusTrackBar : TrackBar
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

        private static int MakeParam(int loWord, int hiWord) => hiWord << 16 | loWord & ushort.MaxValue;

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            SendMessage(Handle, 296U, MakeParam(1, 1), 0);
        }
    }
}