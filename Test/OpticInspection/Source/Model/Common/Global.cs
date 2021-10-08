using System;

namespace EmWorks.App.OpticInspection
{
    public static class Global
    {
        #region Fields

        public static AlarmModel _AlarmWriter;
        public static DataWriter _CellInfoWriter;
        public static bool _IsProgramLoadComplete = false;

        // Motion
        public static MotionControl _MotionControl;
        public static DeltatauPmac _PmacX1;
        public static DeltatauPmac _PmacX3;
        public static DeltatauPmac _PmacY2;
        public static DeltatauPmac _PmacZ4;
        public static DeltatauPmac _PmacZ5;

        // SR-5000
        public static TopconSR5000 _TopconSR5000;

        public static TimeSpan _tactTime;
        public static ConfigGeneralModel ConfigGeneral;

        public static ModelRecipe _ModelRecipe;

        #endregion Fields
    }
}