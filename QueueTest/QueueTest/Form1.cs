using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QueueTest
{
    public partial class Form1 : Form
    {
        Config _config;
        private String _path = Application.StartupPath + @"\config.json";

        public Form1()
        {
            InitializeComponent();

            initControl();

        }

        private void initControl()
        {
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            btnPause.Enabled = false;
            btnRestart.Enabled = false;
            btnAbort.Enabled = false;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //StartStages();

            _config = new Config();

            

            LoadConfig();

            //Todo : Cell 을 Load해서 넣는게 필요

        }

        private void SaveConfig()
        {
            if (_config == null)
            {
                _config = new Config();
            }            

            _config.RecipeList = new string[lsbRecipe.Items.Count];
            for (int i = 0; i < lsbRecipe.Items.Count; i++)
            {
                _config.RecipeList[i] = lsbRecipe.Items[i].ToString();
            }            

            JSON.Save(_path, _config);
        }

        private void LoadConfig()
        {
            _config = JSON.Load(_path);

            if (_config != null)
            {
                if (_config.RecipeList != null)
                {
                    for (int i = 0; i < _config.RecipeList.Length; i++)
                    {
                        lsbRecipe.Items.Add(_config.RecipeList[i]);
                    }
                }                
            }
        }

        private void StartStages()
        {
            ucStage1.Start(Idx.StageId.Load);
            ucStage2.Start(Idx.StageId.Inspection);
            ucStage3.Start(Idx.StageId.Unload);
        }

        private void StopStages()
        {
            //ucStage1.Stop();
            //ucStage2.Stop();
            //ucStage3.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CommandCenter.InsertCell(sender);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CommandCenter.InsertCell(sender);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CommandCenter.InsertCell(sender);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CommandCenter.InsertCell(sender);
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            InputDialog id = new InputDialog();
            id.ShowDialog();

            try
            {
                if (id.DigitaldialogResult != InputDialog.digitaldialogResult.Cancel)
                {
                    string data = id.GetInputData();
                    lsbRecipe.Items.Add(data);
                } // else
            }
            catch
            {
            }

            id.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lsbRecipe.SelectedItems.Count > 0)
            {
                lsbRecipe.Items.Remove(lsbRecipe.SelectedItems[0]);                
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveConfig();
        }

        private void btnListUp_Click(object sender, EventArgs e)
        {            
            if (lsbRecipe.SelectedIndex > 0)
            {
                ChangeItem(false);
            }
        }

        private void btnListDown_Click(object sender, EventArgs e)
        {
            if (lsbRecipe.SelectedIndex < lsbRecipe.Items.Count -1)
            {
                ChangeItem(true);
            }
        }

        private void ChangeItem(bool isUp)
        {
            int selectIndex = lsbRecipe.SelectedIndex;
            int ChangeIndex = isUp? selectIndex + 1 : selectIndex - 1; // isUp

            List<string> items = new List<string>();

            foreach (string it in lsbRecipe.Items)
            {
                items.Add(it);
            }

            string temp = items[ChangeIndex];
            items[ChangeIndex] = items[selectIndex];
            items[selectIndex] = temp;

            lsbRecipe.Items.Clear();
            foreach (string it in items)
            {
                lsbRecipe.Items.Add(it);
            }

            lsbRecipe.SelectedIndex = ChangeIndex;
            
        }

        private void btnStart_Click(object sender, EventArgs e)
        {            
            OriginBeforeStart();

            CommandCenter.Start(_config.RecipeList);

            btnStart.Enabled = false;
            btnStop.Enabled = true;
            btnPause.Enabled = true;
            btnRestart.Enabled = false;
            btnAbort.Enabled = true;

            StartStages();

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            CommandCenter.ChangeStatus(CommandCenter.Operation.Stop);

            btnStart.Enabled = true;
            btnStop.Enabled = false;
            btnPause.Enabled = false;
            btnRestart.Enabled = false;
            btnAbort.Enabled = false;            
        }

        private void OriginBeforeStart()
        {
            CommandCenter.DeleteAllCell();

            button1.Left = 50;
            button1.Top = 450;

            button2.Left = 150;
            button2.Top = 450;

            button3.Left = 250;
            button3.Top = 450;

            button4.Left = 350;
            button4.Top = 450;

        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            CommandCenter.ChangeStatus(CommandCenter.Operation.Pause);

            btnStart.Enabled = false;
            btnStop.Enabled = true;
            btnPause.Enabled = false;
            btnRestart.Enabled = true;
            btnAbort.Enabled = true;

        }

        private void tbnRestart_Click(object sender, EventArgs e)
        {
            CommandCenter.ChangeStatus(CommandCenter.Operation.Resume);

            btnStart.Enabled = false;
            btnStop.Enabled = true;
            btnPause.Enabled = true;
            btnRestart.Enabled = false;
            btnAbort.Enabled = true;

        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            CommandCenter.ChangeStatus(CommandCenter.Operation.Abort);

            btnStart.Enabled = true;
            btnStop.Enabled = false;
            btnPause.Enabled = false;
            btnRestart.Enabled = false;
            btnAbort.Enabled = false;
        }
    }
}
