using EmWorks.Lib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmWorks.App.OpticInspection
{
    public class ShuttleSequence1
    {        
        public bool Func1_ExistWafer()
        {
            Logger.Debug("Start Func1_ExistWafer");

            // 현재 Spec 에서는 Wrap의 유무를 알 수 없음
            // IO 등을 확인하여 추가 필요함.
            //if ()

            // 있으면 Cell Id를 변경한다.
            CommandCenter._ShuttleCommander.UpdateCellId();
            return true;                           
        }

        public bool Func2_insertWafer()
        {
            Logger.Debug("Start Func2_insertWafer");

            if (CommandCenter._ShuttleCommander.Move(Idx.MotionAxisNo.ShuttleX1, "insert") == false)
            {
                return false;
            } // else

            if (CommandCenter._ShuttleCommander.Move(Idx.MotionAxisNo.ShuttleY2, "insert") == false)
            {
                return false;
            } // else

            Thread.Sleep(500);
            
            return true;
        }


    }
}
