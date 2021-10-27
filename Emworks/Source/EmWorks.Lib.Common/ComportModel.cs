using System;
using System.IO.Ports;

namespace EmWorks.Lib.Common
{
    [Serializable]
    public class ComportModel : ICloneable
    {
        #region Properties

        public string PortNum { get; set; } = "";
        public string BaudRate { get; set; } = "9600";
        public string DataBits { get; set; } = "8";
        public Parity ParityBit { get; set; } = Parity.None;
        public string ParityBitsString { get { return ParityBit.ToString(); } }
        public StopBits StopBit { get; set; } = StopBits.One;
        public string StopBitsString { get { return StopBit.ToString(); } }
        public bool DtrEnable { get; set; } = false;
        public bool RtsEnable { get; set; } = false;
        public string RecvTerminateString { get; set; } = "";
        public string SendTerminateString { get; set; } = "";

        #endregion Properties

        public object Clone()
        {
            ComportModel clone = new ComportModel();

            clone.PortNum = this.PortNum;
            clone.BaudRate = this.BaudRate;
            clone.DataBits = this.DataBits;
            clone.ParityBit = this.ParityBit;
            clone.StopBit = this.StopBit;
            clone.DtrEnable = this.DtrEnable;
            clone.RtsEnable = this.RtsEnable;
            clone.RecvTerminateString = this.RecvTerminateString;
            clone.SendTerminateString = this.SendTerminateString;

            return clone;
        }
    }
}