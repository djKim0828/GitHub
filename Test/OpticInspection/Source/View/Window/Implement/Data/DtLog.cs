using EmWorks.Lib.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace EmWorks.App.OpticInspection
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="DtLog"/>
    /// Author : Andrew, Yoon
    /// Description : object detail description.
    /// </summary>
    public class DtLog : View.Window.Design.Data.DtLogWindow
    {
        #region locale variable

        private bool _isLoop;
        private JavaScriptSerializer _jsonConverter;
        private int _locale;
        private string _logDirPath;
        private int _loopInterval;
        private TimeDefinition _timeDefinition;

        #endregion locale variable

        #region member properties

        public bool _IsInitialled { get; protected set; }

        #endregion member properties

        #region EmWorks convention functions

        /// <summary>
        /// Author : Andrew, Yoon
        /// Description :  objcet constructor.
        /// </summary>
        public DtLog()
        {
            Initialize();
            // add your code here
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description :  object destructor.
        /// </summary>
        ~DtLog()
        {
            _isLoop = false;
            // add your code here
        }

        internal void ScrollToLog(Logger.ReadLogData selectedLog)
        {
            List<Logger.ReadLogData> allLogData = lvLog.ItemsSource as List<Logger.ReadLogData>;

            if (allLogData == null) return;

            int index = allLogData.FindIndex(x => x.LogTime == selectedLog.LogTime && x.Message == selectedLog.Message);

            if (index == -1) return;

            lvLog.SelectedItem = lvLog.Items[index];
            lvLog.ScrollIntoView(lvLog.Items[index]);
        }

        private void BtnEndtime_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Logger.ReadLogData selectedLogData = GetSelectedLogData();
            _timeDefinition._endtime = selectedLogData.LogTime.ToString("HH:mm:ss.fff");

            DisplayIntervalValue();
        }

        private void BtnOpen_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Process.Start(_logDirPath);
        }

        private void BtnSearch_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SearchLogFiles();
        }

        private void BtnSearchDlg_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            puContentMenu.IsOpen = false;

            Logger.ReadLogData logData = GetSelectedLogData();
            ShowSearchWindow(logData.Message);
        }

        private void BtnStartTime_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Logger.ReadLogData selectedLogData = GetSelectedLogData();
            _timeDefinition._startTime = selectedLogData.LogTime.ToString("HH:mm:ss.fff");

            DisplayIntervalValue();
        }

        private void DisplayIntervalValue()
        {
            puContentMenu.IsOpen = false;
            //Todo
            string information = "Interval : " + _timeDefinition.GetInterval() + ", " +
                                "Start Time : " + _timeDefinition._startTime + ", " +
                                "End Time : " + _timeDefinition._endtime;
            lblOtherInformation.Content = information;
        }

        private void DisplayIntervalValue(string context)
        {
            if (string.IsNullOrEmpty(context))
            {
                return;
            }
            puContentMenu.IsOpen = false;
            lblOtherInformation.Content = context;
        }

        private void DisplaySelectedLog()
        {
            Logger.ReadLogData logData = GetSelectedLogData();

            if (logData != null)
            {
                txtLogDetail.Text = logData.GetDetailInfo();
            }
        }

        private void Dlg_EventSetSelectLog(object sendor, LogSearch.EventSetSelectLogMessageArgs e)
        {
            ScrollToLog(e.SelectLog);
        }

        private void DtLog_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            dtpStartDate.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            dtpEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
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

        private List<Logger.ReadLogData> GetLogDatas(string logFilePath)
        {
            List<Logger.ReadLogData> logDatas = new List<Logger.ReadLogData>();

            // 파일 읽는 루틴이 이상(읽고 파싱)해서 수정 [2020/09/02 djkim]
            //string Alllog = File.ReadAllText(logFilePath);
            //string[] logs = Alllog.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            string[] logs = File.ReadAllLines(logFilePath);

            foreach (var log in logs)
            {
                try
                {
                    Logger.ReadLogData logData = _jsonConverter.Deserialize<Logger.ReadLogData>(log);

                    logDatas.Add(logData);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            return logDatas;
        }

        private Logger.ReadLogData GetSelectedLogData()
        {
            if (lvLog.SelectedItems == null || lvLog.SelectedItems.Count == 0)
            {
                return null;
            }

            Logger.ReadLogData logData = lvLog.SelectedItems[0] as Logger.ReadLogData;
            if (logData == null)
            {
                return null;
            }

            return logData;
        }

        private string GetSelectedLogDirPath()
        {
            string dirName = lstLogDir.SelectedItem.ToString();

            return _logDirPath + "\\" + dirName;
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
            try
            {
                _IsInitialled = false;

                _logDirPath = Global.ConfigGeneral.RootPath + Global.ConfigGeneral.LogFilePath;

                return Idx.FunctionResult.True;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return Idx.FunctionResult.False;
            }
        }

        private bool IsLogFileExist(DirectoryInfo di)
        {
            foreach (FileInfo fi in di.GetFiles())
            {
                if (fi.Extension == ".log")
                {
                    return true;
                }
            }

            return false;
        }

        private bool LoadFromLogDir(string logDirPath)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(logDirPath);

                AllLogDatas allLogDatas = LoadFromLogFiles(dirInfo.GetFiles());

                List<Logger.ReadLogData> AllLogData = allLogDatas.GetAllLogData();

                List<string> userIdList = new List<string>();
                userIdList.Add("All");

                this.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    lvLog.Tag = AllLogData;
                    LoadSelectedLogs();
                });

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        private AllLogDatas LoadFromLogFiles(FileInfo[] fileInfos)
        {
            AllLogDatas allLogDatas = new AllLogDatas();

            foreach (FileInfo fileInfo in fileInfos)
            {
                if (fileInfo.Extension.CompareTo(".log") == 0)
                {
                    allLogDatas.Logs = GetLogDatas(fileInfo.FullName);
                }
            }

            return allLogDatas;
        }

        private void LoadSelectedLogs()
        {
            EmWorks.Lib.Common.Logger.Level selectedLevel = (EmWorks.Lib.Common.Logger.Level)cmbLogType.SelectedItem;

            List<Logger.ReadLogData> allLogData = lvLog.Tag as List<Logger.ReadLogData>;

            if (allLogData == null) return;

            allLogData = allLogData.FindAll(x => x.LogLevel <= selectedLevel);

            lvLog.ItemsSource = allLogData;

            lblAllCount.Content = "Rows=" + allLogData.Count.ToString();
        }

        private void LstLogDir_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lstLogDir.SelectedItem == null) return;

            string selectedLogDirPath = GetSelectedLogDirPath();

            ProcessBox processDialog = new ProcessBox();
            processDialog.TitleMessage = i18n.GetLanguage("Loading");
            processDialog.Topmost = true;
            processDialog.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            Task task = Task.Run(() => LoadFromLogDir(selectedLogDirPath));
            task.GetAwaiter().OnCompleted(() =>
            {
                processDialog.Close();
            });

            processDialog.ShowDialog();
        }

        private void LvLog_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DisplaySelectedLog();
        }

        private void LvLog_PreviewMouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            puContentMenu.IsOpen = true;
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            // add your code here
            this.Loaded += DtLog_Loaded;

            this.btnSearch.Click += BtnSearch_Click;
            this.btnOpen.Click += BtnOpen_Click;

            this.lstLogDir.MouseDoubleClick += LstLogDir_MouseDoubleClick;

            this.lvLog.PreviewMouseRightButtonUp += LvLog_PreviewMouseRightButtonUp;
            this.lvLog.PreviewMouseLeftButtonUp += LvLog_PreviewMouseLeftButtonUp;

            btnStartTime.Click += BtnStartTime_Click;
            btnEndtime.Click += BtnEndtime_Click;
            btnSearchDlg.Click += BtnSearchDlg_Click;
            return Idx.FunctionResult.True;
        }

        private void SearchLogFiles()
        {
            lstLogDir.Items.Clear();

            string[] dirs = Directory.GetDirectories(this._logDirPath);

            foreach (string dirPath in dirs)
            {
                DirectoryInfo di = new DirectoryInfo(dirPath);

                if (IsLogFileExist(di))
                {
                    string FolderName = di.Name;
                    try
                    {
                        DateTime tempDateTime = Convert.ToDateTime(FolderName.Substring(0, 4) + "-" +
                                                                    FolderName.Substring(4, 2) + "-" +
                                                                    FolderName.Substring(6, 2)
                            );

                        if (tempDateTime > dtpStartDate.SelectedDate.Value.AddDays(-1) &&
                        tempDateTime < dtpEndDate.SelectedDate.Value.AddDays(1))
                        {
                            lstLogDir.Items.Add(FolderName);
                        } // else {}
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Exception(ex);
                        lstLogDir.Items.Add(FolderName);
                    }
                }
            }
        }

        private void ShowSearchWindow(string searchLog = null)
        {
            List<Logger.ReadLogData> displayLogData = lvLog.ItemsSource as List<Logger.ReadLogData>;

            LogSearch dlg = new LogSearch(displayLogData, searchLog);
            dlg.Show();

            dlg.EventSetSelectLog += Dlg_EventSetSelectLog;
        }

        #endregion EmWorks convention functions
    }

    internal class AllLogDatas
    {
        #region Constructors

        public AllLogDatas()
        {
            Logs = new List<Logger.ReadLogData>();
        }

        #endregion Constructors

        #region Properties

        public List<Logger.ReadLogData> Logs { get; set; }

        #endregion Properties

        #region Methods

        public List<Logger.ReadLogData> GetAllLogData()
        {
            try
            {
                List<Logger.ReadLogData> allLogData = new List<Logger.ReadLogData>();

                allLogData.AddRange(Logs);

                //allLogData = allLogData.OrderBy(x => x.LogTime).ToList(); 시간으로 정렬기능 제외[2020/09/17 djkim]

                return allLogData;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return null;
            }
        }

        #endregion Methods
    }
}