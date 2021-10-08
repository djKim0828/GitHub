using EmWorks.Lib.Common;
using System;
using System.Threading;

namespace EmWorks.App.OpticInspection
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="OpMenu"/>
    /// Author : Andrew, Yoon
    /// Description : object detail description.
    /// </summary>
    public class OpMenu : View.Window.Design.Operation.OpMenuWindow
    {
        #region Fields

        private bool _isLoop;
        private int _loopInterval;       
        private int _selectRecipe = -1;
        private string _selectRecipeName = string.Empty;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Author : Andrew, Yoon
        /// Description :  objcet constructor.
        /// </summary>
        public OpMenu()
        {
            Initialize();
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description :  object destructor.
        /// </summary>
        ~OpMenu()
        {
            _isLoop = false;
            // add your code here
        }

        #endregion Destructors

        #region Properties

        public bool _IsInitialled { get; protected set; }

        #endregion Properties

        #region Methods

        public void SetButtonEnable()
        {
            rdbAbort.IsEnabled = false;
            rdbPause.IsEnabled = false;
            rdbResume.IsEnabled = false;
            rdbStart.IsEnabled = false;
            rdbStop.IsEnabled = false;

            if (rdbStart.IsChecked == true)
            {
                rdbStop.IsEnabled = true;
                rdbAbort.IsEnabled = true;
                rdbPause.IsEnabled = true;
            }
            else if (rdbStop.IsChecked == true)
            {
                rdbStart.IsEnabled = true;
            }
            else if (rdbPause.IsChecked == true)
            {
                rdbStop.IsEnabled = true;
                rdbAbort.IsEnabled = true;
                rdbResume.IsEnabled = true;
            }
            else if (rdbResume.IsChecked == true)
            {
                rdbStop.IsEnabled = true;
                rdbAbort.IsEnabled = true;
                rdbPause.IsEnabled = true;
            }
            else if (rdbAbort.IsChecked == true)
            {
                //rdbStart.IsEnabled = true;
            }
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

        private void LoadRecipes()
        {
            try
            {
                if (cmbRecipe.SelectedIndex > 0)
                {
                    _selectRecipe = cmbRecipe.SelectedIndex;
                    _selectRecipeName = cmbRecipe.Items[cmbRecipe.SelectedIndex].ToString();
                } // else

                Global._ModelRecipe = Global._ModelRecipe.Load(Global.ConfigGeneral.RootPath + Global.ConfigGeneral.RecipeFilePath);

                cmbRecipe.Items.Clear();
                foreach (ModelRecipe.RecipeItem item in Global._ModelRecipe._Item)
                {
                    cmbRecipe.Items.Add(item.Name);
                }

                if (_selectRecipe > 0)
                {
                    if (cmbRecipe.Items[_selectRecipe].ToString() == _selectRecipeName)
                    {
                        //인덱스와 이름이 같으면,
                        cmbRecipe.SelectedIndex = _selectRecipe;
                    } // else
                } // else
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        private void OpMenu_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == System.Windows.Visibility.Visible)
            {
                // comboBox 갱식
                LoadRecipes();
            } // else
        }

        private void RdbAbort_Clicked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (rdbAbort.IsChecked == true)
            {
                StatusMachine.Request(StatusType.Operation.Abort);
            }
        }

        private void RdbPause_Clicked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (rdbPause.IsChecked == true)
            {
                StatusMachine.Request(StatusType.Operation.Pause);
            }
        }

        private void RdbResume_Clicked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (rdbResume.IsChecked == true)
            {
                StatusMachine.Request(StatusType.Operation.Resume);
            }
        }

        private void RdbStart_Clicked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (rdbStart.IsChecked == true)
            {
                //Todo : Test를 쉽게 하기 위하여 - 삭제 必
                if (cmbRecipe.SelectedIndex < 0)
                {
                    cmbRecipe.SelectedIndex = 0;
                }

                if (cmbRecipe.SelectedIndex < 0)
                {
                    MessageBox.ShowDialog("Please, select a recipe");
                    return;
                } // else

                CommandCenter._Recipe = Global._ModelRecipe._Item[cmbRecipe.SelectedIndex];

                if (txtWaferId.Text == string.Empty)
                {
                    txtWaferId.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
                } // else               

                CommandCenter._CellId = txtWaferId.Text;

                

                StatusMachine.Request(StatusType.Operation.Start);
            }
        }

        private void RdbStop_Clicked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (rdbStop.IsChecked == true)
            {
                StatusMachine.Request(StatusType.Operation.Stop);
            }
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            this.Loaded += UcOpMenu_Loaded;
            this.IsVisibleChanged += OpMenu_IsVisibleChanged;

            this.rdbAbort.Click += RdbAbort_Clicked;
            this.rdbPause.Click += RdbPause_Clicked;
            this.rdbResume.Click += RdbResume_Clicked;
            this.rdbStart.Click += RdbStart_Clicked;
            this.rdbStop.Click += RdbStop_Clicked;

            return Idx.FunctionResult.True;
        }

        private void UcOpMenu_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            rdbStop.IsChecked = true;
            SetButtonEnable();
        }

        #endregion Methods
    }
}