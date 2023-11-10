using HouseControl.Common;
using HouseControl.model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HouseControl
{
    public partial class Form1 : Form
    {
        // toolStripStatusLabel2
        internal delegate void toolStripStatusLabel2CallBack(string msg);
        internal delegate void toolStripStatusLabel3CallBack(string msg);

        FormSetting frmsetting;

        SerialSelectForm sf;
        ConsoleForm cf;
        FormTEST ft;

        FormWindHouse fwh01;
        FormWindHouse fwh02;
        FormWindHouse fwh03;
        FormWindHouse fwh04;

        FormValleryE formValleryE;

        RemoteControlForm rcf;

        // ABC 1 2 3
        Dictionary<string, RoomInfo> dic01 = new Dictionary<string, RoomInfo>();
        // D 4
        Dictionary<string, RoomInfo> dic02 = new Dictionary<string, RoomInfo>();
        // E 5
        Dictionary<string, RoomInfo> dic03 = new Dictionary<string, RoomInfo>();
        // F 6
        Dictionary<string, RoomInfo> dic04 = new Dictionary<string, RoomInfo>();


        // 마스터랑 하우스의 모든 정보를 가지고 있어야 전체를 조회하는 서비스를 할수 있다. 

        Dictionary<string, HouseInfo> dichouse = new Dictionary<string, HouseInfo>();


        ArrayList arrSchedule = new ArrayList();


        // queue 에 넣자.
        // 실제로 uart로 나가는 형태로 하자. 
        ConcurrentQueue<JobItem> queue = new ConcurrentQueue<JobItem>();
        ConcurrentQueue<JobItem> priorityqueue = new ConcurrentQueue<JobItem>();


        ConcurrentQueue<JobItem> queue2 = new ConcurrentQueue<JobItem>();
        ConcurrentQueue<JobItem> priorityqueue2 = new ConcurrentQueue<JobItem>();


        int previousJobIndex = -1;
        int currentJobIndex = 0;

        ArrayList houseSchedule = new ArrayList();
        int houseJobIndex = 0;
        string houseJobCmd = "";
        string houseJobCmdValue = "";
       

        string[] ports = new string[100];

        string COMPORT;
        string COMPORT2;

        JObject joSetting;


        int tbd1sendcount = 0;

        CbFrame cbframe;

        

        string previousStatus = "idle";
        string currentStatus = "idle";
        int presettingindex = 0;
        string[] arrpresetting = { "$Tbd2c=120:0\n", "$Tbd2c=120:0\n", "$Tbd1=\n", "$Tbd1=\n", "$Tbd2=9000\n", "$Tbd2=9010\n", "$Trdpc=119:9000:2\n", "$Trdpc=119:9010:2\n" };

        // 이동만
        int presettingindex2 = 0;
        string[] arrpresetting2 = { "$Tbd2c=120:0\n", "$Tbd2c=120:0\n", "$Tbd1=\n", "$Tbd1=\n", "$Tbd2=9000\n", "$Tbd2=9010\n", "$Trdpc=119:9000:2\n", "$Trdpc=119:9010:2\n" };

        // "$Tbd2c=120:0\n"
        // "$Tbd1=\n"
        // "$Tbd2=9000\n"
        // "$Tbd2=9010\n"
        // "$Trdpc=119:9000:2\n"
        // "$Trdpc=119:9010:2\n"

        //=======================================
        public string dfolderpath = "";
        public string dhomename = "TheStarHue";
        public string dhomename_config = "config";
        public string dhomename_data = "data";
        public string dhomename_event = "event";
        public string datasavefolderpath = "";
        public string todaypath = "";
        //=======================================



        public void updatetoolStripStatusLabel2(string msg)
        {

            if (this.statusStrip1.InvokeRequired)
            {
                this.Invoke(new toolStripStatusLabel2CallBack(updatetoolStripStatusLabel2), msg);
            }
            else
            {
                if (msg.Length > 0)
                {
                  //  Thread t1 = new Thread(new ThreadStart(deleteupdatetoolStripStatusLabel2));
                  //  t1.Start();

                }

                toolStripStatusLabel2.Text = msg.Replace("\n", "");
            }
        }

        private void deleteupdatetoolStripStatusLabel2()
        {
            Thread.Sleep(2000);
            updatetoolStripStatusLabel2("");
        }

        public void updatetoolStripStatusLabel3(string msg)
        {

            if (this.statusStrip1.InvokeRequired)
            {
                this.Invoke(new toolStripStatusLabel3CallBack(updatetoolStripStatusLabel3), msg);
            }
            else
            {
                toolStripStatusLabel3.Text = msg;
            }
        }


        private void firstSetting()
        {
           // myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string dpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            dfolderpath = dpath + "\\" + dhomename;

            if (!Directory.Exists(dfolderpath))
            {
                Directory.CreateDirectory(dfolderpath);
            }
            if (!Directory.Exists(dfolderpath + "\\" + dhomename_config))
            {
                Directory.CreateDirectory(dfolderpath + "\\" + dhomename_config);
            }

            datasavefolderpath = dfolderpath + "\\" + dhomename_data;
            if (!Directory.Exists(datasavefolderpath))
            {
                Directory.CreateDirectory(datasavefolderpath);
            }
            todaypath = datasavefolderpath + "\\" + DateTime.Now.ToString("yyyyMMdd");
            if (!Directory.Exists(todaypath))
            {
                Directory.CreateDirectory(todaypath);
            }

            if (!Directory.Exists(dfolderpath + "\\" + dhomename_event))
            {
                Directory.CreateDirectory(dfolderpath + "\\" + dhomename_event);
            }

            //Console.WriteLine(DateTime.Now.ToString("yyyyMMddHHmmss"));

        }

        //==============================================================================

        public Form1()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;

            firstSetting();


            string configpath = dfolderpath + "\\" + dhomename_config+"\\";
            dic01 = initLoadDeviceSetting(configpath+"Book1.csv");
            dic02 = initLoadDeviceSetting(configpath + "Book2.csv");
            dic03 = initLoadDeviceSetting(configpath + "Book3.csv");
            dic04 = initLoadDeviceSetting(configpath + "Book4.csv");

            // 한바퀴 다 돌고 현재 상태를 저장한다. 
            // timeout 에 있는 놈인지 확인해야 한다. 
//            string output = JsonConvert.SerializeObject(dic01);
//            System.IO.File.WriteAllText(dfolderpath + "\\" + dhomename_data+"\\"+"test" + ".json", output);


            //string output = JsonConvert.SerializeObject(dic04);
            //Console.Write(output);

            // 스케줄로 돌아가는 dic를 만든다. 
            foreach (var room in dic01)
            {
                arrSchedule.Add(room.Value);
            }

            foreach (var room in dic02)
            {
                arrSchedule.Add(room.Value);
            }

            foreach (var room in dic03)
            {
                arrSchedule.Add(room.Value);
            }

            foreach (var room in dic04)
            {
                arrSchedule.Add(room.Value);
            }

            //=========================================
//            Console.WriteLine("보일러 콘트롤 갯수 : {0}", dicmaster.Count);

    //        Console.WriteLine("총 분양 세대 :: {0}", dichouse.Count);
            dichouse = initLoadHouseSetting(configpath + "housesetting.csv");

            cbframe = new CbFrame();
           
            //Console.WriteLine("xxxx :: "+dic01.Count);
            //=======================================================

           // 

            //=======================================================

            int i = 0;
            foreach (string comport in SerialPort.GetPortNames())
            {
                ports[i++]=comport;
            }

            
            if(i>0)
            {
                serialPort1.PortName = ports[0];
                serialPort2.PortName = ports[0];
            }

            serialPort1.BaudRate = 115200;
            serialPort1.DataBits = (int)8;
            serialPort1.Parity = Parity.None;
            serialPort1.StopBits = StopBits.One;

            serialPort2.BaudRate = 115200;
            serialPort2.DataBits = (int)8;
            serialPort2.Parity = Parity.None;
            serialPort2.StopBits = StopBits.One;


            string settingtext = System.IO.File.ReadAllText(configpath+"setting.json");

          //  Console.WriteLine("{0}", settingtext);
            joSetting = JObject.Parse(settingtext);
            //Console.WriteLine("{0}", joSetting["com"]);
            COMPORT = joSetting["com"].ToString();
            COMPORT2 = joSetting["com2"].ToString();

            if (!COMPORT.Equals(""))
            {
                serialPort1.PortName = COMPORT;
            }
            if (!COMPORT2.Equals(""))
            {
                serialPort2.PortName = COMPORT2;
            }

            try
            {

                serialPort1.Open();
                serialPort2.Open();
            }
            catch (IOException ioe)
            {
              
            }

            

            //=======================================================

            frmsetting = new FormSetting();
            

            sf = new SerialSelectForm(ports);
            sf.SerialConnectionAction += SerialConnectionAction;

            cf = new ConsoleForm();
            cf.sendCommand += sendCommand;
            

            ft = new FormTEST();
            ft.sendCommand += ft_sendCommand;


            rcf = new RemoteControlForm();
            // rcf.sendCommand += rcf_sendCommand;  // 직접 명령어 형태로는 주지 않는다. 
            rcf.sendNewCommand += rcf_sendNewCommand;
            rcf.sendHouseCommand += rcf_sendHouseCommand;

            //=======================================================
            

            // broadcasting command 
           // sendCommand("$Tbd1=");

          
           
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // json 에서 deserializeObject 하기 
            //string text = System.IO.File.ReadAllText(todaypath + "\\" + "dic01_" + ".json");
            //JObject jo = JObject.Parse(text);
            //dic01 = JsonConvert.DeserializeObject<Dictionary<string, RoomInfo>>(text);

            fwh01 = new FormWindHouse("윈드하우스 (A,B,C)", dic01);
            fwh01.TopLevel = false;
            fwh01.Dock = DockStyle.Fill;
            panel1.Controls.Add(fwh01);
            fwh01.sendMainCommand += sendMainCommand;
            fwh01.openRemoteControlForm += fwh_openRemoteControlForm;
            fwh01.Show();

            fwh02 = new FormWindHouse("브릿지 하우스 (D)", dic02);
            fwh02.TopLevel = false;
            fwh02.Dock = DockStyle.Fill;
            panel2.Controls.Add(fwh02);
            fwh02.sendMainCommand += sendMainCommand;
            fwh02.openRemoteControlForm += fwh_openRemoteControlForm;
            fwh02.Show();

            fwh03 = new FormWindHouse("밸리 하우스 (E)", dic03);
            fwh03.TopLevel = false;
            fwh03.Dock = DockStyle.Fill;
            panel3.Controls.Add(fwh03);
            fwh03.sendMainCommand += sendMainCommand;
            fwh03.openRemoteControlForm += fwh_openRemoteControlForm;
            fwh03.Show();

            fwh04 = new FormWindHouse("밸리 하우스 (F)", dic04);
            fwh04.TopLevel = false;
            fwh04.Dock = DockStyle.Fill;
            panel4.Controls.Add(fwh04);
            fwh04.sendMainCommand += sendMainCommand;
            fwh04.openRemoteControlForm += fwh_openRemoteControlForm;
            fwh04.Show();

            // 그룹별 ( 동별, 층별 )
            //===============================================
            formValleryE = new FormValleryE();
            formValleryE.TopLevel = false;
            formValleryE.Dock = DockStyle.Fill;
            panel5.Controls.Add(formValleryE);
            formValleryE.sendHouseCommand += rcf_sendHouseCommand;
            formValleryE.Show();
            //===============================================

            tabControl1.SelectedIndex = 1;
            tabControl1.SelectedIndex = 2;
            tabControl1.SelectedIndex = 3;
            tabControl1.SelectedIndex = 0;


            MakeJobQueue();
            MakeJobQueue2();
            // schedulejob 
            timer2.Start();
            timer2_2.Start();


            //previousStatus = "schedulejob";
            //currentStatus = "schedulejob";


            previousStatus = "idle";
            currentStatus = "presetting";


            currentJobIndex = 0;
            //schedulejobfunc(false);

            //
            panelOverlay.Visible = false;

        }

        //=====================================================
        // MakeQueue
        // 프로그램이 실행하면 한바퀴 돌아야 하는데 이에 들어가는 내용을 만들자.
        // 
        private void MakeJobQueue()
        { 
            // presetting 에서 해야 할일을 등록 
            // presetting 에 관련된 queue 를 만들자. 
            foreach (string str in arrpresetting)
            {
                //Console.WriteLine(str);
                //serialPort1.Write(Encoding.UTF8.GetBytes(text), 0, text.Length);
                JobItem ji = new JobItem();
                ji.CMD = Encoding.UTF8.GetBytes(str);
                ji.Label = str;
                ji.Delay = 5;
                queue.Enqueue(ji);
            }

            //Console.WriteLine("cnt :: {0}", queue.Count);

            // 스케줄로 돌아가는 명령어 리스트
            // 상태값을 읽는 놈으로 한다. 
            foreach (var room in dic01)
            {
                JobItem ji = new JobItem();
                int nid = int.Parse(room.Value.NID);
                int masterid = int.Parse(room.Value.MasterID);
                int roomid = int.Parse(room.Value.Id);
                ji.CMD = cbframe.requestRoomInfo(nid, masterid, roomid);
                ji.Label = String.Format("상태조회 : {0}동 {1}호 {2}", room.Value.Dong,room.Value.HouseID, room.Value.Name);
                ji.Delay = 6;
                queue.Enqueue(ji);
            }

            foreach (var room in dic02)
            {
                JobItem ji = new JobItem();
                int nid = int.Parse(room.Value.NID);
                int masterid = int.Parse(room.Value.MasterID);
                int roomid = int.Parse(room.Value.Id);
                ji.CMD = cbframe.requestRoomInfo(nid, masterid, roomid);
                ji.Label = String.Format("상태조회 : {0}동 {1}호 {2}", room.Value.Dong, room.Value.HouseID, room.Value.Name);
                ji.Delay = 6;
                queue.Enqueue(ji);
            }
            
            /**
            foreach (var room in dic03)
            {
                JobItem ji = new JobItem();
                int nid = int.Parse(room.Value.NID);
                int masterid = int.Parse(room.Value.MasterID);
                int roomid = int.Parse(room.Value.Id);
                ji.CMD = cbframe.requestRoomInfo(nid, masterid, roomid);
                ji.Label = String.Format("상태조회 : {0}동 {1}호 {2}", room.Value.Dong, room.Value.HouseID, room.Value.Name);
                ji.Delay = 6;
                queue.Enqueue(ji);
            }
            **/

            foreach (var room in dic04)
            {

                JobItem ji = new JobItem();
                int nid = int.Parse(room.Value.NID);
                int masterid = int.Parse(room.Value.MasterID);
                int roomid = int.Parse(room.Value.Id);
                ji.CMD = cbframe.requestRoomInfo(nid, masterid, roomid);
                ji.Label = String.Format("상태조회 : {0}동 {1}호 {2}", room.Value.Dong, room.Value.HouseID, room.Value.Name);
                ji.Delay = 6;
                queue.Enqueue(ji);
            }
        
        }


        //=====================================================
        // e동만 혼자 돌려야 한다.
        //
        private void MakeJobQueue2()
        {
            // presetting 에서 해야 할일을 등록 
            // presetting 에 관련된 queue 를 만들자. 
            foreach (string str in arrpresetting2)
            {
                //Console.WriteLine(str);
                //serialPort1.Write(Encoding.UTF8.GetBytes(text), 0, text.Length);
                JobItem ji = new JobItem();
                ji.CMD = Encoding.UTF8.GetBytes(str);
                ji.Label = str;
                ji.Delay = 5;
                queue2.Enqueue(ji);
            }

            //Console.WriteLine("cnt :: {0}", queue.Count);

            foreach (var room in dic03)
            {
                JobItem ji = new JobItem();
                int nid = int.Parse(room.Value.NID);
                int masterid = int.Parse(room.Value.MasterID);
                int roomid = int.Parse(room.Value.Id);
                ji.CMD = cbframe.requestRoomInfo(nid, masterid, roomid);
                ji.Label = String.Format("상태조회 : {0}동 {1}호 {2}", room.Value.Dong, room.Value.HouseID, room.Value.Name);
                ji.Delay = 6;
                queue2.Enqueue(ji);
            }
        }




        // 요구사항 1
        // 난방 OFF 에서 
        // 난방 온도를 설정하면 난방 ON 모드로 자동으로 변경되고 온도 설정으로 가도록 해 달라는 내용이구나 
        // 

        private void rcf_sendNewCommand(RoomInfo ri, string cmdtype, int cmdvalue)
        {
            // 현재상태를 먼저 확인해야 한다.
            // 동정보를 가지고 E동만 별도로 처리하도록 하자. 
            Console.WriteLine("rcf_sendNewCommand {0} 동  {1} {2}", ri.Dong, ri.Power, ri.Outing);
            // 바로 실행할수 있는 명령인지 아니면 추가로 작업이 필요한 명령인지 확인할 필요가 있다.

            int nid = int.Parse(ri.NID);
            int masterid = int.Parse(ri.MasterID);
            int roomid = int.Parse(ri.Id);

            if (cmdtype.Equals("settemp"))
            {
                if (ri.Power == false)
                { 
                    // 난방을 켜야 하고 
                    JobItem jipower = new JobItem();
                    jipower.CMD = cbframe.requestSetRoomPower(nid, masterid, roomid, 1);
                    jipower.Label = String.Format("난방설정 : {0}동 {1}호 {2}", ri.Dong, ri.HouseID, ri.Name);
                    jipower.Delay = 6;
                    if (ri.Dong.Equals("E"))
                    {
                        priorityqueue2.Enqueue(jipower);
                    }
                    else
                    {
                        priorityqueue.Enqueue(jipower);
                    }
                    
                }

                if (ri.Outing == true)
                { 
                    // 외출을 OFF 해야 하고
                    JobItem jiout = new JobItem();
                    jiout.CMD = cbframe.requestSetRoomOut(nid, masterid, roomid, 0);
                    jiout.Label = String.Format("외출설정 : {0}동 {1}호 {2}", ri.Dong, ri.HouseID, ri.Name);
                    jiout.Delay = 6;

                    if (ri.Dong.Equals("E"))
                    {
                        priorityqueue2.Enqueue(jiout);
                    }
                    else
                    {
                        priorityqueue.Enqueue(jiout);
                    }
                
                }

                // 온도설정을 해야 한다. 
                JobItem ji = new JobItem();
                ji.CMD = cbframe.requestSetRoomTemp(nid, masterid, roomid,cmdvalue);
                ji.Label = String.Format("온도설정 : {0}동 {1}호 {2}", ri.Dong, ri.HouseID, ri.Name);
                ji.Delay = 6;

                
                if (ri.Dong.Equals("E"))
                {
                    priorityqueue2.Enqueue(ji);
                }
                else
                {
                    priorityqueue.Enqueue(ji);
                }
            }
            // 상태 
            else if (cmdtype.Equals("status"))
            {
                JobItem ji = new JobItem();
                ji.CMD = cbframe.requestRoomInfo(nid, masterid, roomid);
                ji.Label = String.Format("상태조회 : {0}동 {1}호 {2}", ri.Dong, ri.HouseID, ri.Name);
                ji.Delay = 6;

                
                if (ri.Dong.Equals("E"))
                {
                    priorityqueue2.Enqueue(ji);
                }
                else
                {
                    priorityqueue.Enqueue(ji);
                }
            }
            // 난방
            else if (cmdtype.Equals("power"))
            {
                JobItem ji = new JobItem();
                ji.CMD = cbframe.requestSetRoomPower(nid, masterid, roomid, cmdvalue);
                ji.Label = String.Format("난방설정 : {0}동 {1}호 {2}", ri.Dong, ri.HouseID, ri.Name);
                ji.Delay = 6;

                if (ri.Dong.Equals("E"))
                {
                    priorityqueue2.Enqueue(ji);
                }
                else
                {
                    priorityqueue.Enqueue(ji);
                }
            }
            // 외출 
            else if (cmdtype.Equals("outing"))
            {
                JobItem ji = new JobItem();
                ji.CMD = cbframe.requestSetRoomOut(nid, masterid, roomid, cmdvalue);
                ji.Label = String.Format("외출설정 : {0}동 {1}호 {2}", ri.Dong, ri.HouseID, ri.Name);
                ji.Delay = 6;

                if (ri.Dong.Equals("E"))
                {
                    priorityqueue2.Enqueue(ji);
                }
                else
                {
                    priorityqueue.Enqueue(ji);
                }
            }


        }

        // ==============================================================
        // 집 전체를 명령하는 부분 
        // ==============================================================
        //
        private void rcf_sendHouseCommand(string houseid, string cmdtype, int cmdvalue)
        {
          // 기존에 arr를 이용하자. 

            foreach (var room in arrSchedule)
            {
                RoomInfo temproom = (RoomInfo)room;

                if (!dichouse.ContainsKey(houseid))
                {
                    if (temproom.HouseID.Equals(houseid))
                    {
                        rcf_sendNewCommand(temproom, cmdtype, cmdvalue);
                    }
                }
            }
        }


        // 테스트에서 만들어서 사용하는 거다. 
        // 일단 2개로 분리해서 처리하는것이 어떤건지 모르겟으니까 일단 보자 ??? 
        void ft_sendCommand(byte[] cmd)
        {
            previousStatus = currentStatus;
            currentStatus = "singlecmd";
            if (cmd[0] == 0x02)
            {
                int nid = cmd[1] * 256 + cmd[2];
                int temp = cmd[5];
                updatetoolStripStatusLabel2(String.Format("NID :: {0} X {1:X2}", nid,temp));
            }

            
            timer4.Start();

            if (serialPort1.IsOpen)
            {
                serialPort1.Write(cmd, 0, cmd.Length);
            }

            if (serialPort2.IsOpen)
            {
                serialPort2.Write(cmd, 0, cmd.Length);
            }

        }



        private void SerialConnectionAction(string port, string baud, string port2, string baud2)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }

            if (serialPort2.IsOpen)
            {
                serialPort2.Close();
            }

            try
            {

                serialPort1.PortName = port;
                serialPort1.BaudRate = int.Parse(baud);
                serialPort1.Open();

                serialPort2.PortName = port2;
                serialPort2.BaudRate = int.Parse(baud2);
                serialPort2.Open();

                joSetting["com"] = port;
                joSetting["com2"] = port2;

                string configpath = dfolderpath + "\\" + dhomename_config + "\\";
                System.IO.File.WriteAllText(configpath + "setting.json", joSetting.ToString());

            }
            catch (IOException ioe)
            {
                Console.WriteLine(ioe.ToString());

            }
        }


        // wkyoon two uart [[
        private void sendCommand(string text)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Write(Encoding.UTF8.GetBytes(text), 0, text.Length);
            }

            if (serialPort2.IsOpen)
            {
                serialPort2.Write(Encoding.UTF8.GetBytes(text), 0, text.Length);
            }
        }
        // wkyoon two uart ]]
       
        //=====================================================================
        

        private void fwh_openRemoteControlForm(RoomInfo rinfo)
        {
            rcf.setRoomInfo(rinfo);
            rcf.Show();
            rcf.Focus();
        }


        private void sendMainCommand(byte[] cmd)
        {
            if (serialPort1.IsOpen)
            {
                /**
                Console.WriteLine("ft_sendCommand {0} ", cmd.Length);
                for (int i = 0; i < cmd.Length; i++)
                {
                    Console.WriteLine("xxx {0:X2} ", cmd[i]);
                }
                 **/

                serialPort1.Write(cmd, 0, cmd.Length);
            }

            if (serialPort2.IsOpen)
            {
                /**
                Console.WriteLine("ft_sendCommand {0} ", cmd.Length);
                for (int i = 0; i < cmd.Length; i++)
                {
                    Console.WriteLine("xxx {0:X2} ", cmd[i]);
                }
                 **/

                serialPort2.Write(cmd, 0, cmd.Length);
            }

        }

        private void sendByteCommand(byte[] cmd,int port)
        {
            /**
            Console.WriteLine("");
            for (int i = 0; i < cmd.Length; i++)
            {
                Console.Write("{0:X2} ", cmd[i]);
            }
            Console.WriteLine("");
            **/

            if (port == 1)
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Write(cmd, 0, cmd.Length);
                }
            }
            else if (port == 2)
            {
                if (serialPort2.IsOpen)
                {
                    serialPort2.Write(cmd, 0, cmd.Length);
                }
            }
            
            
        }

        //==========================================================================
        // cvs 파일에서 마스터의 정보를 가져오는 함수
        // 사용하는 이유 
        // 신규 마스터는 한번에 모든 방 정보를 가져오는데 기존것은 개별로 가져와야 해서 
        // 어떤 놈이 신규고 어떤놈이 이전 놈인지 알아야 해서 필요한 설정 값이다. 
        //==========================================================================

        public Dictionary<string, HouseInfo> initLoadHouseSetting(string filename)
        {
            Dictionary<string, HouseInfo> dic = new Dictionary<string, HouseInfo>();
            Encoding encode = System.Text.Encoding.GetEncoding("ks_c_5601-1987");

            // 100,100호,1
            using (var reader = new StreamReader(filename, encode))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    Console.WriteLine(values[0] + " " + values[1] + " " + values[2]);
                    string houseid = values[0];
                    string name = values[1];
                    string type = values[2];


                    HouseInfo hi = new HouseInfo();
                    hi.Id = houseid;
                    hi.Name = name;
                    hi.Type = type;

                    if (!dic.ContainsKey(hi.Id))
                    {
                        dic.Add(hi.Id, hi);
                    }

                }


            }

            return dic;

        }


        //==========================================================================
        // cvs 파일에서 룸에 대한 정보를 가져오는 함수 
        //==========================================================================

        public Dictionary<string, RoomInfo> initLoadDeviceSetting(string filename)
        {
            Dictionary<string, RoomInfo> dic = new Dictionary<string, RoomInfo>();
            Encoding encode = System.Text.Encoding.GetEncoding("ks_c_5601-1987");
            using (var reader = new StreamReader(filename, encode))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    //Console.WriteLine(values[0] + " " + values[2] + " " + values[4]);
                    string houseid = values[0];
                    string dong = values[1];
                    string nid = values[2];
                    string masterid = values[3];
                    string roomid = values[5];

                    // houseid 는 필요없다.
                    // 통신상에서 받는 거는
                    // nid masterid roomid 밖에 없어서 key 도 동일하게 받도록 한다. 
                    string key = String.Format("{0}-{1}-{2}", nid, masterid, roomid);

                    string jsonfile = dfolderpath + "\\" + dhomename_data+"\\"+key+".json";

                    RoomInfo ri = new RoomInfo();
                    ri.Key = key;
                    ri.HouseID = houseid;
                    ri.Dong = dong;
                    ri.NID = nid;
                    ri.MasterID = masterid;
                    ri.Id = roomid;
                    ri.Name = values[6];

                    // 기존 데이터 업데이트 하는 부분
                    // 일괄로 처리하는것으로 변경 필요 
                    if(true)
                    {
                        if (File.Exists(jsonfile))
                        {
                            string text = System.IO.File.ReadAllText(dfolderpath + "\\" + dhomename_data + "\\" + key + ".json");
                            JObject jo = JObject.Parse(text);
                            //joSetting["com"].ToString();
                            // Console.WriteLine("exit :: " + text);
                            // Console.WriteLine("TInfo :: " + jo["TInfo"].ToString());

                            ri.Power = bool.Parse(jo["Power"].ToString());
                            ri.Outing = bool.Parse(jo["Outing"].ToString());
                            ri.CurTemp = jo["CurTemp"].ToString();
                            ri.SetTemp = jo["SetTemp"].ToString();
                            ri.TInfo = jo["TInfo"].ToString();
                            ri.DESC = jo["DESC"].ToString();
                        }
                    }
                    
                   // else
                   // {
                   //     Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxx no xxxxxxxxxxxxxxxxxxxxxxxxxx");
                  //  }

                    if (!dic.ContainsKey(key))
                    {
                        dic.Add(key, ri);
                    }

                }

                
            }

            return dic;
        
        }

        //================================================================================
        // 메뉴의 toolstrip 부분 
        //================================================================================

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void DeviceConnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sf.Show();
            sf.Focus();
        }


        private void DebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cf.isShow = true;
            cf.Show();
            cf.Focus();
        }

        private void tESTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ft.Show();
            ft.Focus();
        }

        // 장치 설정 관련 
        private void SettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmsetting.Show();
            frmsetting.Focus();
        }

        //================================================================================
        // uart received data 
        //================================================================================
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            int receivedCnt = sp.BytesToRead;
            if (receivedCnt > 0)
            {
                byte[] buffer = new byte[receivedCnt];
                sp.Read(buffer, 0, buffer.Length);

               // Console.WriteLine("=== {0}",buffer[0]);

                if (buffer[0] == 0x02)
                {
                    RoomInfo tempri = new RoomInfo();
                    int nid = (int)(buffer[1] << 8 | buffer[2]);
                   // Console.WriteLine("nid :: {0:X4}",nid);
                    //int len = buffer[3];
                    //Console.WriteLine(len);
                    int masterid = (int)((buffer[5] & 0xF0) >> 4);
                    int roomid = (int)(buffer[5] & 0x0F);

                   // Console.WriteLine("마스터 아이디 {0} 룸아이디 {1}", masterid, roomid);
                    string key = String.Format("{0}-{1}-{2}", nid, masterid, roomid);


                    tempri.Key = key;
                    tempri.NID = nid.ToString();
                    tempri.MasterID = masterid.ToString();
                    tempri.Id = roomid.ToString();

                    //Console.WriteLine("rec key :: " + key);

        //            5211-1-1
//5211-1-2
//5211-1-3

                    // ABC D E F 동으로 되어 있고 숫자 123 4 5 6 으로 시작 첫번째로 구분한다. 
                    string dongvalue = nid.ToString().Substring(0, 1);

                  //  Console.WriteLine("rec nid :: {0}", nid.ToString().Substring(0,1));

                    




                    // 마스터 특성 요구 응답
                    if (buffer[6] == 0x8f)
                    {

                        // 정상 응답
                        if (buffer[8] == 0x00)
                        {
                            // 9 회사명
                            // 10 제어방식 ( 0x02 온도조절기 )
                            // 11 설정치 상한값 (BCD)
                            // 12 설정치 하한값 (BCD)
                            // 13 온도 조절기 갯수 
                        }
                        // 비정상 응답 error
                        else
                        {

                        }

                    }
                    else
                    {

                    

                        // 온도조절기 상태요구 응답
                        if (buffer[6] == 0x81)
                        {

                        }
                        // 난방 응답
                        else if (buffer[6] == 0xc3)
                        {

                        }
                        // 설정 온도 응답
                        else if (buffer[6] == 0xc4)
                        {

                        }
                        // 외출 응답
                        else if (buffer[6] == 0xc5)
                        {

                        }

                        Console.WriteLine("rec buffer 8 === {0:X2}", buffer[8]);

                        // 정상 응답
                        if (buffer[8] == 0x00)
                        {
                            // 9  상태의 값 상위 비트는 reserved 하위 비트만 사용 예약 온수 외출모드 난방 
                            int outing = (int)((buffer[9] & 0x02)>>1);
                            int power = (int)(buffer[9] & 0x01);

                            //Console.WriteLine("outing  {0} power {1}", outing, power);

                            if(outing==1)
                                tempri.Outing = true;
                            else
                                tempri.Outing = false;

                            if(power==1)
                                tempri.Power = true;
                            else
                                tempri.Power = false;


                            // 10 설정된 온도 (BCD)
                            // 11 소수점 
                            //Console.WriteLine("설정온도 {0:X2}", buffer[10]);
                            //Console.WriteLine("설정온도 {0:X2}", buffer[11]);
                            tempri.SetTemp = String.Format("{0:X2}.{1:X2}", buffer[10], buffer[11]);
                            // 12 현재온도 (BCD)
                            // 13 소수점
                           // Console.WriteLine("현재온도 {0:X2}", buffer[12]);
                           // Console.WriteLine("현재온도 {0:X2}", buffer[13]);
                            tempri.CurTemp = String.Format("{0:X2}.{1:X2}", buffer[12], buffer[13]);
                            // 14 예약 00 으로 들어옴 

                            //시간 정보도 입력할수 있도록 필드 추가 필요 
                            tempri.TInfo = DateTime.Now.ToString();
                            //Console.WriteLine(DateTime.Now.ToString("yyyyMMddHHmmss"));
                            tempri.DESC = "정상응답";
                            
                            //toolStripStatusLabel3.Text = String.Format("응답 : NID {0} 마스터 {1} 룸 {2} {3} {4}", tempri.NID, tempri.MasterID, tempri.Id, tempri.DESC, tempri.CurTemp);
                            // 다음을 실행한다. 
                            /*
                            currentJobIndex++;
                            if (currentJobIndex == arrSchedule.Count)
                            {
                                currentJobIndex = 0;
                            }

                            RoomInfo rinfo = (RoomInfo)arrSchedule[currentJobIndex];
                            int next_nid = int.Parse(rinfo.NID);
                            int next_masterid = int.Parse(rinfo.MasterID);
                            int next_roomid = int.Parse(rinfo.Id);

                            sendMainCommand(cbframe.requestRoomInfo(next_nid, next_masterid, next_roomid));
                            */
                            

                        }
                        // 비정상 응답 error
                        else
                        {

                        }
                    }

                    // 여기서 몇 동인지 확인한다. 
                    if (dongvalue.Equals("6"))
                    {
                        if (dic04.ContainsKey(key))
                        {
                            //tempri.Name = dic04[key].Name;
                            //tempri.HouseID = dic04[key].HouseID;
                            dic04[key].Power = tempri.Power;
                            dic04[key].Outing = tempri.Outing;
                            dic04[key].CurTemp = tempri.CurTemp;
                            dic04[key].SetTemp = tempri.SetTemp;
                            dic04[key].TInfo = tempri.TInfo;
                            dic04[key].DESC = tempri.DESC;
                            fwh04.updateDicNGridView(dic04[key]);

                            updatetoolStripStatusLabel3(String.Format("응답 : {0}동 {1}호 {2} 현재온도 {3} 설정온도 {4} ", dic04[key].Dong, dic04[key].HouseID, dic04[key].Name, dic04[key].CurTemp, dic04[key].SetTemp));
                        }
                        else
                        {
                           // Console.WriteLine("존재하지 않는 key 값 입니다. ");
                            cf.appendLog("존재하지 않는 key 값 입니다.\n");
                        }
                    }
                    else if (dongvalue.Equals("5"))
                    {
                        if (dic03.ContainsKey(key))
                        {
                            //tempri.Name = dic03[key].Name;
                            //tempri.HouseID = dic03[key].HouseID;
                            dic03[key].Power = tempri.Power;
                            dic03[key].Outing = tempri.Outing;
                            dic03[key].CurTemp = tempri.CurTemp;
                            dic03[key].SetTemp = tempri.SetTemp;
                            dic03[key].TInfo = tempri.TInfo;
                            dic03[key].DESC = tempri.DESC;
                            fwh03.updateDicNGridView(dic03[key]);
                           // Console.WriteLine(" 5의 키의 값을 업데이트 한다. ");
                            updatetoolStripStatusLabel3(String.Format("응답 : {0}동 {1}호 {2} 현재온도 {3} 설정온도 {4} ", dic03[key].Dong, dic03[key].HouseID, dic03[key].Name, dic03[key].CurTemp, dic03[key].SetTemp));
                            
                        }
                        else
                        {
                            //Console.WriteLine("존재하지 않는 key 값 입니다. ");
                            cf.appendLog("존재하지 않는 key 값 입니다.\n");
                        }
                    }
                    else if (dongvalue.Equals("4"))
                    {
                        if (dic02.ContainsKey(key))
                        {
                            //tempri.Name = dic02[key].Name;
                            //tempri.HouseID = dic02[key].HouseID;
                            
                            dic02[key].Power = tempri.Power;
                            dic02[key].Outing = tempri.Outing;
                            dic02[key].CurTemp = tempri.CurTemp;
                            dic02[key].SetTemp = tempri.SetTemp;
                            dic02[key].TInfo = tempri.TInfo;
                            dic02[key].DESC = tempri.DESC;

                            fwh02.updateDicNGridView(dic02[key]);
                            updatetoolStripStatusLabel3(String.Format("응답 : {0}동 {1}호 {2} 현재온도 {3} 설정온도 {4} ", dic02[key].Dong, dic02[key].HouseID, dic02[key].Name, dic02[key].CurTemp, dic02[key].SetTemp));
                        }
                        else
                        {
                            //Console.WriteLine("존재하지 않는 key 값 입니다. ");
                            cf.appendLog("존재하지 않는 key 값 입니다.\n");
                        }
                    }
                    else
                    {
                        if (dic01.ContainsKey(key))
                        {
                            //tempri.Name = dic01[key].Name;
                            //tempri.HouseID = dic01[key].HouseID;
                            
                            dic01[key].Power = tempri.Power;
                            dic01[key].Outing = tempri.Outing;
                            dic01[key].CurTemp = tempri.CurTemp;
                            dic01[key].SetTemp = tempri.SetTemp;
                            dic01[key].TInfo = tempri.TInfo;
                            dic01[key].DESC = tempri.DESC;
                            fwh01.updateDicNGridView(dic01[key]);
                            updatetoolStripStatusLabel3(String.Format("응답 : {0}동 {1}호 {2} 현재온도 {3} 설정온도 {4} ", dic01[key].Dong, dic01[key].HouseID, dic01[key].Name, dic01[key].CurTemp, dic01[key].SetTemp));
                        }
                        else
                        {
                            //Console.WriteLine("존재하지 않는 key 값 입니다. ");
                            cf.appendLog("존재하지 않는 key 값 입니다.\n");
                        }
                    }

                    Console.WriteLine("rec data after  currentStatus == {0}", currentStatus);

                    if (currentStatus.Equals("schedulejob"))
                    {
                        if (!currentStatus.Equals(previousStatus))
                        {
                            currentJobIndex = previousJobIndex;
                        }
                     //   Thread t3 = new Thread(() => schedulejobfunc(true));
                     //   t3.Start();

                        //schedulejobfunc();
                     
                    }
                    else if (currentStatus.Equals("housejob"))
                    {
                        Console.WriteLine("rec after :: === housejob ===");
                        if (houseJobIndex == houseSchedule.Count)
                        {
                            timer3.Stop();
                            rcf.setButtonEnable(true);

                            currentStatus = previousStatus;

                            //Console.WriteLine("=> End timer3 Start Timer2 work schedule job ");
                            // cf.appendLog("=> End timer3 Start Timer2 work schedule job \n");
                            // timer2.Start();
                        }
                        else
                        {
                            housejob();
                            houseJobIndex++;
                        }

                    //    Thread t4 = new Thread(() => housejob());
                   //     t4.Start();


                       
                        //housejob();
                    }
                    // si
                    else if (currentStatus.Equals("singlecmd"))
                    {
                        timer4.Stop();
                        currentStatus = previousStatus;
                        previousStatus = "singlecmd";
                     
                    }

                    /*
                    // protocol message 
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        Console.WriteLine("rec :: {0:X2} {1} ", buffer[i], i);
                    }
                     */

                }
                else
                { 
                    // string 값의 출력 
                  
                   StringBuilder sb = new StringBuilder();

                   for (int i = 0; i < buffer.Length; i++)
                   {
                    //   Console.WriteLine("rec :: {0:X2} {1} ", buffer[i], buffer[i]);
                       char c = (char)buffer[i];
                       string s = c.ToString();
                       sb.Append(s);
                   }

                /**
                   if(sb.ToString().Contains("Ubup:RS:Seq:"))
                   {
 
                   }
                    **/
                 //  Console.WriteLine(sb.ToString());
                   cf.appendLog(sb.ToString()+"\n");

                   


               
                }

                


               

            }
        }



        private void serialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            int receivedCnt = sp.BytesToRead;
            if (receivedCnt > 0)
            {
                byte[] buffer = new byte[receivedCnt];
                sp.Read(buffer, 0, buffer.Length);

                // Console.WriteLine("=== {0}",buffer[0]);

                if (buffer[0] == 0x02)
                {
                    RoomInfo tempri = new RoomInfo();
                    int nid = (int)(buffer[1] << 8 | buffer[2]);
                    // Console.WriteLine("nid :: {0:X4}",nid);
                    //int len = buffer[3];
                    //Console.WriteLine(len);
                    int masterid = (int)((buffer[5] & 0xF0) >> 4);
                    int roomid = (int)(buffer[5] & 0x0F);

                    // Console.WriteLine("마스터 아이디 {0} 룸아이디 {1}", masterid, roomid);
                    string key = String.Format("{0}-{1}-{2}", nid, masterid, roomid);


                    tempri.Key = key;
                    tempri.NID = nid.ToString();
                    tempri.MasterID = masterid.ToString();
                    tempri.Id = roomid.ToString();

                    //Console.WriteLine("rec key :: " + key);

                    //            5211-1-1
                    //5211-1-2
                    //5211-1-3

                    // ABC D E F 동으로 되어 있고 숫자 123 4 5 6 으로 시작 첫번째로 구분한다. 
                    string dongvalue = nid.ToString().Substring(0, 1);

                    //  Console.WriteLine("rec nid :: {0}", nid.ToString().Substring(0,1));






                    // 마스터 특성 요구 응답
                    if (buffer[6] == 0x8f)
                    {

                        // 정상 응답
                        if (buffer[8] == 0x00)
                        {
                            // 9 회사명
                            // 10 제어방식 ( 0x02 온도조절기 )
                            // 11 설정치 상한값 (BCD)
                            // 12 설정치 하한값 (BCD)
                            // 13 온도 조절기 갯수 
                        }
                        // 비정상 응답 error
                        else
                        {

                        }

                    }
                    else
                    {



                        // 온도조절기 상태요구 응답
                        if (buffer[6] == 0x81)
                        {

                        }
                        // 난방 응답
                        else if (buffer[6] == 0xc3)
                        {

                        }
                        // 설정 온도 응답
                        else if (buffer[6] == 0xc4)
                        {

                        }
                        // 외출 응답
                        else if (buffer[6] == 0xc5)
                        {

                        }

                        Console.WriteLine("rec buffer 8 === {0:X2}", buffer[8]);

                        // 정상 응답
                        if (buffer[8] == 0x00)
                        {


                            // 9  상태의 값 상위 비트는 reserved 하위 비트만 사용 예약 온수 외출모드 난방 
                            int outing = (int)((buffer[9] & 0x02) >> 1);
                            int power = (int)(buffer[9] & 0x01);

                            //Console.WriteLine("outing  {0} power {1}", outing, power);

                            if (outing == 1)
                                tempri.Outing = true;
                            else
                                tempri.Outing = false;

                            if (power == 1)
                                tempri.Power = true;
                            else
                                tempri.Power = false;


                            // 10 설정된 온도 (BCD)
                            // 11 소수점 
                            //Console.WriteLine("설정온도 {0:X2}", buffer[10]);
                            //Console.WriteLine("설정온도 {0:X2}", buffer[11]);
                            tempri.SetTemp = String.Format("{0:X2}.{1:X2}", buffer[10], buffer[11]);
                            // 12 현재온도 (BCD)
                            // 13 소수점
                            // Console.WriteLine("현재온도 {0:X2}", buffer[12]);
                            // Console.WriteLine("현재온도 {0:X2}", buffer[13]);
                            tempri.CurTemp = String.Format("{0:X2}.{1:X2}", buffer[12], buffer[13]);
                            // 14 예약 00 으로 들어옴 

                            //시간 정보도 입력할수 있도록 필드 추가 필요 
                            tempri.TInfo = DateTime.Now.ToString();
                            //Console.WriteLine(DateTime.Now.ToString("yyyyMMddHHmmss"));
                            tempri.DESC = "정상응답";

                            //toolStripStatusLabel3.Text = String.Format("응답 : NID {0} 마스터 {1} 룸 {2} {3} {4}", tempri.NID, tempri.MasterID, tempri.Id, tempri.DESC, tempri.CurTemp);
                            // 다음을 실행한다. 
                            /*
                            currentJobIndex++;
                            if (currentJobIndex == arrSchedule.Count)
                            {
                                currentJobIndex = 0;
                            }

                            RoomInfo rinfo = (RoomInfo)arrSchedule[currentJobIndex];
                            int next_nid = int.Parse(rinfo.NID);
                            int next_masterid = int.Parse(rinfo.MasterID);
                            int next_roomid = int.Parse(rinfo.Id);

                            sendMainCommand(cbframe.requestRoomInfo(next_nid, next_masterid, next_roomid));
                            */


                        }
                        // 비정상 응답 error
                        else
                        {

                        }
                    }

                    // 여기서 몇 동인지 확인한다. 
                    if (dongvalue.Equals("6"))
                    {
                        if (dic04.ContainsKey(key))
                        {
                            //tempri.Name = dic04[key].Name;
                            //tempri.HouseID = dic04[key].HouseID;
                            dic04[key].Power = tempri.Power;
                            dic04[key].Outing = tempri.Outing;
                            dic04[key].CurTemp = tempri.CurTemp;
                            dic04[key].SetTemp = tempri.SetTemp;
                            dic04[key].TInfo = tempri.TInfo;
                            dic04[key].DESC = tempri.DESC;
                            fwh04.updateDicNGridView(dic04[key]);

                            updatetoolStripStatusLabel3(String.Format("응답 : {0}동 {1}호 {2} 현재온도 {3} 설정온도 {4} ", dic04[key].Dong, dic04[key].HouseID, dic04[key].Name, dic04[key].CurTemp, dic04[key].SetTemp));
                        }
                        else
                        {
                            // Console.WriteLine("존재하지 않는 key 값 입니다. ");
                            cf.appendLog("존재하지 않는 key 값 입니다.\n");
                        }
                    }
                    else if (dongvalue.Equals("5"))
                    {
                        if (dic03.ContainsKey(key))
                        {
                            //tempri.Name = dic03[key].Name;
                            //tempri.HouseID = dic03[key].HouseID;
                            dic03[key].Power = tempri.Power;
                            dic03[key].Outing = tempri.Outing;
                            dic03[key].CurTemp = tempri.CurTemp;
                            dic03[key].SetTemp = tempri.SetTemp;
                            dic03[key].TInfo = tempri.TInfo;
                            dic03[key].DESC = tempri.DESC;
                            fwh03.updateDicNGridView(dic03[key]);
                            // Console.WriteLine(" 5의 키의 값을 업데이트 한다. ");
                            updatetoolStripStatusLabel3(String.Format("응답 : {0}동 {1}호 {2} 현재온도 {3} 설정온도 {4} ", dic03[key].Dong, dic03[key].HouseID, dic03[key].Name, dic03[key].CurTemp, dic03[key].SetTemp));

                        }
                        else
                        {
                            //Console.WriteLine("존재하지 않는 key 값 입니다. ");
                            cf.appendLog("존재하지 않는 key 값 입니다.\n");
                        }
                    }
                    else if (dongvalue.Equals("4"))
                    {
                        if (dic02.ContainsKey(key))
                        {
                            //tempri.Name = dic02[key].Name;
                            //tempri.HouseID = dic02[key].HouseID;

                            dic02[key].Power = tempri.Power;
                            dic02[key].Outing = tempri.Outing;
                            dic02[key].CurTemp = tempri.CurTemp;
                            dic02[key].SetTemp = tempri.SetTemp;
                            dic02[key].TInfo = tempri.TInfo;
                            dic02[key].DESC = tempri.DESC;

                            fwh02.updateDicNGridView(dic02[key]);
                            updatetoolStripStatusLabel3(String.Format("응답 : {0}동 {1}호 {2} 현재온도 {3} 설정온도 {4} ", dic02[key].Dong, dic02[key].HouseID, dic02[key].Name, dic02[key].CurTemp, dic02[key].SetTemp));
                        }
                        else
                        {
                            //Console.WriteLine("존재하지 않는 key 값 입니다. ");
                            cf.appendLog("존재하지 않는 key 값 입니다.\n");
                        }
                    }
                    else
                    {
                        if (dic01.ContainsKey(key))
                        {
                            //tempri.Name = dic01[key].Name;
                            //tempri.HouseID = dic01[key].HouseID;

                            dic01[key].Power = tempri.Power;
                            dic01[key].Outing = tempri.Outing;
                            dic01[key].CurTemp = tempri.CurTemp;
                            dic01[key].SetTemp = tempri.SetTemp;
                            dic01[key].TInfo = tempri.TInfo;
                            dic01[key].DESC = tempri.DESC;
                            fwh01.updateDicNGridView(dic01[key]);
                            updatetoolStripStatusLabel3(String.Format("응답 : {0}동 {1}호 {2} 현재온도 {3} 설정온도 {4} ", dic01[key].Dong, dic01[key].HouseID, dic01[key].Name, dic01[key].CurTemp, dic01[key].SetTemp));
                        }
                        else
                        {
                            //Console.WriteLine("존재하지 않는 key 값 입니다. ");
                            cf.appendLog("존재하지 않는 key 값 입니다.\n");
                        }
                    }

                    Console.WriteLine("rec data after  currentStatus == {0}", currentStatus);

                    if (currentStatus.Equals("schedulejob"))
                    {
                        if (!currentStatus.Equals(previousStatus))
                        {
                            currentJobIndex = previousJobIndex;
                        }
                    }
                    // timer3이랑 같은 놈을 하나 더 만들어야 한다. 
                    else if (currentStatus.Equals("housejob"))
                    {
                        Console.WriteLine("rec after :: === housejob ===");
                        if (houseJobIndex == houseSchedule.Count)
                        {
                            timer3.Stop();
                            rcf.setButtonEnable(true);

                            currentStatus = previousStatus;

                        }
                        else
                        {
                            housejob();
                            houseJobIndex++;
                        }

                        //    Thread t4 = new Thread(() => housejob());
                        //     t4.Start();



                        //housejob();
                    }
                    // si
                    else if (currentStatus.Equals("singlecmd"))
                    {
                        timer4.Stop();
                        currentStatus = previousStatus;
                        previousStatus = "singlecmd";

                    }

                    /*
                    // protocol message 
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        Console.WriteLine("rec :: {0:X2} {1} ", buffer[i], i);
                    }
                     */

                }
                else
                {
                    // string 값의 출력 

                    StringBuilder sb = new StringBuilder();

                    for (int i = 0; i < buffer.Length; i++)
                    {
                        //   Console.WriteLine("rec :: {0:X2} {1} ", buffer[i], buffer[i]);
                        char c = (char)buffer[i];
                        string s = c.ToString();
                        sb.Append(s);
                    }

                    /**
                       if(sb.ToString().Contains("Ubup:RS:Seq:"))
                       {
 
                       }
                        **/
                    //  Console.WriteLine(sb.ToString());
                    cf.appendLog(sb.ToString() + "\n");





                }






            }
        }
        //================================================================================
        // uart received data ]]
        //================================================================================

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Console.WriteLine("SelectedIndex :: {0}", tabControl1.SelectedIndex);
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = String.Format("{0}", DateTime.Now.ToString());

            todaypath = datasavefolderpath + "\\" + DateTime.Now.ToString("yyyyMMdd");
            if (!Directory.Exists(todaypath))
            {
                Directory.CreateDirectory(todaypath);
            }

            //    String.Format("{0}시 {1:0#}분 {2}초",
            //    DateTime.Now.Hour, DateTime.Now.Minute,
            //    DateTime.Now.Second.ToString().PadLeft(2, '0'));
            /**
            if (serialPort1.IsOpen)
            {
                toolStripStatusLabel2.ForeColor = Color.Black;
                toolStripStatusLabel2.Text = "";

                if (tbd1sendcount == 0)
                {
                    sendCommand("$Tbd1=\n");
                    
                }
                tbd1sendcount++;

                if (tbd1sendcount > 3600)
                {
                    tbd1sendcount = 0;
                }
            }
            else
            {
                toolStripStatusLabel2.ForeColor = Color.Red;
                toolStripStatusLabel2.Text = "현재 장비가 연결되어 있지 않습니다.";
                tbd1sendcount = 0;
            }
            **/

        }

        
        // 여기를 고쳐야 한다.
        // 고정 시간으로 명령을 주는것이 좋아 보인다. 
        // 3초 이내로 명령을 주면 안들어 오는 놈들이 있다.
        // 파일로 기록하는 것도 전체를 한번 돌고 저정하는 형태로 변경하자.
        private void timer2_Tick(object sender, EventArgs e)
        {
            //Console.WriteLine("timer2_Tick timer2_Tick {0}   {1}  {2}", DateTime.Now.ToString("yyyyMMddHHmmss"), currentJobIndex, currentStatus);
            if (priorityqueue.Count > 0)
            {
                JobItem ji = new JobItem();
                priorityqueue.TryDequeue(out ji);
                
                sendByteCommand(ji.CMD,1);

                updatetoolStripStatusLabel2(String.Format("우선 요청 : {0}", ji.Label));
                cf.appendLog(String.Format("우선 요청 : {0}", ji.Label));
            
            }
            else if (queue.Count > 0)
            {
                JobItem ji = new JobItem();
                queue.TryDequeue(out ji);

                sendByteCommand(ji.CMD, 1);

                updatetoolStripStatusLabel2(String.Format("요청 : {0}",ji.Label));
                cf.appendLog(String.Format("요청 : {0}", ji.Label));

            }
            else
            {
                //timer2.Stop();

                //Console.WriteLine("timer2 queue empty end end end ");
                updatetoolStripStatusLabel2("한번 다 돌았군요. 다시 돌아 볼까요. ");
                cf.appendLog("한번 다 돌았군요. 다시 돌아 볼까요. ");


                string dic01_out = JsonConvert.SerializeObject(dic01);
                System.IO.File.WriteAllText(todaypath + "\\" + "dic01_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".json", dic01_out);

                string dic02_out = JsonConvert.SerializeObject(dic01);
                System.IO.File.WriteAllText(todaypath + "\\" + "dic02_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".json", dic02_out);

                // 이동만 [[
                //string dic03_out = JsonConvert.SerializeObject(dic01);
                //System.IO.File.WriteAllText(todaypath + "\\" + "dic03_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".json", dic03_out);
                // 이동만 ]]

                string dic04_out = JsonConvert.SerializeObject(dic01);
                System.IO.File.WriteAllText(todaypath + "\\" + "dic04_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".json", dic04_out);

                MakeJobQueue();
            }
        }

        private void timer2_2_Tick(object sender, EventArgs e)
        {
            if (priorityqueue2.Count > 0)
            {
                JobItem ji = new JobItem();
                priorityqueue2.TryDequeue(out ji);

                sendByteCommand(ji.CMD, 2);

                updatetoolStripStatusLabel3(String.Format("우선 요청 : {0}", ji.Label));
                cf.appendLog(String.Format("우선 요청 : {0}", ji.Label));

            }
            else if (queue.Count > 0)
            {
                JobItem ji = new JobItem();
                queue2.TryDequeue(out ji);

                sendByteCommand(ji.CMD, 2);

                updatetoolStripStatusLabel3(String.Format("요청 : {0}", ji.Label));
                cf.appendLog(String.Format("요청 : {0}", ji.Label));

            }
            else
            {
                updatetoolStripStatusLabel3("한번 다 돌았군요. 다시 돌아 볼까요. ");
                cf.appendLog("한번 다 돌았군요. 다시 돌아 볼까요. ");

                string dic03_out = JsonConvert.SerializeObject(dic01);
                System.IO.File.WriteAllText(todaypath + "\\" + "dic03_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".json", dic03_out);

                MakeJobQueue2();

            }
        }

        // 이동만 ]]



        private void schedulejobfunc(bool isCallRecv)
        {

          //  Console.WriteLine("timer2_Tick timer2_Tick 3333333");
            /** 
             if (previousJobIndex > -1)
             {
                 RoomInfo prerinfo = (RoomInfo)arrSchedule[previousJobIndex];
                 if (isCallRecv)
                 {
                   //  Console.WriteLine("timer2_Tick timer2_Tick 44444444444");
                 }
                 else
                 {
                    // Console.WriteLine("timer2_Tick timer2_Tick 55555555555");
                    
                     /**
                     prerinfo.DESC = "통신응답 없음";
                     toolStripStatusLabel3.Text = String.Format("응답 : {0}호 마스터 {1} 룸 {2} {3}", prerinfo.HouseID, prerinfo.MasterID, prerinfo.Id, prerinfo.DESC);
                     prerinfo.TInfo = DateTime.Now;
                     string dongvalue = prerinfo.NID.Substring(0, 1);

                     if (dongvalue.Equals("6"))
                     {
                         fwh04.updateDicNGridView(prerinfo);
                     }
                     else if (dongvalue.Equals("5"))
                     {
                         fwh03.updateDicNGridView(prerinfo);
                     }
                     else if (dongvalue.Equals("4"))
                     {
                         fwh02.updateDicNGridView(prerinfo);
                     }
                     else
                     {
                         fwh01.updateDicNGridView(prerinfo);
                     }
            
                 }
             }
                      **/

            //Thread.Sleep(1000);
            if (currentJobIndex == arrSchedule.Count)
            {
                previousJobIndex = arrSchedule.Count - 1;
                currentJobIndex = 0;
                previousStatus = currentStatus;
                currentStatus = "presetting";
            }

            RoomInfo rinfo = (RoomInfo)arrSchedule[currentJobIndex];
            int nid = int.Parse(rinfo.NID);
            int masterid = int.Parse(rinfo.MasterID);
            int roomid = int.Parse(rinfo.Id);

            sendMainCommand(cbframe.requestRoomInfo(nid, masterid, roomid));


            updatetoolStripStatusLabel2(String.Format("요청 : {0}호 마스터 {1} 룸 {2}", rinfo.HouseID, rinfo.MasterID, rinfo.Id));
           // toolStripStatusLabel2.Text = String.Format("요청 : {0}호 마스터 {1} 룸 {2}", rinfo.HouseID, rinfo.MasterID, rinfo.Id);
            
            

            cf.appendLog(String.Format("타이머 요청 : {0}호 마스터 {1} 룸 {2}\n", rinfo.HouseID, rinfo.MasterID, rinfo.Id));

            previousJobIndex = currentJobIndex;
            
        
        }


        private void timer3_Tick(object sender, EventArgs e)
        {
           // Console.WriteLine("house command timer worker index :: {0}  max cnt :: {1}", houseJobIndex, houseSchedule.Count);

            if (houseJobIndex == houseSchedule.Count)
            {
                timer3.Stop();
                rcf.setButtonEnable(true);

                currentStatus = previousStatus;

                //Console.WriteLine("=> End timer3 Start Timer2 work schedule job ");
                // cf.appendLog("=> End timer3 Start Timer2 work schedule job \n");
                // timer2.Start();
            }
            else
            {
                housejob();
                houseJobIndex++;
            }

            
        }

        private void timer3_2_Tick(object sender, EventArgs e)
        {
            if (houseJobIndex == houseSchedule.Count)
            {
                timer3_2.Stop();

                rcf.setButtonEnable(true);

                currentStatus = previousStatus;

                Console.WriteLine("timer3_2_Tick currentStatus {0}", currentStatus);

            }
            else
            {
                housejob();
                houseJobIndex++;
            }
        }

        private void housejob()
        {
            if (houseJobIndex < houseSchedule.Count)
            {
                if (houseSchedule[houseJobIndex] is RoomInfo)
                {
                    Console.WriteLine("======= housejob send index :: {0} =============",houseJobIndex);

                    RoomInfo temproomx = (RoomInfo)houseSchedule[houseJobIndex];
                    int next_nid = int.Parse(temproomx.NID);
                    int next_masterid = int.Parse(temproomx.MasterID);
                    int next_roomid = int.Parse(temproomx.Id);

                    if (houseJobCmd.Equals("status"))
                    {
                        sendMainCommand(cbframe.requestRoomInfo(next_nid, next_masterid, next_roomid));
                    }
                    else if (houseJobCmd.Equals("poweron"))
                    {
                        sendMainCommand(cbframe.requestSetRoomPower(next_nid, next_masterid, next_roomid, 1));
                    }
                    else if (houseJobCmd.Equals("poweroff"))
                    {
                        sendMainCommand(cbframe.requestSetRoomPower(next_nid, next_masterid, next_roomid, 0));
                    }
                    else if (houseJobCmd.Equals("outingon"))
                    {
                        sendMainCommand(cbframe.requestSetRoomOut(next_nid, next_masterid, next_roomid, 1));
                    }
                    else if (houseJobCmd.Equals("outingoff"))
                    {
                        sendMainCommand(cbframe.requestSetRoomOut(next_nid, next_masterid, next_roomid, 0));
                    }
                    else if (houseJobCmd.Equals("settemp"))
                    {
                        sendMainCommand(cbframe.requestSetRoomTemp(next_nid, next_masterid, next_roomid, int.Parse(houseJobCmdValue)));

                    }
                }
                else
                {
                    Console.WriteLine("======= house type =============");
                }
            }
            
            


            
        }

        // singlecmd response handle 
        private void timer4_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("timer4_Tick");
            if (currentStatus.Equals("singlecmd"))
            { 
                // no response from lora 
                // update status 
                currentStatus = previousStatus;
                previousStatus = "singlecmd";
                
            }
            timer4.Stop();
        }




        // test menu part [[
        private void pollingStartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer2.Start();
        }

        private void pollingStopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer2.Stop();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

       

       

        // test menu part ]]
       

       

    }
}
