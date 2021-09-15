using EmWorks.Lib.Common;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace FPMS.E2105_FS111_121
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="RpMain"/>
    /// Author : Andrew, Yoon
    /// Description : object detail description.
    /// </summary>
    public class RpMain : EmWorks.View.RpMainWindow
    {
        #region Fields

        private bool _isLoop;
        private int _locale;
        private int _loopInterval;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Author : Andrew, Yoon
        /// Description :  objcet constructor.
        /// </summary>
        public RpMain()
        {
            Initialize();
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description :  object destructor.
        /// </summary>
        ~RpMain()
        {
            _isLoop = false;
            // add your code here
        }

        #endregion Destructors

        #region Properties

        public bool _IsInitialled { get; protected set; }

        #endregion Properties

        #region Methods

        public void LoadFileList()
        {
            try
            {
                DirectoryInfo fileDirectory = new DirectoryInfo(Global.ConfigGeneral.RootPath + Global.ConfigGeneral.SequenceFilePath);
                if (fileDirectory.Exists == false)
                {
                    fileDirectory.Create();
                } // else

                List<string> list = new List<string>();
                string name = "";
                foreach (var item in fileDirectory.GetFiles())
                {
                    if (item.Extension.ToLower() == ".sequence")
                    {
                        name = Path.GetFileNameWithoutExtension(item.Name);
                        list.Add(name);
                    } // eles
                }
                rpSequenceFileList.SetList(list);

                fileDirectory = new DirectoryInfo(Global.ConfigGeneral.RootPath + Global.ConfigGeneral.RecipeFilePath);
                if (fileDirectory.Exists == false)
                {
                    fileDirectory.Create();
                } // else

                list = new List<string>();
                foreach (var item in fileDirectory.GetFiles())
                {
                    if (item.Extension.ToLower() == ".macro")
                    {
                        name = Path.GetFileNameWithoutExtension(item.Name);
                        list.Add(name);
                    } // esle
                }
                rpRecipeFileList.SetList(list);
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        private void BtnDeleteItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        }

        private void BtnDeleteRecipe_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        }

        private void BtnInsertItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        }

        private void BtnInsertRecipe_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description : EmWorks base thread.
        /// </summary>
        private void EmWorksProc(object state)
        {
            while (_isLoop)
            {
                Thread.Sleep(_loopInterval);
            }
        }

        /// <summary>
        /// Author : Andrew, Yoon
        /// Description : controls initialization.
        /// </summary>
        private int InitControls()
        {
            rpSequenceFileList.lblTitle.Content = "Sequence File";
            rpRecipeFileList.lblTitle.Content = "Recipe File";
            rpItemList.lblTitle.Content = "Item";

            rpSequenceEdit.grdBase.ColumnDefinitions[int.Parse(rpSequenceEdit.grdSplit.Tag.ToString())].Width = new System.Windows.GridLength(0);
            rpRecipeFileList.grdBase.ColumnDefinitions[int.Parse(rpRecipeFileList.grdSplit.Tag.ToString())].Width = new System.Windows.GridLength(0);
            rpRecipeEdit.grdBase.ColumnDefinitions[int.Parse(rpRecipeEdit.grdSplit.Tag.ToString())].Width = new System.Windows.GridLength(0);
            
            rpSequenceFileList.txbFileName.Visibility = System.Windows.Visibility.Hidden;
            rpRecipeFileList.txbFileName.Visibility = System.Windows.Visibility.Hidden;
            rpItemList.txbFileName.Visibility = System.Windows.Visibility.Hidden;

            rpSequenceFileList.grdMain.RowDefinitions[int.Parse(rpSequenceFileList.grdUpDown.Tag.ToString())].Height = new System.Windows.GridLength(0);
            rpRecipeFileList.grdMain.RowDefinitions[int.Parse(rpRecipeFileList.grdUpDown.Tag.ToString())].Height = new System.Windows.GridLength(0);
            rpItemList.grdMain.RowDefinitions[int.Parse(rpItemList.grdUpDown.Tag.ToString())].Height = new System.Windows.GridLength(0);

            rpSequenceEdit.btnDelete.Visibility = System.Windows.Visibility.Hidden;
            rpRecipeEdit.btnDelete.Visibility = System.Windows.Visibility.Hidden;
            rpItemList.grdMain.RowDefinitions[int.Parse(rpItemList.grdButton.Tag.ToString())].Height = new System.Windows.GridLength(0);

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

            LoadFileList();
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description :  Instance initialization.
        /// </summary>
        private int InitInstance()
        {
            _IsInitialled = false;
            _loopInterval = 5;
            _isLoop = false;

            return Idx.FunctionResult.True;
        }

        private void ItemList_lsvMain_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        }

        private void RecipeEdit_BtnClear_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        }

        private void RecipeEdit_BtnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        }

        private void RecipeFileList_BtnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        }

        private void RecipeFileList_lsvMain_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string fileName = rpRecipeFileList.lsvMain.SelectedItem.ToString();

            rpRecipeEdit.txbFileName.Text = fileName;

            //Todo : 선택한 파일 내용 rpRecipeEdit에 뿌리기
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            rpSequenceFileList.lsvMain.PreviewMouseDoubleClick += SequenceFileList_lsvMain_PreviewMouseDoubleClick;
            rpRecipeFileList.lsvMain.PreviewMouseDoubleClick += RecipeFileList_lsvMain_PreviewMouseDoubleClick;
            rpItemList.lsvMain.PreviewMouseDoubleClick += ItemList_lsvMain_PreviewMouseDoubleClick;

            rpSequenceFileList.btnDelete.Click += SequenceFileList_BtnDelete_Click;
            rpSequenceEdit.btnSave.Click += SequenceEdit_BtnSave_Click;
            rpRecipeFileList.btnDelete.Click += RecipeFileList_BtnDelete_Click;
            rpRecipeEdit.btnSave.Click += RecipeEdit_BtnSave_Click;

            btnInsertRecipe.Click += BtnInsertRecipe_Click;
            btnDeleteRecipe.Click += BtnDeleteRecipe_Click;
            btnInsertItem.Click += BtnInsertItem_Click;
            btnDeleteItem.Click += BtnDeleteItem_Click;

            return Idx.FunctionResult.True;
        }

        private void SequenceEdit_BtnClear_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        }

        private void SequenceEdit_BtnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        }

        private void SequenceFileList_BtnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        }

        private void SequenceFileList_lsvMain_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string fileName = rpSequenceFileList.lsvMain.SelectedItem.ToString();

            rpSequenceEdit.txbFileName.Text = fileName;

            //Todo : 선택한 파일 내용 rpSequenceEdit에 뿌리기
        }

        #endregion Methods
    }
}