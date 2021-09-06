using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;

namespace FPMS.E2105_FS111_121
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="UcMaintIO"/>
    /// Author : DongJun.Kim
    /// Date : 2020-09-04 11:30
    /// Description : object detail description.
    /// </summary>
    public class MtIo : EmWorks.View.MtIoWindow
    {
        #region Fields

        private ObservableCollection<IoStatus> _ioInputStatus;

        private ObservableCollection<IoStatus> _ioOutputStatus;

        private bool _isFirstVisible = true;

        private bool _isLoop;

        private int _loopInterval;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Author : DongJun.Kim
        /// Date :  2020-09-04 11:30
        /// Description :  objcet constructor.
        /// </summary>
        public MtIo()
        {
            Initialize();
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-04 11:30
        /// Description :  object destructor.
        /// </summary>
        ~MtIo()
        {
            _isLoop = false;
        }

        #endregion Destructors

        #region Properties

        public bool _IsInitialled { get; protected set; }

        #endregion Properties

        #region Methods

        private void ChangeListItem(ListView listView, int index, object status)
        {
            ListViewItem listItem = listView.ItemContainerGenerator.ContainerFromIndex(index) as ListViewItem;

            if (listItem == null)
            {
                return;
            } // else

            if (status == null)
            {
                return;
            } // else

            if (status.ToString() != Idx.DigitalValue.Disable.ToString())
            {
                listItem.Foreground = System.Windows.Media.Brushes.Green;
            }
            else
            {
                listItem.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void CheckStatus(ListView listView, ref ObservableCollection<IoStatus> Iolist)
        {
            int index = 0;
            try
            {
                foreach (IoStatus ioStatus in Iolist)
                {
                    Tag checkTag;

                    if (Global.AjinIo.GetType().GetProperty(ioStatus.Name) != null)
                    {
                        checkTag = (Tag)Global.AjinIo.GetType().GetProperty(ioStatus.Name).GetValue(Global.AjinIo);
                    }
                    else
                    {
                        //Io 만 체크한다.
                        return;
                    }

                    object status = checkTag.Is;

                    if (status != null)
                    {
                        ioStatus.Value = status.ToString();
                    } // else

                    this.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
                    {
                        ChangeListItem(listView, index, status);
                    });

                    index++;
                }
            }
            catch (System.Exception ex)
            {
                Logger.Error("Error Io : " + index.ToString());
                Logger.Exception(ex);
            }
        }

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-04 11:30
        /// Description : EmWorks base thread.
        /// </summary>
        private void EmWorksProc(object state)
        {
            while (_isLoop)
            {
                if (this.IsVisible == false)
                {
                    _isLoop = false;
                    break;
                } // else

                if (LoadList() == false)
                {
                    Thread.Sleep(_loopInterval);
                    Func.DoEvents();

                    continue;
                } // else

                CheckStatus(lvInput, ref _ioInputStatus);
                CheckStatus(lvOutput, ref _ioOutputStatus);

                Thread.Sleep(_loopInterval);
                Func.DoEvents();
            }
        }

        /// <summary>
        /// Author : DongJun.Kim
        /// Date :  2020-09-04 11:30
        /// Description : controls initialization.
        /// </summary>
        private int InitControls()
        {
            return Idx.FunctionResult.True;
        }

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-04 11:30
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
                _IsInitialled = false;
            }
            else
            {
                _IsInitialled = false;
            }
        }

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-04 11:30
        /// Description :  Instance initialization.
        /// </summary>
        private int InitInstance()
        {
            try
            {
                _IsInitialled = false;
                _loopInterval = 5;
                _isLoop = false;

                return Idx.FunctionResult.True;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return -1;
            }
        }

        private bool LoadList()
        {
            try
            {
                // 한번만 실행
                if (_ioInputStatus != null)
                {
                    return true;
                } // else

                if (Global.AjinIo == null)
                {
                    return false;
                } // else

                _ioInputStatus = new ObservableCollection<IoStatus>();
                _ioOutputStatus = new ObservableCollection<IoStatus>();

                LoadTag(Global.AjinIo.GetTags()); // Io

                this.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    this.lvInput.ItemsSource = _ioInputStatus;
                    this.lvOutput.ItemsSource = _ioOutputStatus;

                    // Virtual Mode 꺼주기 ( 끄지 않은 경우 전체 리스트 확인이 되지 않음 )
                    //VirtualizingStackPanel.SetIsVirtualizing(lvInput, false);
                    //VirtualizingStackPanel.SetIsVirtualizing(lvOutput, false);
                });

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        private bool LoadList(string device, string unit)
        {
            try
            {
                _ioInputStatus = new ObservableCollection<IoStatus>();
                _ioOutputStatus = new ObservableCollection<IoStatus>();

                LoadTag(Global.AjinIo.GetTags(), device, unit); // Io

                this.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    this.lvInput.ItemsSource = _ioInputStatus;
                    this.lvOutput.ItemsSource = _ioOutputStatus;

                    // Virtual Mode 꺼주기 ( 끄지 않은 경우 전체 리스트 확인이 되지 않음 )
                    //VirtualizingStackPanel.SetIsVirtualizing(lvInput, false);
                    //VirtualizingStackPanel.SetIsVirtualizing(lvOutput, false);
                });

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        private void LoadTag(Dictionary<string, Tag> tags, string device = "", string unit = "")
        {
            IoStatus temp;

            int inputIndex = 0;
            int outputIndex = 0;

            if (device == string.Empty && unit == string.Empty)
            {
                // 전체
                //tags = dictionary;
            }
            else if (device != string.Empty && unit == string.Empty)
            {
                tags = (from item in tags
                        where item.Value.Identity.Device == device
                        select item).ToDictionary(x => x.Key, y => y.Value);
            }
            else if (device == string.Empty &&
                unit != string.Empty)
            {
                tags = (from item in tags
                        where item.Value.Identity.Unit == unit
                        select item).ToDictionary(x => x.Key, y => y.Value);
            }
            else
            {
                tags = (from item in tags
                        where item.Value.Identity.Unit == unit &&
                        item.Value.Identity.Device == device
                        select item).ToDictionary(x => x.Key, y => y.Value);
            }

            foreach (Tag tg in tags.Values)
            {
                if (tg.Identity.IoType == 0 || tg.Identity.IoType == 2)
                {
                    temp = new IoStatus();

                    temp.No = inputIndex.ToString();
                    temp.Offset = tg.Identity.Offset.ToString();
                    temp.Name = tg.Name;
                    temp.DataType = tg.Identity.DataType.ToString();
                    temp.Status = "0";

                    if (tg.Identity.Device != null)
                    {
                        temp.Device = tg.Identity.Device;
                    }
                    else
                    {
                        temp.Device = "";
                    }

                    temp.Unit = tg.Identity.Unit;
                    temp.Desc = tg.Identity.Desc;

                    _ioInputStatus.Add(temp);

                    inputIndex++;
                }
                else
                {
                    temp = new IoStatus();

                    temp.No = outputIndex.ToString();
                    temp.Offset = tg.Identity.Offset.ToString();
                    temp.Name = tg.Name;
                    temp.DataType = tg.Identity.DataType.ToString();
                    temp.Status = "0";

                    if (tg.Identity.Device != null)
                    {
                        temp.Device = tg.Identity.Device;
                    }
                    temp.Unit = tg.Identity.Unit;
                    temp.Desc = tg.Identity.Desc;

                    _ioOutputStatus.Add(temp);

                    outputIndex++;
                }
            }
        }

        private void LsvOutput_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                IoStatus ioTag = (IoStatus)lvOutput.SelectedItem;

                if (ioTag.DataType.ToString() == EmWorks.Lib.Core.Tag.Idx.DataType.Int.ToString())
                {
                    RunDigitalCommand(ioTag);
                }
                else
                {
                    RunAnalogCommand(ioTag);
                }
            }
            catch
            {
                // 그냥 리턴한다.
                return;
            }
        }

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-04 11:30
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            this.IsVisibleChanged += UcMaintIO_IsVisibleChanged;

            this.lvOutput.PreviewMouseDoubleClick += LsvOutput_PreviewMouseDoubleClick;

            return Idx.FunctionResult.True;
        }

        private void RunAnalogCommand(IoStatus ioStatus)
        {
            if (SearchAnalogIoTag(ioStatus) == true)
            {
                return;
            } // else
        }

        private void RunDigitalCommand(IoStatus ioStatus)
        {
            Tag tag = Global.AjinIo[ioStatus.Name];

            DigitalCommand dd = new DigitalCommand(tag);
            dd.ShowDialog();
        }

        private bool SearchAnalogIoTag(IoStatus ioStatus)
        {
            Tag tag = null;

            try
            {
                tag = Global.AjinIo[ioStatus.Name];

                if (tag != null)
                {
                    AnalogCommand ac = new AnalogCommand(tag);
                    ac.ShowDialog();
                    return true;
                } // else

                return false;
            }
            catch
            {
                return false;
            }
        }

        private void UcMaintIO_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (_isFirstVisible == true)
            {
                _isFirstVisible = false;
                return;
            } // else

            if (this.IsVisible == true)
            {
                _isLoop = true;

                ThreadPool.QueueUserWorkItem(EmWorksProc);
            } // else
        }

        #endregion Methods

        #region Classes

        public class IoStatus : ViewModelBase
        {
            #region Fields

            private string _value;

            #endregion Fields

            #region Properties

            public string DataType { get; set; }
            public string Desc { get; set; }
            public string Device { get; set; }
            public string Name { get; set; }
            public string No { get; set; }
            public string Offset { get; set; }
            public string Status { get; set; }
            public string Unit { get; set; }

            public string Value
            {
                get
                {
                    return _value;
                }
                set
                {
                    _value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }

            #endregion Properties
        }

        #endregion Classes
    }
}