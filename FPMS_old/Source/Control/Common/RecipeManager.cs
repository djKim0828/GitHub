using System.Collections.Generic;
using System.IO;
using static FPMS.E2105_FS111_121.RecipeItem;

namespace FPMS.E2105_FS111_121
{
    internal class RecipeManager
    {
        #region Fields

        public static List<RecipeMethod> _IsItemInfo = new RecipeItem().GetItemList();

        public List<RecipeFile> _Sequence = new List<RecipeFile>();

        #endregion Fields

        #region Constructors

        public RecipeManager()
        {
            _IsItemInfo = new RecipeItem().GetItemList();
        }

        #endregion Constructors

        #region Methods

        public static List<RecipeMethod> LoadRecipeFile(string path)
        {
            List<RecipeMethod> returnSequence = new List<RecipeMethod>();
            FileInfo readCsvFile = new FileInfo(Global.ConfigGeneral.RootPath + "\\Recipe\\" + Path.GetFileName(path));

            if (readCsvFile.Exists == false)
            {
                return null;
            }

            using (StreamReader streamReader = new StreamReader(readCsvFile.FullName, false))
            {
                string line = string.Empty;

                while ((line = streamReader.ReadLine()) != null)
                {
                    var parameter = line.Split(',');
                    var tempItem = default(RecipeMethod);

                    for (int i = 0; i < parameter.Length; i++)
                    {
                        if (i == 0)
                        {
                            var parameterinfo = _IsItemInfo.Find(x => x._Name.Contains(parameter[0]));
                            var objectInfo = parameterinfo?.Clone();
                            tempItem = objectInfo as RecipeMethod;
                        }
                        else if (tempItem != null)
                        {
                            if (i <= tempItem._Parameter.Count)
                            {
                                tempItem._Parameter[i - 1]._Value = Convert(parameter[i], tempItem._Parameter[i - 1]._ValueType);
                            }//else
                        }
                    }

                    if (tempItem != default(RecipeMethod))
                    {
                        returnSequence.Add(tempItem);
                    }//else
                }
            }

            return returnSequence;
        }

        public static List<RecipeFile> LoadSequenceFile(string path)
        {
            List<RecipeFile> returnSequence = new List<RecipeFile>();
            FileInfo readCsvFile = new FileInfo(path);

            if (readCsvFile.Exists == false)
            {
                return null;
            }//else

            using (StreamReader streamReader = new StreamReader(readCsvFile.FullName, false))
            {
                string line = string.Empty;

                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] parameter = new string[2];
                    int firstDivider = line.IndexOf(',');
                    int lastDivider = line.LastIndexOf(',');

                    if (lastDivider != line.Length - 1)
                    {
                        lastDivider = line.Length;
                    }//else

                    parameter[0] = line.Substring(0, firstDivider);
                    parameter[1] = line.Substring(firstDivider + 1, lastDivider - firstDivider - 1);

                    bool temptodo = false;
                    var tempItem = default(RecipeFile);

                    temptodo = System.Convert.ToBoolean(parameter[0]);
                    tempItem = new RecipeFile(parameter[1]);

                    if (tempItem != default(RecipeFile))
                    {
                        returnSequence.Add(tempItem);
                        returnSequence[returnSequence.Count - 1]._TodoCheck = temptodo;
                    }//else
                }
            }
            return returnSequence;
        }

        public static void SaveRecipeFile(string path, List<RecipeMethod> recipe)
        {
            FileInfo newCsvFile = new FileInfo(path);

            if (newCsvFile.Exists == false)
            {
                FileStream ioFileCreate = newCsvFile.Create();
                ioFileCreate.Close();
            }//else

            using (StreamWriter streamWriter = new StreamWriter(newCsvFile.FullName, false))
            {
                foreach (var item in recipe)
                {
                    streamWriter.Write(item._Name);
                    streamWriter.Write(",");

                    foreach (var itemParameter in item._Parameter)
                    {
                        streamWriter.Write(itemParameter._Value);
                        streamWriter.Write(",");
                    }

                    streamWriter.WriteLine();
                }
            }
        }

        public static void SaveSequenceFile(string path, List<RecipeFile> sequence)
        {
            FileInfo newCsvFile = new FileInfo(path);

            if (newCsvFile.Exists == false)
            {
                FileStream ioFileCreate = newCsvFile.Create();
                ioFileCreate.Close();
            }//else

            using (StreamWriter streamWriter = new StreamWriter(newCsvFile.FullName, false))
            {
                foreach (var item in sequence)
                {
                    streamWriter.Write(item._TodoCheck);
                    streamWriter.Write(",");
                    streamWriter.Write(item._FilePath);
                    streamWriter.Write(",");
                    streamWriter.WriteLine();
                }
            }
        }

        #endregion Methods

        #region Classes

        public class RecipeFile
        {
            #region Fields

            public string _FilePath;
            public List<RecipeMethod> _Recipe = new List<RecipeMethod>();
            public bool _TodoCheck;

            #endregion Fields

            #region Constructors

            public RecipeFile(string FilePath)
            {
                this._FilePath = FilePath;
                _Recipe = RecipeManager.LoadRecipeFile(FilePath);
            }

            #endregion Constructors
        }

        #endregion Classes
    }
}