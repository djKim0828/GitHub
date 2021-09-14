using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace PreCameraTestor
{
    public class ImageMngr
    {
        public static Bitmap BytToBitmap(byte[] pixelData)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = pixelData;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }

        public static Bitmap BytTo8bitBitmap(byte[] pixelData, int width, int height)
        {
            int stride = width;
            Bitmap im = new Bitmap(width, height, stride,
                    System.Drawing.Imaging.PixelFormat.Format8bppIndexed,
                    Marshal.UnsafeAddrOfPinnedArrayElement(pixelData, 0));

            return im;
        }

        public static Bitmap BytTo8bitBitmap2(byte[] pixelData, int width, int height)
        {
            var output = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            var rect = new Rectangle(0, 0, width, height);
            var bmpData = output.LockBits(rect,
                System.Drawing.Imaging.ImageLockMode.ReadWrite, output.PixelFormat);
            var ptr = bmpData.Scan0;
            Marshal.Copy(pixelData, 0, ptr, pixelData.Length);
            output.UnlockBits(bmpData);
            return output;
        }
            

          
    }
}
