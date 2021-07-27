using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace QueueTest
{
    public partial class UCStage : UserControl
    {
        private bool _isLoop = false;
        private int _Interval = 500;

        private Stage _Stage;

        public UCStage()
        {
            InitializeComponent();
        }

        private void UCStage_Load(object sender, EventArgs e)
        {
            
        }

        private Thread _WorkProcThread;

        public void Start(int id)
        {
            if (_Stage != null)
            {                
                _Stage.InitInstance();
                
                return;
            }

            _Stage = new Stage(id, this);

            _isLoop = true;

            _WorkProcThread = new Thread(WorksProc);
            _WorkProcThread.IsBackground = true;
            _WorkProcThread.Start();
            //ThreadPool.QueueUserWorkItem(WorksProc);
        }

        private void WorksProc(object state)
        {
            while (_isLoop)
            {
                lblStatus.Invoke(new MethodInvoker(() =>
                {
                    if (lblStatus.BackColor == Color.LightGreen)
                    {
                        lblStatus.BackColor = Color.White;
                    }
                    else
                    {
                        lblStatus.BackColor = Color.LightGreen;
                    }
                }));

                if (_Stage.GetStatus() < Idx.StageStatus.Busy)
                {
                    lblBusy.BackColor = Color.LightGreen;
                }
                else if (_Stage.GetStatus() == Idx.StageStatus.Busy)
                {
                    lblBusy.Invoke(new MethodInvoker(() =>
                    {
                        if (lblBusy.BackColor == Color.Maroon)
                        {
                            lblBusy.BackColor = Color.White;
                        }
                        else
                        {
                            lblBusy.BackColor = Color.Maroon;
                        }
                    }));
                }
                
                   
                Thread.Sleep(_Interval);
                Application.DoEvents();
            }
        }

    }
}
