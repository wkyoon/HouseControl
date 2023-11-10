using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseControl.model
{


    public class CbFrame
    {
        // 온도 조절기에 들어가 있는 기본 명령어 
        // 1F 형태의 명령은 정상동작을 확인했다.
        // 문제는 응답이 없어서 모든 값을 다시 읽어와야 하는 이슈가 있다. 
        // 결론 일단 한번 1F 로 전송하고 다시 한번 각방 명령으로 다시 주어야 한다. 

        // 마스터 특성 요구 
        public byte[] requestMasterInfo(int nid,int master)
        {
            byte[] rtn={0x02,0x00,0x00,0x04,0x36,0x00,0x0f,0x00,0xff,0xff,0x03};
           
            rtn[1] = (byte)((nid & 0xFF00) >> 8);
            rtn[2] = (byte) (nid & 0x00FF);
            rtn[5] = (byte)((master&0x0F)<<4);
//            Console.WriteLine("requestMasterInfo {0:X} {1:X} ",rtn[1],rtn[2]);
//            Console.WriteLine("requestMasterInfo {0} {1:X4} {2} {3:X2}", nid, nid, master, master);


            return rtn;
        }

        // 온도조절기 상태요구
        public byte[] requestRoomInfo(int nid, int master,int roomid)
        {
            byte[] rtn = { 0x02, 0x00, 0x00, 0x04, 0x36, 0x00, 0x01, 0x00, 0xff, 0xff, 0x03 };
           
            rtn[1] = (byte)((nid & 0xFF00) >> 8);  // nid up
            rtn[2] = (byte)(nid & 0x00FF);         // nid down
            rtn[5] = (byte)(((master & 0x0F) << 4) | roomid); // masterid
//            Console.WriteLine("requestMasterInfo {0:X} ", rtn[5]);
            return rtn;
        }

        // 온도조절기 난방 설정 on / off
        public byte[] requestSetRoomPower(int nid, int master, int roomid,int on)
        {
            byte[] rtn = { 0x02, 0x00, 0x00, 0x05, 0x36, 0x00, 0x43, 0x01, 0x00, 0xff, 0xff, 0x03 };
           
            rtn[1] = (byte)((nid & 0xFF00) >> 8);  // nid up
            rtn[2] = (byte)(nid & 0x00FF);         // nid down
            rtn[5] = (byte)(((master & 0x0F) << 4) | roomid);
            rtn[8] = (byte)(on); 
            //            Console.WriteLine("requestMasterInfo {0:X} ", rtn[5]);
            return rtn;
        }

        // 온도조절기 외출 설정
        public byte[] requestSetRoomOut(int nid, int master, int roomid, int on)
        {
            byte[] rtn = { 0x02, 0x00, 0x00, 0x05, 0x36, 0x00, 0x45, 0x01, 0x00, 0xff, 0xff, 0x03 };
           
            rtn[1] = (byte)((nid & 0xFF00) >> 8);  // nid up
            rtn[2] = (byte)(nid & 0x00FF);         // nid down
            rtn[5] = (byte)(((master & 0x0F) << 4) | roomid);
            rtn[8] = (byte)(on);
            //            Console.WriteLine("requestMasterInfo {0:X} ", rtn[5]);
            return rtn;
        }

        // 온도조절기 온도 설정
        public byte[] requestSetRoomTemp(int nid, int master, int roomid, int temp)
        {
            byte[] rtn = { 0x02, 0x00, 0x00, 0x06, 0x36, 0x00, 0x44, 0x02, 0x00, 0x00, 0xff, 0xff, 0x03 };
          

            rtn[1] = (byte)((nid & 0xFF00) >> 8);  // nid up
            rtn[2] = (byte)(nid & 0x00FF);         // nid down
            rtn[5] = (byte)(((master & 0x0F) << 4) | roomid);

            byte[] bcdvalue = new byte[2];
            bcdvalue = Temp2Bcd(temp);
            rtn[8] = bcdvalue[0];  // BCD  소수점 위에 값
            rtn[9] = bcdvalue[1];  // BCD 소수점 아랫값 
            return rtn;
        }


        // 온도조절기 예약 설정 



        private byte[] Temp2Bcd(int temp)
        {
            if (temp < 0 || temp > 9999) throw new ArgumentException();
            int bcd = 0;
            for (int digit = 0; digit < 4; ++digit)
            {
                int nibble = temp % 10;
                bcd |= nibble << (digit * 4);
                temp /= 10;
            }
            return new byte[] { (byte)((bcd >> 8) & 0xff), (byte)(bcd & 0xff) };
        }


    }
}
