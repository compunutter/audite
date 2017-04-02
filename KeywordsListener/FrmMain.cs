using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Audite
{
    public partial class FrmMain : Form
    {
        private UserActivityMonitor _hook;
        private string[] _triggers;
        private string[] _apps;
        private const int MAX_LENGTH = 10;
        private string _lastKeys = "";
        private bool _msgWindowOpen;

        public FrmMain()
        {
            InitializeComponent();
            _apps = Policies.GetApps();
            _triggers = Policies.GetTriggers();
            _msgWindowOpen = false;
            startListening();
        }

        private void startListening()
        {
            UserActivityMonitor.FrmMain = this;
            _hook = new UserActivityMonitor();
        }

        public void KeyPressed(string key)
        {
            if (key.Length == 1)
            {
                _lastKeys += key;

                if (_lastKeys.Length > MAX_LENGTH)
                {
                    _lastKeys = _lastKeys.Substring(1);
                }

                matchTriggers(); //Self explanitory
            }
            else if (key == "Back")
            {
                if (_lastKeys.Length > 1)
                {
                    // Remove the last char if the users presses backspace
                    _lastKeys = _lastKeys.Substring(0, _lastKeys.Length - 1);
                }
                else if (_lastKeys.Length <= 1)
                {
                    _lastKeys = string.Empty;
                }

            }

            Console.WriteLine(_lastKeys);
        }

        private void matchTriggers()
        {
            foreach (string trigger in _triggers)
            {
                if (_lastKeys.Contains(trigger))
                {
                    new Thread(new ThreadStart(closeActiveWindow)).Start();
                }
            }
        }

        private void closeActiveWindow()
        {
            Thread.Sleep(1000); // For 1 second to allow for a screenshot to be taken by impero
            WindowManager wm = new WindowManager();
            string title = wm.GetActiveWindowTitle();
            foreach (string s in _apps)
            {
                if (title.Contains(s))
                {
                    wm.CloseActiveWindow();
                }
            }
            if (!_msgWindowOpen)
            {
                _msgWindowOpen = true;
                _lastKeys = string.Empty;
                new FrmWarning().ShowForm(this);
            }
        }

        public void MsgWindowIsClosing()
        {
            _msgWindowOpen = false;
        }
    }
}
