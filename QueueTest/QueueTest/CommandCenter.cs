using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace QueueTest
{
    public class CommandCenter
    {
        #region Fields

        private static CommandCenter _instance;

        private static bool _isLoop = false;
        private static string[] _RecipeList;
        private Dictionary<string, Cell> _Cells = null;
        private int _Interval = 500;

        private static Operation _commandStatus;

        #endregion Fields

        #region Properties
        public static Operation _CommandStatus
        {
            get
            {
                return _commandStatus;
            }
            set
            {
                _commandStatus = value;
            }
        }
        #endregion Properties

        #region Constructors

        public CommandCenter()
        {
            Initialize();
        }

        #endregion Constructors

        #region Methods

        public static void DeleteAllCell()
        {
            if (_instance == null)
            {
                _instance = new CommandCenter();
            }

            _instance._Cells = new Dictionary<string, Cell>();
        }

        public static void DeleteCell(string id)
        {
            if (_instance == null)
            {
                _instance = new CommandCenter();
            }

            _instance._Cells.Remove(id);
        }

        public static void InsertCell(object obj)
        {
            if (_instance == null)
            {
                _instance = new CommandCenter();
            }

            Cell cell = new Cell();
            string now = DateTime.Now.ToString("yyyyMMddHHmmss.fff");
            cell._DataWriter = _DataWriter;
            cell.insertTime = now;
            cell.Id = now;
            cell.StageId = Idx.StageId.Load;
            cell.StepId = Idx.StageStap.Step1;
            cell.StatusId = Idx.StageStatus.Wait;
            cell.Temp = obj;

            _instance._Cells.Add(cell.Id, cell);
        }

        public static Dictionary<string, Cell> SearchCell(int stageId, int maxCapacityCell)
        {
            if (_instance == null)
            {
                _instance = new CommandCenter();
            }

            Dictionary<string, Cell> searchCell =
                    (from item in _instance._Cells
                     where item.Value.StageId == stageId
                     && item.Value.StatusId == Idx.StageStatus.Wait
                     orderby item.Value.insertTime
                     select item).ToDictionary(x => x.Key, y => y.Value);

            Dictionary<string, Cell> resultCell = new Dictionary<string, Cell>();

            int i = 0;
            foreach (Cell cell in searchCell.Values)
            {
                if (i >= maxCapacityCell)
                {
                    break;
                }

                resultCell.Add(cell.Id, cell);
                i++;
            }

            return resultCell;
        }

        public static void UpdataeCellInfo(string id, int stageId, int statusId)
        {
            if (_instance == null)
            {
                _instance = new CommandCenter();
            }

            //_instance._Cells.Select(i => i.Value)
            //        .Where(d => d.StageId == stageId)
            //        .FirstOrDefault(x => x.Name == name).Value;


            _instance._Cells.Select(i => i.Value).FirstOrDefault(x => x.Id == id).StageId = stageId;
            _instance._Cells.Select(i => i.Value).FirstOrDefault(x => x.Id == id).StatusId = statusId;
        }

        private Dictionary<string, Cell> SearchComplateCell()
        {
            //if (_instance == null)
            //{
            //    _instance = new CommandCenter();
            //}

            return (from item in _instance._Cells
                    where item.Value.StatusId == Idx.StageStatus.Complate
                    orderby item.Value.insertTime
                    select item).ToDictionary(x => x.Key, y => y.Value);
        }




        public static void Start(string[] recipeList)
        {
            if (_instance == null)
            {
                _instance = new CommandCenter();
            }

            _RecipeList = recipeList;
            _isLoop = true;

            _CommandStatus = Operation.Start;
        }

        public static void ChangeStatus(Operation status)
        {

            _CommandStatus = status;

            //_isLoop = false;
        }
        
        private void AllComplete(Cell cell)
        {
            DeleteCell(cell.Id);

            // Rolling
            Thread.Sleep(10);
            InsertCell(cell.Temp);
        }

        private void ChangeCellStage()
        {
            Dictionary<string, Cell> resultCell = SearchComplateCell();

            foreach (Cell cell in resultCell.Values)
            {
                int nextStage = SearchNextStage(cell.StageId);

                if (nextStage < 0)
                {
                    // Next를 찾지못함. 없거나 끝난 경우이지만 우린 끝난 경우로 처리한다.
                    //MessageBox.Show("Recipe가 잘못되었습니다. 확인 바랍니다.");

                    AllComplete(cell);

                    continue;
                } // else

                UpdataeCellInfo(cell.Id, nextStage, 0);
            }
        }

        private Thread _WorkProcThread;


        private static DataWriter _DataWriter;

        private void Initialize()
        {
            InitInstance();


            _DataWriter = new DataWriter();
                        

            _isLoop = false;

            _WorkProcThread = new Thread(WorksProc);
            _WorkProcThread.IsBackground = true;
            _WorkProcThread.Start();
            //ThreadPool.QueueUserWorkItem(WorksProc);
        }

        private void InitInstance()
        {
            _Cells = new Dictionary<string, Cell>();
        }

        
        private int SearchNextStage(int id)
        {
            int nextId = -1;

            for (int i = 0; i < _RecipeList.Length; i++)
            {
                if (_RecipeList[i] == id.ToString())
                {
                    if (i < _RecipeList.Length - 1)
                    {
                        nextId = Convert.ToInt16(_RecipeList[i + 1]);
                        break;
                    }
                }
            }

            return nextId;
        }

        private void WorksProc(object state)
        {
            while (true)
            {
                if (_CommandStatus == CommandCenter.Operation.Start ||
                    _CommandStatus == CommandCenter.Operation.Resume)
                {
                    ChangeCellStage();

                    Thread.Sleep(_Interval);
                    Application.DoEvents();
                }
                    //if (_isLoop == false)
                    //{
                    //    Thread.Sleep(_Interval);
                    //    Application.DoEvents();

                    //    continue;
                    //} // else
            }
        }

        #endregion Methods


        public enum Operation : int
        {
            Start,
            Stop,
            Pause,
            Resume,
            Abort,
            Max
        }
    }
}