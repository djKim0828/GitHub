using EmWorks.Lib.Common;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;

namespace EmWorks.App.OpticInspection
{
    public class DataWriter
    {
        #region Fields

        private bool _isDataWriterStop = false;

        private JavaScriptSerializer _jsonConvertor;

        private int _storagePeriod = 30;
        private ConcurrentQueue<WriteData> _writeDataQueue = null;

        private string _writeDirPath = null;

        private Thread _writeFileWriteThread = null;

        #endregion Fields

        #region Constructors

        public DataWriter()
        {
            string currentPath = AppDomain.CurrentDomain.BaseDirectory; // 실행프로그램의 경로를 얻어 온다.
            currentPath = currentPath.TrimEnd(new char[] { '\\' });

            _jsonConvertor = new JavaScriptSerializer();
            StartDataWriter(currentPath + @"\Data");
        }

        public DataWriter(string writeDir)
        {
            _jsonConvertor = new JavaScriptSerializer();
            StartDataWriter(writeDir);
        }

        #endregion Constructors

        #region Destructors

        ~DataWriter()
        {
            // N/A
        }

        #endregion Destructors

        #region Enums

        public enum WriteLevel
        {
            INFO = 0,
            DEBUG = 1
        }

        #endregion Enums

        #region Methods

        /// <summary>
        /// Write a <see cref="Debug"/> type writer.
        /// </summary>
        /// <param name="writer">writer message</param>
        /// <param name="objects">other message</param>
        public void Debug(string writer, params object[] objects)
        {
            Write(WriteLevel.DEBUG, GetWriteString(writer, objects));
        }

        /// <summary>
        /// Write a <see cref="Infomation"/> type writer.
        /// </summary>
        /// <param name="writer">writer message</param>
        /// <param name="objects">other message</param>
        public void Infomation(string writer, params object[] objects)
        {
            Write(WriteLevel.INFO, GetWriteString(writer, objects));
        }

        /// <summary>
        /// 로그파일 보관기간을 설정한다. ( Default 는 30일 )
        /// Setting the storage period of writer files.
        /// </summary>
        /// <param name="storagePeriod">Setting value</param>
        public void SetStoragePeriod(int storagePeriod)
        {
            _storagePeriod = storagePeriod;
        }

        /// <summary>
        /// Stop DataWriter
        /// </summary>
        public void StopDataWriter()
        {
            _isDataWriterStop = true;

            if (_writeFileWriteThread != null)
            {
                _writeFileWriteThread = null;
            }
        }

        /// <summary>
        /// static common Trace
        /// </summary>
        /// <param name="writer">writer message </param>
        /// <param name="objects">other message</param>
        public void Trace(string writer, params object[] objects)
        {
#if DEBUG
            System.Diagnostics.Trace.WriteLine(GetWriteString(writer, objects));
#endif
        }

        private bool CreateDirectorySafely(string directory)
        {
            bool success = false;

            try
            {
                if (Directory.Exists(directory) == false)
                    Directory.CreateDirectory(directory);
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
                    Directory.Delete(strDir, bRecursive);
                success = true;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }

            return success;
        }

        private void DeleteOldFiles()
        {
            try
            {
                string[] dirs = Directory.GetDirectories(_writeDirPath);

                foreach (string dir in dirs)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(dir);

                    if (IsValidDate(dirInfo.Name))
                    {
                        string dateData = dirInfo.Name.Substring(0, 8);
                        if (GetDayLength(dateData) > _storagePeriod)
                        {
                            DeleteDirectorySafely(dir, true);
                            Trace("Delete : " + dir);
                        }// else
                    }// else
                }
            }
            catch
            {
            }
        }

        private int GetDayLength(string dateValue)
        {
            DateTime dt;
            DateTime.TryParseExact(dateValue, "yyyyMMdd", null, DateTimeStyles.None, out dt);

            TimeSpan sp = DateTime.Now - dt;
            return sp.Days;
        }

        private string GetFilePath(WriteData writeData)
        {
            string writeDir = string.Format("{0}\\{1}",
                _writeDirPath,
                writeData.GetDateString());

            CreateDirectorySafely(writeDir);

            return string.Format("{0}\\{1}.enc",
                                writeDir,
                                writeData.GetDateString());
        }

        private string GetWriteString(string write, object[] objects)
        {
            string writeValue = string.Empty;

            try
            {
                if (objects != null && objects.Length > 0)
                {
                    if (write != string.Empty)
                    {
                        writeValue += write + ",";
                    } //else

                    for (int i = 0; i < objects.Length; i++)
                    {
                        writeValue += objects[i].ToString() + ",";
                    }
                }
                else
                {
                    writeValue = write;
                }
            }
            catch
            {
                writeValue = write;
            }

            return writeValue;
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
                Trace(ex.ToString());
                return false;
            }
        }

        private void MakeInvariantCultureThread()
        {
            try
            {
                CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();

                culture.NumberFormat = System.Globalization.CultureInfo.InvariantCulture.NumberFormat;
                culture.DateTimeFormat = System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat;

                System.Threading.Thread.CurrentThread.CurrentCulture = culture;
                System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
            }
            catch (Exception ex)
            {
                {
                    Logger.Exception(ex);
                }
            }
        }

        private void StartDataWriter(string writeDir)
        {
            _writeDirPath = writeDir;

            if (Directory.Exists(_writeDirPath) == false)
            {
                Directory.CreateDirectory(_writeDirPath);
            }
            else
            {
                DeleteOldFiles();
            }

            _writeDataQueue = new ConcurrentQueue<WriteData>();

            if (_writeFileWriteThread == null)
            {
                _writeFileWriteThread = new Thread(writeFileWriteThreadProc);
                _writeFileWriteThread.IsBackground = true;
            }

            _writeFileWriteThread.Start();
        }

        private void Write(WriteLevel level, string message)
        {
#if DEBUG
            if (level != WriteLevel.DEBUG)
            {
                Trace(string.Format("[{0}] : {1}", level, message));
            }
#endif

            try
            {
                StackTrace stackTrace = new StackTrace();           // get call stack

                WriteData writeData = new WriteData();
                writeData.WriteDataLevel = level;
                writeData.WriteTime = DateTime.Now;
                writeData.Message = message;

                _writeDataQueue.Enqueue(writeData);
            }
            catch (Exception ex)
            {
                Trace(ex.ToString());
            }
        }

        private void WriteDataToFile(WriteData writeData)
        {
            try
            {
                string filePath = GetFilePath(writeData);

                string writeText = _jsonConvertor.Serialize(new WriteDataForFile(writeData));

                FileStream writeFileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                StreamWriter StreamWriter = new StreamWriter(writeFileStream);

                StreamWriter.WriteLine(writeText);

                StreamWriter.Close();
                writeFileStream.Close();
            }
            catch (Exception ex)
            {
                Trace(ex.ToString());
            }
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

                    WriteData writeData = new WriteData();

                    if (_writeDataQueue.TryDequeue(out writeData))
                    {
                        WriteDataToFile(writeData);
                    }
                    else
                    {
                        Trace("Dequeue Fail !!");
                    }
                }
                catch (Exception ex)
                {
                    Trace(ex.ToString());
                }
            }
        }

        #endregion Methods

        #region Classes

        public class WriteData
        {
            #region Properties

            public string Message { get; set; }

            // 아래 Properties는 Code Convention을 적용하지 않는다. 해당 변수명을 활용하기 때문 [2020/10/05 djkim]
            public WriteLevel WriteDataLevel { get; set; }

            public DateTime WriteTime { get; set; }

            #endregion Properties

            #region Methods

            /// <summary>
            /// Date format change by region
            /// </summary>
            /// <returns></returns>
            public string GetDateString()
            {
                return WriteTime.ToString("yyyyMMdd_HH", CultureInfo.InvariantCulture);
            }

            /// <summary>
            ///
            /// </summary>
            /// <returns></returns>
            public string GetDetailInfo()
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("writeTime : " + WriteTime);
                sb.AppendLine("writeLevel : " + WriteDataLevel.ToString());
                sb.AppendLine("Message : " + Message);

                return sb.ToString();
            }

            #endregion Methods
        }

        public class WriteDataForFile
        {
            #region Constructors

            /// <summary>
            /// Definition class for writing write data
            /// </summary>
            /// <param name="writedata">input write data</param>
            public WriteDataForFile(WriteData writeData)
            {
                WriteDataLevel = writeData.WriteDataLevel;
                WriteTime = writeData.WriteTime.ToString("yyyy/MM/dd HH:mm:ss.fff");
                Message = writeData.Message;
            }

            #endregion Constructors

            #region Properties

            public string Message { get; set; }
            public WriteLevel WriteDataLevel { get; set; }
            public string WriteTime { get; set; }

            #endregion Properties
        }

        #endregion Classes
    }
}