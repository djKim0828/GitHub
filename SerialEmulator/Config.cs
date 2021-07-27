using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StEm
{
    public class Config
    {
        public string Port;
        public string Bardrate;
        public string sendData;
        public string newLine;
        public bool isHex = false;

        public string[] receiveDatas;
        public string[] sendDatas;

        public Config()
        {
            Port = Bardrate = string.Empty;
        }

        ~Config()
        {

        }
    }
}
