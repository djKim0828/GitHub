using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace EmWorks.App.Sample
{
    public abstract class initBase
    {
        public abstract bool dFun1();
        public bool vFunc2()
        {
            return true;
        }

    }
    public class initializeControl : initBase
    {
        public override bool dFun1()
        {
            Thread.Sleep(500);
            return true;
        }

        public bool Funtion1()
        {
            Thread.Sleep(500);
            return true;
        }

        public bool Funtion3()
        {
            Thread.Sleep(500);
            return true;
        }

        public bool Funtion2()
        {
            Thread.Sleep(500);
            return true;
        }

        public bool Funtion4()
        {
            Thread.Sleep(500);
            return true;
        }

        public bool Funtion5()
        {
            Thread.Sleep(500);
            return true;
        }

        public bool Funtion6()
        {
            Thread.Sleep(500);
            return true;
        }

        public bool Funtion7()
        {
            Thread.Sleep(500);
            return true;
        }

        public bool Funtion8()
        {
            Thread.Sleep(500);
            return true;
        }

        public bool Funtion9()
        {
            Thread.Sleep(500);
            return true;
        }

        public bool Funtion10()
        {
            Thread.Sleep(500);
            return false;
        }

    }
}
