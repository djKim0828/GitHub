using System;

namespace EmWorks.DevDrv.AjinMotionIo
{
    public class EmDataBuffer
    {
        #region Fields

        public double[] Ai;
        public int[] AiOffset;
        public uint[] AiRaw;
        public short[] AiSign;
        public double[] Ao;
        public int[] AoOffset;
        public int[] Di;
        public int[] DiOffset;
        public int[] Do;
        public int[] DoOffset;

        #endregion Fields

        #region Constructors

        public EmDataBuffer()
        {
            Initialization();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="isDio">if true = dio,  false = aio</param>
        /// <param name="inSize"></param>
        /// <param name="outSize"></param>
        public EmDataBuffer(bool isDio, int inSize, int outSize)
        {
            Initialization();

            if (isDio)
                InitDigitalBuffer(inSize, outSize);
            else
                InitAnalogBuffer(inSize, outSize);
        }

        public EmDataBuffer(int diSize, int doSize, int aiSize, int aoSize)
        {
            Initialization();

            InitDigitalBuffer(diSize, doSize);
            InitAnalogBuffer(aiSize, aoSize);
        }

        #endregion Constructors

        #region Properties

        public int AiBufferLength
        {
            get
            {
                if (Ai == null)
                    return 0;
                return Ai.Length;
            }
        }

        public int AoBufferLength
        {
            get
            {
                if (Ao == null)
                    return 0;
                return Ao.Length;
            }
        }

        public int DiBufferLength
        {
            get
            {
                if (Di == null)
                    return 0;
                return Di.Length;
            }
        }

        public int DoBufferLength
        {
            get
            {
                if (Do == null)
                    return 0;
                return Do.Length;
            }
        }

        #endregion Properties

        #region Methods

        public double GetAi(int channelOffset)
        {
            if (Ai == null)
                return 0.0f;
            else if (Ai.Length <= channelOffset)
                return 0.0f;

            return Ai[channelOffset];
        }

        public double[] GetAi(int size, int startChannelOffset)
        {
            if (Ai == null)
                return null;
            else if ((Ai.Length - startChannelOffset) < size || 0 > startChannelOffset)
                return null;

            double[] dest = new double[size];
            Array.Copy(Ai, startChannelOffset, dest, 0, size);

            return dest;
        }

        public double GetAo(int channelOffset)
        {
            if (Ao == null)
                return 0.0f;
            else if (Ao.Length <= channelOffset)
                return 0.0f;

            return Ao[channelOffset];
        }

        public double[] GetAo(int size, int startChannelOffset)
        {
            if (Ao == null)
                return null;
            else if ((Ao.Length - startChannelOffset) < size || 0 > startChannelOffset)
                return null;

            double[] dest = new double[size];
            Array.Copy(Ao, startChannelOffset, dest, 0, size);

            return dest;
        }

        public int GetDi(int bitOffset)
        {
            if (Di == null)
                return 0;
            else if (Di.Length <= bitOffset || 0 > bitOffset)
                return 0;

            return Di[bitOffset];
        }

        public int GetDo(int bitOffset)
        {
            if (Do == null)
                return 0;
            else if (Do.Length <= bitOffset || 0 > bitOffset)
                return 0;

            return Do[bitOffset];
        }

        public void InitAnalogBuffer(int inSize, int outSize)
        {
            if (inSize > 0)
            {
                AiOffset = new int[inSize];
                AiRaw = new uint[inSize];
                AiSign = new short[inSize];
                Ai = new double[inSize];

                for (int i = 0; i < Ai.Length; i++)
                {
                    Ai[i] = 0;
                    AiRaw[i] = 0;
                    AiSign[i] = 0;
                    AiOffset[i] = i;
                }
            }

            if (outSize > 0)
            {
                AoOffset = new int[outSize];
                Ao = new double[outSize];

                for (int i = 0; i < Ai.Length; i++)
                {
                    Ao[i] = 0;
                    AoOffset[i] = i;
                }
            }
        }

        public void InitBuffer(int diSize, int doSize, int aiSize, int aoSize)
        {
            InitDigitalBuffer(diSize, doSize);
            InitAnalogBuffer(aiSize, aoSize);
        }

        public void InitDigitalBuffer(int inSize, int outSize)
        {
            if (inSize > 0)
            {
                DiOffset = Di = new int[inSize];
                for (int i = 0; i < Di.Length; i++)
                {
                    Di[i] = 0;
                    DiOffset[i] = i;
                }
            }

            if (outSize > 0)
            {
                DoOffset = Do = new int[outSize];
                for (int i = 0; i < Do.Length; i++)
                {
                    Do[i] = 0;
                    DoOffset[i] = i;
                }
            }
        }

        public void SetAo(int channelOffset, double value)
        {
            if (Ao == null)
                return;
            else if (Ao.Length <= channelOffset)
                return;

            Ao[channelOffset] = value;
        }

        /// <summary>
        /// 16 bits
        /// </summary>
        /// <param name="startOffset"></param>
        /// <param name="setData"></param>
        public void Setdi(int startOffset, short[] setData)
        {
            if (Di == null)
                return;
            else if ((Di.Length - startOffset) < setData.Length || 0 > startOffset)
                return;

            Array.Copy(setData, 0, Di, startOffset, setData.Length);

            //for (int i = 0; i < setData.Length; i++)
            //    Di[startOffset + i] = setData[i];
        }

        /// <summary>
        /// 32 bits
        /// </summary>
        /// <param name="startOffset"></param>
        /// <param name="setData"></param>
        public void Setdi(int startOffset, int[] setData)
        {
            if (Di == null)
                return;
            else if ((Di.Length - startOffset) < setData.Length || 0 > startOffset)
                return;

            for (int i = 0; i < setData.Length; i++)
                Di[startOffset + i] = setData[i];
        }

        /// <summary>
        /// 16 or 32 bits
        /// </summary>
        /// <param name="size">16 or 32</param>
        /// <param name="startOffset"></param>
        /// <param name="setData"></param>
        public void Setdi(int size, int startOffset, uint setData)
        {
            if (Di == null)
                return;
            else if (size > Di.Length)
                return;

            if (size == 16)
                Setdi(startOffset, ToInt32Array16(setData));
            else if (size == 32)
                Setdi(startOffset, ToInt32Array32(setData));
        }

        public void SetDo(int bitOffset, object bitData)
        {
            if (Do == null)
                return;
            else if (Do.Length <= bitOffset || 0 > bitOffset)
                return;

            Do[bitOffset] = Convert.ToInt32(bitData);
        }

        public int[] ToInt32Array16(object data)
        {
            string binary = Convert.ToString(Convert.ToInt16(data.ToString(), 10), 2);
            binary = binary.PadLeft(16, '0');

            char[] array = binary.ToCharArray();

            int[] ret = new int[array.Length];
            ////for (int i = 0; i < ret.Length; i++)
            ////    ret[i] = Convert.ToInt16(array[i].ToString());

            int index = 0;
            for (int i = ret.Length - 1; i >= 0; i--)
            {
                ret[index] = Convert.ToInt32(array[i].ToString());
                index++;
            }

            return ret;
        }

        public int[] ToInt32Array32(object data)
        {
            string binary = Convert.ToString(Convert.ToInt32(data.ToString(), 10), 2);
            binary = binary.PadLeft(32, '0');

            char[] array = binary.ToCharArray();

            int[] ret = new int[array.Length];
            //for (int i = 0; i < ret.Length; i++)
            //    ret[i] = Convert.ToInt32(array[i].ToString());
            int index = 0;
            for (int i = ret.Length - 1; i >= 0; i--)
            {
                ret[index] = Convert.ToInt32(array[i].ToString());
                index++;
            }

            return ret;
        }

        private void Initialization()
        {
        }

        #endregion Methods
    }
}
