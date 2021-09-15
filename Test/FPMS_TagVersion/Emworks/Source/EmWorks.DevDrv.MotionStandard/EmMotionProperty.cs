using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmWorks.DevDrv.MotionStandard
{
    /// <summary>
    /// Motion 동작을 위한 Property 저장 Class
    /// </summary>
    public class EmMotionProperty
    {
        public int AxisIndex;

        public double xHomeVelocity1;
        public double xHomeVelocity2;
        public double xHomeVelocity3;
        public double xHomeVelocity4;
        public double xHomeAccel1;
        public double xHomeAccel2;

        public double yHomeVelocity1;
        public double yHomeVelocity2;
        public double yHomeVelocity3;
        public double yHomeVelocity4;
        public double yHomeAccel1;
        public double yHomeAccel2;

        public double CmdPos;
        public double Accel;
        public double Decel;
        public double Velocity;

        public double JogVelocity;
        public double JogAccel;
        public double JogDecel;

        // Trigger Set time level
        public double TriggerTime;

        // Trigger Set Block
        public double TriggerStartPos;
        public double TriggerEndPos;
        public double TriggerPeriodPos;


        public EmMotionProperty()
        {
            xHomeVelocity1 = xHomeVelocity2 = xHomeVelocity3 = xHomeVelocity4 = 0;
            xHomeAccel1 = xHomeAccel2 = 0;

            yHomeVelocity1 = yHomeVelocity2 = yHomeVelocity3 = yHomeVelocity4 = 0;
            yHomeAccel1 = yHomeAccel2 = 0;

            CmdPos = Accel = Decel = Velocity = 0;

            JogAccel = JogDecel = 0;
        }
    }
}
