using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseControl.model
{
    public class JobItem
    {
        private int delay;  // 다음 호출을 하기 위한 delay 시간 
                            // 응답에 대한 time out 으로 사용하자. 
        private byte[] cmd; // uart로 전송하는 명령어 
        private string label; // cmd가 string 인 경우 cmd 에 해당하는 string 값 
        private string dummy;  // timer 를 skip 하기 위해 사용 

        public int Delay
        {
            get
            {
                return delay;
            }
            set
            {
                delay = value;
            }
        }

        public byte[] CMD
        {
            get
            {
                return cmd;
            }
            set
            {
                cmd = value;
            }
        }

        public string Label
        {
            get
            {
                return label;
            }
            set
            {
                label = value;
            }
        }

        public string Dummy
        {
            get
            {
                return dummy;
            }
            set
            {
                dummy = value;
            }
        }
        
    }
}
