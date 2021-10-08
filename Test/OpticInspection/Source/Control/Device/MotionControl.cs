using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using EmWorks.Lib.Identity;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace EmWorks.App.OpticInspection
{
    public class MotionControl
    {
        #region Methods

        public void ChangeCurrentPosition(Slider sld,
                                            Tag tag,
                                            double maxPosition,
                                            double minPosition,
                                            bool isInvertMode = false)
        {
            try
            {
                double actPosition = 0;

                if (tag.Is == null)
                {
                    return;
                } // else

                actPosition = (double)tag.Is;

                double diff = Math.Abs(maxPosition - minPosition);

                // 현재 위치를 백분율로 환산 ( 현재값 환산 - 최저값의 환산값)
                double value = actPosition / diff * 100 - (minPosition / diff * 100);

                if (isInvertMode == true)
                {
                    sld.Value = sld.Maximum - (value * 2);
                }
                else
                {
                    sld.Value = value * 2;
                }
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        public void ChangStatus(ToggleButton tgb, Tag tag, bool isInvert = false)
        {
            if (tag.Is == null)
            {
                tag.Is = Idx.DigitalValue.Disable;
            }// else

            object status = tag.Is;

            if (status.ToString() == Idx.DigitalValue.Enable.ToString())
            {
                tgb.IsChecked = !isInvert;
            }
            else
            {
                tgb.IsChecked = isInvert;
            }
        }

        public void GetMaxMinPosition(string axisName, out double maxPosition, out double minPosition)
        {
            // 계산전 초기값
            minPosition = 1000000;
            maxPosition = -1000000;

            try
            {
                List<ConfigIdentity> CategoryList = ConfigMotion.GetValuefromName(Idx.MotionInfo.AxisName, axisName);

                foreach (ConfigIdentity item in CategoryList)
                {
                    double positon = System.Convert.ToDouble(ConfigMotion.GetValue(item.Category, Idx.MotionInfo.Position));

                    if (positon > maxPosition)
                    {
                        maxPosition = positon;
                    }// else

                    if (positon < minPosition)
                    {
                        minPosition = positon;
                    }// else
                }
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return;
            }
        }

        public void LoadMotionPosition(int motionIndex, ref Dictionary<string, MotionPosition> _PostionList)
        {
            try
            {
                string categoryAxisNumber = Idx.MotionInfo.TagAxisFirstName + motionIndex.ToString();
                string axisName = ConfigMotion.GetValue(categoryAxisNumber, Idx.MotionInfo.Name);

                MotionPosition motionPosition;
                _PostionList = new Dictionary<string, MotionPosition>();

                List<ConfigIdentity> CategoryList = ConfigMotion.GetValuefromName(Idx.MotionInfo.AxisName, axisName);

                foreach (ConfigIdentity item in CategoryList)
                {
                    motionPosition = new MotionPosition();

                    motionPosition.AxisName = axisName;
                    motionPosition.CategoryName = item.Category;
                    motionPosition.Name = ConfigMotion.GetValue(item.Category, Idx.MotionInfo.Name);
                    motionPosition.MoveType = ConfigMotion.GetValue(item.Category, Idx.MotionInfo.MoveType);
                    motionPosition.Position = Convert.ToDouble(ConfigMotion.GetValue(item.Category, Idx.MotionInfo.Position));
                    motionPosition.Speed = Convert.ToDouble(ConfigMotion.GetValue(item.Category, Idx.MotionInfo.Speed));
                    motionPosition.Accel = Convert.ToDouble(ConfigMotion.GetValue(item.Category, Idx.MotionInfo.Accel));
                    motionPosition.Decel = Convert.ToDouble(ConfigMotion.GetValue(item.Category, Idx.MotionInfo.Decel));

                    _PostionList.Add(motionPosition.Name, motionPosition);
                }
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        #endregion Methods
    }
}