using HsApiNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SR5000Testor
{
    public partial class Form1 : Form
    {
        private SR5000 _SR5000;

        public Form1()
        {
            InitializeComponent();

            _SR5000 = new SR5000();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                int nError = 0;
                Configuration Config = new Configuration();
                List<string> DeviceList = new List<string>();

                
                nError = _SR5000.InitializeDevice(ref Config);

                if (nError < 0)
                {
                    WriteLog("Port Search Error");
                }

                cmbDeviceList.Items.Clear();
                for (int i = 0; i < Config.connected_product_ids.Count; i++)
                {
                    DeviceList.Add(Config.connected_product_ids[i]);
                    cmbDeviceList.Items.Add(Config.connected_product_ids[i]);
                }

                cmbDeviceList.Enabled = true;
                tslStatus.BackColor = Color.Green;
            }
            catch (System.Exception ex)
            {
                WriteLog(ex.Message);
            }            
        }


        public void WriteLog(string msg)
        {
            rcbOutput.Invoke(new MethodInvoker(() =>
            {
                String nowTime = "[" + System.DateTime.Now.ToString() + "] ";

                rcbOutput.AppendText(nowTime + msg + "\r\n");
                rcbOutput.SelectionStart = rcbOutput.Text.Length;
                rcbOutput.ScrollToCaret();
            }));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
