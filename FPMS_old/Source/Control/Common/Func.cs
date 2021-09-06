using EmWorks.Lib.Common;
using EmWorks.Lib.Identity;
using System;
using System.Collections.Generic;
using System.Windows.Threading;

namespace FPMS.E2105_FS111_121
{
    public static class Func
    {
        #region Fields

        private static DispatcherOperationCallback exitFrameCallback = new DispatcherOperationCallback(ExitFrame);

        #endregion Fields

        #region Methods

        public static bool CheckUserLevel(string userLevel, string limitLevel)
        {
            try
            {
                return Convert.ToInt32(userLevel) <= Convert.ToInt32(limitLevel) ? false : true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        /// <summary>
        /// Processes all UI messages currently in the message queue.
        /// </summary>
        public static void DoEvents()
        {
            try
            {
                // Create new nested message pump.
                DispatcherFrame nestedFrame = new DispatcherFrame();

                // Dispatch a callback to the current message queue, when getting called,
                // this callback will end the nested message loop.
                // note that the priority of this callback should be lower than the that of UI event messages.
                DispatcherOperation exitOperation = Dispatcher.CurrentDispatcher.BeginInvoke(
                    DispatcherPriority.Background, exitFrameCallback, nestedFrame);

                // pump the nested message loop, the nested message loop will
                // immediately process the messages left inside the message queue.
                Dispatcher.PushFrame(nestedFrame);

                // If the "exitFrame" callback doesn't get finished, Abort it.
                if (exitOperation.Status != DispatcherOperationStatus.Completed)
                {
                    exitOperation.Abort();
                }
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        private static Object ExitFrame(Object state)
        {
            DispatcherFrame frame = state as DispatcherFrame;

            // Exit the nested message loop.
            frame.Continue = false;

            return null;
        }

        #endregion Methods
    }
}