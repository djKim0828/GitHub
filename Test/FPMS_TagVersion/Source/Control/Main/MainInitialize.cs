using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using System;
using System.Threading;


namespace FPMS.E2105_FS111_121
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
            bool isSimulation = Global.ConfigGeneral.RunMode == Idx.RunMode.Simulation ? true : false;

            if (Global.AjinIo != null)
            {
                Tag tempTag = (Tag)Global.AjinIo.syDeviceIo;
                if (tempTag.Is.ToString() == Idx.DigitalValue.Enable.ToString())
                {
                    return true;
                } // else
            } // else

            Global.AjinIo = new AjinIo(Global.ConfigGeneral.RootPath +
                Global.ConfigGeneral.AjinIoTagFilePath, isSimulation);

            Thread.Sleep(_interval);

            // Open
            if (Global.AjinIo.SetsyDeviceIo(true) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool LoadAccount()
        {
            return true;
        }

        public bool LoadDeviceMoitionX()
        {
            try
            {
                bool isSimulation = Global.ConfigGeneral.RunMode == Idx.RunMode.Simulation ? true : false;

                if (Global.AjinMotionX != null)
                {
                    Tag tempTag = (Tag)Global.AjinMotionX.syDeviceMotion;
                    if (tempTag.Is.ToString() == Idx.DigitalValue.Enable.ToString())
                    {
                        return true;
                    } //else
                } //else

                Global.AjinMotionX = new AjinMotion(Global.ConfigGeneral.RootPath + Global.ConfigGeneral.AjinMotionXTagFilePath, isSimulation);

                Thread.Sleep(_interval);

                // Open
                if (Global.AjinMotionX.SetsyDeviceMotion(true) == true)
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

        public bool LoadDeviceMoitionY()
        {
            try
            {
                bool isSimulation = Global.ConfigGeneral.RunMode == Idx.RunMode.Simulation ? true : false;

                if (Global.AjinMotionY != null)
                {
                    Tag tempTag = (Tag)Global.AjinMotionY.syDeviceMotion;
                    if (tempTag.Is.ToString() == Idx.DigitalValue.Enable.ToString())
                    {
                        return true;
                    } //else
                } //else

                Global.AjinMotionY = new AjinMotion(Global.ConfigGeneral.RootPath + Global.ConfigGeneral.AjinMotionYTagFilePath, isSimulation);

                Thread.Sleep(_interval);

                // Open
                if (Global.AjinMotionY.SetsyDeviceMotion(true) == true)
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

        public bool LoadDeviceMoitionZ()
        {
            try
            {
                bool isSimulation = Global.ConfigGeneral.RunMode == Idx.RunMode.Simulation ? true : false;

                if (Global.AjinMotionZ != null)
                {
                    Tag tempTag = (Tag)Global.AjinMotionZ.syDeviceMotion;
                    if (tempTag.Is.ToString() == Idx.DigitalValue.Enable.ToString())
                    {
                        return true;
                    } //else
                } //else

                Global.AjinMotionZ = new AjinMotion(Global.ConfigGeneral.RootPath + Global.ConfigGeneral.AjinMotionZTagFilePath, isSimulation);

                Thread.Sleep(_interval);

                // Open
                if (Global.AjinMotionZ.SetsyDeviceMotion(true) == true)
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

        public bool LoadDeviceMoitionV()
        {
            try
            {
                bool isSimulation = Global.ConfigGeneral.RunMode == Idx.RunMode.Simulation ? true : false;

                if (Global.AjinMotionV != null)
                {
                    Tag tempTag = (Tag)Global.AjinMotionV.syDeviceMotion;
                    if (tempTag.Is.ToString() == Idx.DigitalValue.Enable.ToString())
                    {
                        return true;
                    } //else
                } //else

                Global.AjinMotionV = new AjinMotion(Global.ConfigGeneral.RootPath + Global.ConfigGeneral.AjinMotionVTagFilePath, isSimulation);

                Thread.Sleep(_interval);

                // Open
                if (Global.AjinMotionV.SetsyDeviceMotion(true) == true)
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

        public bool LoadDeviceMoitionH()
        {
            try
            {
                bool isSimulation = Global.ConfigGeneral.RunMode == Idx.RunMode.Simulation ? true : false;

                if (Global.AjinMotionH != null)
                {
                    Tag tempTag = (Tag)Global.AjinMotionH.syDeviceMotion;
                    if (tempTag.Is.ToString() == Idx.DigitalValue.Enable.ToString())
                    {
                        return true;
                    } //else
                } //else

                Global.AjinMotionH = new AjinMotion(Global.ConfigGeneral.RootPath + Global.ConfigGeneral.AjinMotionHTagFilePath, isSimulation);

                Thread.Sleep(_interval);

                // Open
                if (Global.AjinMotionH.SetsyDeviceMotion(true) == true)
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

        public bool LoadDeviceMoitionR()
        {
            try
            {
                bool isSimulation = Global.ConfigGeneral.RunMode == Idx.RunMode.Simulation ? true : false;

                if (Global.AjinMotionR != null)
                {
                    Tag tempTag = (Tag)Global.AjinMotionR.syDeviceMotion;
                    if (tempTag.Is.ToString() == Idx.DigitalValue.Enable.ToString())
                    {
                        return true;
                    } //else
                } //else

                Global.AjinMotionR = new AjinMotion(Global.ConfigGeneral.RootPath + Global.ConfigGeneral.AjinMotionRTagFilePath, isSimulation);

                Thread.Sleep(_interval);

                // Open
                if (Global.AjinMotionR.SetsyDeviceMotion(true) == true)
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

        public bool LoadTemp1500()
        {
            try
            {
                bool isSimulation = Global.ConfigGeneral.RunMode == Idx.RunMode.Simulation ? true : false;
                if (Global.ConfigSamwonTemp1500.RunMode == Idx.RunMode.Simulation)
                {
                    isSimulation = true;
                } // else

                if (Global.SamwonTemp1500 != null)
                {
                    Tag tempTag = (Tag)Global.SamwonTemp1500.syDeviceTemp1500;
                    if (tempTag.Is != null &&
                        tempTag.Is.ToString() == Idx.DigitalValue.Enable.ToString())
                    {
                        return true;
                    } // else
                }
                else
                {
                    Global.SamwonTemp1500 = new SamwonTemp1500(Global.ConfigGeneral.RootPath + Global.ConfigGeneral.SamwonTemp1500TagFilePath, isSimulation);
                }

                Global.SamwonTemp1500.yTemp1500Comport = Global.ConfigSamwonTemp1500.Comport.Clone();

                Thread.Sleep(_interval);

                // Open
                if (Global.SamwonTemp1500.SetsyDeviceTemp1500(true) == true)
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

        public bool LoadSDR100()
        {
            try
            {
                bool isSimulation = Global.ConfigGeneral.RunMode == Idx.RunMode.Simulation ? true : false;
                if (Global.ConfigSamwonSDR100.RunMode == Idx.RunMode.Simulation)
                {
                    isSimulation = true;
                } // else

                if (Global.SamwonSDR100 != null)
                {
                    Tag tempTag = (Tag)Global.SamwonSDR100.syDeviceSDR100;
                    if (tempTag.Is != null &&
                        tempTag.Is.ToString() == Idx.DigitalValue.Enable.ToString())
                    {
                        return true;
                    } // else
                }
                else
                {
                    Global.SamwonSDR100 = new SamwonSDR100(Global.ConfigGeneral.RootPath + Global.ConfigGeneral.SamwonSDR100TagFilePath, isSimulation);
                }

                Global.SamwonSDR100.ySDR100Comport = Global.ConfigSamwonSDR100.Comport.Clone();

                Thread.Sleep(_interval);

                // Open
                if (Global.SamwonSDR100.SetsyDeviceSDR100(true) == true)
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
