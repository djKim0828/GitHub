using System;

namespace FPMS.E2105_FS111_121
{
    public static class Global
    {
        #region Fields

        public static bool IsProgramLoadComplete = false;
        public static TimeSpan TactTime;

        public static AlarmModel AlarmWriter;

        //Config
        public static ConfigGeneralModel ConfigGeneral;
        public static AutonicsTK4SModel ConfigAutonicsTK4S;
        public static SamwonTemp1500Model ConfigSamwonTemp1500;
        public static KeyenceIL100Model ConfigKeyenceIL100;
        public static SamwonSDR100Model ConfigSamwonSDR100;

        //Tag

        public static AjinIo AjinIo;
        
        public static AjinMotion AjinMotionX;
        public static AjinMotion AjinMotionY;
        public static AjinMotion AjinMotionZ;
        public static AjinMotion AjinMotionV;
        public static AjinMotion AjinMotionH;
        public static AjinMotion AjinMotionR;
        
        public static SamwonTemp1500 SamwonTemp1500;
        public static SamwonSDR100 SamwonSDR100;
        public static KeyenceIL100 KeyenceIL100;

        #endregion Fields
    }
}
