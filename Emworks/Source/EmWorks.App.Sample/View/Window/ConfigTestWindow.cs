using EmWorks.Lib.Identity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EmWorks.App.Sample.View.Window
{
    public partial class ConfigTestWindow : Form
    {
        #region Fields

        //private Config _cf;

        #endregion Fields

        #region Constructors

        public ConfigTestWindow()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        public string ProjectMainDir()
        {
            string currentPath = System.Windows.Forms.Application.StartupPath;

            return currentPath;
        }

        private void btnCategoryDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int selectRowIndex = dgvGeneralCategory.SelectedCells[0].RowIndex;

                string category = string.Empty;
                if (dgvGeneralCategory.Rows[selectRowIndex].Cells[1].Value != null)
                {
                    category = dgvGeneralCategory.Rows[selectRowIndex].Cells[1].Value.ToString();
                }

                string name = string.Empty;
                if (dgvGeneralCategory.Rows[selectRowIndex].Cells[2].Value != null)
                {
                    name = dgvGeneralCategory.Rows[selectRowIndex].Cells[2].Value.ToString();
                }

                if (Config.Remove(category, name) == true)
                {
                    MessageBox.Show("Success");
                }
                else
                {
                    MessageBox.Show("Failed");
                    return;
                }

                RefreashGeneralList();

                RefreashGeneralCategoryList();
            }
            catch
            {
                MessageBox.Show("Failed");
            }
        }

        private void btnCategoryGet_Click(object sender, EventArgs e)
        {
            try
            {
                int selectRowIndex = dgvGeneralCategory.SelectedCells[0].RowIndex;

                string name = dgvGeneralCategory.Rows[selectRowIndex].Cells[2].Value.ToString();

                string result = Config.GetValue(cmbCategoryName.SelectedItem.ToString(), name);

                MessageBox.Show(result);
            }
            catch
            {
            }
        }

        private void btnCategoryInsert_Click(object sender, EventArgs e)
        {
            if (txtInsertName.Text == string.Empty)
            {
                MessageBox.Show("이름을 입력하세요.");
                return;
            }

            if (chkInserCategory.Checked == true)
            {
                if (txtInsertCategory.Text == string.Empty)
                {
                    MessageBox.Show("이름을 입력하세요.");
                    return;
                }

                // 같은 아이디 검사
                foreach (string ca in cmbCategoryName.Items)
                {
                    if (ca == txtInsertCategory.Text)
                    {
                        MessageBox.Show("추가할 Category 이름을 다시 확인하세요.");
                        return;
                    }
                }

                if (Config.insert(txtInsertCategory.Text,
                            txtInsertName.Text,
                            txtInsertValue.Text) == true)
                {
                    cmbCategoryName.Items.Add(txtInsertCategory.Text);
                }
                else
                {
                    MessageBox.Show("Failed");
                    return;
                }
            }
            else
            {
                if (Config.insert(cmbCategoryName.SelectedItem.ToString(),
                            txtInsertName.Text,
                            txtInsertValue.Text) == true)
                {
                    // N/A
                }
                else
                {
                    MessageBox.Show("Failed");
                    return;
                }
            }

            RefreashGeneralList();

            RefreashGeneralCategoryList();

            dgvGeneralCategory.FirstDisplayedScrollingRowIndex = dgvGeneralCategory.Rows.Count - 1;  // 마지막 Row로 포커스 이동

            MessageBox.Show("Success");
        }

        private void btnCategorySave_Click(object sender, EventArgs e)
        {
            int selectRowIndex = dgvGeneralCategory.SelectedCells[0].RowIndex;

            string category = cmbCategoryName.SelectedItem.ToString();
            if (dgvGeneralCategory.Rows[selectRowIndex].Cells[1].Value != null)
            {
                category = dgvGeneralCategory.Rows[selectRowIndex].Cells[1].Value.ToString();
            }

            string name = string.Empty;
            if (dgvGeneralCategory.Rows[selectRowIndex].Cells[2].Value != null)
            {
                name = dgvGeneralCategory.Rows[selectRowIndex].Cells[2].Value.ToString();
            }

            string valuse = string.Empty;
            if (dgvGeneralCategory.Rows[selectRowIndex].Cells[3].Value != null)
            {
                valuse = dgvGeneralCategory.Rows[selectRowIndex].Cells[3].Value.ToString();
            }

            Config.SetValue(category, name, txtCategoryChagneValue.Text);

            RefreashGeneralCategoryList();

            MessageBox.Show("Success");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int selectRowIndex = dgvGeneralList.SelectedCells[0].RowIndex;

                string category = string.Empty;
                if (dgvGeneralList.Rows[selectRowIndex].Cells[1].Value != null)
                {
                    category = dgvGeneralList.Rows[selectRowIndex].Cells[1].Value.ToString();
                }

                string name = string.Empty;
                if (dgvGeneralList.Rows[selectRowIndex].Cells[2].Value != null)
                {
                    name = dgvGeneralList.Rows[selectRowIndex].Cells[2].Value.ToString();
                }

                if (Config.Remove(category, name) == true)
                {
                    MessageBox.Show("Success");
                }
                else
                {
                    MessageBox.Show("Failed");
                    return;
                }

                RefreashGeneralList();

                RefreashGeneralCategoryList();
            }
            catch
            {
                MessageBox.Show("Failed");
            }
        }

        private void btnGeneralGet_Click(object sender, EventArgs e)
        {
            try
            {
                int selectRowIndex = dgvGeneralList.SelectedCells[0].RowIndex;

                string category = string.Empty;
                if (dgvGeneralList.Rows[selectRowIndex].Cells[1].Value != null)
                {
                    category = dgvGeneralList.Rows[selectRowIndex].Cells[1].Value.ToString();
                }

                string name = string.Empty;
                if (dgvGeneralList.Rows[selectRowIndex].Cells[2].Value != null)
                {
                    name = dgvGeneralList.Rows[selectRowIndex].Cells[2].Value.ToString();
                }

                string result = Config.GetValue(category, name);

                MessageBox.Show(result);
            }
            catch
            {
            }
        }

        private void btnGeneralGet2_Click(object sender, EventArgs e)
        {
            try
            {
                int selectRowIndex = dgvGeneralList.SelectedCells[0].RowIndex;

                string result = "재확인해주세요.";

                if (selectRowIndex == 0)
                {
                    result = Config.RunMode;
                }
                //else if (selectRowIndex == 1)
                //{
                //    result = _cf.Runmode2;
                //}
                //else if (selectRowIndex == 2)
                //{
                //    result = _cf.Runmode3;
                //}

                MessageBox.Show(result);
            }
            catch
            {
            }
        }

        private void btnGeneralInsert_Click(object sender, EventArgs e)
        {
            if (txtGeneralInsertName.Text == string.Empty)
            {
                MessageBox.Show("이름을 입력하세요.");
                return;
            }

            Config.insert(string.Empty, txtGeneralInsertName.Text, txtGeneralInsertValue.Text);

            RefreashGeneralList(); // 리스트 리플레쉬

            dgvGeneralList.FirstDisplayedScrollingRowIndex = dgvGeneralList.Rows.Count - 1;  // 마지막 Row로 포커스 이동

            MessageBox.Show("Success");
        }

        private void btnGeneralLoad_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.InitialDirectory = ProjectMainDir() + @"\Config";
            openFileDialog.Filter = "Json files (*.json)|*.json|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            string filePath = string.Empty;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Get the path of specified file
                filePath = openFileDialog.FileName;

                //_cf = new Config(filePath); // 객체 생성시에 설정파일(Json)을 파라미터로 넣으면 자동으로 Load된다.
                Config.InitInstance(filePath);

                dgvGeneralList.DataSource = new List<ConfigIdentity>(Config.GetAllData().Values);

                cmbCategoryName.Items.Clear();

                List<string> list = Config.GetCategory();

                cmbCategoryName.Items.Add("Select Category");

                foreach (string name in list)
                {
                    if (name == null) continue;

                    cmbCategoryName.Items.Add(name);
                }

                cmbCategoryName.SelectedIndex = 0;

                grbGeneral.Enabled = true;
                btnGeneralSave.Enabled = true;
            }
            else
            {
                return;
            }
        }

        private void btnGeneralSave_Click(object sender, EventArgs e)
        {
            Config.Save();

            dgvGeneralList.Refresh();
            dgvGeneralCategory.Refresh();

            MessageBox.Show("Success");
        }

        private void btnGeneralSaveTyp2_Click(object sender, EventArgs e)
        {
            int selectRowIndex = dgvGeneralList.SelectedCells[0].RowIndex;

            string category = string.Empty;
            if (dgvGeneralList.Rows[selectRowIndex].Cells[1].Value != null)
            {
                category = dgvGeneralList.Rows[selectRowIndex].Cells[1].Value.ToString();
            }

            string name = string.Empty;
            if (dgvGeneralList.Rows[selectRowIndex].Cells[2].Value != null)
            {
                name = dgvGeneralList.Rows[selectRowIndex].Cells[2].Value.ToString();
            }

            string valuse = string.Empty;
            if (dgvGeneralList.Rows[selectRowIndex].Cells[3].Value != null)
            {
                valuse = dgvGeneralList.Rows[selectRowIndex].Cells[3].Value.ToString();
            }

            Config.SetValue(category, name, txtGeneralChagneValue.Text);

            RefreashGeneralList();

            MessageBox.Show("Success");
        }

        private void btnGeneralSaveTyp3_Click(object sender, EventArgs e)
        {
            try
            {
                Config.RunMode = txtGeneralChagneValue.Text;

                RefreashGeneralList();

                MessageBox.Show("Success");
            }
            catch
            {
                MessageBox.Show("Failed");
            }
        }

        private void chkInserCategory_CheckedChanged(object sender, EventArgs e)
        {
            txtInsertCategory.Enabled = chkInserCategory.Checked;
        }

        private void cmbCategoryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreashGeneralCategoryList();
        }

        private void RefreashGeneralCategoryList()
        {
            List<ConfigIdentity> result = Config.GetNamefromCategory(cmbCategoryName.SelectedItem.ToString());
            dgvGeneralCategory.DataSource = result;
        }

        private void RefreashGeneralList()
        {
            dgvGeneralList.DataSource = new List<ConfigIdentity>(Config.GetAllData().Values);
        }

        #endregion Methods
    }
}