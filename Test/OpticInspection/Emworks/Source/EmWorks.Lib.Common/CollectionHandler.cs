using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Web.Script.Serialization;

namespace EmWorks.Lib.Common
{
    public class CollectionHandler<T> where T : new()
    {
        #region Destructors

        ~CollectionHandler()
        {
        }

        #endregion Destructors

        #region Classes

        private class DataWithSubPath
        {
            #region Fields

            public T _ModelData;
            public string _SubPath;

            #endregion Fields

            #region Constructors

            public DataWithSubPath(string subPath, T modelData)
            {
                _SubPath = subPath;
                _ModelData = modelData;
            }

            public DataWithSubPath()
            {
                _SubPath = default(string);
                _ModelData = default(T);
            }

            #endregion Constructors
        }

        #endregion Classes

        #region Fields

        private string _baseDirPath;
        private bool _isDataWriterStop = false;
        private JavaScriptSerializer _jsonConvertor;
        private int _storagePeriod = 180;
        private string _typeName;
        private ConcurrentQueue<DataWithSubPath> _writeDataQueue = null;
        private Thread _writeFileWriteThread = null;

        #endregion Fields

        #region Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="filePath">Base 저장 경로</param>
        public CollectionHandler(string filePath = "")
        {
            if (filePath == "")
            {
                _typeName = typeof(T).Name;

                string currentPath = AppDomain.CurrentDomain.BaseDirectory; // 실행프로그램의 경로를 얻어 온다.
                currentPath = currentPath.TrimEnd(new char[] { '\\' });

                string newPath = Path.GetFullPath(Path.Combine(currentPath, @"..\")); // 표준은 Bin 폴더와 동일 위치이므로 한단계 상위 폴더
                newPath = newPath + @"\Data\" + _typeName + "\\";
                filePath = newPath;
            }

            _baseDirPath = filePath;
            _jsonConvertor = new JavaScriptSerializer();
            StartDataWriter(_baseDirPath);
        }

        #endregion Constructors

        #region Methods

        public bool ChangeDir(string filePath)
        {
            _baseDirPath = filePath;
            return true;
        }
        
        public List<T> LoadList(string filePath)
        {
            List<T> _data = new List<T>();

            if (File.Exists(filePath) == false)
            {
                return _data;
            }

            try
            {
                string jsonString = File.ReadAllText(filePath) + ']';
                _data = _jsonConvertor.Deserialize<List<T>>(jsonString);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }

            return _data;
        }
        
        /// <summary>
        ///
        /// </summary>
        /// <param name="subPath">하위 디렉토리와 파일 확장자를 포함한 저장 경로.</param>
        /// <param name="modelData"></param>
        /// <returns></returns>
        public bool SaveLine(string subPath, T modelData)
        {
            bool success = false;
            DataWithSubPath dataWId = new DataWithSubPath(subPath, modelData);
            try
            {
                _writeDataQueue.Enqueue(dataWId);
                success = true;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);

                return success;
            }
            return success;
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

        private bool DeleteDirectorySafely(string strDir, bool bRecursive)
        {
            bool success = false;

            try
            {
                if (Directory.Exists(strDir) == true)
                {
                    Directory.Delete(strDir, bRecursive);
                }
                success = true;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }

            return success;
        }

        private void DeleteOldFiles()
        {//파일 생성날자 기준으로
            try
            {
                string[] dirs = Directory.GetDirectories(_baseDirPath);

                foreach (string dir in dirs)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(dir);
                    if (IsValidDate(dirInfo.Name))
                    {
                        string dateData = dirInfo.Name.Substring(0, 8);
                        if (GetDayLength(dateData) > _storagePeriod)
                        {
                            DeleteDirectorySafely(dir, true);
                            Logger.Debug("Delete Directory : " + dir);
                        }// else
                    }// else
                }
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        private int GetDayLength(string dateValue)
        {
            DateTime dt;
            DateTime.TryParseExact(dateValue, "yyyyMMdd", null, DateTimeStyles.None, out dt);

            TimeSpan sp = DateTime.Now - dt;

            return sp.Days;
        }

        private bool IsValidDate(string dateValue)
        {
            try
            {
                string dateData = dateValue.Substring(0, 8);
                DateTime dt;

                return DateTime.TryParseExact(dateData, "yyyyMMdd", null, DateTimeStyles.None, out dt);
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }
        }

        private void MakeInvariantCultureThread()
        {
            try
            {
                CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();

                culture.NumberFormat = CultureInfo.InvariantCulture.NumberFormat;
                culture.DateTimeFormat = CultureInfo.InvariantCulture.DateTimeFormat;

                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
            catch (Exception ex)
            {
                {
                    Logger.Exception(ex);
                }
            }
        }

        private bool MakeNewFile(string filePath, T modelData)
        {
            try
            {
                List<T> _data = new List<T>();
                _data.Add(modelData);
                string jsondata = _jsonConvertor.Serialize(_data);
                int removeindex = jsondata.LastIndexOf(']');
                jsondata = jsondata.Remove(removeindex, 1);

                File.WriteAllText(filePath, jsondata);

                return true;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);

                return false;
            }
        }

        private void StartDataWriter(string writeDir)
        {
            _baseDirPath = Path.GetDirectoryName(writeDir);

            if (Directory.Exists(_baseDirPath) == false)
            {
                Directory.CreateDirectory(_baseDirPath);
            }
            else
            {
                DeleteOldFiles();
            }

            _writeDataQueue = new ConcurrentQueue<DataWithSubPath>();

            if (_writeFileWriteThread == null)
            {
                _writeFileWriteThread = new Thread(writeFileWriteThreadProc);
                _writeFileWriteThread.IsBackground = true;
            }

            _writeFileWriteThread.Start();
        }

        private void writeFileWriteThreadProc()
        {
            MakeInvariantCultureThread();

            while (true)
            {
                Thread.Sleep(10);

                if (_isDataWriterStop)
                {
                    break;
                }

                try
                {
                    if (_writeDataQueue.Count == 0)
                    {
                        continue;
                    }
                    DataWithSubPath writeData = new DataWithSubPath();

                    if (_writeDataQueue.TryDequeue(out writeData))
                    {
                        WriteLine(writeData);
                    }
                    else
                    {
                        Logger.Debug("Dequeue Fail !!");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex);
                }
            }
        }

        private bool WriteLine(DataWithSubPath dataWsPath)
        {
            //Todo:
            //파일, 디렉토리 만들기 확인 필요
            //시간으로 파일 path .json 만들기
            //리스트 모델 생성단에서 time 같이 보내게 Template 만들기.

            string filePath = _baseDirPath + dataWsPath._SubPath;
            CreateDirectorySafely(filePath);
            if (File.Exists(filePath) == false)
            {
                return MakeNewFile(filePath, dataWsPath._ModelData);
            }
            else
            {
                try
                {
                    string jsondata = ',' + _jsonConvertor.Serialize(dataWsPath._ModelData);

                    //allText = allText.Replace(@"{", "{\r\n\t");
                    //allText = allText.Replace(@",", ",\r\n\t");
                    //필요하면 vscode에서 전체선택하여 ctrl + K, ctrl + F 로 정렬하여 볼것.
                    //Visual Studio CodeMaid에서도 가능.

                    FileStream writeFileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                    StreamWriter StreamWriter = new StreamWriter(writeFileStream);

                    StreamWriter.WriteLine(jsondata);

                    StreamWriter.Close();
                    writeFileStream.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex);

                    return false;
                }
            }
        }

        #endregion Methods
    }
}