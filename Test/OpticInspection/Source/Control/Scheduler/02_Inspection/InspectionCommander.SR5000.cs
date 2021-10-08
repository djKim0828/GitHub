using EmWorks.Lib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace EmWorks.App.OpticInspection
{
    public partial class InspectionCommander
    {
        public bool LoadRecipeSR5000()
        {
            try
            {
                Global._TopconSR5000.ySR5000RecipeFilePath = CommandCenter._Recipe.RecipeFilepath;

                if (Global._TopconSR5000.SetySR5000RecipeLoad(true, 2000))
                {
                    return false;
                } // else

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        public bool StartSR5000(ModelRecipe.LedItem ldItem)
        {
            try
            {
                CommandCenter.SendSR5000(CommandCenter.CommandType.StartInspection, null);

                // Todo : 실제 검사를 할 수 없으르므로 이미 검사한 파일 로드
                Global._TopconSR5000.ySR5000MeasurementFilePath = Global.ConfigGeneral.RootPath +
                    @"\Config\SR5000\Measurement\Result.hsm";

                if (Global._TopconSR5000.xSR5000MeasurementLoadResult.Is == null)
                {
                    if (Global._TopconSR5000.SetySR5000MeasurementLoad(true, 60000) == false)
                    {
                        return false;
                    } // else
                }
                

                if (Global._TopconSR5000.SetySR5000PseudoImageRequest(true, 10000) == false)
                {
                    return false;
                } // else

                // Save 이미지                
                SavePseudoImage(ldItem.FovNumber, ldItem.LedNumber, Global._TopconSR5000.xSR5000PseudoImageResult.Is);                                               

                if (Global._TopconSR5000.SetySR5000RGBImageRequest(true, 10000) == false)
                {
                    return false;
                } // else

                SaveRGBImage(ldItem.FovNumber, ldItem.LedNumber, Global._TopconSR5000.xSR5000RGBImageResult.Is);                                

                CommandCenter.SendSR5000(CommandCenter.CommandType.CompleteInspection, null);

                

                //Global._TopconSR5000.ySR5000RecipeFilePath = CommandCenter._Recipe.RecipeFilepath;

                //if (Global._TopconSR5000.SetySR5000RecipeLoad(true, 1000))
                //{
                //    return false;
                //} // else

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        private bool SaveRGBImage(int fovNumber, int ledNumber, object image)
        {
            try
            {
                string now = DateTime.Now.ToString("yyyyMMddHHmmss");

                // 폴더 유무 검사                
                DirectoryInfo di = new DirectoryInfo(Global.ConfigGeneral.RootPath +
                    Global.ConfigGeneral.ResultFilePath +
                    Global.ConfigGeneral.ResultRGBFilePath);

                if (di.Exists == false)
                {
                    di.Create();
                } // else

                
                string filePath = Global.ConfigGeneral.RootPath +
                    Global.ConfigGeneral.ResultFilePath +
                    Global.ConfigGeneral.ResultRGBFilePath +
                    "RGB_" +
                    fovNumber.ToString() + "_" +
                    ledNumber.ToString() + "_" +
                    now +
                    ".png";

                Type temp = image.GetType();
                if (temp.Equals(typeof(System.Drawing.Bitmap)))
                {
                    System.Drawing.Bitmap img = (System.Drawing.Bitmap)image;
                    img.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);

                    CommandCenter.SendSR5000(CommandCenter.CommandType.RGBData,
                    filePath);
                } // else

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        private bool SavePseudoImage(int fovNumber, int ledNumber, object image)
        {
            try
            {
                string now = DateTime.Now.ToString("yyyyMMddHHmmss");

                // 폴더 유무 검사                
                DirectoryInfo di = new DirectoryInfo(Global.ConfigGeneral.RootPath +
                    Global.ConfigGeneral.ResultFilePath);

                if (di.Exists == false)
                {
                    di.Create();
                } // else

                di = new DirectoryInfo(Global.ConfigGeneral.RootPath +
                    Global.ConfigGeneral.ResultFilePath + 
                    Global.ConfigGeneral.ResultPsudoFilePath);

                if (di.Exists == false)
                {
                    di.Create();
                } // else

                string filePath = Global.ConfigGeneral.RootPath +
                    Global.ConfigGeneral.ResultFilePath +
                    Global.ConfigGeneral.ResultPsudoFilePath +
                    "Pseudo_" +
                    fovNumber.ToString() + "_" +
                    ledNumber.ToString() + "_" +
                    now +
                    ".png";

                Type temp = image.GetType();
                if (temp.Equals(typeof(System.Drawing.Bitmap)))
                {
                    System.Drawing.Bitmap img = (System.Drawing.Bitmap)image;
                    img.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                    img.Dispose();

                    CommandCenter.SendSR5000(CommandCenter.CommandType.PseudoData,
                    filePath);
                } // else

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }
    }
}
