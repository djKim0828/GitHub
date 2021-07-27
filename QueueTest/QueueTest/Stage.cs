using System;
using System.Threading;
using System.Windows.Forms;

namespace QueueTest
{
    public class Stage : StageBase
    {
        #region Fields

        private Control _myUI;

        #endregion Fields

        #region Constructors

        public Stage(int stageId, Control myUI) : base(stageId)
        {
            _myUI = myUI;
        }

        #endregion Constructors



        #region Methods

        public override void ProcessStatus()
        {
            
            ChangeCellStatus();

            // 아래는 실제 Code 적용

            if (_ProcessStep == Idx.StageStap.Step1)
            {
                _myUI.Invoke(new MethodInvoker(() =>
                {
                    if (CheckCommmadCenterStatus() == false)
                    {
                        return;
                    }

                    int startTop = _myUI.Top + 30;
                    foreach (Cell cell in _MyCells.Values)
                    {
                        Button btn = (Button)cell.Temp;

                        btn.Left = _myUI.Left + 10;
                        btn.Top = startTop;
                        startTop = startTop + btn.Height + 10;
                    }
                }));

                Thread.Sleep(10);
                Application.DoEvents();

                _ProcessStep++;
            }
            else if (_ProcessStep == Idx.StageStap.Step2)
            {
                _myUI.Invoke(new MethodInvoker(() =>
                {
                    for (int i = 0; i < 20; i++)
                    {
                        foreach (Cell cell in _MyCells.Values)
                        {
                            if (CheckCommmadCenterStatus() == false)
                            {
                                return;
                            }                                                     

                            Button btn = (Button)cell.Temp;

                            btn.Left = btn.Left + 5;
                            btn.Top = btn.Top + 5;

                            Thread.Sleep(10);
                            Application.DoEvents();
                        }
                    }
                }));

                _ProcessStep++;
            }
            else if (_ProcessStep == Idx.StageStap.Step3)
            {
                _myUI.Invoke(new MethodInvoker(() =>
                {
                    for (int i = 0; i < 20; i++)
                    {
                        foreach (Cell cell in _MyCells.Values)
                        {
                            if (CheckCommmadCenterStatus() == false)
                            {
                                return;
                            }

                            Button btn = (Button)cell.Temp;

                            btn.Left = btn.Left - 5;
                            btn.Top = btn.Top - 5;

                            Thread.Sleep(10);
                            Application.DoEvents();
                        }
                    }
                }));

                _ProcessStep++;
            }
            else if (_ProcessStep == Idx.StageStap.Step4)
            {
                _Status = Idx.StageStatus.Complate;
            }
        }

        #endregion Methods
    }
}