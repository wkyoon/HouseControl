using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HouseControl.Common
{
    public partial class SerialSelectForm : Form
    {
        public string[] baudrates = { "9600", "19200", "115200" };

        public delegate void SerialConnectionCallBack(string port, string baud, string port2, string baud2);
        public event SerialConnectionCallBack SerialConnectionAction;


        public SerialSelectForm(string[] ports)
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;

            foreach (string comport in ports)
            {
                if (comport != null)
                {
                    comboBox1.Items.Add(comport);
                    comboBox2.Items.Add(comport);
                }
                
            }

            //comboBox1.SelectedIndex = 0;
        }

        private void SerialSelectForm_Load(object sender, EventArgs e)
        {
           
        }

        private void btnOpenClose_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem==null||comboBox1.SelectedItem=="")
            {
                MessageBox.Show("현재 연결할수 있는 장비가 없습니다. \n 장비 연결 후 다시 시작하세요.");
                
            }
            else if (comboBox2.SelectedItem == null || comboBox2.SelectedItem == "")
            {
                MessageBox.Show("현재 연결할수 있는 장비가 없습니다. \n 장비 연결 후 다시 시작하세요.");
                
            }
            else
            {
                SerialConnectionAction.Invoke(comboBox1.SelectedItem.ToString(), "115200", comboBox2.SelectedItem.ToString(), "115200");
                Hide();
            }
            
        }

       


        private void SerialSelectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

       

    }
    
}
