using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using EmWorks.Lib.Identity;
using System;
using System.Collections.Generic;

namespace FPMS.E2105_FS111_121
{
    internal class JogControl : EmWorks.View.JogControlComponent
    {

        public JogControl()
        {
            Initialize();
        }

        ~JogControl()
        {
        }

        public bool _IsInitialled { get; protected set; }

        private void BtnMinueH_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MoveMotion(Global.AjinMotionH, true);
        }

        private void BtnMinueR_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MoveMotion(Global.AjinMotionR, true);
        }

        private void BtnMinueV_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MoveMotion(Global.AjinMotionV, true);
        }

        private void BtnMinueX_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MoveMotion(Global.AjinMotionX, true);
        }

        private void BtnMinueY_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MoveMotion(Global.AjinMotionY, true);
        }

        private void BtnMinueZ_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MoveMotion(Global.AjinMotionZ, true);
        }

        private void BtnPlusH_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MoveMotion(Global.AjinMotionH, false);
        }

        private void BtnPlusR_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MoveMotion(Global.AjinMotionR, false);
        }

        private void BtnPlusV_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MoveMotion(Global.AjinMotionV, false);
        }

        private void BtnPlusX_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MoveMotion(Global.AjinMotionX, false);
        }

        private void BtnPlusY_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MoveMotion(Global.AjinMotionY, false);
        }

        private void BtnPlusZ_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MoveMotion(Global.AjinMotionZ, false);
        }

        private void BtnStop_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CommandStop();
        }

        private double CalculationMovePos(AjinMotion ajinMotion, string inputValue, bool isMinus)
        {
            try
            {
                double value = double.Parse(inputValue);

                if (isMinus == true)
                {
                    value = value * -1;
                } // else

                // 현재 위치 가져와서 value에 +
                if (ajinMotion.xMotionActual.Is == null)
                {
                    return 0;
                } // else

                double actPos = (double)ajinMotion.xMotionActual.Is;

                return actPos + value;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return 0;
            }
        }

        private void ChangeCurrentPosition(object obj)
        {
            try
            {
                if (obj == null)
                {
                    return;
                } // else

                Tag tag = (Tag)obj;

                if (tag.Is == null)
                {
                    return;
                } // else

                double cmdPosition = 0;
                cmdPosition = (double)tag.Is;

                switch (tag.Identity.Index)
                {
                    case Idx.MotionAxisNo.X:
                        lblCurPosDataX.Content = cmdPosition.ToString();
                        break;

                    case Idx.MotionAxisNo.Y:
                        lblCurPosDataY.Content = cmdPosition.ToString();
                        break;

                    case Idx.MotionAxisNo.Z:
                        lblCurPosDataZ.Content = cmdPosition.ToString();
                        break;

                    case Idx.MotionAxisNo.V:
                        lblCurPosDataV.Content = cmdPosition.ToString();
                        break;

                    case Idx.MotionAxisNo.H:
                        lblCurPosDataH.Content = cmdPosition.ToString();
                        break;

                    case Idx.MotionAxisNo.R:
                        lblCurPosDataR.Content = cmdPosition.ToString();
                        break;

                    default:
                        // n/a
                        break;
                }
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        private bool CheckHoming(AjinMotion ajinMotion)
        {
            try
            {
                object status = null;

                if (ajinMotion.xMotionHomingStatus.Is == null)
                {
                    return false;
                } // else

                status = ajinMotion.xMotionHomingStatus.Is;
                if (status.ToString() == Idx.DigitalValue.Enable.ToString())
                {
                    return true;
                } // else

                return false;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        private bool CheckServoOn(AjinMotion ajinMotion)
        {
            try
            {
                object status = null;

                if (ajinMotion.xMotionServoOn.Is == null)
                {
                    return false;
                } // else

                status = ajinMotion.xMotionServoOn.Is;
                if (status.ToString() == Idx.DigitalValue.Enable.ToString())
                {
                    return true;
                } // else

                return false;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        private void CommandMove(AjinMotion ajinMotion, double position)
        {
            int axisNo = ajinMotion.sxDeviceMotion.Identity.Index;

            MotionPosition mp = GetDefaultPostionData(axisNo);

            if (mp == null)
            {
                return;
            } // else

            ajinMotion.yMotionMoveMode = Convert.ToInt32(Idx.MotionMoveType.AbsoluteType);
            ajinMotion.yMotionVelocity = mp.Speed;
            ajinMotion.yMotionAccel = mp.Accel;
            ajinMotion.yMotionDecel = mp.Decel;

            ajinMotion.yMotionMove = position; // 동작 실행
        }

        private void CommandStop()
        {
            Global.AjinMotionX.yMotionSStop = Idx.DigitalValue.Enable;
            Global.AjinMotionY.yMotionSStop = Idx.DigitalValue.Enable;
            Global.AjinMotionZ.yMotionSStop = Idx.DigitalValue.Enable;
            Global.AjinMotionV.yMotionSStop = Idx.DigitalValue.Enable;
            Global.AjinMotionH.yMotionSStop = Idx.DigitalValue.Enable;
            Global.AjinMotionR.yMotionSStop = Idx.DigitalValue.Enable;
        }

        private void FirstCheck()
        {
            ChangeCurrentPosition(Global.AjinMotionX);
            ChangeCurrentPosition(Global.AjinMotionY);
            ChangeCurrentPosition(Global.AjinMotionZ);
            ChangeCurrentPosition(Global.AjinMotionV);
            ChangeCurrentPosition(Global.AjinMotionH);
            ChangeCurrentPosition(Global.AjinMotionR);
        }

        private MotionPosition GetDefaultPostionData(int axisNo)
        {
            try
            {
                string axisNumber = Idx.MotionInfo.TagAxisFirstName + axisNo.ToString();
                string axisName = ConfigMotion.GetValue(axisNumber, Idx.MotionInfo.Name);

                List<ConfigIdentity> positionList = ConfigMotion.GetValuefromName(Idx.MotionInfo.AxisName, axisName);

                if (positionList.Count < 0)
                {
                    return null;
                } // else

                MotionPosition motionPosition = new MotionPosition();
                foreach (ConfigIdentity item in positionList)
                {
                    motionPosition.Name = ConfigMotion.GetValue(item.Category, Idx.MotionInfo.Name);
                    if (motionPosition.Name == Idx.DefaultPositionName)
                    {
                        motionPosition.AxisName = axisName;
                        motionPosition.CategoryName = item.Category;
                        motionPosition.MoveType = ConfigMotion.GetValue(item.Category, Idx.MotionInfo.MoveType);
                        motionPosition.Position = Convert.ToDouble(ConfigMotion.GetValue(item.Category, Idx.MotionInfo.Position));
                        motionPosition.Speed = Convert.ToDouble(ConfigMotion.GetValue(item.Category, Idx.MotionInfo.Speed));
                        motionPosition.Accel = Convert.ToDouble(ConfigMotion.GetValue(item.Category, Idx.MotionInfo.Accel));
                        motionPosition.Decel = Convert.ToDouble(ConfigMotion.GetValue(item.Category, Idx.MotionInfo.Decel));

                        break;
                    } // else

                    motionPosition = null;
                }

                return motionPosition;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return null;
            }
        }

        private int InitControls()
        {
            cmbStep.Items.Clear();
            cmbStep.Items.Add("0.5");
            cmbStep.Items.Add("1");
            cmbStep.Items.Add("5");
            cmbStep.Items.Add("10");
            cmbStep.Items.Add("50");
            cmbStep.Items.Add("100");
            cmbStep.SelectedIndex = 0;

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

        private void MotionActual_OnChanged(object sendor, EventTagChangedArgs e)
        {
            this.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
                ChangeCurrentPosition(sendor);
            });
        }

        private void MoveMotion(AjinMotion ajinMotion, bool isMinus)
        {
            try
            {
                if (CheckServoOn(ajinMotion) == false)
                {
                    MessageBox.Show("Check the motion status");
                    return;
                } // else

                if (CheckHoming(ajinMotion) == false)
                {
                    MessageBox.Show("Check the motion homing status");
                    return;
                } // else

                double value = CalculationMovePos(ajinMotion, cmbStep.Text.Trim(), isMinus);

                //Todo : Limit Check

                CommandMove(ajinMotion, value);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        private int RegistEvents()
        {
            //XYZ
            btnMinueX.Click += BtnMinueX_Click;
            btnPlusX.Click += BtnPlusX_Click;
            btnMinueY.Click += BtnMinueY_Click;
            btnPlusY.Click += BtnPlusY_Click;
            btnMinueZ.Click += BtnMinueZ_Click;
            btnPlusZ.Click += BtnPlusZ_Click;

            // VHR
            btnMinueV.Click += BtnMinueV_Click;
            btnPlusV.Click += BtnPlusV_Click;
            btnMinueH.Click += BtnMinueH_Click;
            btnPlusH.Click += BtnPlusH_Click;
            btnMinueR.Click += BtnMinueR_Click;
            btnPlusR.Click += BtnPlusR_Click;

            // Stop
            btnStop.Click += BtnStop_Click;

            // Current Pos. Changed
            Global.AjinMotionX.xMotionActual.OnChanged += MotionActual_OnChanged;
            Global.AjinMotionY.xMotionActual.OnChanged += MotionActual_OnChanged;
            Global.AjinMotionZ.xMotionActual.OnChanged += MotionActual_OnChanged;
            Global.AjinMotionV.xMotionActual.OnChanged += MotionActual_OnChanged;
            Global.AjinMotionH.xMotionActual.OnChanged += MotionActual_OnChanged;
            Global.AjinMotionR.xMotionActual.OnChanged += MotionActual_OnChanged;

            return Idx.FunctionResult.True;
        }

    }
}