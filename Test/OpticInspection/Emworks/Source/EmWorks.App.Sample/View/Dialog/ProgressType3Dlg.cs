using EmWorks.Lib.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace EmWorks.App.Sample
{
    public partial class ProgressType3Dlg : Form
    {
        MethodInfo[] _functions;
        object _classObject;
        string _namespaceName;
        string _className;

        public ProgressType3Dlg(string namespaceName, string className)
        {
            InitializeComponent();

            _namespaceName = namespaceName;
            _className = className;

        }

        private bool GetFunctions(object targetClass)
        {
            try
            {
                _functions = targetClass.GetType().
                    GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);                

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }
           
        }

        private void WriteLog(string data)
        {
            rcbOutput.Invoke(new MethodInvoker(() =>
            {
                String nowTime = "[" + System.DateTime.Now.ToString() + "] ";

                rcbOutput.AppendText(nowTime + data + "\r\n");
                rcbOutput.SelectionStart = rcbOutput.Text.Length;
                rcbOutput.ScrollToCaret();
            }));

        }

        private void RunProcess()
        {
            foreach(MethodInfo mi in _functions)
            {
                WriteLog("Start Function [ " + mi.Name + "]");

                object[] parametersArray = new object[] { "" };

                object result =  mi.Invoke(_classObject, new object[]{});

                if ((bool)result == false)
                {
                    WriteLog("***** Failed [ " + mi.Name + "]");
                }
                else
                {
                    WriteLog("Success [ " + mi.Name + "]");
                }

                FunctionprogressBar.Invoke(new MethodInvoker(() =>
                {
                    FunctionprogressBar.Value++;
                }));
            }
        }

        private void ProgressType3Dlg_Load(object sender, EventArgs e)
        {
            try
            {
                Assembly assembly = Assembly.Load(_namespaceName);

                Type type = assembly.GetType(_namespaceName + "." + _className);// 클래스 이름에 해당하는 Type을 가져옮
                _classObject = Activator.CreateInstance(type);

                if (GetFunctions(_classObject) == false)
                {
                    MessageBox.Show("Check a namespace name or a class name.");
                    this.Close();
                    return;
                }

                FunctionprogressBar.Maximum = _functions.Length;

                Action action = () =>
                {
                    RunProcess();        
                };

                action.BeginInvoke(null, null);
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                MessageBox.Show("Check a namespace name or a class name.");
                this.Close();
            }
        }
    }
}
