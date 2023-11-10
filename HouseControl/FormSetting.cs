using Newtonsoft.Json.Linq;
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
    public partial class FormSetting : Form
    {
        string dfolderpath = "";
        string dhomename = "TheStarHue";
        string dhomename_config = "config";

        JObject joSetting;

        int temp1 = 15;
        int temp2 = 15;


        public FormSetting()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
        }

        private void FormSetting_Load(object sender, EventArgs e)
        {

            string dpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dfolderpath = dpath + "\\" + dhomename;
            string configpath = dfolderpath + "\\" + dhomename_config + "\\";
            string settingtext = System.IO.File.ReadAllText(configpath + "settingtemp.json");

            //Console.WriteLine("{0}", settingtext);
            joSetting = JObject.Parse(settingtext);
            //Console.WriteLine("{0}", joSetting["temp1"]);
            //COMPORT = joSetting["com"].ToString();

            tbTemp1.Text = joSetting["temp1"].ToString();
            tbTemp2.Text = joSetting["temp2"].ToString();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            temp1 = int.Parse(tbTemp1.Text.ToString());

            if (temp1 < 4 || temp1 > 40)
            {
                MessageBox.Show("설정범위 4 ~ 40 이내 입니다.");
                return;
            }


            temp2 = int.Parse(tbTemp2.Text.ToString());

            if (temp2 < 4 || temp2 > 40)
            {
                MessageBox.Show("설정범위 4 ~ 40 이내 입니다.");
                return;
            }

            // 저장하자 온도 설정으로 
            joSetting["temp1"] = tbTemp1.Text.ToString();
            joSetting["temp2"] = tbTemp2.Text.ToString();

            string configpath = dfolderpath + "\\" + dhomename_config + "\\";
            //     Console.WriteLine(joSetting.ToString());
            System.IO.File.WriteAllText(configpath + "settingtemp.json", joSetting.ToString());


            MessageBox.Show("자주 쓰는 난방온도를 설정하였습니다.");
            Hide();
        }

        //========================================================

        private void btnup1_Click(object sender, EventArgs e)
        {
            temp1 = int.Parse(tbTemp1.Text.ToString());
            temp1++;
            if (temp1 > 40) temp1 = 40;
            tbTemp1.Text = temp1.ToString();
        }

        private void btndown1_Click(object sender, EventArgs e)
        {
            temp1 = int.Parse(tbTemp1.Text.ToString());
            temp1--;
            if (temp1 < 5) temp1 = 4;
            tbTemp1.Text = temp1.ToString();
        }

        private void btnup2_Click(object sender, EventArgs e)
        {
            temp2 = int.Parse(tbTemp2.Text.ToString());
            temp2++;
            if (temp2 > 40) temp2 = 40;
            tbTemp2.Text = temp2.ToString();
        }

        private void btndown2_Click(object sender, EventArgs e)
        {
            temp2 = int.Parse(tbTemp2.Text.ToString());
            temp2--;
            if (temp2 < 5) temp2 = 4;
            tbTemp2.Text = temp2.ToString();
        }


        //=================================================================

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }


        //==================================================================

        private void FormSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
