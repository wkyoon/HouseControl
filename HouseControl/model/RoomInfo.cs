using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseControl
{
    public class RoomInfo
    {
        private string key; // houseid-masterid-id 형태의 key index
        private string houseid; // 하우스의 아이디
        private string dong;   // A ~ F 동까지 지정할수 있도록 한다. 
        private string nid;   // 로라 통신 nid 도 넣는다. 
        private string masterid; // 마스터의 아이디 는 1 또는 2 로 한다. 대부분 1이지만 복층은 2로 하는 경우도 있다. 



        // 골프텔은 ABCDEF 를 123456 으로 하였고
        // 호수 번호와 조합하여 C동은 3  100 호는 3100 으로 표시

        // A동의 경우 복층이라 마스터가 2개 여서
        // 1 101의 1층은 11101 로
        //         2층은 12101 로 표현하였습니다. 

        private string id;       // 룸의 아이디 
        private string name;  // 방이름 // 거실 , 안방 
        private bool power;  // 난방 ON OFF
        private bool outing; // 외출 ON OFF
        private string curtemp;  // 현재 온도
        private string settemp;  // 설정 온도 

        private string tinfo;  // 마지막 업데이트 된 시간을 가지고 있는다. 
        private string desc;   // 통신상태를 나타내는 용도로 

        // 예약 상태  ???
        // 타이머 설정  ???



        public string Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
            }

        }

        // house id 
        public string HouseID
        {
            get
            {
                return houseid;
            }
            set
            {
                houseid = value;
            }
        }

        public string Dong
        {
            get
            {
                return dong;
            }
            set
            {
                dong = value;
            }
        }

        public string NID
        {
            get
            {
                return nid;
            }
            set
            {
                nid = value;
            }
        }
       


        // master id 
        public string MasterID
        {
            get
            {
                return masterid;
            }
            set
            {
                masterid = value;
            }
        }

        // device idx
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        // 온도조절기가 설치되어 있는 위치의 이름 
        // 룸이름 거실 안방 
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }


        // 난방 
        public bool Power
        {
            get
            {
                return power;
            }
            set
            {
                power = value;
            }
        }

        // 외출
        public bool Outing
        {
            get
            {
                return outing;
            }
            set
            {
                outing = value;
            }
        }

        // 현재 온도
        public string CurTemp
        {
            get
            {
                return curtemp;
            }
            set
            {
                curtemp = value;
            }

        }

        //설정 온도
        public string SetTemp
        {
            get
            {
                return settemp;
            }
            set
            {
                settemp = value;
            }

        }

        public string TInfo
        {
            get
            {
                return tinfo;
            }
            set
            {
                tinfo = value;
            }
        }

        public string DESC
        {
            get 
            {
                return desc;
            }
            set 
            {
                desc = value;
            }
        }


    }
}
