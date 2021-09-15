using EmWorks.DevDrv.SamwonSDR100;
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
    public class SamwonSDR100
    {
        #region Fields

        public static JavaScriptSerializer _jsonConvertor;
        private const int _defaultTime = 5000;
        private EmSamwonSDR100 _device;
        private string _filePath;
        private System.Collections.Generic.Dictionary<string, Tag> _tags;

        #endregion Fields

        #region Constructors

        public SamwonSDR100(string filePath, bool isSimulationMode)
        {
            _filePath = filePath;

            _tags = new Dictionary<string, Tag>();

            Load();

            _device = new EmSamwonSDR100(_tags, isSimulationMode);
        }

        #endregion Constructors

        #region Destructors

        ~SamwonSDR100()
        {
        }

        #endregion Destructors

        #region Properties

        public Tag sxDeviceSDR100
        {
            get
            {
                return _tags[nameof(sxDeviceSDR100)];
            }
        }

        public object syDeviceSDR100
        {
            get
            {
                return _tags[nameof(syDeviceSDR100)];
            }
            set
            {
                _tags[nameof(syDeviceSDR100)].Is = Convert.ToInt32(value);
            }
        }

        public Tag xSDR100GetPV1
        {
            get
            {
                return _tags[nameof(xSDR100GetPV1)];
            }
        }

        public Tag xSDR100GetPV2
        {
            get
            {
                return _tags[nameof(xSDR100GetPV2)];
            }
        }

        public Tag xSDR100GetPV3
        {
            get
            {
                return _tags[nameof(xSDR100GetPV3)];
            }
        }

        public Tag xSDR100GetPV4
        {
            get
            {
                return _tags[nameof(xSDR100GetPV4)];
            }
        }

        public Tag xSDR100GetPV5
        {
            get
            {
                return _tags[nameof(xSDR100GetPV5)];
            }
        }

        public Tag xSDR100GetPV6
        {
            get
            {
                return _tags[nameof(xSDR100GetPV6)];
            }
        }

        public object ySDR100Comport
        {
            get
            {
                return _tags[nameof(ySDR100Comport)];
            }
            set
            {
                _tags[nameof(ySDR100Comport)].Is = value;
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

                // 개행 넣기
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

        public bool SetsyDeviceSDR100(bool isExecute, int timeOut = _defaultTime)
        {
            const int error = -1;

            int Common = (isExecute == true) ? 1 : 0;

            syDeviceSDR100 = Common;

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

                    if (sxDeviceSDR100.Is == null)
                    {
                        continue;
                    } // else

                    if (sxDeviceSDR100.Is.ToString() == Common.ToString())
                    {
                        break;
                    } // else

                    if (sxDeviceSDR100.Is.ToString() == error.ToString())
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
            else // timeout
            {
                Logger.Error("Timeout");
                return false;
            }
        }

        #endregion Methods
    }
}