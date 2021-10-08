using EmWorks.Lib.Common;
using System;
using System.Collections.Generic;

namespace EmWorks.App.OpticInspection
{
    [Serializable]
    internal class CellModel
    {
        #region Fields

        private CollectionHandler<CellModel> _collectionHandler;
        private string _fileNameExtension = "enc";
        private string _subPath;

        #endregion Fields

        #region Properties

        public DateTime _DateTime { get; set; } = DateTime.Now;
        public string _Id { get; set; } = "SampleId";
        public string _StepID { get; set; } = "Init";

        #endregion Properties

        #region Constructors

        public CellModel()
        {
            _collectionHandler = new CollectionHandler<CellModel>();
        }

        public CellModel(string filePath)
        {
            _collectionHandler = new CollectionHandler<CellModel>(filePath);
        }

        #endregion Constructors

        #region Methods

        public List<CellModel> LoadList(string filepath)
        {
            return _collectionHandler.LoadList(filepath);
        }

        private string GetSubPath()
        {
            return GetSubPath(this);
        }

        private string GetSubPath(CellModel data)
        {
            return data._Id + "//" + data._Id + "." + _fileNameExtension;
        }

        private bool SaveLine(CellModel data)
        {
            string subPath = GetSubPath(data);
            return _collectionHandler.SaveLine(subPath, data);
        }

        private bool SaveLine()
        {
            return SaveLine(this);
        }

        #endregion Methods
    }
}