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
                    _MotionProperty[axisno].xHomeVelocity1 = vel1st;
                    _MotionProperty[axisno].xHomeVelocity2 = vel2nd;
                    _MotionProperty[axisno].xHomeVelocity3 = vel3rd;
                    _MotionProperty[axisno].xHomeVelocity4 = vel4th;
                    _MotionProperty[axisno].xHomeAccel1 = acc1st;
                    _MotionProperty[axisno].xHomeAccel2 = acc2nd;
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
                                            _MotionProperty[axisno].yHomeVelocity1,
                                            _MotionProperty[axisno].yHomeVelocity2,
                                            _MotionProperty[axisno].yHomeVelocity3,
                                            _MotionProperty[axisno].yHomeVelocity4,
                                            _MotionProperty[axisno].yHomeAccel1,
                                            _MotionProperty[axisno].yHomeAccel2);
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
            
            _MotionProperty[axisno].CmdPos = tag.d; // Todo : 왜 넣는걸까? 장비 테스트시 확인 필요

            try
            {                
                FuncRt = CAXM.AxmMoveStartPos(axisno,
                                                _MotionProperty[axisno].CmdPos,
                                                _MotionProperty[axisno].Velocity,
                                                _MotionProperty[axisno].Accel,
                                                _MotionProperty[axisno].Decel);

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
                                                _MotionProperty[axisno].TriggerTime,
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
                                                _MotionProperty[axisno].TriggerStartPos,
                                                _MotionProperty[axisno].TriggerEndPos,
                                                _MotionProperty[axisno].TriggerPeriodPos);

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
                _MotionProperty[axisno].JogVelocity = tag.d;

                FuncRt = CAXM.AxmMoveVel(axisno,
                                        _MotionProperty[axisno].JogVelocity,
                                        _MotionProperty[axisno].JogAccel,
                                        _MotionProperty[axisno].JogDecel);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }

            return FuncRt;
        }

    }
}