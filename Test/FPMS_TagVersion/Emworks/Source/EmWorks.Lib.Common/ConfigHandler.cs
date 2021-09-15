using System;
using System.IO;
using System.Web.Script.Serialization;

namespace EmWorks.Lib.Common
{
    public class ConfigHandler<T> where T : new()
    {
        #region Fields

        private string _filePath;
        private JavaScriptSerializer _jsonConvertor;
        private string _typeName;

        #endregion Fields

        #region Constructors

        public ConfigHandler(string filePath)
        {
            _filePath = filePath;
            _jsonConvertor = new JavaScriptSerializer();
        }

        public ConfigHandler()
        {
            _jsonConvertor = new JavaScriptSerializer();
            _typeName = typeof(T).Name;

            string currentPath = AppDomain.CurrentDomain.BaseDirectory; // 실행프로그램의 경로를 얻어 온다.
            currentPath = currentPath.TrimEnd(new char[] { '\\' });

            string newPath = Path.GetFullPath(Path.Combine(currentPath, @"..\")); // 표준은 Bin 폴더와 동일 위치이므로 한단계 상위 폴더
            newPath = newPath + @"Config\" + _typeName + ".json";
            _filePath = newPath;
        }

        #endregion Constructors

        #region Methods

        public bool LoadConfig(string filePath, ref T ConfigData)
        {
            T _data = new T();

            if (File.Exists(filePath) == false)
            {
                return false;
            }// else

            _filePath = filePath;
            try
            {
                string jsonString = File.ReadAllText(_filePath);
                _data = _jsonConvertor.Deserialize<T>(jsonString);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
            ConfigData = _data;
            return true;
        }

        public bool LoadConfig(ref T ConfigData)
        {
            return LoadConfig(_filePath, ref ConfigData);
        }

        public bool SaveFile(string filePath, T config)
        {
            try
            {
                CreateDirectorySafely(filePath);
                string allText = _jsonConvertor.Serialize(config);

                //allText = allText.Replace(@"{", "{\r\n\t");
                //allText = allText.Replace(@",", ",\r\n\t");
                //필요하면 vscode에서 전체선택하여 ctrl + K, ctrl + F 로 정렬하여 볼것.
                //Visual Studio CodeMaid에서도 가능.

                File.WriteAllText(filePath, allText);

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }
        }

        private bool CreateDirectorySafely(string directory)
        {
            bool success = false;
            string directoryName = Path.GetDirectoryName(directory);
            try
            {
                if (Directory.Exists(directoryName) == false)
                {
                    Directory.CreateDirectory(directoryName);
                }
                success = true;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
            return success;
        }

        public bool SaveFile(T config)
        {
            return SaveFile(_filePath, config);
        }

        #endregion Methods
    }
}