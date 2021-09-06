using System;
using System.Diagnostics;
using System.Windows;
using EmWorks.View;

namespace FPMS.E2105_FS111_121
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        #region Fields

        public static string strCurProcName = string.Empty;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// app 시작(생성)
        /// </summary>
        public App()
        {
            if (DuplicateExecutionCheck() == false)
            {
                return;
            }

            SetWPFResource(); // WPF 즉시 적용

            Main main = new Main();
            main.ShowDialog();
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// app 소멸
        /// </summary>
        ~App()
        {
        }

        #endregion Destructors

        #region Methods

        public bool DuplicateExecutionCheck()
        {
            // 중복 실행 체크
            int nMainCount = 0, nCurProcID = 0;

            Process[] ProcList = Process.GetProcesses();

            nCurProcID = Process.GetCurrentProcess().Id;
            strCurProcName = Process.GetCurrentProcess().ProcessName;

            foreach (Process ProcItem1 in ProcList)
            {
                if (ProcItem1.ProcessName.ToLower().Equals(strCurProcName.ToLower()))
                    nMainCount++;
            }

            if (nMainCount > 1)
            {
                // 중복
                string strMessage = string.Empty, strTitle = string.Empty;

                if (strMessage == string.Empty)
                    strMessage = "Main Application is already running. Do you want to quit the process?";

                if (strTitle == string.Empty)
                {
                    strTitle = "Main Application";

                    MessageBox.Show(strMessage,
                                                            strTitle,
                                                            MessageBoxButtonType.OKCANCEL,
                                                            MessageBoxImageType.QUESTION);
                    return false;
                }
            }

            return true;
        }

        private void SetWPFResource()
        {
            Application.Current.Resources.MergedDictionaries.Clear();

            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("Resources/resStyleCommon.xaml", UriKind.RelativeOrAbsolute)
            });

            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("Resources/resIconPathData.xaml", UriKind.RelativeOrAbsolute)
            });

            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("Resources/resStyleCommon.xaml", UriKind.RelativeOrAbsolute)
            });
        }

        #endregion Methods
    }
}