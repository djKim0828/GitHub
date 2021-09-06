using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmWorks.Lib.Common
{
    public class BitCtrl
    {
        public class Mask
        {
            public const uint DW01 = 0x00000001;
            public const uint DW02 = 0x00000002;
            public const uint DW03 = 0x00000004;
            public const uint DW04 = 0x00000008;
            public const uint DW05 = 0x00000010;
            public const uint DW06 = 0x00000020;
            public const uint DW07 = 0x00000040;
            public const uint DW08 = 0x00000080;
            public const uint DW09 = 0x00000100;
            public const uint DW10 = 0x00000200;
            public const uint DW11 = 0x00000400;
            public const uint DW12 = 0x00000800;
            public const uint DW13 = 0x00001000;
            public const uint DW14 = 0x00002000;
            public const uint DW15 = 0x00004000;
            public const uint DW16 = 0x00008000;
            public const uint DW17 = 0x00010000;
            public const uint DW18 = 0x00020000;
            public const uint DW19 = 0x00040000;
            public const uint DW20 = 0x00080000;
            public const uint DW21 = 0x00100000;
            public const uint DW22 = 0x00200000;
            public const uint DW23 = 0x00400000;
            public const uint DW24 = 0x00800000;
            public const uint DW25 = 0x01000000;
            public const uint DW26 = 0x02000000;
            public const uint DW27 = 0x04000000;
            public const uint DW28 = 0x08000000;
            public const uint DW29 = 0x10000000;
            public const uint DW30 = 0x20000000;
            public const uint DW31 = 0x40000000;
            public const uint DW32 = 0x80000000;
        }
        /*
byte	            1	0 to 255
sbyte	        1	-128 to 127
short	        2	-32,768 to 32,767
ushort	        2	0 to 65,535
int		        4	-2 billion to 2 billion
uint	            4   0 to 4 billion
long	            8	-9 quintillion to 9 quintillion
ulong	        8	0 to 18 quintillion
float	        4	7 significant digits1
double	    8	15 significant digits2
object	        4 	byte address	All C# Objects
char	            2	Unicode characters
string	        4 	byte address	Length up to 2 billion bytes3
decimal	    24	28 to 29 significant digits4
bool	        1	true, false 5
         */

        /// <summary>
        ///  WORD, 16 bit
        /// </summary>
        /// <param name="index">0~15</param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int GetValue(int index, uint data)
        {
            uint ret = 0;
            switch (index)
            {
                case 0: ret = Mask.DW01 & data; break;
                case 1: ret = Mask.DW02 & data; break;
                case 2: ret = Mask.DW03 & data; break;
                case 3: ret = Mask.DW04 & data; break;
                case 4: ret = Mask.DW05 & data; break;
                case 5: ret = Mask.DW06 & data; break;
                case 6: ret = Mask.DW07 & data; break;
                case 7: ret = Mask.DW08 & data; break;
                case 8: ret = Mask.DW09 & data; break;
                case 9: ret = Mask.DW10 & data; break;
                case 10: ret = Mask.DW11 & data; break;
                case 11: ret = Mask.DW12 & data; break;
                case 12: ret = Mask.DW13 & data; break;
                case 13: ret = Mask.DW14 & data; break;
                case 14: ret = Mask.DW15 & data; break;
                case 15: ret = Mask.DW16 & data; break;
                case 16: ret = Mask.DW17 & data; break;
                case 17: ret = Mask.DW18 & data; break;
                case 18: ret = Mask.DW19 & data; break;
                case 19: ret = Mask.DW20 & data; break;
                case 20: ret = Mask.DW21 & data; break;
                case 21: ret = Mask.DW22 & data; break;
                case 22: ret = Mask.DW23 & data; break;
                case 23: ret = Mask.DW24 & data; break;
                case 24: ret = Mask.DW25 & data; break;
                case 25: ret = Mask.DW26 & data; break;
                case 26: ret = Mask.DW27 & data; break;
                case 27: ret = Mask.DW28 & data; break;
                case 28: ret = Mask.DW29 & data; break;
                case 29: ret = Mask.DW30 & data; break;
                case 30: ret = Mask.DW31 & data; break;
                case 31: ret = Mask.DW32 & data; break;
                default:
                    break;
            }
            return ret == 0 ? 0 : 1;
        }

        public static void SetValue(int index, ref uint data, int setValue)
        {
            int result = (int)data;

            if (setValue == 1) result |= (1 << index); else result &= (~(1 << index));
            #region 
            //switch (index)
            //{
            //    case 0: if(setValue == 1) result |= (1 << 0); else result &= (~(1 << 0)); break;
            //    case 1: if (setValue == 1) result |= (1 << 1); else result &=(~(1 << 1)); break;
            //    case 2: if (setValue == 1) result |= (1 << 2); else result &=(~(1 << 2)); break;
            //    case 3: if (setValue == 1) result |= (1 << 3); else result &=(~(1 << 3)); break;
            //    case 4: if (setValue == 1) result |= (1 << 4); else result &=(~(1 << 4)); break;
            //    case 5: if (setValue == 1) result |= (1 << 5); else result &=(~(1 << 5)); break;
            //    case 6: if (setValue == 1) result |= (1 << 6); else result &=(~(1 << 6)); break;
            //    case 7: if (setValue == 1) result |= (1 << 7); else result &=(~(1 << 7)); break;
            //    case 8: if (setValue == 1) result |= (1 << 8); else result &=(~(1 << 8)); break;
            //    case 9: if (setValue == 1) result |= (1 << 9); else result &= (~(1 << 9)); break;
            //    case 10: if (setValue == 1) result |= (1 << 10); else result &=(~(1 << 10)); break;
            //    case 11: if (setValue == 1) result |= (1 << 11); else result &=(~(1 << 11)); break;
            //    case 12: if (setValue == 1) result |= (1 << 12); else result &=(~(1 << 12)); break;
            //    case 13: if (setValue == 1) result |= (1 << 13); else result &=(~(1 << 13)); break;
            //    case 14: if (setValue == 1) result |= (1 << 14); else result &=(~(1 << 14)); break;
            //    case 15: if (setValue == 1) result |= (1 << 15); else result &= (~(1 << 15)); break;
            //    case 16: if (setValue == 1) result |= (1 << 16); else result &= (~(1 << 10)); break;
            //    case 17: if (setValue == 1) result |= (1 << 17); else result &= (~(1 << 11)); break;
            //    case 18: if (setValue == 1) result |= (1 << 18); else result &= (~(1 << 12)); break;
            //    case 19: if (setValue == 1) result |= (1 << 19); else result &= (~(1 << 13)); break;
            //    case 20: if (setValue == 1) result |= (1 << 20); else result &= (~(1 << 14)); break;
            //    case 21: if (setValue == 1) result |= (1 << 15); else result &= (~(1 << 15)); break;
            //    case 22: if (setValue == 1) result |= (1 << 10); else result &= (~(1 << 10)); break;
            //    case 23: if (setValue == 1) result |= (1 << 11); else result &= (~(1 << 11)); break;
            //    case 24: if (setValue == 1) result |= (1 << 12); else result &= (~(1 << 12)); break;
            //    case 25: if (setValue == 1) result |= (1 << 13); else result &= (~(1 << 13)); break;
            //    case 26: if (setValue == 1) result |= (1 << 14); else result &= (~(1 << 14)); break;
            //    case 27: if (setValue == 1) result |= (1 << 15); else result &= (~(1 << 15)); break;
            //    case 28: if (setValue == 1) result |= (1 << 10); else result &= (~(1 << 10)); break;
            //    case 29: if (setValue == 1) result |= (1 << 11); else result &= (~(1 << 11)); break;
            //    case 30: if (setValue == 1) result |= (1 << 12); else result &= (~(1 << 12)); break;
            //    case 31: if (setValue == 1) result |= (1 << 13); else result &= (~(1 << 13)); break;

            //    default:
            //        break;
            //}
            #endregion
            data = (uint)result;
        }

        public static void SetValue(int index, ref uint data, bool setValue)
        {
            int result = (int)data;
            if (setValue == true) result |= (1 << index); else result &= (~(1 << index));
            #region 
            //switch (index)
            //{
            //    case 0: if (setValue == true) result |= (1 << 0); else result &= (~(1 << 0)); break;
            //    case 1: if (setValue == true) result |= (1 << 1); else result &= (~(1 << 1)); break;
            //    case 2: if (setValue == true) result |= (1 << 2); else result &= (~(1 << 2)); break;
            //    case 3: if (setValue == true) result |= (1 << 3); else result &= (~(1 << 3)); break;
            //    case 4: if (setValue == true) result |= (1 << 4); else result &= (~(1 << 4)); break;
            //    case 5: if (setValue == true) result |= (1 << 5); else result &= (~(1 << 5)); break;
            //    case 6: if (setValue == true) result |= (1 << 6); else result &= (~(1 << 6)); break;
            //    case 7: if (setValue == true) result |= (1 << 7); else result &= (~(1 << 7)); break;
            //    case 8: if (setValue == true) result |= (1 << 8); else result &= (~(1 << 8)); break;
            //    case 9: if (setValue == true) result |= (1 << 9); else result &= (~(1 << 9)); break;
            //    case 10: if (setValue == true) result |= (1 << 10); else result &= (~(1 << 10)); break;
            //    case 11: if (setValue == true) result |= (1 << 11); else result &= (~(1 << 11)); break;
            //    case 12: if (setValue == true) result |= (1 << 12); else result &= (~(1 << 12)); break;
            //    case 13: if (setValue == true) result |= (1 << 13); else result &= (~(1 << 13)); break;
            //    case 14: if (setValue == true) result |= (1 << 14); else result &= (~(1 << 14)); break;
            //    case 15: if (setValue == true) result |= (1 << 15); else result &= (~(1 << 15)); break;
            //    default:
            //        break;
            //}
            #endregion
            data = (uint)result;
        }

        public static int[] ToInt32Array(object data)
        {
            string binary = Convert.ToString(Convert.ToInt32(data.ToString(), 10), 2);
            binary = binary.PadLeft(32, '0');

            char[] array = binary.ToCharArray();

            int[] ret = new int[array.Length];
            for (int i = 0; i < ret.Length; i++)
                ret[i] = Convert.ToInt32(array[i].ToString());

            return ret;
        }

        public static short[] ToInt16Array(object data)
        {
            string binary = Convert.ToString(Convert.ToInt16(data.ToString(), 10), 2);
            binary = binary.PadLeft(16, '0');

            char[] array = binary.ToCharArray();

            short[] ret = new short[array.Length];
            for (int i = 0; i < ret.Length; i++)
                ret[i] = Convert.ToInt16(array[i].ToString());

            return ret;
        }
    }
}
