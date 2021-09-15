using EmWorks.DevDrv.SamwonTemp1500;
using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Script.Serialization;

namespace FPMS.E2105_FS111_121
{
    public class SamwonTemp1500
    {
        #region Fields

        public static JavaScriptSerializer _jsonConvertor;
        private const int _defaultTime = 5000;
        private EmSamwonTemp1500 _device;
        private string _filePath;
        private System.Collections.Generic.Dictionary<string, Tag> _tags;

        #endregion Fields

        #region Constructors

        public SamwonTemp1500(string filePath, bool isSimulationMode)
        {
            _filePath = filePath;

            _tags = new Dictionary<string, Tag>();

            Load();

            _device = new EmSamwonTemp1500(_tags, isSimulationMode);
        }

        #endregion Constructors

        #region Destructors

        ~SamwonTemp1500()
        {
        }

        #endregion Destructors

        #region Properties

        public Tag sxDeviceTemp1500
        {
            get
            {
                return _tags[nameof(sxDeviceTemp1500)];
            }
        }

        public object syDeviceTemp1500
        {
            get
            {
                return _tags[nameof(syDeviceTemp1500)];
            }
            set
            {
                _tags[nameof(syDeviceTemp1500)].Is = Convert.ToInt32(value);
            }
        }

        public Tag xTemp1500GetPV
        {
            get
            {
                return _tags[nameof(xTemp1500GetPV)];
            }
        }

        public Tag xTemp1500GetSV
        {
            get
            {
                return _tags[nameof(xTemp1500GetSV)];
            }
        }

        public Tag xTemp1500RunStop
        {
            get
            {
                return _tags[nameof(xTemp1500RunStop)];
            }
        }

        public Tag xTemp1500SetSV
        {
            get
            {
                return _tags[nameof(xTemp1500SetSV)];
            }
        }

        public object yTemp1500Comport
        {
            get
            {
                return _tags[nameof(yTemp1500Comport)];
            }
            set
            {
                _tags[nameof(yTemp1500Comport)].Is = value;
            }
        }

        public object yTemp1500RunStop
        {
            get
            {
                return _tags[nameof(yTemp1500RunStop)];
            }
            set
            {
                _tags[nameof(yTemp1500RunStop)].Is = Convert.ToInt32(value);
            }
        }

        public object yTemp1500SetSV
        {
            get
            {
                return _tags[nameof(yTemp1500SetSV)];
            }
            set
            {
                _tags[nameof(yTemp1500SetSV)].Is = Convert.ToDouble(value);
            }
        }

        #endregion Properties

        #region Indexers

        public Tag this[string name]
        {
            get
            {
                return _tags[name];
            }
        }

        #endregion Indexers

        #region Methods

        public Dictionary<string, TagIdentity> GetAllData()
        {
            Dictionary<string, TagIdentity> temp = new Dictionary<string, TagIdentity>();
            foreach (Tag ti in _tags.Values)
            {
                temp.Add(ti.Name, ti.Identity);
            }

            return temp;
        }

        public Dictionary<string, Tag> GetTags()
        {
            return _tags;
        }

        public bool Load()
        {
            try
            {
                _jsonConvertor = new JavaScriptSerializer();

                string jsonString = File.ReadAllText(_filePath);

                Dictionary<string, TagIdentity> readData = _jsonConvertor.Deserialize<Dictionary<string, TagIdentity>>(jsonString);

                _tags = new Dictionary<string, Tag>();

                foreach (TagIdentity ti in readData.Values)
                {
                    Tag temp = new Tag(ti);
                    _tags.Add(ti.Name, temp);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);
                return false;
            }
        }

        public bool Save()
        {
            try
            {
                string allText = _jsonConvertor.Serialize(GetAllData());

                // 개행 ?�기
                allText = allText.Replace(@"{", "{\r\n  ");
                allText = allText.Replace(@",", ",\r\n  ");

                File.WriteAllText(_filePath, allText);

                return true;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.ToString());
                return false;
            }
        }

        public bool SetsyDeviceTemp1500(bool isExecute, int timeOut = _defaultTime)
        {
            const int error = -1;

            int Common = (isExecute == true) ? 1 : 0;

            syDeviceTemp1500 = Common;

            bool isActionBreak = false;
            bool isError = false;
            Action action = () =>
            {
                while (true)
                {
                    Thread.Sleep(5);

                    if (isActionBreak == true)
                    {
                        break;
                    } // else

                    if (sxDeviceTemp1500.Is == null)
                    {
                        continue;
                    } // else

                    if (sxDeviceTemp1500.Is.ToString() == Common.ToString())
                    {
                        break;
                    } // else

                    if (sxDeviceTemp1500.Is.ToString() == error.ToString())
                    {
                        isError = true;
                        break;
                    } // else
                }
            };

            if (WaitResult(timeOut, action) == false)
            {
                isActionBreak = true;
                return false;
            }
            else
            {
                if (isError == true)
                {
                    return false;
                } // else

                return true;
            }
        }

        public bool SetyTemp1500RunStop(bool isExecute, int timeOut = _defaultTime)
        {
            const int error = -1;

            int Common = (isExecute == true) ? 1 : 0;

            yTemp1500RunStop = Common;

            bool isActionBreak = false;
            bool isError = false;
            Action action = () =>
            {
                while (true)
                {
                    Thread.Sleep(5);

                    if (isActionBreak == true)
                    {
                        break;
                    } // else

                    if (xTemp1500RunStop.Is == null)
                    {
                        continue;
                    } // else

                    if (xTemp1500RunStop.Is.ToString() == Common.ToString())
                    {
                        break;
                    } // else

                    if (xTemp1500RunStop.Is.ToString() == error.ToString())
                    {
                        isError = true;
                        break;
                    } // else
                }
            };

            if (WaitResult(timeOut, action) == false)
            {
                isActionBreak = true;
                return false;
            }
            else
            {
                if (isError == true)
                {
                    return false;
                } // else

                return true;
            }
        }

        public bool SetyTemp1500SetSV(double isExecute, int timeOut = _defaultTime)
        {
            const int error = -1;

            yTemp1500SetSV = isExecute;

            bool isActionBreak = false;
            bool isError = false;
            Action action = () =>
            {
                while (true)
                {
                    Thread.Sleep(5);

                    if (isActionBreak == true)
                    {
                        break;
                    } // else

                    if (xTemp1500SetSV.Is == null)
                    {
                        continue;
                    } // else

                    if (xTemp1500SetSV.Is.ToString() == isExecute.ToString())
                    {
                        break;
                    } // else

                    if (xTemp1500SetSV.Is.ToString() == error.ToString())
                    {
                        isError = true;
                        break;
                    } // else
                }
            };

            if (WaitResult(timeOut, action) == false)
            {
                isActionBreak = true;
                return false;
            }
            else
            {
                if (isError == true)
                {
                    return false;
                } // else

                return true;
            }
        }

        private Tag GetpropertyName(string propertyName)
        {
            return _tags.FirstOrDefault(x => x.Value.Name == propertyName).Value;
        }

        private bool WaitResult(int timeOut, Action action)
        {
            int timeout = timeOut == 0 ? 100 : timeOut;
            IAsyncResult async_result;
            async_result = action.BeginInvoke(null, null);

            if (async_result.AsyncWaitHandle.WaitOne(timeout))
            {
                return true;
            }
            else
            {
                Logger.Error("Timeout");
                return false;
            }
        }

        #endregion Methods
    }
}