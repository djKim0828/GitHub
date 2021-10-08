using EmWorks.Lib.Common;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

namespace EmWorks.App.OpticInspection
{
    public class ModelRecipe
    {
        #region Constructors

        public ModelRecipe()
        {
            _Item = new List<RecipeItem>();
        }

        #endregion Constructors

        #region Destructors

        ~ModelRecipe()
        {
        }

        #endregion Destructors

        #region Properties

        public List<RecipeItem> _Item { get; set; }

        #endregion Properties

        #region Methods

        public ModelRecipe Load(string path)
        {
            ModelRecipe openObject = new ModelRecipe();

            try
            {
                if (File.Exists(path) == false)
                {
                    // 파일이 없으면 만들자!!
                    File.WriteAllText(path, "");
                } // else

                string jsonString = File.ReadAllText(path);

                JavaScriptSerializer jsonConvertor = new JavaScriptSerializer();

                openObject = jsonConvertor.Deserialize<ModelRecipe>(jsonString);
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }

            if (openObject == null)
            {
                openObject = new ModelRecipe();
            } // else

            return openObject;
        }

        public bool Save(string path, ModelRecipe src)
        {
            try
            {
                JavaScriptSerializer jsonConvertor = new JavaScriptSerializer();

                string allText = jsonConvertor.Serialize(src);

                // 개행 넣기
                allText = allText.Replace(@"{", "{\r\n\t");
                allText = allText.Replace(@",", ",\r\n\t");

                File.WriteAllText(path, allText);

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        #endregion Methods

        #region Classes

        public class LedItem
        {
            #region Properties

            public int FovColCount { get; set; }
            public int FovColIndex { get; set; }
            public int FovNumber { get; set; }
            public int FovRowCount { get; set; }
            public int FovRowIndex { get; set; }

            public int LedNumber { get; set; }

            #endregion Properties
        }

        public class RecipeItem
        {
            #region Fields

            public List<LedItem> _Item;

            #endregion Fields

            #region Properties

            public string Name { get; set; }
            public string Type { get; set; }

            //FOV
            public string FovWidthSpacing { get; set; }
            public string FovHeightSpacing { get; set; }
            public string LedWidthSpacing { get; set; }
            public string LedHeightSpacing { get; set; }

            //SR-5000 Auto spot
            public string AutoSpotType { get; set; }
            public string AutoSpotWidth { get; set; }
            public string AutoSpotHeight { get; set; }
            public string AutoSpotMin { get; set; }
            public string AutoSpotMax { get; set; }
            public string AutoSpotYth { get; set; }
            public string RecipeFilepath { get; set; }

            #endregion Properties
        }

        #endregion Classes
    }
}