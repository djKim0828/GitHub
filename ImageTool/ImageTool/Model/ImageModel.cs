using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageTool
{
    public class ImageModel
    {
        public class ToolType
        {
            public const int None = 0;
            public const int ZoomFit = None + 1;
            public const int RotateCW = ZoomFit + 1;
            public const int RotateCCW = RotateCW + 1;
            public const int Length = RotateCCW + 1;
            public const int Angel = Length + 1;

        }
    }
}
