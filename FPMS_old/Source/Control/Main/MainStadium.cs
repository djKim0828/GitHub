using EmWorks.Lib.Common;
using System;
using System.Windows;

namespace FPMS.E2105_FS111_121
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

                case StatusType.Group.Language:
                    SwitchingLanguage(e);
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

        private void SwitchingData(IRequestStatusInfo info)
        {
            StatusType.Data sw = StatusMachine.ConvertToEnum<StatusType.Data>.Parse(info.StatusId);
            switch (sw)
            {
                case StatusType.Data.Default:
                    break;

                case StatusType.Data.ChangedView:
                    _main.HiddenAllSubMenuWindows();
                    _main.RpMenu.Visibility = Visibility.Visible;
                    _main.rdbDtLogger();
                    break;

                case StatusType.Data.Logger:
                    _main.HiddenAllSubWindows();
                    _main.HiddenAllSubMenuWindows();
                    _main.DtLogger.Visibility = Visibility.Visible;
                    _main.DtMenu.Visibility = Visibility.Visible;
                    break;

                case StatusType.Data.Alarm:
                    _main.HiddenAllSubWindows();
                    _main.HiddenAllSubMenuWindows();
                    _main.DtAlarm.Visibility = Visibility.Visible;
                    _main.DtMenu.Visibility = Visibility.Visible;
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
                case StatusType.Maint.Exit:
                    MessageBox dlg = new MessageBox("Are you sure you want to exit?", "Question", MessageBoxButtonType.YESNO, MessageBoxImageType.QUESTION);
                    dlg.ShowDialog();
                    if (dlg.ReturnValue == MessageBoxReturnValue.YES)
                    {
                        Environment.Exit(0);
                        System.Diagnostics.Process.GetCurrentProcess().Kill();
                        _main.Close();
                    }  // else

                    break;
                case StatusType.Maint.initalize:
                    break;
                case StatusType.Maint.ChangedView:
                    _main.HiddenAllSubMenuWindows();
                    _main.MtMenu.Visibility = Visibility.Visible;
                    _main.rdbMotion();
                    break;

                case StatusType.Maint.Motion:
                    _main.HiddenAllSubWindows();
                    _main.HiddenAllSubMenuWindows();
                    _main.MtMotion.Visibility = Visibility.Visible;
                    _main.MtMenu.Visibility = Visibility.Visible;
                    break;

                case StatusType.Maint.Detector:
                    _main.HiddenAllSubWindows();
                    _main.HiddenAllSubMenuWindows();
                    _main.MtDetector.Visibility = Visibility.Visible;
                    _main.MtMenu.Visibility = Visibility.Visible;
                    break;

                case StatusType.Maint.Chamber:
                    _main.HiddenAllSubWindows();
                    _main.HiddenAllSubMenuWindows();
                    _main.MtChamber.Visibility = Visibility.Visible;
                    _main.MtMenu.Visibility = Visibility.Visible;
                    break;

                case StatusType.Maint.Monitoring:
                    _main.HiddenAllSubWindows();
                    _main.HiddenAllSubMenuWindows();
                    _main.MtMonitoring.Visibility = Visibility.Visible;
                    _main.MtMenu.Visibility = Visibility.Visible;
                    break;

                case StatusType.Maint.Io:
                    _main.HiddenAllSubWindows();
                    _main.HiddenAllSubMenuWindows();
                    _main.MtIo.Visibility = Visibility.Visible;
                    _main.MtMenu.Visibility = Visibility.Visible;
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
                            _main.HiddenAllSubMenuWindows();
                            _main.OpMenu.Visibility = Visibility.Visible;
                            _main.rdbOpMain();
                            break;

                        case StatusType.Operation.OpMain:
                            _main.HiddenAllSubWindows();
                            _main.HiddenAllSubMenuWindows();
                            _main.OpMain.Visibility = Visibility.Visible;
                            _main.OpMenu.Visibility = Visibility.Visible;
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
                    _main.HiddenAllSubMenuWindows();
                    _main.RpMenu.Visibility = Visibility.Visible;
                    _main.rdbRpMain();
                    break;

                case StatusType.Recipe.RpMain:
                    _main.HiddenAllSubWindows();
                    _main.HiddenAllSubMenuWindows();
                    _main.RpMain.Visibility = Visibility.Visible;
                    _main.RpMenu.Visibility = Visibility.Visible;
                    break;
            }
            _main.ChangeMainControls(info.StatusId);
        }

        #endregion Methods
    }
}