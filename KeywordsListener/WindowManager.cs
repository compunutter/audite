using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Audite
{
    class WindowManager
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_CLOSE = 0xF060;

        private IntPtr _handle;

        public string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            _handle = GetForegroundWindow();

            if (GetWindowText(_handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        // close the active window using API        
        public void CloseActiveWindow()
        {
            SendMessage(_handle, WM_SYSCOMMAND, SC_CLOSE, 0);
        }
    }
}
