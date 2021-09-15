using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmWorks.DevDrv.AutonicsTK4S
{
    public class OpenClose
    {
        public const int Error = -1;
        public const int Close = 0;
        public const int Open = 1;
    }

    public class FunctionResult
    {
        public const int False = 0;
        public const int True = 1;
    }

    public class RelatedTag
    {
        public string Name = "0";
        public string Type = "1";
        public string DefaultValue = "2";
        public string ReActValue = "3";
        public string DelayTime = "4";
    }

    public class CommandTagType
    {
        #region Fields

        public const string Defualt = "0";
        public const string Invert = "1";
        public const string ReActValueOut = "2";
        public const string AlwaysZero = "3";
        public const string AlwaysOne = "4";

        #endregion Fields
    }
}
