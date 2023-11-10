using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 2018.03.02. 전체 command 적용을 하기 때문에 기존에 있는 놈이랑 
//             구분해야 한다. ( 이유 기존은 하나씩 모든 온도조절기에 명령을 주없다면 변경 내용은 마스터에 한번만 주면 된다. 
//             타입을 구분해서 기존도 되고 신규도 되어야 하는 이슈가 있다.
//             변경된 내용을 한번에 적용하지 못하고 운영중이라서 이 지랄을 해야 한다.   
namespace HouseControl
{
    public class HouseInfo
    {
        private string id;   // 하우스 id
        private string name;  // 하우스 호수 
        private string type; // 마스터가 신규인지 이전인지 알아야 해서 적용 2018.03.02.

        private Dictionary<string, MasterInfo> master; // 마스터가 1개 이상일 수도 있는 경우도 있기에 dictionary type 으로 정의 

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

        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        
        }


        // 온도 조절기 
        public Dictionary<string, MasterInfo> Master
        {
            get
            {
                return master;
            }
            set
            {
                master = value;
            }

        }

    }
}
