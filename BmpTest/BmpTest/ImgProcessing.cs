using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BmpTest
{
    public static class ImgProcessing
    {
        public static Bitmap myEdge(Bitmap src, int amp)
        {

            //Bitmap은 회색조(Gray)로 영상을 바꾼 소스를 얻어 내며,
            //amp는 사용자에게 입력받은 값으로 출력 레벨을 결정하는데 사용되는 수이다.
            int i, j, iColorValue;

            // 라프라시안 필터
            int[] iFilter = new int[] { -1, -1, -1, -1, 8, -1, -1, -1, -1 };
            int[,] iArrayValue = new int[src.Width, src.Height];

            Color[] cArrayColor = new Color[9]; // 색정보의 배열 중간점을 기준으로

            //라프라시안 필터링 적용할 픽셀들
            Color color;

            // 화상에 대한 필터 처리
            // 각각 너비와 길이에 대하여 -1을 하는 이유는 맨 마지막 pixel을
            // 기준으로 잡을 수 없기 때문
            for (i = 1; i < src.Width - 1; i++)
            {
                for (j = 1; j < src.Height - 1; j++)
                {

                    cArrayColor[0] = src.GetPixel(i - 1, j - 1);

                    cArrayColor[1] = src.GetPixel(i - 1, j);

                    cArrayColor[2] = src.GetPixel(i - 1, j + 1);

                    cArrayColor[3] = src.GetPixel(i, j - 1);

                    cArrayColor[4] = src.GetPixel(i, j);

                    cArrayColor[5] = src.GetPixel(i, j + 1);
                    cArrayColor[6] = src.GetPixel(i + 1, j - 1);
                    cArrayColor[7] = src.GetPixel(i + 1, j);
                    cArrayColor[8] = src.GetPixel(i + 1, j + 1);

                    // 필터 처리

                    iColorValue = iFilter[0] * cArrayColor[0].R + iFilter[1] * cArrayColor[1].R +

                    iFilter[2] * cArrayColor[2].R + iFilter[3] *

                    cArrayColor[3].R + iFilter[4] * cArrayColor[4].R +

                    iFilter[5] * cArrayColor[5].R + iFilter[6] *

                    cArrayColor[6].R + iFilter[7] * cArrayColor[7].R +

                    iFilter[8] * cArrayColor[8].R;



                    //출력 레벨에 따라서 각기 다른 결과물이 나올 수 있다.

                    iColorValue = amp * iColorValue;   // 출력 레벨의 설정

                    // iColorValue가 0보다 작은 경우

                    if (iColorValue < 0)

                        iColorValue = -iColorValue;     // 정의값에 변환

                    // iColorValue가255보다 클 경우

                    if (iColorValue > 255)

                        iColorValue = 255;     // iColorValue를 255으로 설정

                    iArrayValue[i, j] = iColorValue;      // iColorValue의 설정

                }
            }

            // 필터 처리 결과 출력
            for (i = 1; i < src.Width - 1; i++)
            {
                for (j = 1; j < src.Height - 1; j++)
                {

                    color = Color.FromArgb(iArrayValue[i, j], iArrayValue[i, j],

                               iArrayValue[i, j]);

                    // iArrayValue 값에 의한 색 설정

                    src.SetPixel(i, j, color);   // 픽셀의 색 설정

                }
            }

            //pictureBox1.Image = bBitmap;   // 변경 결과 출력
             return src;

        }

        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }


        public static Bitmap ByteToImage(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }
    }
}
