using HouseControl.model;
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
    public partial class RemoteControlForm : Form
    {
        RoomInfo ri;

        internal delegate void updateEnableButtonCallBack(bool on);

        // 직접 명령하는 부분은 없다.
//        internal delegate void sendCommandDele(byte[] cmd);
//        internal event sendCommandDele sendCommand;

        internal delegate void sendNewCommandDele(RoomInfo ri, string cmdtype, int cmdvalue);
        internal event sendNewCommandDele sendNewCommand;


        //  cmdtype status,poweron,poweroff,outingon,outingoff,settemp
        // cmdvalue settemp value 
        internal delegate void sendHouseStatusDele(string houseid,string cmdtype,int cmdvalue);
        internal event sendHouseStatusDele sendHouseCommand;


        CbFrame cbframe;

        int nid;
        int masterid;
        int id;


        int temp1 = 15;
        int temp2 = 15;


        string dfolderpath = "";
        string dhomename = "TheStarHue";
        string dhomename_config = "config";

        JObject joSetting;




        public RemoteControlForm()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
        }

        private void RemoteControlForm_Load(object sender, EventArgs e)
        {
            // 전체 제어 페이지는 일단 숨기자. 
            //tabControl1.TabPages.Remove(tabPage2);

            lbRun.Visible = false;
            lbRun2.Visible = false;

            rbPowerOff.Checked = true;
            rbPowerOff2.Checked = true;

            rbOutOff.Checked = true;
            rbOutOff2.Checked = true;

           

            cbframe = new CbFrame();


            string dpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dfolderpath = dpath + "\\" + dhomename;
            string configpath = dfolderpath + "\\" + dhomename_config + "\\";
            string settingtext = System.IO.File.ReadAllText(configpath + "settingtemp.json");

            
            joSetting = JObject.Parse(settingtext);


            tbSettedTemp1.Text = joSetting["temp1"].ToString();
            tbSettedTemp2.Text = joSetting["temp2"].ToString();
            tbSettedTemp3.Text = joSetting["temp1"].ToString();
            tbSettedTemp4.Text = joSetting["temp2"].ToString();
            

        }

        public void setRoomInfo(RoomInfo ri)
        {
            this.ri = ri;
            this.lbHoinfo.Text = String.Format("{0}호 전체제어", ri.HouseID);
            this.lbRoomInfo.Text = String.Format("{0}호 {1} 제어", ri.HouseID,ri.Name);

            this.nid = int.Parse(ri.NID);
            this.masterid = int.Parse(ri.MasterID);
            this.id = int.Parse(ri.Id);
        }

        // 호의 전체 정보도 가지고 가야 된다. 


        // 난방 설정
        private void btnSave1_Click(object sender, EventArgs e)
        {
            //string msg = "send test";
            if (rbPowerOn.Checked)
            {
                //sendCommand.Invoke(cbframe.requestSetRoomPower(nid, masterid, id, 1));
                sendNewCommand.Invoke(ri, "power", 1);
            }
            else
            {
//                sendCommand.Invoke(cbframe.requestSetRoomPower(nid, masterid, id, 0));
                sendNewCommand.Invoke(ri, "power", 0);
            }
            timer1.Start();
          
            setButtonEnable(false);
            
          
        }


        // 외출 설정
        private void btnSave2_Click(object sender, EventArgs e)
        {

            if (rbOutOn.Checked)
            {
               // sendCommand.Invoke(cbframe.requestSetRoomOut(nid, masterid, id, 1));
                sendNewCommand.Invoke(ri, "outing", 1);
            }
            else
            {
              //  sendCommand.Invoke(cbframe.requestSetRoomOut(nid, masterid, id, 0));
                sendNewCommand.Invoke(ri, "outing", 0);
            }
            timer1.Start();
            setButtonEnable(false);
            
        }

        //=================================================
        // 온도 설정
        private void btnSave3_Click(object sender, EventArgs e)
        {

            temp1 = int.Parse(tbTemp1.Text.ToString());
            if (temp1 < 4 || temp1 > 40)
            {
                MessageBox.Show("설정범위 4 ~ 40 이내 입니다.");
                return;
            }

            sendNewCommand.Invoke(ri, "settemp", temp1 * 100);

            //sendCommand.Invoke(cbframe.requestSetRoomTemp(nid, masterid, id, temp1*100));

            timer1.Start();
            setButtonEnable(false);
        }


        private void btnSettedCmd1_Click(object sender, EventArgs e)
        {
            int temp = int.Parse(tbSettedTemp1.Text.ToString());
           // sendCommand.Invoke(cbframe.requestSetRoomTemp(nid, masterid, id, temp * 100));
            sendNewCommand.Invoke(ri, "settemp", temp * 100);
            timer1.Start();
            setButtonEnable(false);
        }

        private void btnSettedCmd2_Click(object sender, EventArgs e)
        {
            int temp = int.Parse(tbSettedTemp2.Text.ToString());
            //sendCommand.Invoke(cbframe.requestSetRoomTemp(nid, masterid, id, temp * 100));
            sendNewCommand.Invoke(ri, "settemp", temp * 100);
            timer1.Start();
            setButtonEnable(false);
        }
        // ]]
        //=================================================

        // 현재상태 요청 
        // 기존 구조에서 완전히 변경한다. 
        // 해당 명령을 바로 주는 것에서 
        // 요청 사항 roominfo 를 주는 형태로 변경 필요 
        private void btnCurrent1_Click(object sender, EventArgs e)
        {
            sendNewCommand.Invoke(ri, "status", 0);

            //sendCommand.Invoke(cbframe.requestRoomInfo(nid, masterid, id));
            timer1.Start();
            setButtonEnable(false);
        }







        //======================================================================================
        private void btnSave4_Click(object sender, EventArgs e)
        {
            if (rbPowerOn2.Checked)
            {
                sendHouseCommand.Invoke(ri.HouseID, "power", 1);
            }
            else
            {
                sendHouseCommand.Invoke(ri.HouseID, "power", 0);
            }

            timer1.Start();
            setButtonEnable(false);
        }

        private void btnSave5_Click(object sender, EventArgs e)
        {

            if (rbOutOn2.Checked)
            {
                sendHouseCommand.Invoke(ri.HouseID, "outing", 1);
            }
            else
            {
                sendHouseCommand.Invoke(ri.HouseID, "outing", 0);
            }
            timer1.Start();
            setButtonEnable(false);
        }

        //=========================================================
        // 여기는 많이 변경 될거지만 일단 GUI 는 동일하게 구성해야 하니까 그냥 기존 code 에 추가 한다. 
        // 그룹의 온도 설정 버튼 
        private void btnSave6_Click(object sender, EventArgs e)
        {

            temp2 = int.Parse(tbTemp2.Text.ToString());

            if (temp2 < 4 || temp2 > 40)
            {
                MessageBox.Show("설정범위 4 ~ 40 이내 입니다.");
                return;
            }

            int temp = temp2 * 100;
            //sendHouseStatus.Invoke(ri.HouseID, "settemp", temp.ToString());
            sendHouseCommand.Invoke(ri.HouseID, "settemp", temp);
            timer1.Start();
            setButtonEnable(false);
        }

        private void btnSettedCmd3_Click(object sender, EventArgs e)
        {
            int temp = int.Parse(tbSettedTemp3.Text.ToString());
            temp = temp * 100;
            //sendHouseStatus.Invoke(ri.HouseID, "settemp", temp.ToString());
            sendHouseCommand.Invoke(ri.HouseID, "settemp", temp);
            timer1.Start();
            setButtonEnable(false);
        }

        private void btnSettedCmd4_Click(object sender, EventArgs e)
        {
            int temp = int.Parse(tbSettedTemp4.Text.ToString());
            temp = temp * 100;
            //sendHouseStatus.Invoke(ri.HouseID, "settemp", temp.ToString());
            sendHouseCommand.Invoke(ri.HouseID, "settemp", temp);
            timer1.Start();
            setButtonEnable(false);
        }
        // ]]
        //==============================================================


        private void btnCurrent2_Click(object sender, EventArgs e)
        {
            // 하우스 정보를 여기서 던저야 한다. 
            
            sendHouseCommand.Invoke(ri.HouseID, "status", 0);
            timer1.Start();
            setButtonEnable(false);
           
        }

        //======================================================================================

        private void RemoteControlForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            setButtonEnable(true);

        }

        public void setButtonEnable(bool on)
        {
            if (this.lbRun.InvokeRequired)
            {
                this.Invoke(new updateEnableButtonCallBack(setButtonEnable), on);
            }
            else
            {
                lbRun.Visible = !on;
                lbRun2.Visible = !on;

                // 개별 command 에 있는 버튼들
                btnSave1.Enabled = on;
                btnSave2.Enabled = on;
                btnSave3.Enabled = on;
                btnCurrent1.Enabled = on;
                btnSettedCmd1.Enabled = on;
                btnSettedCmd2.Enabled = on;


                // 그룹에 있는 버튼들 
                btnSave4.Enabled = on;
                btnSave5.Enabled = on;
                btnSave6.Enabled = on;
                btnCurrent2.Enabled = on;

                btnSettedCmd3.Enabled = on;
                btnSettedCmd4.Enabled = on;



            }

            


        }

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

        private void btnup2_Click(object sender, EventArgs e)
        {
            temp2 = int.Parse(tbTemp2.Text.ToString());
            temp2++;
            if (temp2 >40) temp2 = 40;
            tbTemp2.Text = temp2.ToString();
        }

        private void btndown2_Click(object sender, EventArgs e)
        {
            temp2 = int.Parse(tbTemp2.Text.ToString());
            temp2--;
            if (temp2 < 5) temp2 = 4;
            tbTemp2.Text = temp2.ToString();
        }


        //============================================================



        

       


    }
}
