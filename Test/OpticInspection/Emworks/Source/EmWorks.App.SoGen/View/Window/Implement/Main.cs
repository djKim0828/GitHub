using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace EmWorks.App.SoGen
{
    public abstract class Device
    {
        #region Methods

        public abstract uint Close();

        public abstract bool Command();

        public abstract uint Open();

        #endregion Methods
    }

    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="Main"/>
    /// Author : Dong-Jun, Kim
    /// Date : 2020-11-27
    /// Description : object detail description.
    /// </summary>
    public class Main : MainWindow
    {
        #region Fields

        //public static JavaScriptSerializer _jsonConvertor;
        private List<Object> _objects;

        private bool isCmbObjectSelectionHandled = true;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Author : Dong-Jun, Kim
        /// Date :  2020-11-27
        /// Description :  objcet constructor.
        /// </summary>
        public Main()
        {
            Initialize();
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2020-11-27
        /// Description :  object destructor.
        /// </summary>
        ~Main()
        {
        }

        #endregion Destructors

        #region Properties

        public string AlarmOutputPath
        {
            get { return HomeDir() + Properties.Settings.Default.AlarmPath; }
        }

        public string ConfigOutputPath
        {
            get { return HomeDir() + Properties.Settings.Default.ConfigPath; }
        }

        public bool IsInitialled { get; protected set; }

        public string OutputPath
        {
            get
            {
                if (cmbObject.SelectedIndex == 0)
                {
                    return TagOutputPath;
                }
                else if (cmbObject.SelectedIndex == 1)
                {
                    return ConfigOutputPath;
                }
                else if (cmbObject.SelectedIndex == 2)
                {
                    return AlarmOutputPath;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string SelectClassName
        {
            get { return cmbObject.SelectedItem.ToString(); }
        }

        public string TagOutputPath
        {
            get { return HomeDir() + Properties.Settings.Default.TagPath; }
        }

        private string _objectsNamespace
        {
            get
            {
                if (cmbObject.SelectedIndex == 0)
                {
                    return "EmWorks.Lib.Core";
                }
                else
                {
                    return "EmWorks.Lib.Identity";
                }
            }
        }

        #endregion Properties

        #region Methods

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (CheckSelectItem() == false) return;

            object obj = CreateEmptyObject();
            _objects.Add(obj);

            dgvItemList.DataSource = null;
            dgvItemList.DataSource = _objects;

            InitDataGridView();

            dgvItemList.Invalidate();
        }

        private void BtnConfig_Click(object sender, RoutedEventArgs e)
        {
            Setting setting = new Setting();
            setting.ShowDialog();
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (CheckSelectItem() == false) return;

            OutputMessage("Start");

            dgvItemList.EndEdit();

            if (JSONHandler.CreateJsonFile(OutputPath, lblFileName.Content.ToString(), _objects, dgvItemList) == true)
            {
                OutputMessage("Success : Crated " + SelectClassName + " Json File.");
            }
            else
            {
                OutputMessage("Error : Crated " + SelectClassName + " Json File.");
            }

            if (cmbObject.SelectedIndex == 0)
            {
                // Tag Identity 만들기
                if (CodeGenerator.CreateTagGeneralCode(lblFileName.Content.ToString(),
                                                          OutputPath,
                                                          ExtractClass(),
                                                          dgvItemList,
                                                          ref txtResult) == true)
                {
                    OutputMessage("Success : Crated " + SelectClassName + " General Code.");
                }
                else
                {
                    OutputMessage("Error : Crated " + SelectClassName + " General Code.");
                }
            }
            else
            {
                if (CodeGenerator.CreateConfigGeneralCode(lblFileName.Content.ToString(),
                                                          OutputPath,
                                                          ExtractClass(),
                                                          dgvItemList,
                                                          ref txtResult) == true)
                {
                    OutputMessage("Success : Crated " + SelectClassName + " General Code.");
                }
                else
                {
                    OutputMessage("Error : Crated " + SelectClassName + " General Code.");
                }
            }

            RefreshFileList();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (CheckSelectItem() == false) return;

            _objects.RemoveAt(dgvItemList.CurrentCell.RowIndex);

            dgvItemList.DataSource = null;
            dgvItemList.DataSource = _objects;

            InitDataGridView();

            dgvItemList.Invalidate();
        }

        private void BtnDialogWindowStatueChange_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ChangeWindowStatus();
        }

        private void BtnDlgClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Message dlg = new Message("프로그램을 종료하시겠습니까?", "Question", MessageBoxButtonType.YESNO, MessageBoxImageType.QUESTION);
            dlg.ShowDialog();
            if (dlg.ReturnValue == MessageBoxReturnValue.YES)
            {
                this.Close();
            }
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            if (CheckSelectItem() == false) return;

            if (CSVHandler.RunExport(dgvItemList, lblFileName.Content.ToString(), OutputPath, ref txtResult) == true)
            {
                OutputMessage("Data Exported Successfully !!!");
            } // else
        }

        private void BtnImport_Click(object sender, RoutedEventArgs e)
        {
            if (CheckSelectItem() == false) return;

            if (CSVHandler.RunImport(lblFileName.Content.ToString(),
                                    OutputPath,
                                    _objectsNamespace,
                                    SelectClassName,
                                    ref _objects,
                                    ref txtResult) == true)
            {
                OutputMessage("CSV 파일 열기에 성공했습니다.");
                dgvItemList.DataSource = _objects;
                InitDataGridView();
            }
        }

        private void BtnListRefresh_Click(object sender, RoutedEventArgs e)
        {
            puList.IsOpen = false;
            RefreshFileList();
        }

        private void BtnMini_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            if (CheckSelectItem() == false) return;

            InputBox dlg = new InputBox("New", "File Name");
            dlg.ShowDialog();
            if (dlg.ReturnValue == InputBox.MessageBoxReturnValue.OK)
            {
                string fileName = dlg.outputData;

                CreateNewObject(fileName);
            }
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            if (CheckSelectItem() == false) return;

            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.InitialDirectory = OutputPath;
            openFileDialog.Filter = "Json files (*.json)|*.json|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            string filePath = string.Empty;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Get the path of specified file
                filePath = openFileDialog.FileName;
            }
            else
            {
                return;
            }

            string jsonString = File.ReadAllText(filePath);

            string loggerData = string.Empty;
            if (JSONHandler.OpenJsonText(jsonString, _objectsNamespace, SelectClassName, ref _objects, out loggerData) == true)
            {
                OutputMessage("파일 열기에 성공했습니다.");

                dgvItemList.DataSource = _objects;
                InitDataGridView();

                lblFileName.Content = filePath;
            }
            else
            {
                OutputMessage("파일 열기에 실패 했습니다. [ " + loggerData + "]");

                lblFileName.Content = string.Empty;
            }
        }

        private void BtnOpenOutput_Click(object sender, RoutedEventArgs e)
        {
            if (CheckSelectItem() == false) return;

            System.Diagnostics.Process.Start(OutputPath);
        }

        private void BtnSaveAs_Click(object sender, RoutedEventArgs e)
        {
            if (CheckSelectItem() == false) return;

            OutputMessage("Start");

            dgvItemList.EndEdit();

            if (JSONHandler.CreateJsonFile(OutputPath, lblFileName.Content.ToString(), _objects, dgvItemList) == true)
            {
                OutputMessage("Success : Crated " + SelectClassName + " Json File.");
            }
            else
            {
                OutputMessage("Error : Crated " + SelectClassName + " Json File.");
            }
        }

        private void ChangeWindowStatus()
        {
            if (this.WindowState == System.Windows.WindowState.Maximized)
            {
                this.WindowState = System.Windows.WindowState.Normal;

                btnChange.Style = (Style)System.Windows.Application.Current.Resources["btnMax"];
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Maximized;

                btnChange.Style = (Style)System.Windows.Application.Current.Resources["btnWindowNormal"];
            }
        }

        private bool CheckSelectItem()
        {
            if (cmbObject.SelectedItem == null)
            {
                OutputMessage("클래스를 선택하십시오.");
                return false;
            } // else

            return true;
        }

        private void ClearGridAndData()
        {
            lblFileName.Content = string.Empty;
            dgvItemList.DataSource = null;
            InitDataGridView();
        }

        private void CmbObject_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (isCmbObjectSelectionHandled)
            {
                if (lblFileName.Content.ToString() != string.Empty)
                {
                    Message dlg = new Message("열린 파일이 있습니다. 객체 변경을 진행하시겠습니까?", "Question", MessageBoxButtonType.YESNO, MessageBoxImageType.QUESTION);
                    dlg.ShowDialog();
                    if (dlg.ReturnValue == MessageBoxReturnValue.NO)
                    {
                        System.Windows.Controls.ComboBox combo = (System.Windows.Controls.ComboBox)sender;
                        isCmbObjectSelectionHandled = false;
                        if (e.RemovedItems.Count > 0)
                            combo.SelectedItem = e.RemovedItems[0];
                        return;
                    }
                }

                ClearGridAndData();

                RefreshFileList();
            }
            isCmbObjectSelectionHandled = true;
        }

        private object CreateEmptyObject()
        {
            try
            {
                Type type = ExtractClass();

                // 객체 생성
                return Activator.CreateInstance(type);
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                OutputMessage(ex.ToString());
                return null;
            }
        }

        private void CreateNewObject(string fileName)
        {
            _objects = new List<Object>();

            object obj = CreateEmptyObject();
            _objects.Add(obj);

            dgvItemList.DataSource = _objects;
            InitDataGridView();

            lblFileName.Content = OutputPath + @"\" + fileName + ".json";
            JSONHandler.CreateJsonFile(OutputPath, lblFileName.Content.ToString(), _objects, dgvItemList);

            RefreshFileList();
        }

        private void DgvItemList_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 19)
            {
                string simCmd = string.Empty;
                if (dgvItemList[e.ColumnIndex, e.RowIndex].Value != null)
                {
                    simCmd = dgvItemList[e.ColumnIndex, e.RowIndex].Value.ToString();
                }

                string[] nameList = new string[dgvItemList.RowCount];

                for (int i = 0; i < dgvItemList.RowCount; i++)
                {
                    nameList[i] = dgvItemList[0, i].Value.ToString();
                }

                InputSimCmd dlg = new InputSimCmd(nameList, simCmd);
                dlg.ShowDialog();

                if (dlg.ReturnValue == InputSimCmd.MessageBoxReturnValue.OK)
                {
                    dgvItemList[e.ColumnIndex, e.RowIndex].Value = dlg.SimCmd;
                    dgvItemList.RefreshEdit();
                }
            }
        }

        private Type ExtractClass()
        {
            try
            {
                // 클래스명칭으로 클래스 가져오기
                // 현재 assembly object
                //Assembly assembly = Assembly.GetExecutingAssembly();
                Assembly assembly = Assembly.Load(_objectsNamespace);

                // 클래스 이름에 해당하는 Type을 가져옮
                Type type = assembly.GetType(_objectsNamespace + "." + SelectClassName);

                return type;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                OutputMessage(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2020-11-27
        /// Description : KeyDown Event
        /// </summary>
        private void Form_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                BtnDlgClose_Click(null, null);
            }
        }

        private bool GetClasses(string nameSpace, ref List<string> list)
        {
            try
            {
                Assembly abl = Assembly.Load(nameSpace);
                foreach (Type t in abl.GetTypes())
                {
                    if (t.Namespace == nameSpace)
                    {
                        list.Add(t.Name);
                    }
                }
                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                OutputMessage("Failed get class name");
                return false;
            }
        }

        private void GrdDlgTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (e.ClickCount == 2)
                {
                    ChangeWindowStatus();
                }
            }
        }

        private void GrdDlgTitle_MouseMove(object sender, MouseEventArgs e)
        {
            System.Threading.Thread.Sleep(10); // 더블클릭 오동작 방지
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                System.Windows.Point Temp = e.GetPosition(this);
                if (Temp.Y < 32)
                {
                    if (this.WindowState == System.Windows.WindowState.Maximized)
                    {
                        this.WindowState = System.Windows.WindowState.Normal;
                        this.Top = 0;

                        btnChange.Style = (Style)System.Windows.Application.Current.Resources["btnMax"];
                    }

                    DragMove();
                }
            }
        }

        private string HomeDir()
        {
            string currentPath = Properties.Settings.Default.RootPath;
            currentPath = currentPath.TrimEnd(new char[] { '\\' });
            return currentPath;
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2020-11-27
        /// Description : controls initialization.
        /// </summary>
        private int InitControls()
        {
            this.cmbObject.Items.Clear();

            // Tag Class는 직접 추가
            cmbObject.Items.Add("TagIdentity");

            //EmWorks.Lib.Identity의 Class 명을 추출한다.
            List<string> classNames = new List<string>();
            GetClasses(_objectsNamespace, ref classNames);

            foreach (string name in classNames)
            {
                cmbObject.Items.Add(name);
            }

            btnOpen.ToolTip = "열기";
            btnNew.ToolTip = "새글";
            btnImport.ToolTip = "CSV 파일에서 불러오기";
            btnExport.ToolTip = "CSV 파일로 내보내기";
            btnAdd.ToolTip = "열 추가";
            btnDelete.ToolTip = "열 삭제";
            btnConfig.ToolTip = "설정";

            return 0;
        }

        private void InitDataGridView()
        {
            int colCnt = dgvItemList.ColumnCount;

            int width = 180;

            if (colCnt > 0 && colCnt < 5)
            {
                width = (dgvItemList.Width - 50) / colCnt;

                for (int i = 0; i < colCnt; i++)
                {
                    dgvItemList.Columns[i].Width = width;
                }
            }
            else
            {
                dgvItemList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
                dgvItemList.AutoResizeColumns();
                dgvItemList.AllowUserToResizeColumns = true;
                dgvItemList.AllowUserToOrderColumns = true;
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2020-11-27
        /// Description :  object initialization.
        /// </summary>
        private void Initialize()
        {
            EmWorks.Lib.Common.Logger.Infomation("Start Program");

            int resultInstance = InitInstance();

            int resultLocale = InitLocale();
            int resultControls = InitControls();
            int resultEvent = RegistEvents();

            if (resultInstance == 0 || resultLocale == 0 || resultControls == 0 || resultEvent == 0)
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
        /// Author :  Dong-Jun, Kim
        /// Date :  2020-11-27
        /// Description :  Instance initialization.
        /// </summary>
        private int InitInstance()
        {
            try
            {
                IsInitialled = false;

                //_jsonConvertor = new JavaScriptSerializer();

                if (Properties.Settings.Default.RootPath == string.Empty)
                {
                    // 최초 한번 설정한다.
                    Properties.Settings.Default.RootPath = System.Windows.Forms.Application.StartupPath;
                    Properties.Settings.Default.ConfigPath = @"\Config";
                    Properties.Settings.Default.AlarmPath = @"\Config\Alarm";
                    Properties.Settings.Default.TagPath = @"\Config\Tag";
                    Properties.Settings.Default.Save();
                }

                return 0;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                OutputMessage(ex.ToString());
                return -1;
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2020-11-27
        /// Description : location(language) initialization.
        /// </summary>
        private int InitLocale()
        {
            return 0;
        }

        private void LsbSelectObject_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string jsonString = File.ReadAllText(OutputPath + @"\" + lsbSelectObject.SelectedValue);

                string loggerData = string.Empty;
                if (JSONHandler.OpenJsonText(jsonString, _objectsNamespace, SelectClassName, ref _objects, out loggerData) == true)
                {
                    OutputMessage("파일 열기에 성공했습니다.");

                    lblFileName.Content = OutputPath + @"\" + lsbSelectObject.SelectedValue;

                    dgvItemList.DataSource = _objects;
                    InitDataGridView();
                }
                else
                {
                    OutputMessage("파일 열기에 실패 했습니다. [ " + loggerData + "]");

                    lblFileName.Content = string.Empty;
                }
            }
            catch
            {
                OutputMessage("Please select a file.");
            }
        }

        private void LsbSelectObject_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            puList.IsOpen = true;
        }

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            //TO DO : 완료시에 로그인 활성화
            //bool isLogin = false;

            //for (int i = 0; i < 3; i++)
            //{
            //    // 3회까지만 허용
            //    InputBox dlg = new InputBox("Log In", "Insert Password", "", true);
            //    dlg.ShowDialog();
            //    if (dlg.ReturnValue == InputBox.MessageBoxReturnValue.OK)
            //    {
            //        if (dlg.outputPassword == "")
            //        {
            //            isLogin = true;
            //            break;
            //        }
            //        else
            //        {
            //            continue;
            //        }
            //    }
            //    else
            //    {
            //        break;
            //    }
            //}

            //if (isLogin != true)
            //{
            //    this.Close();
            //}
        }

        private void OutputMessage(string message)
        {
            txtResult.AppendText(message + "\n");
            txtResult.ScrollToEnd();
        }

        private void RefreshFileList()
        {
            // 파일 리스트
            lsbSelectObject.Items.Clear();

            try
            {
                String FolderName = OutputPath;

                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(FolderName);
                foreach (System.IO.FileInfo File in di.GetFiles())
                {
                    if (File.Extension.ToLower().CompareTo(".json") == 0)
                    {
                        String FileNameOnly = File.Name.Substring(0, File.Name.Length);
                        String FullFileName = File.FullName;

                        lsbSelectObject.Items.Add(FileNameOnly);
                    }
                }
            }
            catch (System.Exception ex)
            {
                OutputMessage("정해진 위치에 파일이 없습니다. 파일유뮤 또는 설정에서 root path을 확인하세요.");
                Logger.Exception(ex);
            }
        }

        /// <summary>
        /// Author :  Dong-Jun, Kim
        /// Date :  2020-11-27
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            // Form
            this.PreviewKeyDown += Form_PreviewKeyDown;
            this.Loaded += Main_Loaded;

            // Topbar
            this.grdDlgTitle.MouseLeftButtonDown += GrdDlgTitle_MouseLeftButtonDown;
            this.grdDlgTitle.MouseMove += GrdDlgTitle_MouseMove;
            this.btnDlgClose.Click += BtnDlgClose_Click;
            this.btnChange.Click += BtnDialogWindowStatueChange_Click;
            this.btnMini.Click += BtnMini_Click;

            // Toolbar Buttons
            this.btnNew.PreviewMouseDown += BtnNew_Click;
            this.btnOpen.Click += BtnOpen_Click;
            this.btnConfig.Click += BtnConfig_Click;
            this.btnAdd.Click += BtnAdd_Click;
            this.btnDelete.Click += BtnDelete_Click;
            this.btnImport.Click += BtnImport_Click;
            this.btnExport.Click += BtnExport_Click;

            this.btnSaveAs.Click += BtnSaveAs_Click;
            this.btnStart.Click += BtnCreate_Click;
            this.btnOpenOutput.Click += BtnOpenOutput_Click;
            // List
            lsbSelectObject.MouseDoubleClick += LsbSelectObject_MouseDoubleClick;
            lsbSelectObject.MouseRightButtonDown += LsbSelectObject_MouseRightButtonDown;

            // Comob
            cmbObject.SelectionChanged += CmbObject_SelectionChanged;

            // Pop-up 버튼
            this.btnListRefresh.Click += BtnListRefresh_Click;

            dgvItemList.CellClick += DgvItemList_CellClick;

            return 0;
        }

        #endregion Methods
    }
}