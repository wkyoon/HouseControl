using Newtonsoft.Json;
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
    public partial class FormWindHouse : Form
    {
        Dictionary<string, RoomInfo> dic;
       

        internal delegate void updateDatagrideviewCallBack(RoomInfo ri);

        internal delegate void sendCommandDele(byte[] cmd);
        internal event sendCommandDele sendMainCommand;

        internal delegate void openRemoteControlFormDele(RoomInfo rinfo);
        internal event openRemoteControlFormDele openRemoteControlForm;


        //=======================================
        public string dfolderpath = "";
        public string dhomename = "TheStarHue";
        public string dhomename_config = "config";
        public string dhomename_data = "data";
        public string dhomename_event = "event";


        public FormWindHouse(string title,Dictionary<string, RoomInfo> dic)
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;

            this.dataGridView1.CellPainting += DataGridView1_CellPainting;
            this.dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
            this.dic = dic;
            this.label1.Text = title;

            string dpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dfolderpath = dpath + "\\" + dhomename;
           

        }

        


        // 이것도 callback 으로 해야 되지 않나 싶다.
        // 내일 검토하자. 
        public void updateDicNGridView(RoomInfo ri)
        {

            if (this.dataGridView1.InvokeRequired)
            {
                this.Invoke(new updateDatagrideviewCallBack(updateDicNGridView), ri);
            }
            else
            {
                this.dic[ri.Key] = ri;
             //   Console.WriteLine("업데이트 하기 {0} {1} masterid :: {2}", ri.Key, ri.TInfo,ri.MasterID);

                string output = JsonConvert.SerializeObject(ri);
              //  Console.WriteLine(output);
               // System.IO.File.WriteAllText(dfolderpath + "\\" + dhomename_data+"\\"+ri.Key + ".json", output);

                string[] row = new string[] { "key", "houseid", "roomname", "난방상태", "외출상태", "현재온도","설정온도","" };

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (ri.Key.Equals(dataGridView1.Rows[i].Cells[0].Value.ToString()))
                    {
                       // Console.WriteLine(dataGridView1.Rows[i].Cells[0].Value.ToString());
                        if (ri.Power)
                        {
                            dataGridView1.Rows[i].Cells[3].Value = "난방 ON";
                            dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.White;
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells[3].Value = "난방 OFF";
                            dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.Yellow;
                        }


                        if (ri.Outing)
                        {
                            dataGridView1.Rows[i].Cells[4].Value = "외출모드";
                            dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.Green;
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells[4].Value = "노멀모드";
                            dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.White;
                        }

                        if (ri.CurTemp != null)
                        {
                            dataGridView1.Rows[i].Cells[5].Value = ri.CurTemp.ToString();
                        }
                        if (ri.SetTemp != null)
                        {
                            dataGridView1.Rows[i].Cells[6].Value = ri.SetTemp.ToString();
                        }
                        
                        dataGridView1.Rows[i].Cells[7].Value = ri.DESC;
                        dataGridView1.Rows[i].Cells[8].Value = ri.TInfo;
                        dataGridView1.Rows[i].Selected = true;
                        break;
                    }
                }
            }
        }

       



        private void FormWindHouse_Load(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = 9;
            dataGridView1.Columns[0].Name = "";
            dataGridView1.Columns[1].Name = "호수";
            dataGridView1.Columns[2].Name = "룸정보";
            dataGridView1.Columns[3].Name = "난방상태";
            dataGridView1.Columns[4].Name = "외출상태";
            dataGridView1.Columns[5].Name = "현재온도";
            dataGridView1.Columns[6].Name = "설정온도";
            dataGridView1.Columns[7].Name = "통신상태";
            dataGridView1.Columns[8].Name = "통신시간";
            dataGridView1.Columns[8].Width = 180;

            string[] row = new string[] { "key", "houseid", "roomname", "난방상태", "외출상태", "현재온도", "설정온도", "","" }; 

            foreach (var pair in dic)
            {
             //   Console.WriteLine("{0}, {1} {2}", pair.Key, pair.Value.HouseID, pair.Value.Name);
                row[0] = pair.Key;
                row[1] = pair.Value.HouseID;
                row[2] = pair.Value.Name;


                if(pair.Value.Power)
                {
                    row[3] = "난방 ON";
                }
                else
                {
                    row[3] = "난방 OFF";
                }

                if (pair.Value.Outing)
                {
                    row[4] = "외출모드";
                }
                else
                {
                    row[4] = "노멀모드";
                }
                
                
                row[5] = pair.Value.CurTemp;
                row[6] = pair.Value.SetTemp;
                row[7] = pair.Value.DESC;
                row[8] = pair.Value.TInfo;//.ToString("yyyy-MM-dd hh:mm:ss");
                dataGridView1.Rows.Add(row);
            }


            //============================================

            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.HeaderText = "설정 변경";
            btn.Text = "설정";
            btn.Name = "btn";
            btn.UseColumnTextForButtonValue = true;

//            DataGridViewTextBoxColumn txt = new DataGridViewTextBoxColumn();
//            dataGridView1.Columns.Add(txt);
//            txt.HeaderText = "통신상태";

            //dataGridView1.Rows[1].Cells[8].Value = "xxx";


            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

          
            foreach (DataGridViewRow rowx in dataGridView1.Rows)
            {
                rowx.ReadOnly = true;
             //   Console.WriteLine("rows " + rowx.Index);
                if (rowx.Cells[3].Value.Equals("난방 OFF"))
                {
                    dataGridView1.Rows[rowx.Index].Cells[3].Style.BackColor = Color.Yellow;
                }

                if (rowx.Cells[4].Value.Equals("외출모드"))
                {
                    dataGridView1.Rows[rowx.Index].Cells[4].Style.BackColor = Color.Green;
                }
                
            }

            this.dataGridView1.Columns[0].Visible = false;
        }

        private void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex != this.dataGridView1.Rows.Count - 1)
            {
                DataGridViewRow thisRow = this.dataGridView1.Rows[e.RowIndex];
                DataGridViewRow nextRow = this.dataGridView1.Rows[e.RowIndex + 1];

                // 룸 번호가 다른 경우 line 을 표시하도록 하였다. 
                if (thisRow.Cells[1].FormattedValue.ToString() != nextRow.Cells[1].FormattedValue.ToString())
                {
                    e.Paint(e.ClipBounds, DataGridViewPaintParts.All);

                    Rectangle divider = new Rectangle(e.CellBounds.X, e.CellBounds.Y + e.CellBounds.Height - 2, e.CellBounds.Width, 2);
                    e.Graphics.FillRectangle(Brushes.Black, divider);

                    e.Handled = true;
                }
            }
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                if (e.RowIndex > 0)
                {
                    this.dataGridView1.InvalidateRow(e.RowIndex - 1);
                }

                this.dataGridView1.InvalidateRow(e.RowIndex);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                if (e.ColumnIndex == 9)
                {
                    // MessageBox.Show((e.RowIndex + 1) + "  Row  " + (e.ColumnIndex + 1) + "  Column button clicked ");
                    Console.WriteLine(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    Console.WriteLine(dic[dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()].Name);

                    openRemoteControlForm.Invoke((RoomInfo)dic[dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()]);

                    //                rcf.setRoomInfo((RoomInfo)dic[dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()]);
                    //                rcf.Show();
                    //                rcf.Focus();

                }
            }
            
        }


    }
}