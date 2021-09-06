using System;
using System.Threading;

namespace FPMS.E2105_FS111_121
{
    public partial class RecipeItem
    {
        #region Fields

        private SamwonTemp1500 _chamberContoroller = Global.SamwonTemp1500;
        private SamwonSDR100 _chamberRecoder = Global.SamwonSDR100;

        private int _tempstart = 0;

        #endregion Fields

        #region Enums

        public enum ERunStop
        {
            Run,
            Stop,
        }

        #endregion Enums

        #region Methods

        [IsItem(true)]
        [IsComboBox("RunStop", typeof(ERunStop))]
        public bool Chamber_RunStop(ERunStop RunStop)
        {
            //Todo:enum 사용
            var x = _chamberContoroller.xTemp1500RunStop.Is;
            //_chamberContoroller.SetRunOnOff(OnOff);
            return true;
        }

        [IsItem(true)]
        public bool Chamber_SetTemp(double Temp)
        {
            _chamberContoroller.yTemp1500SetSV = Temp;

            if (_chamberContoroller.xTemp1500SetSV == null)
            {
                return false;
            }// else

            double setVelue = (double)_chamberContoroller.xTemp1500SetSV.Is;

            if (Temp != setVelue)
            {
                return false;
            }// else

            return true;
        }

        [IsItem(true)]
        public bool Chamber_WaitTemp(double Temp, double Gap)
        {
            TimeSpan sleeptime = new TimeSpan(0, 1, 0);
            _chamberContoroller.yTemp1500SetSV = Temp;

            if (_chamberContoroller.xTemp1500SetSV == null)
            {
                return false;
            }// else

            double setVelue = (double)_chamberContoroller.xTemp1500SetSV.Is;

            if (Temp != setVelue)
            {
                return false;
            }// else

            if (_chamberRecoder.xSDR100GetPV1 == null)
            {
                return false;
            }// else

            double getPV1 = (double)_chamberRecoder.xSDR100GetPV1.Is;

            while (Math.Abs(Temp - getPV1) > Gap)
            {
                getPV1 = (double)_chamberRecoder.xSDR100GetPV1.Is;
                Thread.Sleep(500);

                if (false)//Global.Global.IsProcessMain.StopedProcess())
                {
                    return false;
                }// else
            }

            getPV1 = (double)_chamberRecoder.xSDR100GetPV4.Is;
            DateTime timeout = DateTime.Now.AddHours(1);
            while (Math.Abs(Temp - getPV1) > Gap)
            {
                getPV1 = (double)_chamberRecoder.xSDR100GetPV4.Is;
                Thread.Sleep(500);
                if (timeout <= DateTime.Now)
                {
                    break;
                }// else

                if (false)//Global.Global.IsProcessMain.StopedProcess())
                {
                    return false;
                }// else
            }

            if ((double)_chamberRecoder.xSDR100GetPV1.Is < -30.0)
            {
                _tempstart++;
            }// else

            return true;
        }

        #endregion Methods
    }
}