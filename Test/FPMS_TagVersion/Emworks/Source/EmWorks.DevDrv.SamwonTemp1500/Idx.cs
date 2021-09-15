namespace EmWorks.DevDrv.SamwonTemp1500
{
    public enum ReceiveResult
    {
        None,
        Complete,
        Error
    }

    public enum RunStop
    {
        Run = 1,
        Stop = 4
    }

    public class CommandTagType
    {
        #region Fields

        public const string AlwaysOne = "4";
        public const string AlwaysZero = "3";
        public const string Defualt = "0";
        public const string Invert = "1";
        public const string ReActValueOut = "2";

        #endregion Fields
    }

    public class ExeCmd
    {
        #region Fields

        public const string Comport = "Comport";
        public const string RunStop = "RunStop";
        public const string SetSV = "SetSV";

        #endregion Fields
    }

    public class FunctionResult
    {
        #region Fields

        public const int Error = -1;
        public const int False = 0;
        public const int True = 1;

        #endregion Fields
    }

    public class IoType
    {
        #region Fields

        public const int input = 0;
        public const int output = 1;
        public const int StateInput = 2;
        public const int StateOutput = 3;

        #endregion Fields
    }

    public class OpenCloseStatus
    {
        #region Fields

        public const int Close = 0;
        public const int Error = -1;
        public const int Open = 1;

        #endregion Fields
    }

    public class RelatedTag
    {
        #region Fields

        public string DefaultValue = "2";
        public string DelayTime = "4";
        public string Name = "0";
        public string ReActValue = "3";
        public string Type = "1";

        #endregion Fields
    }

    public class SerialCommand
    {
        #region Fields

        public const string AMI = "AMI";
        public const string RSD = "RSD";
        public const string WSD = "WSD";

        #endregion Fields
    }
}