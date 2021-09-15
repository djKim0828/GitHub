using EmWorks.Lib.Common;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace FPMS.E2105_FS111_121
{
    public class MtChamber : EmWorks.View.MtChamberWindow
    {
        #region Fields

        private Comport _sdr100Comport;
        private Comport _temp1500Comport;

        #endregion Fields

        #region Properties

        public bool _IsInitialled { get; protected set; }

        #endregion Properties

        #region Constructors

        public MtChamber()
        {
            Initialize();
        }

        #endregion Constructors

        #region Destructors

        ~MtChamber()
        {
        }

        #endregion Destructors

        #region Methods

        private void BtnSDR100Connect_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //Todo:Comport View의 Parameter를 Model에 저장하는 함수 호출 필요
            //Global.ConfigSamwonSDR100.Comport = mtSDR100Comport.Loaded();
            Global.ConfigSamwonSDR100.Save();
            Global.SamwonSDR100.ySDR100Comport = Global.ConfigSamwonSDR100.Comport.Clone();

            if (Global.SamwonSDR100.sxDeviceSDR100.Is.ToString() != Idx.OpenClose.Open.ToString())
            {
                if (Global.SamwonSDR100.SetsyDeviceSDR100(true) == true)
                {
                    txtMessage.addText("Ok> Connect");
                }
                else
                {
                    txtMessage.addText("Error> Connect");
                }
            }
            else
            {
                if (Global.SamwonSDR100.SetsyDeviceSDR100(false) == true)
                {
                    txtMessage.addText("Ok> Disconnect");
                }
                else
                {
                    txtMessage.addText("Error> Disconnect");
                }
            }
        }

        private void BtnTemp1500Connect_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //Todo:Comport View의 Parameter를 Model에 저장하는 함수 호출 필요
            //Global.ConfigSamwonTemp1500.Comport = mtTemp1500Comport.Loaded();
            Global.ConfigSamwonTemp1500.Save();
            Global.SamwonTemp1500.yTemp1500Comport = Global.ConfigSamwonTemp1500.Comport.Clone();

            if (Global.SamwonTemp1500.sxDeviceTemp1500.Is.ToString() != Idx.OpenClose.Open.ToString())
            {
                if (Global.SamwonTemp1500.SetsyDeviceTemp1500(true) == true)
                {
                    txtMessage.addText("Ok> Connect");
                }
                else
                {
                    txtMessage.addText("Error> Connect");
                }
            }
            else
            {
                if (Global.SamwonTemp1500.SetsyDeviceTemp1500(false) == true)
                {
                    txtMessage.addText("Ok> Disconnect");
                }
                else
                {
                    txtMessage.addText("Error> Disconnect");
                }
            }
        }

        private void BtnTemp1500Run_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Global.SamwonTemp1500.SetyTemp1500RunStop(true) == true)
            {
                txtMessage.addText("Ok> Run");
            }
            else
            {
                txtMessage.addText("Error> Run");
            }
        }

        private void BtnTemp1500SetSV_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (txtTemp1500SetSV.Text != "")
            {
                if (Global.SamwonTemp1500.SetyTemp1500SetSV(double.Parse(txtTemp1500SetSV.Text.Trim())) == true)
                {
                    txtMessage.addText("Ok> SetSV");
                }
                else
                {
                    txtMessage.addText("Error> SetSV");
                }
            }
        }

        private void BtnTemp1500Stop_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Global.SamwonTemp1500.SetyTemp1500RunStop(false) == true)
            {
                txtMessage.addText("Ok> Stop");
            }
            else
            {
                txtMessage.addText("Error> Stop");
            }
        }

        private int InitControls()
        {
            grdTemp1500.Children.Clear();
            grdTemp1500.Children.Add(_temp1500Comport = new Comport(Global.ConfigSamwonTemp1500.Comport, Global.ConfigSamwonTemp1500.DefaultComport));
            grdSDR100.Children.Clear();
            grdSDR100.Children.Add(_sdr100Comport = new Comport(Global.ConfigSamwonTemp1500.Comport, Global.ConfigSamwonTemp1500.DefaultComport));

            return Idx.FunctionResult.True;
        }

        private void Initialize()
        {
            int resultInstance = InitInstance();
            int resultControls = InitControls();
            int resultEvent = RegistEvents();

            if (resultInstance == Idx.FunctionResult.True &&
                resultControls == Idx.FunctionResult.True &&
                resultEvent == Idx.FunctionResult.True)
            {
                _IsInitialled = true;
            }
            else
            {
                _IsInitialled = false;
            }
        }

        private int InitInstance()
        {
            try
            {
                _IsInitialled = false;

                return Idx.FunctionResult.True;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);

                return Idx.FunctionResult.False;
            }
        }

        private int RegistEvents()
        {
            btnTemp1500Connect.Click += BtnTemp1500Connect_Click;
            btnTemp1500Run.Click += BtnTemp1500Run_Click;
            btnTemp1500Stop.Click += BtnTemp1500Stop_Click;
            btnTemp1500SetSV.Click += BtnTemp1500SetSV_Click;
            txtTemp1500SetSV.KeyDown += TxtTemp1500SetSV_KeyDown;

            btnSDR100Connect.Click += BtnSDR100Connect_Click;

            Global.SamwonTemp1500.sxDeviceTemp1500.OnChanged += SxDeviceTemp1500_OnChanged;
            Global.SamwonTemp1500.xTemp1500GetPV.OnChanged += XTemp1500GetPV_OnChanged;
            Global.SamwonTemp1500.xTemp1500GetSV.OnChanged += XTemp1500GetSV_OnChanged;

            Global.SamwonSDR100.sxDeviceSDR100.OnChanged += SxDeviceSDR100_OnChanged;
            Global.SamwonSDR100.xSDR100GetPV1.OnChanged += XSDR100GetPV1_OnChanged;
            Global.SamwonSDR100.xSDR100GetPV2.OnChanged += XSDR100GetPV2_OnChanged;
            Global.SamwonSDR100.xSDR100GetPV3.OnChanged += XSDR100GetPV3_OnChanged;
            Global.SamwonSDR100.xSDR100GetPV4.OnChanged += XSDR100GetPV4_OnChanged;
            Global.SamwonSDR100.xSDR100GetPV5.OnChanged += XSDR100GetPV5_OnChanged;
            Global.SamwonSDR100.xSDR100GetPV6.OnChanged += XSDR100GetPV6_OnChanged;

            return Idx.FunctionResult.True;
        }

        private void SxDeviceSDR100_OnChanged(object sendor, EmWorks.Lib.Core.EventTagChangedArgs e)
        {
            if (Global.SamwonSDR100 != null &&
                Global.SamwonSDR100.sxDeviceSDR100 != null &&
                Global.SamwonSDR100.sxDeviceSDR100.Is != null)
            {
                string connect = Global.SamwonSDR100.sxDeviceSDR100.Is.ToString();
                
                if (connect == Idx.DeviceOpen.Open) 
                {
                    this.Dispatcher.Invoke(new System.Windows.Forms.MethodInvoker(delegate ()
                    {
                        btnSDR100Connect.Content = i18n.GetLanguage("Disconnect");
                    }));
                }
                else
                {
                    this.Dispatcher.Invoke(new System.Windows.Forms.MethodInvoker(delegate ()
                    {
                        btnSDR100Connect.Content = i18n.GetLanguage("Connect");
                    }));
                }
            } // else
        }

        private void SxDeviceTemp1500_OnChanged(object sendor, EmWorks.Lib.Core.EventTagChangedArgs e)
        {
            if (Global.SamwonTemp1500 != null &&
                Global.SamwonTemp1500.sxDeviceTemp1500 != null &&
                Global.SamwonTemp1500.sxDeviceTemp1500.Is != null)
            {
                string connect = Global.SamwonTemp1500.sxDeviceTemp1500.Is.ToString();

                if (connect == Idx.DeviceOpen.Open)
                {
                    this.Dispatcher.Invoke(new System.Windows.Forms.MethodInvoker(delegate ()
                    {
                        btnTemp1500Connect.Content = i18n.GetLanguage("Disconnect");
                    }));
                }
                else
                {
                    this.Dispatcher.Invoke(new System.Windows.Forms.MethodInvoker(delegate ()
                    {
                        btnTemp1500Connect.Content = i18n.GetLanguage("Connect");
                    }));
                }
            } // else
        }

        private void TxtTemp1500SetSV_KeyDown(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;

            if (!(char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key))))
            {
                e.Handled = true;
                if ((textBox != null && textBox.Text != "" && false == (textBox.Text.IndexOf('.') != -1) && e.Key == Key.Decimal) ||
                    (Key.NumPad0 <= e.Key && e.Key <= Key.NumPad9))
                {
                    e.Handled = false;
                } // else
            } // else
        }

        private void XSDR100GetPV1_OnChanged(object sendor, EmWorks.Lib.Core.EventTagChangedArgs e)
        {
            if (Global.SamwonSDR100 != null &&
                Global.SamwonSDR100.xSDR100GetPV1 != null &&
                Global.SamwonSDR100.xSDR100GetPV1.Is != null)
            {
                string pv = Global.SamwonSDR100.xSDR100GetPV1.Is.ToString();

                this.Dispatcher.Invoke(new System.Windows.Forms.MethodInvoker(delegate ()
                {
                    lblSDR100GetPV1.Content = pv;
                }));
            } // else
        }

        private void XSDR100GetPV2_OnChanged(object sendor, EmWorks.Lib.Core.EventTagChangedArgs e)
        {
            if (Global.SamwonSDR100 != null &&
                Global.SamwonSDR100.xSDR100GetPV2 != null &&
                Global.SamwonSDR100.xSDR100GetPV2.Is != null)
            {
                string pv = Global.SamwonSDR100.xSDR100GetPV2.Is.ToString();

                this.Dispatcher.Invoke(new System.Windows.Forms.MethodInvoker(delegate ()
                {
                    lblSDR100GetPV2.Content = pv;
                }));
            } // else
        }

        private void XSDR100GetPV3_OnChanged(object sendor, EmWorks.Lib.Core.EventTagChangedArgs e)
        {
            if (Global.SamwonSDR100 != null &&
                Global.SamwonSDR100.xSDR100GetPV3 != null &&
                Global.SamwonSDR100.xSDR100GetPV3.Is != null)
            {
                string pv = Global.SamwonSDR100.xSDR100GetPV3.Is.ToString();

                this.Dispatcher.Invoke(new System.Windows.Forms.MethodInvoker(delegate ()
                {
                    lblSDR100GetPV3.Content = pv;
                }));
            } // else
        }

        private void XSDR100GetPV4_OnChanged(object sendor, EmWorks.Lib.Core.EventTagChangedArgs e)
        {
            if (Global.SamwonSDR100 != null &&
                Global.SamwonSDR100.xSDR100GetPV4 != null &&
                Global.SamwonSDR100.xSDR100GetPV4.Is != null)
            {
                string pv = Global.SamwonSDR100.xSDR100GetPV4.Is.ToString();

                this.Dispatcher.Invoke(new System.Windows.Forms.MethodInvoker(delegate ()
                {
                    lblSDR100GetPV4.Content = pv;
                }));
            } // else
        }

        private void XSDR100GetPV5_OnChanged(object sendor, EmWorks.Lib.Core.EventTagChangedArgs e)
        {
            if (Global.SamwonSDR100 != null &&
                Global.SamwonSDR100.xSDR100GetPV5 != null &&
                Global.SamwonSDR100.xSDR100GetPV5.Is != null)
            {
                string pv = Global.SamwonSDR100.xSDR100GetPV5.Is.ToString();

                this.Dispatcher.Invoke(new System.Windows.Forms.MethodInvoker(delegate ()
                {
                    lblSDR100GetPV5.Content = pv;
                }));
            } // else
        }

        private void XSDR100GetPV6_OnChanged(object sendor, EmWorks.Lib.Core.EventTagChangedArgs e)
        {
            if (Global.SamwonSDR100 != null &&
                Global.SamwonSDR100.xSDR100GetPV6 != null &&
                Global.SamwonSDR100.xSDR100GetPV6.Is != null)
            {
                string pv = Global.SamwonSDR100.xSDR100GetPV6.Is.ToString();

                this.Dispatcher.Invoke(new System.Windows.Forms.MethodInvoker(delegate ()
                {
                    lblSDR100GetPV6.Content = pv;
                }));
            } // else
        }

        private void XTemp1500GetPV_OnChanged(object sendor, EmWorks.Lib.Core.EventTagChangedArgs e)
        {
            if (Global.SamwonTemp1500 != null &&
                Global.SamwonTemp1500.xTemp1500GetPV != null &&
                Global.SamwonTemp1500.xTemp1500GetPV.Is != null)
            {
                string pv = Global.SamwonTemp1500.xTemp1500GetPV.Is.ToString();

                this.Dispatcher.Invoke(new System.Windows.Forms.MethodInvoker(delegate ()
                {
                    lblTemp1500GetPV.Content = pv;
                }));
            } // else
        }

        private void XTemp1500GetSV_OnChanged(object sendor, EmWorks.Lib.Core.EventTagChangedArgs e)
        {
            if (Global.SamwonTemp1500 != null &&
                Global.SamwonTemp1500.xTemp1500GetSV != null &&
                Global.SamwonTemp1500.xTemp1500GetSV.Is != null)
            {
                string sv = Global.SamwonTemp1500.xTemp1500GetSV.Is.ToString();

                this.Dispatcher.Invoke(new System.Windows.Forms.MethodInvoker(delegate ()
                {
                    lblTemp1500GetSV.Content = sv;
                }));
            } // else
        }

        #endregion Methods
    }
}
