using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMACTest
{
    public class Config
    {
        public string DeviceIndex = "0";

        public string MotionIndexX = "0";
        public string MotionCmdPosX = "0";
        public string MotionRelPosX = "0";

        public string MotionIndexY = "2";
        public string MotionCmdPosY = "0";
        public string MotionRelPosY = "0";

        public string MotionIndexZ = "4";
        public string MotionCmdPosZ = "0";
        public string MotionRelPosZ = "0";

        public Config()
        {

        }

        ~Config()
        {

        }
    }
}
