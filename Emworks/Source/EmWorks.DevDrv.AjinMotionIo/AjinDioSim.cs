using System.Collections.Generic;

namespace EmWorks.DevDrv.AjinMotionIo
{
    internal class AjinDioSim
    {
        #region Fields

        private EmAjinDio _dio;

        #endregion Fields

        #region Constructors

        internal AjinDioSim(EmAjinDio dio)
        {
            _dio = dio;
        }

        #endregion Constructors

        #region Methods

        internal bool DefineInputList(Dictionary<string, int> ioList)
        {
            bool result = true;
            _dio.DIList.Clear();

            int maxnumber = -1;
            foreach (var item in ioList)
            {
                if (maxnumber < item.Value)
                {
                    maxnumber = item.Value;
                }//else
            }

            while (_dio.DIList.Count <= maxnumber)
            {
                _dio.DIList.Add(new DioModel(_dio.DIList.Count));
            }

            foreach (var item in _dio.DIList)
            {
                item.OnChange += _dio.DI_OnChange;
            }

            foreach (var item in ioList)
            {
                if (_dio.DIList[item.Value].DIoName != item.Key)
                {
                    _dio.DIList[item.Value].DIoName = item.Key;

                    if (_dio.DigitalInput.ContainsKey(item.Key) == true)
                    {
                        _dio.DigitalInput[item.Key] = _dio.DIList[item.Value];
                    }
                    else
                    {
                        _dio.DigitalInput.Add(item.Key, _dio.DIList[item.Value]);
                    }
                }//else
            }

            return true;
        }

        internal bool DefineOutputList(Dictionary<string, int> ioList)
        {
            bool result = true;
            _dio.DOList.Clear();

            int maxnumber = -1;
            foreach (var item in ioList)
            {
                if (maxnumber < item.Value)
                {
                    maxnumber = item.Value;
                }//else
            }

            while (_dio.DOList.Count <= maxnumber)
            {
                _dio.DOList.Add(new DioModel(_dio.DOList.Count));
            }

            foreach (var item in _dio.DOList)
            {
                item.OnChange += _dio.DO_OnChange;
            }

            foreach (var item in ioList)
            {
                if (_dio.DOList[item.Value].DIoName != item.Key)
                {
                    _dio.DOList[item.Value].DIoName = item.Key;

                    if (_dio.DigitalOutput.ContainsKey(item.Key) == true)
                    {
                        _dio.DigitalOutput[item.Key] = _dio.DOList[item.Value];
                    }
                    else
                    {
                        _dio.DigitalOutput.Add(item.Key, _dio.DOList[item.Value]);
                    }
                }//else
            }

            return true;
        }

        internal bool GetExtenalLibVersion()
        {
            _dio._version = "Simulation";
            return true;
        }

        internal bool GetIsOpen()
        {
            return _dio.IsOpen;
        }

        internal bool GetModuleCount(ref int count)
        {
            count = 20;

            return true;
        }

        internal bool OpenLib()
        {
            return true;
        }

        internal bool SetDigitalInput(DioModel dio, bool signal)
        {
            dio.Signal = signal;
            return true;
        }

        internal bool SetDigitalOutput(DioModel dio, bool signal)
        {
            dio.Signal = signal;
            return true;
        }

        #endregion Methods
    }
}