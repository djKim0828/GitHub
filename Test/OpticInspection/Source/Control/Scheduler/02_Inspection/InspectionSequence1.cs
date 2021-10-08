using EmWorks.Lib.Common;
using System.Threading;

namespace EmWorks.App.OpticInspection
{
    public class InspectionSequence1
    {

        public bool StartInspection()
        {
           CommandCenter._InspectionCommander.LoadRecipeSR5000(); // 검사 시작시에 한번 로드한다.

            // 레시피를 읽고 레시피의 검사 위치 갯수 만큼 동작
            foreach (ModelRecipe.LedItem ldItem in CommandCenter._Recipe._Item)
            {
                // 미세 움직임
                CommandCenter.ChangeFov(ldItem.FovNumber);

                if (MoveCameraInspectionPos() == false)
                {
                    //알람 발생?
                    return false;
                } // else

                if (InspectionWithCamera() == false)
                {
                    //알람 발생?
                    return false;
                } // else

                if (MoveSR5000InspectionPos() == false)
                {
                    //알람 발생?
                    return false;
                } // else

                if (InspectionWithSR5000(ldItem) == false)
                {
                    //알람 발생?
                    return false;
                } // else

            }

            MoveWait();

            return true;
        }

        private bool InspectionWithCamera()
        {
            // 카메라 촬영, Align
            Logger.Debug("Start InspectionWithCamera");

            MoveCameraDown();

            // 현재 Spec 정의가 없으므로 Skip
            Thread.Sleep(500);

            MoveCameraUp();

            return true;
        }

        private bool InspectionWithSR5000(ModelRecipe.LedItem ldItem)
        {
            //SR-5000 검사
            Logger.Debug("Start InspectionWithSR5000");

            MoveSR5000Down();

            CommandCenter._InspectionCommander.StartSR5000(ldItem);

            MoveSR5000Up();

            return true;
        }

        private bool MoveCameraDown()
        {
            Logger.Debug("Start MoveCameraDown");

            if (CommandCenter._InspectionCommander.Move(Idx.MotionAxisNo.CameraZ4, "inspection") == false)
            {
                return false;
            } // else

            return true;
        }

        private bool MoveCameraInspectionPos()
        {
            Logger.Debug("Start Func1_MoveCameraInspectionPos");

            if (CommandCenter._InspectionCommander.Move(Idx.MotionAxisNo.InspectorX3, "Camera") == false)
            {
                return false;
            } // else

            return true;
        }

        private bool MoveCameraUp()
        {
            Logger.Debug("Start MoveCameraUp");

            if (CommandCenter._InspectionCommander.Move(Idx.MotionAxisNo.CameraZ4, "min") == false)
            {
                return false;
            } // else

            return true;
        }

        private bool MoveSR5000Down()
        {
            Logger.Debug("Start MoveSR5000Down");

            if (CommandCenter._InspectionCommander.Move(Idx.MotionAxisNo.Sr5000Z5, "inspection") == false)
            {
                return false;
            } // else

            return true;
        }

        private bool MoveSR5000InspectionPos()
        {
            Logger.Debug("Start MoveSR5000InspectionPos");

            if (CommandCenter._InspectionCommander.Move(Idx.MotionAxisNo.InspectorX3, "SR5000") == false)
            {
                return false;
            }

            Thread.Sleep(1000);

            return true;
        }

        private bool MoveSR5000Up()
        {
            Logger.Debug("Start MoveSR5000Up");

            if (CommandCenter._InspectionCommander.Move(Idx.MotionAxisNo.Sr5000Z5, "min") == false)
            {
                return false;
            } // else

            return true;
        }

        private bool MoveWait()
        {
            Logger.Debug("Start MoveWait");

            if (CommandCenter._InspectionCommander.Move(Idx.MotionAxisNo.InspectorX3, "min", 10000, true) == false)
            {
                return false;
            } // else

            return true;
        }

    }
}