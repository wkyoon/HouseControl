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
    public partial class FormValleryE : Form
    {

        internal delegate void sendHouseStatusDele(string houseid, string cmdtype, int cmdvalue);
        internal event sendHouseStatusDele sendHouseCommand;


        CheckBox[,] checkBox = new CheckBox[3, 10];

        CheckBox checkBox0cheung;
        CheckBox checkBox2cheung;
        CheckBox checkBox3cheung;
        CheckBox checkBox5cheung;


        string dfolderpath = "";
        string dhomename = "TheStarHue";
        string dhomename_config = "config";

        JObject joSetting;

        int temp1 = 15;
        int temp2 = 15;

        public FormValleryE()
        {
            InitializeComponent();
        }

        private void FormValleryE_Load(object sender, EventArgs e)
        {

            checkBox0cheung = new CheckBox();
            checkBox0cheung.Tag = "0";
            checkBox0cheung.Text = "전체";
            checkBox0cheung.CheckedChanged += cheung_CheckedChanged;
            tableLayoutPanel1.Controls.Add(checkBox0cheung, 0, 0);

            checkBox2cheung = new CheckBox();
            checkBox2cheung.Tag = "2";
            checkBox2cheung.Text = "2층";
            checkBox2cheung.CheckedChanged += cheung_CheckedChanged;
            tableLayoutPanel1.Controls.Add(checkBox2cheung, 0, 3);

            checkBox3cheung = new CheckBox();
            checkBox3cheung.Tag = "3";
            checkBox3cheung.Text = "3층";
            checkBox3cheung.CheckedChanged += cheung_CheckedChanged;
            tableLayoutPanel1.Controls.Add(checkBox3cheung, 0, 2);

            checkBox5cheung = new CheckBox();
            checkBox5cheung.Tag = "5";
            checkBox5cheung.Text = "5층";
            checkBox5cheung.CheckedChanged += cheung_CheckedChanged;
            tableLayoutPanel1.Controls.Add(checkBox5cheung, 0, 1);

           // Label lb2cheung = new Label();
           // lb2cheung.Text = "2층";
           // lb2cheung.Margin = new Padding(10, 10, 4, 4);
           // tableLayoutPanel1.Controls.Add(lb2cheung, 0, 3);

            //Label lb3cheung = new Label();
            //lb3cheung.Text = "3층";
            //lb3cheung.Margin = new Padding(10, 10, 4, 4);
            //tableLayoutPanel1.Controls.Add(lb3cheung, 0, 2);

            //Label lb5cheung = new Label();
            //lb5cheung.Text = "5층";
            //lb5cheung.Margin = new Padding(10, 10, 4, 4);
            //tableLayoutPanel1.Controls.Add(lb5cheung, 0, 1);


            for (int i = 1; i < 11; i++)
            {
               Label lbho = new Label();
               int hosil = 210;
               lbho.Text = String.Format("{0}호", hosil+i);
               lbho.Margin = new Padding(10, 10, 4, 4);
               
               checkBox[0, i - 1] = new CheckBox();
               checkBox[0, i - 1].Tag = String.Format("{0}", hosil + i);
               checkBox[0, i - 1].Text = String.Format("{0}호", hosil + i);
               //checkBox[0, i - 1].CheckedChanged += ho_CheckedChanged;

               Panel panel = new Panel();
              // panel.Controls.Add(lbho);
               panel.Controls.Add(checkBox[0, i - 1]);
               tableLayoutPanel1.Controls.Add(panel, i, 3);    
            }


            for (int i = 1; i < 11; i++)
            {
                Label lbho = new Label();
                int hosil = 310;
                lbho.Text = String.Format("{0}호", hosil + i);
                lbho.Margin = new Padding(10, 10, 4, 4);

                checkBox[1, i - 1] = new CheckBox();
                checkBox[1, i - 1].Tag = String.Format("{0}", hosil + i);
                checkBox[1, i - 1].Text = String.Format("{0}호", hosil + i);
                //checkBox[0, i - 1].CheckedChanged += ho_CheckedChanged;

                Panel panel = new Panel();
                // panel.Controls.Add(lbho);
                panel.Controls.Add(checkBox[1, i - 1]);
                tableLayoutPanel1.Controls.Add(panel, i, 2);
            }

            for (int i = 1; i < 11; i++)
            {
                Label lbho = new Label();
                int hosil = 510;
                lbho.Text = String.Format("{0}호", hosil + i);
                lbho.Margin = new Padding(10, 10, 4, 4);

                checkBox[2, i - 1] = new CheckBox();
                checkBox[2, i - 1].Tag = String.Format("{0}", hosil + i);
                checkBox[2, i - 1].Text = String.Format("{0}호", hosil + i);
                //checkBox[0, i - 1].CheckedChanged += ho_CheckedChanged;

                Panel panel = new Panel();
                // panel.Controls.Add(lbho);
                panel.Controls.Add(checkBox[2, i - 1]);
                tableLayoutPanel1.Controls.Add(panel, i, 1);
            }


            checkBox0cheung.Checked = true;


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

        private void ho_CheckedChanged(object sender, EventArgs e)
        {

            Console.WriteLine("{0}", (sender as CheckBox).Tag.ToString());
        }



        private void cheung_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("zzz {0}", (sender as CheckBox).Tag.ToString());
            bool isChecked = false;
            int floor = 0;
            if ((sender as CheckBox).Checked)
            {
                isChecked = true;
            }
            else
            {
                isChecked = false;
            }


            if ((sender as CheckBox).Tag.ToString().Equals("0"))
            {
                for (int h = 0; h < 3; h++)
                {
                    for (int i = 1; i < 11; i++)
                    {
                        checkBox[h, i - 1].Checked = isChecked;
                    }
                }

               // checkBox2cheung.Checked = isChecked;
               // checkBox3cheung.Checked = isChecked;
               // checkBox5cheung.Checked = isChecked;
                

            }
            else
            {

                // 2층 3층 5층 으로 되어 있고
                // 0 1 2 로 indexing 한다. 
                if ((sender as CheckBox).Tag.ToString().Equals("2"))
                {
                    floor = 0;
                }
                else if ((sender as CheckBox).Tag.ToString().Equals("3"))
                {
                    floor = 1;
                }
                else if ((sender as CheckBox).Tag.ToString().Equals("5"))
                {
                    floor = 2;
                }

                for (int i = 1; i < 11; i++)
                {
                    checkBox[floor, i - 1].Checked = isChecked;
                }

            }

        }

        //=============================================================
        // [[
        // 선택되어 있는 놈들을 전달해야 한다. 
        private void btnPowerOn1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.WriteLine("{0} {1} {2} {3}", i, j, checkBox[i, j].Tag, checkBox[i, j].Checked);
                    if (checkBox[i, j].Checked)
                    {
                        sendHouseCommand.Invoke(checkBox[i, j].Tag.ToString(), "power", 1);
                    }

                }
            }

        }

        private void btnPowerOff1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.WriteLine("{0} {1} {2} {3}", i, j, checkBox[i, j].Tag, checkBox[i, j].Checked);
                    if (checkBox[i, j].Checked)
                    {
                        sendHouseCommand.Invoke(checkBox[i, j].Tag.ToString(), "power", 0);
                    }

                }
            }
        }

        private void btnOutOn1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.WriteLine("{0} {1} {2} {3}", i, j, checkBox[i, j].Tag, checkBox[i, j].Checked);
                    if (checkBox[i, j].Checked)
                    {
                        sendHouseCommand.Invoke(checkBox[i, j].Tag.ToString(), "outing", 1);
                    }

                }
            }
        }

        private void btnOutOff1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.WriteLine("{0} {1} {2} {3}", i, j, checkBox[i, j].Tag, checkBox[i, j].Checked);
                    if (checkBox[i, j].Checked)
                    {
                        sendHouseCommand.Invoke(checkBox[i, j].Tag.ToString(), "outing", 0);
                    }

                }
            }
        }

        private void btnSetTemp1_Click(object sender, EventArgs e)
        {
            temp1 = int.Parse(tbTemp1.Text.ToString());
            int temp = temp1 * 100;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.WriteLine("{0} {1} {2} {3}", i, j, checkBox[i, j].Tag, checkBox[i, j].Checked);
                    if (checkBox[i, j].Checked)
                    {
                        sendHouseCommand.Invoke(checkBox[i, j].Tag.ToString(), "settemp", temp);
                    }

                }
            }
        }

        private void btnSetTemp2_Click(object sender, EventArgs e)
        {
            temp2 = int.Parse(tbTemp2.Text.ToString());
            int temp = temp2 * 100;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.WriteLine("{0} {1} {2} {3}", i, j, checkBox[i, j].Tag, checkBox[i, j].Checked);
                    if (checkBox[i, j].Checked)
                    {
                        sendHouseCommand.Invoke(checkBox[i, j].Tag.ToString(), "settemp", temp);
                    }

                }
            }
        }

        
        // ]]
        //=============================================================

        private void tbTemp1_KeyPress(object sender, KeyPressEventArgs e)
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

        private void tbTemp2_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
