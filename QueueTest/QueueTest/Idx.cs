using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueTest
{
    public class Idx
    {
        public class StageId
        {
            public const int Load = 0;
            public const int Inspection = 1;
            public const int Unload = 2;
        }

        public class StageStatus
        {
            public const int Wait = 0;
            public const int Busy = 1;
            public const int Complate = 2;
        }

        public class StageStap
        {
            public const int Step1 = 0;
            public const int Step2 = 1;
            public const int Step3 = 2;
            public const int Step4 = 3;
        }
    }
}
