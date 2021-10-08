using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmWorks.App.OpticInspection
{
    public class MotionPosition
    {
        public string AxisName;
        public string CategoryName;
        public string Name { get; set; }
        public string MoveType { get; set; }
        public double Position { get; set; }
        public double Speed { get; set; }
        public double Accel { get; set; }
        public double Decel { get; set; }
    }
}
