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
    /// Class Name : <see cref="DtAlarm"/>
    /// Author : Andrew, Yoon
    /// Description : object detail description.
    /// </summary>
    public class DtAlarm : EmWorks.View.DtAlarmWindow
    {
        #region Fields

        private string _alarmDirPath;
        private string _datePathFormat;
        private string _fileNameExtension;
        private bool _isLoop;
        private int _locale;
        private int _loopInterval;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Author : Andrew, Yoon
        /// Description :  objcet constructor.
        /// </summary>
        public DtAlarm()
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
        ~DtAlarm()
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
            Process.Start(_alarmDirPath);
        }

        private void BtnSearch_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SearchAlarmFiles();
        }

        private void DtAlarm_Loaded(object sender, System.Windows.RoutedEventArgs e)
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

        private string GetSelectedAlarmDirPath()
        {
            string dirName = lstAlarmDir.SelectedItem.ToString();

            return _alarmDirPath + "\\" + dirName;
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
            int resultInstance = InitInstance();
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
            try
            {
                _IsInitialled = false;
                _loopInterval = 5;
                _isLoop = false; // if do you want to use the EmWorksProc(), chage to true.

                _fileNameExtension = Global.AlarmWriter.GetFileExtension();
                _datePathFormat = Global.AlarmWriter.GetDatePathFormat();
                _alarmDirPath = Global.ConfigGeneral.RootPath + Global.ConfigGeneral.AlarmDataFilePath;

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
                if (fi.Extension == _fileNameExtension)
                {
                    return true;
                }//else
            }

            return false;
        }

        private bool LoadFromAlarmDir(string alarmDirPath)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(alarmDirPath);

                List<AlarmModel> alarmList = LoadFromAlarmFiles(dirInfo.GetFiles());

                this.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    lvAlarm.ItemsSource = alarmList;
                });

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }
        }

        private List<AlarmModel> LoadFromAlarmFiles(FileInfo[] fileInfos)
        {
            List<AlarmModel> allAlarmDatas = new List<AlarmModel>();

            foreach (FileInfo fileInfo in fileInfos)
            {
                if (fileInfo.Extension.CompareTo(_fileNameExtension) == 0)
                {
                    List<AlarmModel> alarmData = Global.AlarmWriter.LoadList(fileInfo.FullName);

                    allAlarmDatas.AddRange(alarmData);
                }//else
            }

            return allAlarmDatas;
        }

        private void LstAlarmDir_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lstAlarmDir.SelectedItem == null) return;

            string selectedAlarmDirPath = GetSelectedAlarmDirPath();
            //ProcessBox processDialog = new ProcessBox();
            //processDialog.TitleMessage = i18n.GetLanguage("Loading");
            //processDialog.Topmost = true;
            //processDialog.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            Task task = Task.Run(() => LoadFromAlarmDir(selectedAlarmDirPath));
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
            this.lstAlarmDir.MouseDoubleClick += LstAlarmDir_MouseDoubleClick;

            this.Loaded += DtAlarm_Loaded;

            return Idx.FunctionResult.True;
        }

        private void SearchAlarmFiles()
        {
            lstAlarmDir.Items.Clear();

            string[] dirs = Directory.GetDirectories(this._alarmDirPath);

            foreach (string dirPath in dirs)
            {
                DirectoryInfo di = new DirectoryInfo(dirPath);

                if (IsLogFileExist(di))
                {
                    string FolderName = di.Name;
                    try
                    {
                        DateTime tempDateTime = DateTime.ParseExact(FolderName, _datePathFormat, CultureInfo.InvariantCulture);

                        if (tempDateTime > dtpStartDate.SelectedDate.Value.AddDays(-1) &&
                        tempDateTime < dtpEndDate.SelectedDate.Value.AddDays(1))
                        {
                            lstAlarmDir.Items.Add(FolderName);
                        } // else {}
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Exception(ex);
                        lstAlarmDir.Items.Add(FolderName);
                    }
                }
            }
        }

        #endregion Methods
    }
}