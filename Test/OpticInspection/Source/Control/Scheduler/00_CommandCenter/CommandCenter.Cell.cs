using EmWorks.Lib.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmWorks.App.OpticInspection
{
    public partial class CommandCenter
    {
        #region Fields

        private static DateTime _beforeTime;

        #endregion Fields

        #region Methods

        public static object ChangeCell(CellChangeInfo ccif)
        {
            object returnValue = new object();
            try
            {
                lock (_IsKey)
                {
                    switch (ccif.ChangeType)
                    {
                        case ChangeType.Insert:
                            _instance.InsertCell(ccif.TempObject);
                            break;

                        case ChangeType.DeleteAll:
                            _instance.DeleteAllCell();
                            break;

                        case ChangeType.Delete:
                            _instance.DeleteCell(ccif.Id);
                            break;

                        case ChangeType.UpdateCellId:
                            _instance.UpdataeCellId(ccif.Id, ccif.CellId, ccif.ClassName);
                            break;

                        case ChangeType.UpdataeCellStatusId:
                            _instance.UpdataeCellStatus(ccif.Id, ccif.StatusId, ccif.ClassName);
                            break;

                        case ChangeType.UpdataeNextStage:
                            _instance.UpdataeNextStage(ccif.Id, ccif.StageId, ccif.StatusId);
                            break;

                        case ChangeType.SearchCell:
                            returnValue = _instance.SearchCell(ccif.StageId, _MaximumCapacity);
                            break;

                        case ChangeType.SearchWaitCell:
                            returnValue = _instance.SearchWaitCell(ccif.StageId, _MaximumCapacity);
                            break;

                        case ChangeType.SearchComplateCell:
                            returnValue = _instance.SearchComplateCell();
                            break;
                    }
                }

                return returnValue;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                return null;
            }
        }

        private void DeleteAllCell()
        {
            if (_instance == null)
            {
                _instance = new CommandCenter();
            } // else

            lock (_IsKey)
            {
                _instance._cells = new Dictionary<string, Cell>();
            }
        }

        private void DeleteCell(string id)
        {
            if (_instance == null)
            {
                _instance = new CommandCenter();
            } // else

            lock (_IsKey)
            {
                _instance._cells.Remove(id);
            }
        }

        private void InsertCell(object obj)
        {
            if (_instance == null)
            {
                _instance = new CommandCenter();
            } // else

            Cell cell = new Cell();
            string now = DateTime.Now.ToString("yyyyMMddHHmmss.fff");

            // Toido :
            //cell._DataWriter = Global._CellInfoWriter;
            cell.insertTime = now;
            cell.Id = now;
            cell.CellId = string.Empty;
            cell.StageId = Convert.ToInt32(_scheduleList[0]);
            cell.StepId = Idx.StageStap.None;
            cell.StatusId = Idx.StageStatus.Wait;
            cell.Temp = obj;

            lock (_IsKey)
            {
                _instance._insertCells.Add(cell.Id, cell);
            }
        }

        private Dictionary<string, Cell> SearchCell(int stageId, int maxCapacityCell)
        {
            if (_instance == null)
            {
                _instance = new CommandCenter();
            } // else

            Dictionary<string, Cell> searchCell =
                    (from item in _instance._cells
                     where item.Value.StageId == stageId
                     && item.Value.CellId != string.Empty
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

        private Dictionary<string, Cell> SearchWaitCell(int stageId, int maxCapacityCell)
        {
            Dictionary<string, Cell> resultCell = new Dictionary<string, Cell>();

            try
            {
                if (_instance == null)
                {
                    _instance = new CommandCenter();
                } // else

                Dictionary<string, Cell> searchCell =
                        (from item in _instance._cells
                         where item.Value.StageId == stageId
                         && item.Value.StatusId == Idx.StageStatus.Wait
                         orderby item.Value.insertTime
                         select item).ToDictionary(x => x.Key, y => y.Value);

                int i = 0;
                foreach (Cell cell in searchCell.Values)
                {
                    if (i >= maxCapacityCell)
                    {
                        break;
                    } // else

                    resultCell.Add(cell.Id, cell);
                    i++;
                }

                return resultCell;
            }
            catch
            {
                return resultCell;
            }
        }

        private void UpdataeCellId(string id, string cellId, string className)
        {
            try
            {
                if (_instance == null)
                {
                    _instance = new CommandCenter();
                } // else

                lock (_IsKey)
                {
                    _instance._cells.Select(i => i.Value).FirstOrDefault(x => x.Id == id).CellId = cellId;
                }
            }
            catch (System.Exception ex)
            {
                string cName = className;
                Logger.Exception(ex);
            }
        }

        private void UpdataeCellStatus(string id, int status, string className)
        {
            try
            {
                if (_instance == null)
                {
                    _instance = new CommandCenter();
                } // else

                lock (_IsKey)
                {
                    _instance._cells.Select(i => i.Value).FirstOrDefault(x => x.Id == id).StatusId = status;
                }
            }
            catch (System.Exception ex)
            {
                string cName = className;
                Logger.Exception(ex);
            }
        }

        private void UpdataeNextStage(string id, int stageId, int status)
        {
            try
            {
                if (_instance == null)
                {
                    _instance = new CommandCenter();
                } // else

                lock (_IsKey)
                {
                    _instance._cells.Select(i => i.Value).FirstOrDefault(x => x.Id == id).StageId = 0; // Defualt로 바꾸고,

                    _instance._cells.Select(i => i.Value).FirstOrDefault(x => x.Id == id).StatusId = status;

                    _instance._cells.Select(i => i.Value).FirstOrDefault(x => x.Id == id).StageId = stageId;
                }
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        private Dictionary<string, Cell> SearchComplateCell()
        {
            try
            {
                if (_instance == null)
                {
                    _instance = new CommandCenter();
                } // else

                return (from item in _instance._cells
                        where item.Value.StatusId == Idx.StageStatus.Complate
                        orderby item.Value.insertTime
                        select item).ToDictionary(x => x.Key, y => y.Value);
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                return null;
            }
        }

        #endregion Methods

        #region Classes

        public static class ChangeType
        {
            #region Fields

            public const int Insert = 0;
            public const int DeleteAll = 1;
            public const int Delete = 2;
            public const int UpdateCellId = 3;
            public const int UpdataeCellStatusId = 4;
            public const int UpdataeNextStage = 5;
            public const int SearchCell = 6;
            public const int SearchWaitCell = 7;
            public const int SearchComplateCell = 8;
           
            #endregion Fields
        }

        #endregion Classes
    }
}