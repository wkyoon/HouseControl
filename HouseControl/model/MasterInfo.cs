using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseControl
{
    public class MasterInfo
    {
        private string id;   // 마스터의 아이디 
        private string houseid;   // 집이 어딘지는 알아야 하지
        private string name;  // 마스터의 이름 라벨 
        private Dictionary<string, RoomInfo> rooms; // 온도조절기 리스트를 가지고 있자. 

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

        // 마스터의 이름
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


        // 온도 조절기 
        public Dictionary<string, RoomInfo> Rooms
        {
            get {
                return rooms;
            }
            set {
                rooms = value;
            }
            
        }

    }

    
}
