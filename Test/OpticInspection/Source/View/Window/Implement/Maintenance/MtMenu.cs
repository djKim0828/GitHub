using System.Threading;

namespace EmWorks.App.OpticInspection
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="MtMenu"/>
    /// Author : Andrew, Yoon
    /// Description : object detail description.
    /// </summary>
    public class MtMenu : View.Window.Design.Maintenance.MtMenuWindow
    {
        #region locale variable

        private bool isLoop;
        private int locale;
        private int loopInterval;

        #endregion locale variable

        #region member properties

        public bool IsInitialled { get; protected set; }

        #endregion member properties

        #region EmWorks convention functions

        /// <summary>
        /// Author : Andrew, Yoon
        /// Description :  objcet constructor.
        /// </summary>
        public MtMenu()
        {
            Initialize();
            // add your code here
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description :  object destructor.
        /// </summary>
        ~MtMenu()
        {
            isLoop = false;
            // add your code here
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description : EmWorks base thread.
        /// </summary>
        private void EmWorksProc(object state)
        {
            while (isLoop)
            {
                // add your code here
                Thread.Sleep(loopInterval);
            }
        }

        /// <summary>
        /// Author : Andrew, Yoon
        /// Description : controls initialization.
        /// </summary>
        private int InitControls()
        {
            // add your code here

            return Idx.FunctionResult.True;
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description :  object initialization.
        /// </summary>
        private void Initialize()
        {
            int resultInstance = resultInstance = InitInstance();
            int resultControls = InitControls();
            int resultEvent = RegistEvents();

            if (resultInstance == Idx.FunctionResult.True && resultControls == Idx.FunctionResult.True && resultEvent == Idx.FunctionResult.True)
            {
                IsInitialled = true;
                ThreadPool.QueueUserWorkItem(EmWorksProc);
            }
            else
            {
                IsInitialled = false;
            }

            // add your code here
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description :  Instance initialization.
        /// </summary>
        private int InitInstance()
        {
            IsInitialled = false;
            loopInterval = 5;
            isLoop = false; // if do you want to use the EmWorksProc(), chage to true.
                            // add your code here

            return Idx.FunctionResult.True;
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            // add your code here

            return Idx.FunctionResult.True;
        }

        #endregion EmWorks convention functions
    }
}