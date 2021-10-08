using EmWorks.Lib.Common;
using System;
using System.Collections.Generic;

namespace EmWorks.App.OpticInspection
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="LogSearch"/>
    /// Author : DonhJun.Kim
    /// Date : 2020-0928 17:00
    /// Description : object detail description.
    /// </summary>
    public class LogSearch : EmWorks.View.LogSearchDialog
    {
        #region Fields

        private List<Logger.ReadLogData> LogDatas;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Author : DonhJun.Kim
        /// Date :  2020-0928 17:00
        /// Description :  objcet constructor.
        /// </summary>
        public LogSearch(List<Logger.ReadLogData> logDatas, String searchLog)
        {
            this.LogDatas = logDatas;

            Initialize();

            if (searchLog != null)
            {
                SearchLog(searchLog);
            }
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Author :  DonhJun.Kim
        /// Date :  2020-0928 17:00
        /// Description :  object destructor.
        /// </summary>
        ~LogSearch()
        {
        }

        #endregion Destructors

        #region Delegates

        public delegate void EventSetSelectLogHandler(object sendor, EventSetSelectLogMessageArgs e);

        #endregion Delegates

        #region Events

        public event EventSetSelectLogHandler EventSetSelectLog;

        #endregion Events

        #region Properties

        public bool IsInitialled { get; protected set; }

        #endregion Properties

        #region Methods

        private void BtnDlgClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void Dialog_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            InitLocale();
        }

        private void Dialog_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Author : DonhJun.Kim
        /// Date :  2020-0928 17:00
        /// Description : controls initialization.
        /// </summary>
        private int InitControls()
        {
            return Idx.FunctionResult.True;
        }

        /// <summary>
        /// Author :  DonhJun.Kim
        /// Date :  2020-0928 17:00
        /// Description :  object initialization.
        /// </summary>
        private void Initialize()
        {
            int resultInstance = InitInstance();
            int resultControls = InitControls();
            int resultEvent = RegistEvents();

            if (resultInstance == Idx.FunctionResult.True && resultControls == Idx.FunctionResult.True && resultEvent == Idx.FunctionResult.True)
            {
                IsInitialled = true;
            }
            else
            {
                IsInitialled = false;
            }

            // add your code here
        }

        /// <summary>
        /// Author :  DonhJun.Kim
        /// Date :  2020-0928 17:00
        /// Description :  Instance initialization.
        /// </summary>
        private int InitInstance()
        {
            try
            {
                IsInitialled = false;

                return Idx.FunctionResult.True;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return Idx.FunctionResult.False;
            }
        }

        /// <summary>
        /// Author :  DonhJun.Kim
        /// Date :  2020-0928 17:00
        /// Description : location(language) initialization.
        /// </summary>
        private int InitLocale()
        {
            AutoLocale autolocale = new AutoLocale();
            autolocale.Start(this);

            return Idx.FunctionResult.True;
        }

        private void LvLog_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lvLog.SelectedItems == null || lvLog.SelectedItems.Count == 0)
            {
                return;
            } // else

            Logger.ReadLogData selectedLog = lvLog.SelectedItems[0] as Logger.ReadLogData;

            EventSetSelectLog?.Invoke(this, new EventSetSelectLogMessageArgs(selectedLog));
        }

        /// <summary>
        /// Author :  DonhJun.Kim
        /// Date :  2020-0928 17:00
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            this.Loaded += Dialog_Loaded;
            this.PreviewKeyDown += Dialog_PreviewKeyDown;
            this.MouseMove += Dialog_MouseMove;

            this.btnDlgClose.Click += BtnDlgClose_Click;

            this.txtSearchLog.PreviewKeyDown += TxtSearchLog_PreviewKeyDown;
            this.lvLog.MouseDoubleClick += LvLog_MouseDoubleClick;

            return Idx.FunctionResult.True;
        }

        private void Dialog_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                System.Windows.Point Temp = e.GetPosition(this);
                if (Temp.Y < 50)
                {
                    DragMove();
                }
            }
        }

        private void SearchLog(string searchLog)
        {
            List<Logger.ReadLogData> resultLogList = LogDatas.FindAll(item => item.Message.ToUpper().Contains(searchLog.ToUpper()));

            lvLog.ItemsSource = resultLogList;

            txtSearchLog.Text = string.Empty;
        }

        private void TxtSearchLog_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                SearchLog(txtSearchLog.Text);
            }
        }

        #endregion Methods

        #region Classes

        public class EventSetSelectLogMessageArgs : EventArgs
        {
            #region Fields

            public Logger.ReadLogData SelectLog;

            #endregion Fields

            #region Constructors

            public EventSetSelectLogMessageArgs(Logger.ReadLogData selectLog)
            {
                Initialize();

                SelectLog = selectLog;
            }

            #endregion Constructors

            #region Properties

            public DateTime OccurredTime { get; protected set; }

            public string Time
            {
                get
                {
                    return OccurredTime.ToString("MMdd_hhmm_ss.fff");
                }
            }

            #endregion Properties

            #region Methods

            protected virtual void Initialize()
            {
                OccurredTime = DateTime.Now;
            }

            protected virtual void RecordLog()
            {
            }

            #endregion Methods
        }

        #endregion Classes
    }
}