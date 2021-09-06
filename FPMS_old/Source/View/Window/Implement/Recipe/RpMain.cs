using EmWorks.Lib.Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

        private const string _recipeExtension = ".macro";
        private const string _sequenceExtension = ".sequence";
        private bool _isLoop;
        private int _loopInterval;
        private ListViewEditor _rpItemList;
        private ListViewEditor _rpRecipeEditor;
        private ListViewEditor _rpRecipeFileList;
        private ListViewEditor _rpSequenceEditor;
        private ListViewEditor _rpSequenceFileList;

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

        private void BtnInsertItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //Todo : Item Insert 기능 구현
        }

        private void BtnInsertRecipe_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //Todo : Recipe Insert 기능 구현
        }

        private void BtnRemoveItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //Todo : Item Remove 기능 구현
        }

        private void BtnRemoveRecipe_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //Todo : Recipe Remove 기능 구현
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
            grdSequenceFileList.Children.Clear();
            grdSequenceFileList.Children.Add(_rpSequenceFileList = new ListViewEditor(Global.ConfigGeneral.SequenceFilePath, _sequenceExtension, ListViewEditor.ViewType.List));
            grdSequenceEditor.Children.Clear();
            grdSequenceEditor.Children.Add(_rpSequenceEditor = new ListViewEditor(Global.ConfigGeneral.SequenceFilePath, _sequenceExtension, ListViewEditor.ViewType.Editor));
            grdRecipeFileList.Children.Clear();
            grdRecipeFileList.Children.Add(_rpRecipeFileList = new ListViewEditor(Global.ConfigGeneral.RecipeFilePath, _recipeExtension, ListViewEditor.ViewType.List));
            grdRecipeEditor.Children.Clear();
            grdRecipeEditor.Children.Add(_rpRecipeEditor = new ListViewEditor(Global.ConfigGeneral.RecipeFilePath, _recipeExtension, ListViewEditor.ViewType.Editor));
            grdItemList.Children.Clear();
            grdItemList.Children.Add(_rpItemList = new ListViewEditor());

            _rpSequenceFileList.lblTitle.Content = i18n.GetLanguage("Sequence File");
            _rpRecipeFileList.lblTitle.Content = i18n.GetLanguage("Recipe File");
            _rpItemList.lblTitle.Content = i18n.GetLanguage("Item");

            _rpSequenceEditor.grdBase.ColumnDefinitions[int.Parse(_rpSequenceEditor.grdSplit.Tag.ToString())].Width = new GridLength(0);
            _rpRecipeFileList.grdBase.ColumnDefinitions[int.Parse(_rpRecipeFileList.grdSplit.Tag.ToString())].Width = new GridLength(0);
            _rpRecipeEditor.grdBase.ColumnDefinitions[int.Parse(_rpRecipeEditor.grdSplit.Tag.ToString())].Width = new GridLength(0);

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

            _rpSequenceFileList.LoadFileList();
            _rpRecipeFileList.LoadFileList();
            //LoadItemList(); //Todo : LoadItemList 불러오기
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
            //Todo : ItemList DoubleClick으로 Item 추가 기능 구현
        }

        private List<MacroFile> LoadSequenceFile(string Path)
        {
            List<MacroFile> returnSequence = new List<MacroFile>();
            FileInfo readCsvFile = new FileInfo(Path);

            try
            {
                if (false == readCsvFile.Exists)
                {
                    return null;
                } // else

                using (StreamReader streamReader = new StreamReader(readCsvFile.FullName, false))
                {
                    string line = string.Empty;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        int temp = line.IndexOf(',');
                        int temp2 = line.LastIndexOf(',');
                        if (temp2 != line.Length - 1)
                        {
                            temp2 = line.Length;
                        } // else

                        string[] parameter = new string[2];
                        parameter[0] = line.Substring(0, temp);
                        parameter[1] = line.Substring(temp + 1, temp2 - temp - 1);

                        bool temptodo = false;
                        var tempItem = default(MacroFile);

                        temptodo = System.Convert.ToBoolean(parameter[0]);
                        tempItem = new MacroFile(parameter[1]);

                        if (tempItem != default(MacroFile))
                        {
                            returnSequence.Add(tempItem);
                            returnSequence[returnSequence.Count - 1]._todoCheck = temptodo;
                        } // else
                    }
                }

                return returnSequence;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                return returnSequence;
            }
        }

        private void RecipeFileList_lsvMain_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_rpRecipeFileList.lsvMain.SelectedItem == null)
            {
                return;
            } // else

            string fileName = _rpRecipeFileList.lsvMain.SelectedItem.ToString();

            _rpRecipeEditor.txbFileName.Text = fileName;

            //Todo : 선택한 파일 _rpRecipeEditor에 펼치기
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            _rpSequenceFileList.lsvMain.PreviewMouseDoubleClick += SequenceFileList_lsvMain_PreviewMouseDoubleClick;
            _rpRecipeFileList.lsvMain.PreviewMouseDoubleClick += RecipeFileList_lsvMain_PreviewMouseDoubleClick;
            _rpItemList.lsvMain.PreviewMouseDoubleClick += ItemList_lsvMain_PreviewMouseDoubleClick;

            btnInsertRecipe.Click += BtnInsertRecipe_Click;
            btnRemoveRecipe.Click += BtnRemoveRecipe_Click;
            btnInsertItem.Click += BtnInsertItem_Click;
            btnRemoveItem.Click += BtnRemoveItem_Click;

            return Idx.FunctionResult.True;
        }

        private void SequenceFileList_lsvMain_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_rpSequenceFileList.lsvMain.SelectedItem == null)
            {
                return;
            } // else

            string fileName = _rpSequenceFileList.lsvMain.SelectedItem.ToString();

            _rpSequenceEditor.txbFileName.Text = fileName;

            List<MacroFile> macrolist = LoadSequenceFile(Global.ConfigGeneral.RootPath + Global.ConfigGeneral.SequenceFilePath + "\\" + fileName + _sequenceExtension);
            if (macrolist != null)
            {
                List<CheckBox> ckbMacrolist = new List<CheckBox>();
                foreach (var item in macrolist)
                {
                    CheckBox ckbMacro = new CheckBox();
                    ckbMacro.Style = (Style)Application.Current.Resources["chkNormalforBlack3"];
                    ckbMacro.Foreground = Brushes.White;
                    ckbMacro.IsChecked = item._todoCheck;
                    ckbMacro.Content = Path.GetFileNameWithoutExtension(item._macroName);
                    ckbMacrolist.Add(ckbMacro);
                }

                _rpSequenceEditor.SetList(ckbMacrolist);
            } // else
        }

        #endregion Methods
    }
}