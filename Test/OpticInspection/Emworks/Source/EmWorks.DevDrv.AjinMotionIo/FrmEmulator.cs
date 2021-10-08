using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace EmWorks.DevDrv.AjinMotionIo
{
    public partial class FrmEmulator : Form
    {
        #region Fields

        private Dictionary<string, Tag> _xTags;
        private Dictionary<string, Tag> _yTags;

        private bool isLoop = false;

        private int loopInterval;

        #endregion Fields

        #region Constructors

        public FrmEmulator(Dictionary<string, Tag> xTags, Dictionary<string, Tag> yTags)
        {
            _xTags = xTags;
            _yTags = yTags;

            InitializeComponent();
            Initialize();
        }

        #endregion Constructors

        #region Properties

        public bool IsInitialled { get; protected set; }

        #endregion Properties

        #region Methods

        private void DgvInputList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                dgvInputList.EndEdit();

                DataGridViewCheckBoxCell cell = dgvInputList.Rows[e.RowIndex].Cells[1] as DataGridViewCheckBoxCell;

                bool isChecked = Convert.ToBoolean(cell.Value);

                string tagName = dgvInputList.Rows[e.RowIndex].Cells[0].Value.ToString();

                if (isChecked == true)
                {
                    _xTags[tagName].Is = 1;
                }
                else
                {
                    _xTags[tagName].Is = 0;
                }
            }
        }

        /// <summary>
        /// Author :  Hans, KIM
        /// Date :  2020-09-17 13:21 (create or edit date.)
        /// Description : EmWorks base thread.
        /// </summary>
        private void EmWorksProc(object state)
        {
            try
            {
                while (isLoop)
                {
                    Thread.Sleep(loopInterval);

                    Invoke(new MethodInvoker(() =>
                    {
                        UpdateYTags();
                    }));

                    loopInterval = 5;
                }
            }
            catch (System.Exception ex)
            {
            	
            }
            
        }

        private int InitControl(DataGridView dgvList)
        {
            try
            {
                DataGridViewTextBoxColumn box = new DataGridViewTextBoxColumn();
                box.HeaderText = "Name";
                box.ValueType = typeof(System.String);
                //box.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                box.Width = 250;
                box.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvList.Columns.Add(box);

                DataGridViewCheckBoxColumn col = new DataGridViewCheckBoxColumn();
                col.HeaderText = "Status";
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                col.TrueValue = true;
                col.FalseValue = false;
                dgvList.Columns.Add(col);

                return 0;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return -1;
            }
        }

        /// <summary>
        /// Author : DongJun.Kim
        /// Date :  2020-09-03 11:30 (create or edit date.)
        /// Description : controls initialization.
        /// </summary>
        private int InitControls()
        {
            int result = InitControl(dgvInputList);

            if (result != 0)
            {
                return -1;
            } // else

            result = InitControl(dgvOutputList);

            if (result != 0)
            {
                return -1;
            } // else

            result = SetInputList(dgvInputList);

            if (result != 0)
            {
                return -1;
            } // else

            result = SetOutputList(dgvOutputList);

            if (result != 0)
            {
                return -1;
            } // else

            return 0;
        }

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-03 11:30 (create or edit date.)
        /// Description :  object initialization.
        /// </summary>
        private void Initialize()
        {
            int resultControls = InitControls();
            int resultEvent = RegistEvents();

            if (resultControls == 0 && resultEvent == 0)
            {
                IsInitialled = true;

                loopInterval = 1000;
                isLoop = true;
                
                ThreadPool.QueueUserWorkItem(EmWorksProc);
            }
            else
            {
                IsInitialled = false;
            }
        }

        /// <summary>
        /// Author :  DongJun.Kim
        /// Date :  2020-09-03 11:30 (create or edit date.)
        /// Description : regist events
        /// </summary>
        private int RegistEvents()
        {
            dgvInputList.CellContentClick += DgvInputList_CellContentClick;

            return 0;
        }

        private int SetInputList(DataGridView dgvList)
        {
            try
            {
                dgvList.Rows.Clear();
                foreach (Tag tag in _xTags.Values)
                {
                    DataGridViewRow row = (DataGridViewRow)dgvList.Rows[0].Clone();
                    row.Cells[0].Value = tag.Name;
                    row.Cells[0].ReadOnly = true;

                    row.Cells[1].Value = false;

                    dgvList.Rows.Add(row);
                }

                dgvList.AllowUserToAddRows = false;

                return 0;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return -1;
            }
        }

        private int SetOutputList(DataGridView dgvList)
        {
            try
            {
                dgvList.Rows.Clear();
                foreach (Tag tag in _yTags.Values)
                {
                    DataGridViewRow row = (DataGridViewRow)dgvList.Rows[0].Clone();
                    row.Cells[0].Value = tag.Name;
                    row.Cells[0].ReadOnly = true;

                    row.Cells[1].ReadOnly = true;

                    dgvList.Rows.Add(row);
                }

                dgvList.AllowUserToAddRows = false;

                return 0;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return -1;
            }
        }

        private void UpdateYTags()
        {
            foreach (DataGridViewRow row in dgvOutputList.Rows)
            {
                string tagName = row.Cells[0].Value.ToString();

                if (_yTags[tagName].Is == null)
                {
                    continue;
                }

                if (_yTags[tagName].Is.ToString() == "1")
                {
                    row.Cells[1].Value = true;
                }
                else
                {
                    row.Cells[1].Value = false;
                }
            }
        }

        #endregion Methods
    }
}