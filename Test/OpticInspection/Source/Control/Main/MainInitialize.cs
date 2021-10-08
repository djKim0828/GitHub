using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using System;
using System.Threading;


namespace EmWorks.App.OpticInspection
{
    public class MainInitialize
    {
        #region Fields

        private int _interval = 0;
        private bool _isInit = false;

        #endregion Fields

        #region Methods

        public bool InitializeIO()
        {
            return true;
        }

        public bool LoadAccount()
        {
            return true;
        }

        public bool LoadDeviceMoitionX1()
        {
            try
            {
                bool isSimulation = Global.ConfigGeneral.RunMode == Idx.RunMode.Simulation ? true : false;

                if (Global._PmacX1 != null)
                {
                    Tag tempTag = (Tag)Global._PmacX1.syDeviceMotion;
                    if (tempTag.Is.ToString() == Idx.DigitalValue.Enable.ToString())
                    {
                        return true;
                    }
                }

                Global._PmacX1 = new DeltatauPmac(Global.ConfigGeneral.RootPath + Global.ConfigGeneral.DeltatauMotionX1TagFilePath, isSimulation);

                Thread.Sleep(_interval);

                // Open
                if (Global._PmacX1.SetsyDeviceMotion(true) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }
        }

        public bool LoadDeviceMoitionX3()
        {
            try
            {
                bool isSimulation = Global.ConfigGeneral.RunMode == Idx.RunMode.Simulation ? true : false;

                if (Global._PmacX3 != null)
                {
                    Tag tempTag = (Tag)Global._PmacX3.syDeviceMotion;
                    if (tempTag.Is.ToString() == Idx.DigitalValue.Enable.ToString())
                    {
                        return true;
                    }
                }

                Global._PmacX3 = new DeltatauPmac(Global.ConfigGeneral.RootPath + Global.ConfigGeneral.DeltatauMotionX3TagFilePath,
                                                    isSimulation);

                Thread.Sleep(_interval);

                // Open
                if (Global._PmacX3.SetsyDeviceMotion(true) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }
        }

        public bool LoadDeviceMoitionY2()
        {
            try
            {
                bool isSimulation = Global.ConfigGeneral.RunMode == Idx.RunMode.Simulation ? true : false;

                if (Global._PmacY2 != null)
                {
                    Tag tempTag = (Tag)Global._PmacY2.syDeviceMotion;
                    if (tempTag.Is.ToString() == Idx.DigitalValue.Enable.ToString())
                    {
                        return true;
                    }
                }

                Global._PmacY2 = new DeltatauPmac(Global.ConfigGeneral.RootPath + Global.ConfigGeneral.DeltatauMotionY2TagFilePath,
                                                    isSimulation);

                Thread.Sleep(_interval);

                // Open
                if (Global._PmacY2.SetsyDeviceMotion(true) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }
        }

        public bool LoadDeviceMoitionZ4()
        {
            try
            {
                bool isSimulation = Global.ConfigGeneral.RunMode == Idx.RunMode.Simulation ? true : false;

                if (Global._PmacZ4 != null)
                {
                    Tag tempTag = (Tag)Global._PmacZ4.syDeviceMotion;
                    if (tempTag.Is.ToString() == Idx.DigitalValue.Enable.ToString())
                    {
                        return true;
                    }
                }

                Global._PmacZ4 = new DeltatauPmac(Global.ConfigGeneral.RootPath + Global.ConfigGeneral.DeltatauMotionZ4TagFilePath,
                                                    isSimulation);

                Thread.Sleep(_interval);

                // Open
                if (Global._PmacZ4.SetsyDeviceMotion(true) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }
        }

        public bool LoadDeviceMoitionZ5()
        {
            try
            {
                bool isSimulation = Global.ConfigGeneral.RunMode == Idx.RunMode.Simulation ? true : false;

                if (Global._PmacZ5 != null)
                {
                    Tag tempTag = (Tag)Global._PmacZ5.syDeviceMotion;
                    if (tempTag.Is.ToString() == Idx.DigitalValue.Enable.ToString())
                    {
                        return true;
                    }
                }

                Global._PmacZ5 = new DeltatauPmac(Global.ConfigGeneral.RootPath + Global.ConfigGeneral.DeltatauMotionZ5TagFilePath,
                                                    isSimulation);

                Thread.Sleep(_interval);

                // Open
                if (Global._PmacZ5.SetsyDeviceMotion(true) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }
        }

        public bool LoadSR5000()
        {
            try
            {
                bool isSimulation = Global.ConfigGeneral.RunMode == Idx.RunMode.Simulation ? true : false;

                if (Global._TopconSR5000 != null)
                {
                    Tag tempTag = (Tag)Global._TopconSR5000.syDeviceSR5000;
                    if (tempTag.Is.ToString() == Idx.DigitalValue.Enable.ToString())
                    {
                        return true;
                    }
                }

                //Global._TopconSR5000 = new TopconSR5000(Global.ConfigGeneral.RootPath + Global.ConfigGeneral.TopconSR5000TagFilePath,
                //                                    isSimulation);

                Global._TopconSR5000 = new TopconSR5000(Global.ConfigGeneral.RootPath + Global.ConfigGeneral.TopconSR5000TagFilePath,
                                    false);

                Thread.Sleep(_interval);

                // Open
                if (Global._TopconSR5000.SetsyDeviceSR5000(true) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }
        }

        #endregion Methods

        #region private Function

        private bool WaitResult(int timeOut, Action action)
        {
            int timeout = timeOut == 0 ? 100 : timeOut;
            IAsyncResult async_result;
            async_result = action.BeginInvoke(null, null);

            if (async_result.AsyncWaitHandle.WaitOne(timeout))
            {
                return true;
            }
            else // timeout
            {
                Logger.Debug("Timeout");
                return false;
            }
        }

        #endregion private Function
    }
}
