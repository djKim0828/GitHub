using EmWorks.Lib.Common;
using System;
using System.Collections.Generic;
using System.Threading;

namespace EmWorks.App.OpticInspection
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="RpMain"/>
    /// Author : Andrew, Yoon
    /// Description : object detail description.
    /// </summary>
    public class RpMain : View.Window.Design.Recipe.RpMainWindow
    {
        #region Fields

        private Fov _fov;
        private bool _isLoop;
        private int _loopInterval;
        private List<ModelRecipe.LedItem> _modelLedItem;
        private Wafer _wafer;

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
        }

        #endregion Destructors

        #region Properties

        public bool _IsInitialled { get; protected set; }

        #endregion Properties

        #region Methods

        private void _Fov_EventLabelClick(object sendor, Fov.EventLabelClickMessageArgs e)
        {
            AddLedItem(e._Message);
        }

        private void _wafer_EventLabelClick(object sendor, Wafer.EventLabelClickMessageArgs e)
        {
            lblSelectFov.Content = e._information._LabelIndex;
            lblSelectFov.Tag = e._information;

            _wafer.SelectLabel(Convert.ToInt16(e._information._LabelIndex));

            _fov.IsEnabled = true;
        }

        private void AddLedItem(string ledNumber)
        {
            try
            {
                if (lblSelectFov.Content.ToString() == string.Empty)
                {
                    MessageBox.ShowDialog("Please, check the FOV select.");
                    return;
                } // else

                _modelLedItem = dtgLed.ItemsSource as List<ModelRecipe.LedItem>;

                if (_modelLedItem == null)
                {
                    _modelLedItem = new List<ModelRecipe.LedItem>();
                } // else

                Wafer.fovInformation info = (Wafer.fovInformation)lblSelectFov.Tag;

                ModelRecipe.LedItem temp = new ModelRecipe.LedItem();

                temp.FovNumber = Convert.ToInt16(info._LabelIndex);
                temp.FovColIndex = Convert.ToInt16(info._ColIndex);
                temp.FovRowIndex = Convert.ToInt16(info._RowIndex);
                temp.FovColCount = Convert.ToInt16(info._ColCount);
                temp.FovRowCount = Convert.ToInt16(info._RowCount);
                temp.LedNumber = Convert.ToInt16(ledNumber);

                _modelLedItem.Add(temp);

                RefreshLedItemList();
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        private void AddRecipe()
        {
            try
            {
                //Check
                if (txtRecipeName.Text == string.Empty)
                {
                    MessageBox.Show("Please, Check a name.");
                    return;
                } // else

                ModelRecipe.RecipeItem item = MakeRecipe();                              

                Global._ModelRecipe._Item.Add(item);
                Global._ModelRecipe.Save(Global.ConfigGeneral.RootPath +
                                Global.ConfigGeneral.RecipeFilePath,
                                Global._ModelRecipe);

                LoadRecipes();
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        private ModelRecipe.RecipeItem MakeRecipe()
        {
            try
            {
                ModelRecipe.RecipeItem item = new ModelRecipe.RecipeItem();

                //Information
                item.Name = txtRecipeName.Text;
                item.Type = cmbRecipeType.SelectedValue.ToString();

                //Setting - Fov
                item.FovWidthSpacing = txtFovWidthSpacing.Text;
                item.FovHeightSpacing = txtFovHeightSpacing.Text;
                item.LedWidthSpacing = txtLedWidthSpacing.Text;
                item.LedHeightSpacing = txtLedHeightSpacing.Text;

                //Setting - Auto Spot
                item.AutoSpotType = rdbCircle.IsChecked == true ? "0" : "1";
                item.AutoSpotHeight = txtAutSpotHeight.Text;
                item.AutoSpotWidth = txtAutSpotWidth.Text;
                item.AutoSpotMin = txtAutSpotMin.Text;
                item.AutoSpotMax = txtAutSpotMax.Text;
                item.AutoSpotYth = txtAutSpotYTh.Text;

                item.RecipeFilepath = txtRecipeFilepath.Text;

                item._Item = _modelLedItem;

                return item;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return null;
            }
            
        }

        private void BtnAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AddRecipe();
        }

        private void BtnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DeleteRecipe();
        }

        private void BtnLedAllDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DeleteLedAllItem();
        }

        private void BtnLedDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (dtgLed.SelectedIndex < 0)
            {
                MessageBox.ShowDialog("Please, Select a item");
                return;
            } // else

            DeleteLedItem();
        }

        private void BtnUpdate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UpdateRecipe();
        }

        private void DeleteLedAllItem()
        {
            try
            {
                if (_modelLedItem == null)
                {
                    return;
                } // else

                _modelLedItem.Clear();

                RefreshLedItemList();
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        private void DeleteLedItem()
        {
            try
            {
                _modelLedItem.RemoveAt(dtgLed.SelectedIndex);

                RefreshLedItemList();
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        private void DeleteRecipe()
        {
            try
            {
                if (MessageBox.ShowDialog("Do you want to delete?",
                    "Question", MessageBoxButtonType.YESNO, MessageBoxImageType.QUESTION) == MessageBoxReturnValue.NO)
                {
                    return;
                } // else


                if (lsbRecipe.SelectedIndex < 0)
                {
                    MessageBox.ShowDialog("Please, Select a item");
                    return;
                }//else

                Global._ModelRecipe._Item.RemoveAt(lsbRecipe.SelectedIndex);

                lsbRecipe.Items.Remove(lsbRecipe.SelectedItem);

                Global._ModelRecipe.Save(Global.ConfigGeneral.RootPath +
                                Global.ConfigGeneral.RecipeFilePath,
                                Global._ModelRecipe);
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
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
            cmbRecipeType.Items.Clear();
            cmbRecipeType.Items.Add(Idx.WafepType.Size4);
            cmbRecipeType.Items.Add(Idx.WafepType.Size6);
            cmbRecipeType.SelectedIndex = 0;

            LoadRecipes();

            _fov.IsEnabled = false; // 초기값

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
            _isLoop = false;

            Global._ModelRecipe = new ModelRecipe();
            Global._ModelRecipe._Item = new List<ModelRecipe.RecipeItem>();

            this.grdWafer.Children.Add(_wafer = new Wafer());
            this.grdFov.Children.Add(_fov = new Fov());

            return Idx.FunctionResult.True;
        }

        private void ShowControl(ModelRecipe.RecipeItem recipeItem)
        {
            //Information
            txtRecipeName.Text = recipeItem.Name;
            cmbRecipeType.SelectedValue = recipeItem.Type;

            //Setting - Fov
            txtFovWidthSpacing.Text = recipeItem.FovWidthSpacing;
            txtFovHeightSpacing.Text = recipeItem.FovHeightSpacing;
            txtLedWidthSpacing.Text = recipeItem.LedWidthSpacing;
            txtLedHeightSpacing.Text = recipeItem.LedHeightSpacing;

            //Setting - Auto Spot
            //rdbCircle.IsChecked = recipeItem.AutoSpotType == "0" ? true : false;
            if (recipeItem.AutoSpotType == "0")
            {
                rdbCircle.IsChecked = true;
                rdbSquare.IsChecked = false;
            }
            else
            {
                rdbCircle.IsChecked = false;
                rdbSquare.IsChecked = true;
            }

            txtAutSpotHeight.Text = recipeItem.AutoSpotHeight;
            txtAutSpotWidth.Text = recipeItem.AutoSpotWidth;
            txtAutSpotMin.Text = recipeItem.AutoSpotMin;
            txtAutSpotMax.Text = recipeItem.AutoSpotMax;
            txtAutSpotYTh.Text = recipeItem.AutoSpotYth;

            txtRecipeFilepath.Text = recipeItem.RecipeFilepath;

            _modelLedItem = recipeItem._Item;

            RefreshLedItemList();
        }

        private void LoadRecipes()
        {
            try
            {
                Global._ModelRecipe = Global._ModelRecipe.Load(Global.ConfigGeneral.RootPath + Global.ConfigGeneral.RecipeFilePath);

                lsbRecipe.Items.Clear();
                foreach (ModelRecipe.RecipeItem item in Global._ModelRecipe._Item)
                {
                    lsbRecipe.Items.Add(item.Name);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.ShowDialog("Failed Load Recipes");
                Logger.Exception(ex);
            }
        }

        private void LsbRecipe_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ShowControl(Global._ModelRecipe._Item[lsbRecipe.SelectedIndex]);
        }

        private void RefreshLedItemList()
        {
            dtgLed.ItemsSource = null;
            dtgLed.ItemsSource = _modelLedItem;
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            //Recipe
            this.lsbRecipe.PreviewMouseDoubleClick += LsbRecipe_PreviewMouseDoubleClick;
            this.btnAdd.Click += BtnAdd_Click;
            this.btnUpdate.Click += BtnUpdate_Click;
            this.btnDelete.Click += BtnDelete_Click;

            this.btnOpenFileDialog.Click += BtnOpenFileDialog_Click;

            // Led
            this.btnLedAllDelete.Click += BtnLedAllDelete_Click;
            this.btnLedDelete.Click += BtnLedDelete_Click;

            this._wafer.EventLabelClick += _wafer_EventLabelClick;
            this._fov.EventLabelClick += _Fov_EventLabelClick;

            return Idx.FunctionResult.True;
        }

        private void BtnOpenFileDialog_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.InitialDirectory = Global.ConfigGeneral.RootPath;
            openFileDialog.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtRecipeFilepath.Text = openFileDialog.FileName;
            } //else
        }

        private void UpdateRecipe()
        {

            int selectIndex = lsbRecipe.SelectedIndex;

            if (selectIndex < 0)
            {
                MessageBox.ShowDialog("Please, Select a item");
                return;
            }//else

            //Check
            if (txtRecipeName.Text == string.Empty)
            {
                MessageBox.Show("Please, Check a name.");
                return;
            } // else

            ModelRecipe.RecipeItem item = MakeRecipe();

            Global._ModelRecipe._Item[selectIndex] = item;
            Global._ModelRecipe.Save(Global.ConfigGeneral.RootPath +
                            Global.ConfigGeneral.RecipeFilePath,
                            Global._ModelRecipe);

            LoadRecipes();

            lsbRecipe.SelectedIndex = selectIndex;

            MessageBox.ShowDialog("update complete.");

        }

        #endregion Methods
    }
}