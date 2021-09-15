using EmWorks.Lib.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FPMS.E2105_FS111_121
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="DtLogger"/>
    /// Author : Andrew, Yoon
    /// Description : object detail description.
    /// </summary>
    public class DtLogger : EmWorks.View.DtLoggerWindow
    {
        #region Fields

        private string _datePathFormat;
        private string _fileNameExtension;
        private bool _isLoop;
        private int _locale;
        private string _logDirPath;

        private int _loopInterval;
        private LogData _templogdata = new LogData();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Author : Andrew, Yoon
        /// Description :  objcet constructor.
        /// </summary>
        public DtLogger()
        {
            Initialize();
            // add your code here
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description :  object destructor.
        /// </summary>
        ~DtLogger()
        {
            _isLoop = false;
            // add your code here
        }

        #endregion Destructors

        #region Properties

        public bool _IsInitialled { get; protected set; }

        #endregion Properties

        #region Methods

        private void BtnOpen_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Process.Start(_logDirPath);
        }

        private void BtnSearch_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SearchLogFiles();
        }

        private void DtLogger_Loaded(object sender, System.Windows.RoutedEventArgs e)
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
            cmbLogType.ItemsSource = Enum.GetValues(typeof(LogData.Level));
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

            if (resultInstance == Idx.FunctionResult.True &&
                resultControls == Idx.FunctionResult.True &&
                resultEvent == Idx.FunctionResult.True)
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

            _fileNameExtension = _templogdata.GetFileExtension();
            _datePathFormat = _templogdata.GetDatePathFormat();
            _logDirPath = Global.ConfigGeneral.RootPath + Global.ConfigGeneral.LogFilePath;

            return Idx.FunctionResult.True;
        }

        private bool IsLogFileExist(DirectoryInfo di)
        {
            foreach (FileInfo fi in di.GetFiles())
            {
                if (fi.Extension == _fileNameExtension)
                {
                    return true;
                }//else
            }

            return false;
        }

        private bool LoadFromLogDir(string logDirPath)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(logDirPath);

                this.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    List<LogData> logList = LoadFromLogFiles(dirInfo.GetFiles());

                    lvLog.ItemsSource = logList;
                });

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }
        }

        private List<LogData> LoadFromLogFiles(FileInfo[] fileInfos)
        {
            LogData.Level selectedLevel = (LogData.Level)cmbLogType.SelectedIndex;

            List<LogData> allLogDatas = new List<LogData>();

            foreach (FileInfo fileInfo in fileInfos)
            {
                if (fileInfo.Extension.CompareTo(_fileNameExtension) == 0)
                {
                    List<LogData> logData = _templogdata.LoadList(fileInfo.FullName);
                    logData = logData.FindAll(x => x.LogLevel <= selectedLevel);
                    allLogDatas.AddRange(logData);
                }//else
            }

            return allLogDatas;
        }

        private void LstLogDir_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lstLogDir.SelectedItem == null) return;

            string selectedlogDirPath = GetSelectedLogDirPath();
            //Todo :
            //ProcessBox processDialog = new ProcessBox();
            //processDialog.TitleMessage = i18n.GetLanguage("Loading");
            //processDialog.Topmost = true;
            //processDialog.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            Task task = Task.Run(() => LoadFromLogDir(selectedlogDirPath));
            task.GetAwaiter().OnCompleted(() =>
            {
                //processDialog.Close();
            });

            //processDialog.ShowDialog();
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            this.btnSearch.Click += BtnSearch_Click;
            this.btnOpen.Click += BtnOpen_Click;
            this.lstLogDir.MouseDoubleClick += LstLogDir_MouseDoubleClick;

            this.Loaded += DtLogger_Loaded;

            return Idx.FunctionResult.True;
        }

        private void SearchLogFiles()
        {
            lstLogDir.Items.Clear();

            string[] dirs = Directory.GetDirectories(this._logDirPath);

            foreach (string dirPath in dirs)
            {
                DirectoryInfo di = new DirectoryInfo(dirPath);

                if (IsLogFileExist(di) == false)
                {
                    continue;
                }// else

                string FolderName = di.Name;
                try
                {
                    DateTime tempDateTime = DateTime.ParseExact(FolderName, _datePathFormat, CultureInfo.InvariantCulture);

                    if (tempDateTime > dtpStartDate.SelectedDate.Value.AddDays(-1) &&
                    tempDateTime < dtpEndDate.SelectedDate.Value.AddDays(1))
                    {
                        lstLogDir.Items.Add(FolderName);
                    } // else
                }
                catch (System.Exception ex)
                {
                    Logger.Exception(ex);
                    lstLogDir.Items.Add(FolderName);
                }
            }
        }

        #endregion Methods
    }
}