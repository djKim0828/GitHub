using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace QueueTest
{
    public class Cell
    {
        //private ConcurrentQueue<Cell> _CellDataQueue = null;

        private int _StageId;
        private int _StepId;
        private int _StatusId;

        public string Id { get; set; }
        public string CellId { get; set; }
        public string insertTime { get; set; }
        public string ChangeTime { get; set; }
        public int StageId
        {
            get
            {
                return _StageId;
            }
            set
            {
                _StageId = value;
                SaveCellInfo();
                WriteLog();
            }
        }
        public int StepId
        {
            get
            {
                return _StepId;
            }

            set
            {
                _StepId = value;
                SaveCellInfo();
                WriteLog();
            }
        }
        public int StatusId
        {
            get
            {
                return _StatusId;
            }

            set
            {
                _StatusId = value;
                SaveCellInfo();
                WriteLog();
            }
        }

        public DataWriter _DataWriter;

        

        public object Temp { get; set; } // Test 용도

        public void WriteLog()
        {
            string msg = "ID = " + Id +  " Cell ID = " + CellId + " Stage ID =" + StageId + " StepId = " + StepId + " StatusID = " + StatusId;
            Logger.Debug(msg);
            
        }

        JavaScriptSerializer jsonConvertor;

        public void SaveCellInfo()
        {
            //string currentPath = AppDomain.CurrentDomain.BaseDirectory; // 실행프로그램의 경로를 얻어 온다.
            //currentPath = currentPath.TrimEnd(new char[] { '\\' });
            ////string newPath = Path.GetFullPath(Path.Combine(currentPath, @"..\")); // 표준은 Bin 폴더와 동일 위치이므로 한단계 상위 폴더

            //string filePath = currentPath + @"\Cell\";
            //string fileName = Id + ".json";

            //if (File.Exists(filePath) == false)
            //{
            //    //폴더 만들기
            //    System.IO.Directory.CreateDirectory(filePath);
            //}


            CellInfo cellInfo = new CellInfo();
            cellInfo.Id = Id;
            cellInfo.CellId = CellId;
            cellInfo.StageId = StageId;
            cellInfo.StatusId = StatusId;
            cellInfo.StepId = StepId;
            cellInfo.insertTime = insertTime;
            ChangeTime = DateTime.Now.ToString("yyyyMMddHHmmss.fff");
            cellInfo.ChangeTime = ChangeTime;

            jsonConvertor = new JavaScriptSerializer();

            string allText = string.Empty;
            try
            {
                allText = jsonConvertor.Serialize(cellInfo);
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }

            _DataWriter.Infomation(allText);

            // 개행 넣기
            //allText = allText.Replace(@"{", "{\r\n  ");
            //allText = allText.Replace(@",", ",\r\n  ");

            ////File.WriteAllText(filePath + fileName, allText);
            //using (StreamWriter outputFile = new StreamWriter(filePath + fileName, true))
            //{
            //    outputFile.WriteLine(allText);
            //}            
        }
    }

    public class CellInfo
    {        
        private int _StageId;
        private int _StepId;
        private int _StatusId;

        public string Id { get; set; }
        public string CellId { get; set; }
        public string insertTime { get; set; }
        public string ChangeTime { get; set; }
        public int StageId
        {
            get
            {
                return _StageId;
            }
            set
            {
                _StageId = value;
            }
        }
        public int StepId
        {
            get
            {
                return _StepId;
            }

            set
            {
                _StepId = value;
            }
        }
        public int StatusId
        {
            get
            {
                return _StatusId;
            }

            set
            {
                _StatusId = value;

            }
        }
    }        
}
