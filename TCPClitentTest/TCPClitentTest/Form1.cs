using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPClitentTest
{
    public partial class Form1 : Form
    {
        EmTCPClient _tcl;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_tcl._isConnent == true)
            {
                _tcl.Send("0");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _tcl = new EmTCPClient();
            _tcl.ReceiveMessage += _tcl_ReceiveMessage;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_tcl._isConnent == true)
            {
                _tcl.Send("1");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_tcl._isConnent == true)
            {
                _tcl.Send("2");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (_tcl._isConnent == true)
            {
                _tcl.Send("3");
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (_tcl.Open("192.168.0.81", 20000) == true)
            {                
                MessageBox.Show("Success");
            }
            else
            {
                MessageBox.Show("Failed");
            }
        }

        private void _tcl_ReceiveMessage(object sendor, EmTCPClient.EventReceiveMessageArgs e)
        {
            MessageBox.Show(e.Message);
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            _tcl.Close();
        }
    }
}
