using System.Threading;

namespace EmWorks.App.OpticInspection
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="RpMenu"/>
    /// Author : Andrew, Yoon
    /// Description : object detail description.
    /// </summary>
    public class RpMenu : View.Window.Design.Recipe.RpMenuWindow
    {
        #region locale variable

        private bool _isLoop;
        private int _locale;
        private int _loopInterval;

        #endregion locale variable

        #region member properties

        public bool _IsInitialled { get; protected set; }

        #endregion member properties

        #region EmWorks convention functions

        /// <summary>
        /// Author : Andrew, Yoon
        /// Description :  objcet constructor.
        /// </summary>
        public RpMenu()
        {
            Initialize();
            // add your code here
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description :  object destructor.
        /// </summary>
        ~RpMenu()
        {
            _isLoop = false;
            // add your code here
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description : EmWorks base thread.
        /// </summary>
        private void EmWorksProc(object state)
        {
            while (_isLoop)
            {
                // add your code here
                Thread.Sleep(_loopInterval);
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
                _IsInitialled = true;
                ThreadPool.QueueUserWorkItem(EmWorksProc);
            }
            else
            {
                _IsInitialled = false;
            }

            // add your code here
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description :  Instance initialization.
        /// </summary>
        private int InitInstance()
        {
            _IsInitialled = false;
            _loopInterval = 5;
            _isLoop = false; // if do you want to use the EmWorksProc(), chage to true.
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