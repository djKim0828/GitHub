using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using HsApiNet;

namespace LiveView
{
    static class Program
    {               
        [STAThread]
        static void Main()
        {           
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            Application.Run(new MainForm());
        }
    }    
}
