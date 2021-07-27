using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StEm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(Application.StartupPath + @"\config.json"));

            //Form1 f1 = new Form1(Application.StartupPath + @"\config.json");
            //Form1 f2 = new Form1(Application.StartupPath + @"\config2.json");

            //f1.Show();
            //f2.ShowDialog();

        }
    }
}
