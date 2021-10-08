using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmWorks.DevDrv.TopconSR5000
{
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

    public class FunctionResult
    {
        public const int False = 0;
        public const int True = 1;
    }

    public class OpenClose
    {
        #region Fields

        public const int Error = -1;
        public const int Close = 0;
        public const int Open = 1;

        #endregion Fields
    }

    public class OnOff
    {
        #region Fields

        public const int Off = 0;
        public const int On = 1;

        #endregion Fields
    }

    public class ExeCmdIndex
    {
        #region Fields

        public const int TargetObject = 0;
        public const int Protocol = 1;

        #endregion Fields
    }

    public class StatusOptionIndex
    {
        #region Fields

        public const int DisplayName = 0;
        public const int IP = 1;
        public const int Port = 2;
        public const int TargetTag = 3;

        #endregion Fields
    }

    public class RelatedTag
    {
        #region Fields

        public string Name = "0";
        public string Type = "1";
        public string DefaultValue = "2";
        public string ReActValue = "3";
        public string DelayTime = "4";

        #endregion Fields
    }

    public class RelatedTagIndex
    {
        #region Fields

        public const int Name = 0;
        public const int Type = 1;
        public const int DefaultValue = 2;
        public const int ReActValue = 3;
        public const int DelayTime = 4;

        #endregion Fields
    }

    public class RelatedTagType
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
