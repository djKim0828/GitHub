using EmWorks.Lib.Common;
using System;
using System.Diagnostics;
using System.Windows;

namespace EmWorks.App.OpticInspection
{
    public class MainStadium
    {
        #region Fields

        private Main _main = null;

        #endregion Fields

        #region Constructors

        public MainStadium(object main)
        {
            _main = main as Main;
        }

        #endregion Constructors

        #region Methods

        internal void Switching(object sendor, StatusMachine.EventChangedStatusArgs e)
        {
            switch (e.GroupEnumId)
            {
                case StatusType.Group.Default:
                    SwitchingDefault(e);
                    break;

                case StatusType.Group.Application:
                    SwitchingApplication(e);
                    break;

                case StatusType.Group.Language:
                    SwitchingLanguage(e);
                    break;

                case StatusType.Group.User:
                    SwitchingUser(e);
                    break;

                case StatusType.Group.Alarm:
                    SwitchingAlarm(e);
                    break;

                case StatusType.Group.Operation:
                    SwitchingOperation(e);
                    break;

                case StatusType.Group.Maint:
                    SwitchingMaint(e);
                    break;

                case StatusType.Group.Recipe:
                    SwitchingRecipe(e);
                    break;

                case StatusType.Group.Data:
                    SwitchingData(e);
                    break;

                default:
                    Logger.Debug($"{nameof(Switching)}() - Undefined Group ID");
                    break;
            }
        }

        private AlarmModel SaveAlarm(IRequestStatusInfo info, StatusType.Alarm sw)
        {
            string tempId = info.Parameters[Idx.AlarmEventIndex.Id].ToString();

            AlarmModel tempData = new AlarmModel(tempId, tempId);

            if (AlarmDefine.GetData(tempId, ref tempData) == false)

            Global._AlarmWriter.SaveData(ref tempData, sw);

            return tempData;
        }

        private void SwitchingAlarm(IRequestStatusInfo info)
        {
            try
            {
                App.Current.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    StatusType.Alarm sw = StatusMachine.ConvertToEnum<StatusType.Alarm>.Parse(info.StatusId);

                    switch (sw)
                    {
                        case StatusType.Alarm.Default:
                            break;

                        case StatusType.Alarm.Occurred:

                            AlarmModel tempdata = SaveAlarm(info, sw);
                            // Todo : Alarm 출력
                            AlarmBox alarm = new AlarmBox(tempdata);
                            alarm.ShowDialog();

                            break;

                        case StatusType.Alarm.Released:
                            SaveAlarm(info, sw);
                            break;

                        case StatusType.Alarm.ChangedClearLevel:
                            break;

                        case StatusType.Alarm.Max:
                            break;

                        default:
                            Logger.Debug($"{nameof(SwitchingAlarm)}() - Undefined Status ID");
                            break;
                    }
                });
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        private void SwitchingApplication(IRequestStatusInfo info)
        {
            StatusType.Application sw = StatusMachine.ConvertToEnum<StatusType.Application>.Parse(info.StatusId);

            switch (sw)
            {
                case StatusType.Application.Default:
                    break;

                case StatusType.Application.Exit:
                    MessageBox dlg = new MessageBox("Are you sure you want to exit?", "Question", MessageBoxButtonType.YESNO, MessageBoxImageType.QUESTION);
                    dlg.ShowDialog();
                    if (dlg.ReturnValue == MessageBoxReturnValue.YES)
                    {
                        Environment.Exit(0);
                        System.Diagnostics.Process.GetCurrentProcess().Kill();
                        _main.Close();
                    }

                    break;

                case StatusType.Application.ExeLogger:
                    Process.Start(@"..\Log\LogViewer.exe");
                    break;

                case StatusType.Application.Max:
                    break;

                default:
                    Logger.Debug($"{nameof(SwitchingApplication)}() - Undefined Status ID");
                    break;
            }
        }

        private void SwitchingData(IRequestStatusInfo info)
        {
            StatusType.Data sw = StatusMachine.ConvertToEnum<StatusType.Data>.Parse(info.StatusId);
            switch (sw)
            {
                case StatusType.Data.Default:
                    break;

                case StatusType.Data.ChangedView:
                    _main.HiddenAllSubMenuWindows();
                    _main.DtMenu.Visibility = Visibility.Visible;
                    _main.ShowRdbLog();
                    break;

                case StatusType.Data.Log:
                    _main.HiddenAllSubWindows();
                    _main.DtLog.Visibility = Visibility.Visible;
                    break;

                case StatusType.Data.Alarm:
                    _main.HiddenAllSubWindows();
                    _main.DtAlarm.Visibility = Visibility.Visible;
                    break;

                case StatusType.Data.Product:
                    _main.HiddenAllSubWindows();
                    //_main.DtProduct.Visibility = Visibility.Visible;
                    break;

                case StatusType.Data.Mcc:
                    break;

                case StatusType.Data.Max:
                    break;

                default:
                    Logger.Debug($"{nameof(SwitchingData)}() - Undefined Status ID");
                    break;
            }
            _main.ChangeMainControls(info.StatusId);
        }

        private void SwitchingDefault(IRequestStatusInfo info)
        {
            StatusType.Default sw = StatusMachine.ConvertToEnum<StatusType.Default>.Parse(info.StatusId);
            switch (sw)
            {
                case StatusType.Default.Default:
                    break;

                case StatusType.Default.Max:
                    break;

                default:
                    Logger.Debug($"{nameof(SwitchingDefault)}() - Undefined Status ID");
                    break;
            }
        }

        private void SwitchingLanguage(IRequestStatusInfo info)
        {
            string countryName = info.StatusName;

            // hans, Kim.
            if (countryName == StatusType.Language.Korean.ToString())
            {
                Global.ConfigGeneral.Language = i18n.LanguageType.KO.ToString();
                i18n.SetLanguageType(i18n.LanguageType.KO);
            }
            else
            {
                Global.ConfigGeneral.Language = i18n.LanguageType.EN.ToString();
                i18n.SetLanguageType(i18n.LanguageType.EN);
                Logger.Debug($"{nameof(SwitchingLanguage)}({countryName}) - Undefined Status ID");
            }

            Global.ConfigGeneral.Save();

            _main.ChangeLocale(_main);
        }

        private void SwitchingMaint(IRequestStatusInfo info)
        {
            StatusType.Maint sw = StatusMachine.ConvertToEnum<StatusType.Maint>.Parse(info.StatusId);
            switch (sw)
            {
                case StatusType.Maint.Default:
                    break;

                case StatusType.Maint.ChangedView:
                    _main.HiddenAllSubMenuWindows();
                    _main.MtMenu.Visibility = Visibility.Visible;
                    _main.ShowRdbManual();

                    break;

                case StatusType.Maint.Manual:
                    _main.HiddenAllSubWindows();
                    _main.MtManual.Visibility = Visibility.Visible;

                    break;

                case StatusType.Maint.Io:
                    _main.HiddenAllSubWindows();
                    _main.MtViewIo.Visibility = Visibility.Visible;
                    break;

                default:
                    Logger.Debug($"{nameof(SwitchingMaint)}() - Undefined Status ID");
                    break;
            }
            _main.ChangeMainControls(info.StatusId);
        }

        private void SwitchingOperation(IRequestStatusInfo info)
        {
            try
            {
                App.Current.Dispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    StatusType.Operation sw = StatusMachine.ConvertToEnum<StatusType.Operation>.Parse(info.StatusId);
                    switch (sw)
                    {
                        case StatusType.Operation.Default:
                            break;

                        case StatusType.Operation.ChangedView:
                            _main.HiddenAllSubWindows();
                            _main.HiddenAllSubMenuWindows();

                            _main.HiddenAllSubWindows(false);

                            _main.OpView.Visibility = Visibility.Visible;
                            _main.OpMenu.Visibility = Visibility.Visible;
                            _main.ChangeMainControls(info.StatusId);

                            _main.OpView.lblSubTitle.Content = "Select";
                            break;

                        case StatusType.Operation.Start:
                            _main.OpView.lblSubTitle.Content = _main.OpMenu.rdbStart.Content;
                            _main.OpMenu.rdbStart.IsChecked = true;
                            _main.OpMenu.SetButtonEnable();
                            CommandCenter.ChangeStatus(sw);
                            break;

                        case StatusType.Operation.Stop:
                            _main.OpView.lblSubTitle.Content = _main.OpMenu.rdbStop.Content;
                            _main.OpMenu.rdbStop.IsChecked = true;
                            _main.OpMenu.SetButtonEnable();
                            CommandCenter.ChangeStatus(sw);
                            break;

                        case StatusType.Operation.Pause:
                            _main.OpView.lblSubTitle.Content = _main.OpMenu.rdbPause.Content;
                            _main.OpMenu.rdbPause.IsChecked = true;
                            _main.OpMenu.SetButtonEnable();
                            CommandCenter.ChangeStatus(sw);
                            break;

                        case StatusType.Operation.Resume:
                            _main.OpView.lblSubTitle.Content = _main.OpMenu.rdbStop.Content;
                            _main.OpMenu.rdbResume.IsChecked = true;
                            _main.OpMenu.SetButtonEnable();
                            CommandCenter.ChangeStatus(sw);
                            break;

                        case StatusType.Operation.Abort:
                            _main.OpView.lblSubTitle.Content = _main.OpMenu.rdbAbort.Content;
                            _main.OpMenu.rdbAbort.IsChecked = true;
                            _main.OpMenu.SetButtonEnable();
                            CommandCenter.ChangeStatus(sw);
                            break;

                        case StatusType.Operation.Max:
                            break;

                        default:
                            Logger.Debug($"{nameof(SwitchingOperation)}() - Undefined Status ID");

                            break;
                    }
                });
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        private void SwitchingRecipe(IRequestStatusInfo info)
        {
            StatusType.Recipe sw = StatusMachine.ConvertToEnum<StatusType.Recipe>.Parse(info.StatusId);
            switch (sw)
            {
                case StatusType.Recipe.Default:
                    break;

                case StatusType.Recipe.ChangedView:
                    _main.HiddenAllSubWindows();
                    _main.HiddenAllSubMenuWindows();
                    _main.RpView.Visibility = Visibility.Visible;
                    _main.RpMenu.Visibility = Visibility.Visible;
                    break;

                case StatusType.Recipe.Recipe:
                    break;

                case StatusType.Recipe.Max:
                    break;

                default:
                    Logger.Debug($"{nameof(SwitchingRecipe)}() - Undefined Status ID");
                    break;
            }
            _main.ChangeMainControls(info.StatusId);
        }

        private void SwitchingUser(IRequestStatusInfo info)
        {
            StatusType.User sw = StatusMachine.ConvertToEnum<StatusType.User>.Parse(info.StatusId);
            switch (sw)
            {
                case StatusType.User.Default:
                    break;

                case StatusType.User.Master:
                    break;

                case StatusType.User.Engineer:
                    break;

                case StatusType.User.Operator:
                    break;

                case StatusType.User.Other:
                    break;

                case StatusType.User.Max:
                    break;

                default:
                    Logger.Debug($"{nameof(SwitchingUser)}() - Undefined Status ID");
                    break;
            }
        }

        #endregion Methods
    }
}