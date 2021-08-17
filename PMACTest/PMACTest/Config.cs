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

        public string MotionIndexX = "1";
        public string MotionCmdPosCntX = "0";
        public string MotionCmdPosContX = "0";
        public string MotionAbsPosX = "0";
        public string MotionSpeedX = "0";

        public string MotionIndexY = "2";
        public string MotionCmdPosCntY = "0";
        public string MotionCmdPosContY = "0";
        public string MotionAbsPosY = "0";
        public string MotionSpeedY = "0";

        public string MotionIndexZ = "5";
        public string MotionCmdPosCntZ = "0";
        public string MotionCmdPosContZ = "0";
        public string MotionAbsPosZ = "0";
        public string MotionSpeedZ = "0";

        public Config()
        {

        }

        ~Config()
        {

        }
    }
}
