using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HouseControl
{
    public partial class ConsoleForm : Form
    {

        internal delegate void appendTextCallBack(string text);

        internal delegate void sendCommandDele(string text);
        internal event sendCommandDele sendCommand;

        public bool isShow;

        public ConsoleForm()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
            isShow = false;
        }

        private void ConsoleForm_Load(object sender, EventArgs e)
        {

        }

        private void tbCommand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string msg = tbCommand.Text.ToString().Trim() + "\n";
               sendCommand.Invoke(msg);
                //enter key is down
                appendLog(msg);
                
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string msg = tbCommand.Text.ToString().Trim() + "\n";
            sendCommand.Invoke(msg );
            //enter key is down
            appendLog(msg);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbLog.Text = "";
        }

        public void appendLog(string text)
        {
            if (isShow)
            {
                if (this.tbLog.InvokeRequired)
                {
                    this.Invoke(new appendTextCallBack(appendLog), text);
                    return;
                }
                else
                {
                    this.tbLog.AppendText(text);
                }
            }
            
        
        }

        private void tbLog_TextChanged(object sender, EventArgs e)
        {
            this.tbLog.ScrollToCaret();
        }

        private void ConsoleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string msg = "$Tbd2c=120:0\n";
            sendCommand.Invoke(msg);
            appendLog(msg);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string msg = "$Tbd1=\n";
            sendCommand.Invoke(msg);
            appendLog(msg);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string msg = "$Tbd2=9000\n";
            sendCommand.Invoke(msg);
            appendLog(msg);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string msg = "$Trdpc=119:9000:2\n";
            sendCommand.Invoke(msg);
            appendLog(msg);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string msg = "$Trdpc=119:9010:2\n";
            sendCommand.Invoke(msg);
            appendLog(msg);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string msg = "$Tbd2=9010\n";
            sendCommand.Invoke(msg);
            appendLog(msg);
        }
    }
}
