using HouseControl.model;
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
    public partial class FormTEST : Form
    {
        internal delegate void sendCommandDele(byte[] cmd);
        internal event sendCommandDele sendCommand;


        CbFrame cbframe;

        int nid;
        int masterid;
        int id;

        int temp;

        public FormTEST()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;

            cbframe = new CbFrame();
        }

        private void FormTEST_Load(object sender, EventArgs e)
        {

        }

        private void btnMasterInfo_Click(object sender, EventArgs e)
        {
            updateTextField();
            
            sendCommand.Invoke(cbframe.requestMasterInfo(nid, masterid));
        }

        private void btnRStatus_Click(object sender, EventArgs e)
        {
            
            updateTextField();

            sendCommand.Invoke(cbframe.requestRoomInfo(nid, masterid,id));
        }

        private void btnPowerOn_Click(object sender, EventArgs e)
        {
            updateTextField();

            sendCommand.Invoke(cbframe.requestSetRoomPower(nid, masterid, id,1));
        }

        private void btnPowerOff_Click(object sender, EventArgs e)
        {
            updateTextField();

            sendCommand.Invoke(cbframe.requestSetRoomPower(nid, masterid, id, 0));
        }

        private void btnOutOn_Click(object sender, EventArgs e)
        {
            updateTextField();

            sendCommand.Invoke(cbframe.requestSetRoomOut(nid, masterid, id,1));
        }

        private void btnOutOff_Click(object sender, EventArgs e)
        {
            updateTextField();

            sendCommand.Invoke(cbframe.requestSetRoomOut(nid, masterid, id, 0));
        }

        private void btnSetTemp_Click(object sender, EventArgs e)
        {
            updateTextField();

            sendCommand.Invoke(cbframe.requestSetRoomTemp(nid, masterid, id,temp));
        }

       

        private void updateTextField()
        {
            nid = int.Parse(tbNID.Text.ToString().Trim());
            masterid = int.Parse(tbMID.Text.ToString().Trim());
            id = int.Parse(tbID.Text.ToString().Trim());
            temp = int.Parse(tbTemp.Text.ToString().Trim());
        }

        private void FormTEST_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

       
    }
}
