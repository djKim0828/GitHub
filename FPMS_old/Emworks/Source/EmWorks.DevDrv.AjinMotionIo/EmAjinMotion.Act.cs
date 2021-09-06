using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using System;

namespace EmWorks.DevDrv.AjinMotionIo
{
    public partial class EmAjinMotion
    {

        public uint HomeGetVel(Tag tag)
        {
            int axisno = tag.Identity.Index;

            double vel1st = 0, vel2nd = 0, vel3rd = 0, vel4th = 0, acc1st = 0, acc2nd = 0;

            try
            {
                FuncRt = CAXM.AxmHomeGetVel(axisno, ref vel1st, ref vel2nd, ref vel3rd, ref vel4th, ref acc1st, ref acc2nd);
                if (FuncRt == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                {
                    _MotionPropertys[axisno].xHomeVelocity1 = vel1st;
                    _MotionPropertys[axisno].xHomeVelocity2 = vel2nd;
                    _MotionPropertys[axisno].xHomeVelocity3 = vel3rd;
                    _MotionPropertys[axisno].xHomeVelocity4 = vel4th;
                    _MotionPropertys[axisno].xHomeAccel1 = acc1st;
                    _MotionPropertys[axisno].xHomeAccel2 = acc2nd;
                }
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
            return FuncRt;
        }

        public uint HomeSetStart(Tag tag)
        {
            int axisno = tag.Identity.Index;
            int offset = tag.Identity.Offset;

            try
            {
                if (tag.d == 1)
                {
                    FuncRt = CAXM.AxmHomeSetStart(axisno);    
                }
                else
                {
                    FuncRt = CAXM.AxmMoveEStop(axisno);
                }
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }

            return FuncRt;
        }

        /// <summary>
        /// only set value
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public uint HomeSetVel(Tag tag)
        {
            int axisno = tag.Identity.Index;            

            try
            {
                FuncRt = CAXM.AxmHomeSetVel(axisno,
                                            _MotionPropertys[axisno].yHomeVelocity1,
                                            _MotionPropertys[axisno].yHomeVelocity2,
                                            _MotionPropertys[axisno].yHomeVelocity3,
                                            _MotionPropertys[axisno].yHomeVelocity4,
                                            _MotionPropertys[axisno].yHomeAccel1,
                                            _MotionPropertys[axisno].yHomeAccel2);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
            return FuncRt;
        }

        public uint MoveStartPos(Tag tag)
        {
            int axisno = tag.Identity.Index;
            
            _MotionPropertys[axisno].CmdPos = tag.d; // Todo : 왜 넣는걸까? 장비 테스트시 확인 필요

            try
            {                
                FuncRt = CAXM.AxmMoveStartPos(axisno,
                                                _MotionPropertys[axisno].CmdPos,
                                                _MotionPropertys[axisno].Velocity,
                                                _MotionPropertys[axisno].Accel,
                                                _MotionPropertys[axisno].Decel);

                return FuncRt;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return FuncRt;
            }
        }

        public uint TriggerSetTimeLevel(Tag tag)
        {
            int axisno = tag.Identity.Index;
            
            try
            {
                FuncRt = CAXM.AxmTriggerSetTimeLevel(axisno,
                                                _MotionPropertys[axisno].TriggerTime,
                                                (uint)AXT_MOTION_LEVEL_MODE.HIGH,
                                                (uint)AXT_MOTION_SELECTION.ACTUAL,
                                                (uint)AXT_USE.ENABLE);

                return FuncRt;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return FuncRt;
            }
        }

        public uint TriggerSetBlock(Tag tag)
        {
            int axisno = tag.Identity.Index;

            try
            {
                FuncRt = CAXM.AxmTriggerSetBlock(axisno,
                                                _MotionPropertys[axisno].TriggerStartPos,
                                                _MotionPropertys[axisno].TriggerEndPos,
                                                _MotionPropertys[axisno].TriggerPeriodPos);

                return FuncRt;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return FuncRt;
            }
        }


        public uint MoveVel(Tag tag)
        {
            //++ 지정한 축을 (+)방향으로 지정한 속도/가속도/감속도로 모션구동합니다.
            int axisno = tag.Identity.Index;

            try
            {
                _MotionPropertys[axisno].JogVelocity = tag.d;

                FuncRt = CAXM.AxmMoveVel(axisno,
                                        _MotionPropertys[axisno].JogVelocity,
                                        _MotionPropertys[axisno].JogAccel,
                                        _MotionPropertys[axisno].JogDecel);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }

            return FuncRt;
        }

    }
}