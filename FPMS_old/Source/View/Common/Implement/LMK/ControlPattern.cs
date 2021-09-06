using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Controls;

namespace FPMS.E2105_FS111_121
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="ControlPattern"/>
    /// Author : Andrew, Yoon
    /// Description : object detail description.
    /// </summary>
    public class ControlPattern : EmWorks.View.ControlPatternComponent
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
        public ControlPattern()
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
        ~ControlPattern()
        {
            _isLoop = false;
            // add your code here
        }

        #endregion Destructors

        #region Properties

        public bool _IsInitialled { get; protected set; }

        #endregion Properties

        #region Methods

        private void BtnColorLevelDipslay_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (cmbColor.SelectedIndex == -1)
            {
                return;
            }//else

            int level = 0;
            if (int.TryParse(txtColorLevel.Text, out level) == false)
            {
                return;
            }//else

            if (Global.ListDisplayForm.Count == 0)
            {
                MessageBox.Show("Please, Search Display",
                                "Control Pattern",
                                MessageBoxButtonType.OK,
                                MessageBoxImageType.INFORMATION);
                return;
            }

            for (int i = 1; i < Global.Listscreens.Count; i++)
            {
                Global.ListDisplayForm[i].DrawColorDisplay(cmbColor.Text, level);
            }
        }

        private void BtnDisplay1OnOff_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //To do...
        }

        private void BtnDisplay2OnOff_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //To do...
        }

        private void BtnMergeDisplay_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (lsbDisplay.Items.Count < 2)
            {
                return;
            }
            //Todo Test 필요
            int isIndex = 0;// lsbDisplay.FindString(lsbDisplay.Items. CheckedItems[0].ToString());
            //if (!(isIndex < checkedListBox1.CheckedItems.Count) || isIndex < 2) return;

            Global.ListDisplayForm[isIndex].Close();

            Global.ListDisplayForm[isIndex + 1].Close();

            Size Newsize = new Size(Global.Listscreens[isIndex].Bounds.Width + Global.Listscreens[isIndex + 1].Bounds.Width, Global.Listscreens[isIndex].Bounds.Height);

            Rectangle NewFormBound = new Rectangle(Global.Listscreens[isIndex].Bounds.Location, Newsize);

            Global.ListDisplayForm[isIndex] = new SubDisplay(NewFormBound, isIndex);

            Global.ListDisplayForm[isIndex].Show();
        }

        private void BtnPatternDipslay_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Global.ListDisplayForm.Count == 0)
            {
                MessageBox.Show("Please, Search Display",
                                "Control Pattern",
                                MessageBoxButtonType.OK,
                                MessageBoxImageType.INFORMATION);
                return;
            }//else

            if (lsbPattern.SelectedIndex != -1)
            {
                string filename = lsbPattern.SelectedItem.ToString();

                for (int i = 1; i < Global.Listscreens.Count; i++)
                {
                    Global.ListDisplayForm[i].DrawFromImgFile(filename);
                }
            }//else
        }

        private void BtnRGBDipslay_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            int red = 0;
            int green = 0;
            int blue = 0;

            if (int.TryParse(txtLevelR.Text, out red) == false)
            {
                return;
            }//else

            if (int.TryParse(txtLevelG.Text, out green) == false)
            {
                return;
            }//else

            if (int.TryParse(txtLevelB.Text, out blue) == false)
            {
                return;
            }//else

            if (Global.ListDisplayForm.Count == 0)
            {
                MessageBox.Show("Please, Search Display",
                                "Control Pattern",
                                MessageBoxButtonType.OK,
                                MessageBoxImageType.INFORMATION);
                return;
            }//else

            for (int i = 1; i < Global.Listscreens.Count; i++)
            {
                Global.ListDisplayForm[i].DrawColorDisplay(red, green, blue);
            }

        }

        private void BtnSearchDisplay_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Global.ListDisplayForm.Count > 1)
            {
                for (int i = 0; i < Global.ListDisplayForm.Count; i++)
                {
                    try
                    {
                        Global.ListDisplayForm[i].Close();
                        //ListDisplayForm[i].Dispose();
                    }
                    catch
                    {
                    }
                }
            }//else

            Global.ListDisplayForm.Clear();

            if (Global.Listscreens != null)
            {
                if (Global.Listscreens.Count > 1)
                {
                    Global.Listscreens.Clear();
                }//else
            }//else

            System.Windows.Forms.Screen.AllScreens.Initialize();

            foreach (var screen in System.Windows.Forms.Screen.AllScreens)
            {
                Global.Listscreens.Add(screen);
            }

            Global.Listscreens.Sort(delegate (System.Windows.Forms.Screen A, System.Windows.Forms.Screen B)
            {
                if (A.Bounds.X > B.Bounds.X)
                {
                    return 1;
                }
                else if (A.Bounds.X < B.Bounds.X)
                {
                    return -1;
                }//else

                return 0;
            });

            lsbDisplay.Items.Clear();

            for (int i = 0; i < Global.Listscreens.Count; i++)
            {
                Global.ListDisplayForm.Add(new SubDisplay(Global.Listscreens[i].Bounds, i + 1));

                CheckBox Addscreen = new CheckBox();
                Addscreen.Content = (i + 1).ToString();
                Addscreen.IsChecked = false;

                lsbDisplay.Items.Add(Addscreen);
            }

            for (int i = 0; i < Global.Listscreens.Count; i++)
            {
                Global.ListDisplayForm[i].Show();
            }

            for (int i = 0; i < Global.Listscreens.Count; i++)
            {
                Global.ListDisplayForm[i].DrawDisplayID();
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
                             // add your code here

            return Idx.FunctionResult.True;
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            btnDisplay1OnOff.Click += BtnDisplay1OnOff_Click;
            btnDisplay2OnOff.Click += BtnDisplay2OnOff_Click;
            btnSearchDisplay.Click += BtnSearchDisplay_Click;
            btnMergeDisplay.Click += BtnMergeDisplay_Click;
            btnColorLevelDipslay.Click += BtnColorLevelDipslay_Click;
            btnRGBDipslay.Click += BtnRGBDipslay_Click;
            btnPatternDipslay.Click += BtnPatternDipslay_Click;

            return Idx.FunctionResult.True;
        }

        #endregion Methods
    }
}