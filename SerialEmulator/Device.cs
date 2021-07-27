using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StEm
{
    public abstract class Device
    {
        public virtual bool IsOpened { get; protected set; } = false;

        public abstract int Open(string port, string baudRate, string newLine);
        public abstract int Close();
        public abstract bool Send(string sendData, bool isHex);

    }
}
