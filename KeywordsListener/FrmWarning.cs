using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Audite
{
    public partial class FrmWarning : Form
    {
        private bool _msgAcked;
        private FrmMain _frmMain;
        public FrmWarning()
        {
            InitializeComponent();
            _msgAcked = false;
            pictureBox1.Left = (this.ClientSize.Width - pictureBox1.Width) / 2;
            button1.Text = Policies.GetBtnLbl();
            label1.Text = Policies.GetWarningMsg();

        }

        public void ShowForm(FrmMain m)
        {
            _frmMain = m;
            this.ShowDialog();
            this.TopMost = true;
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            // The Window was deactivated 
            this.TopMost = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _msgAcked = true;
            this.Close();
        }

        private void FrmWarning_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_msgAcked)
            {
                e.Cancel = true;
            }
            _frmMain.MsgWindowIsClosing();
        }

        protected override void WndProc(ref Message message)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            switch (message.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = message.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                        return;
                    break;
            }

            base.WndProc(ref message);
        }

        private void FrmWarning_Shown(object sender, EventArgs e)
        {
            this.TopMost = true;
        }
    }
}
