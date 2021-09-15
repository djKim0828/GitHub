using EmWorks.DevDrv.KeyenceIL100;
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
    public class KeyenceIL100
    {
        #region Fields

        public static JavaScriptSerializer _JsonConvertor;
        private const int _defaultTime = 5000;
        private DriverBase _device;
        private string _filePath;
        private System.Collections.Generic.Dictionary<string, Tag> _tags;

        #endregion Fields

        #region Constructors

        public KeyenceIL100(string filePath, bool isSimulationMode)
        {
            _filePath = filePath;

            _tags = new Dictionary<string, Tag>();

            Load();

            _device = new EmKeyenceIL100(_tags, isSimulationMode);
        }

        public Tag this[string name]
        {
            get
            {
                return _tags[name];
            }
        }

        #endregion Constructors

        #region Destructors

        ~KeyenceIL100()
        {
        }

        #endregion Destructors

        #region Properties

        public Tag sxDeviceIL
        {
            get
            {
                return _tags[nameof(sxDeviceIL)];
            }
        }

        public object syDeviceIL
        {
            get
            {
                return _tags[nameof(syDeviceIL)];
            }
            set
            {
                _tags[nameof(syDeviceIL)].Is = Convert.ToInt32(value);
            }
        }

        public Tag xILGetDistance
        {
            get
            {
                return _tags[nameof(xILGetDistance)];
            }
        }

        public Tag xILOnOffLazer
        {
            get
            {
                return _tags[nameof(xILOnOffLazer)];
            }
        }

        public object yILOnOffLazer
        {
            get
            {
                return _tags[nameof(yILOnOffLazer)];
            }
            set
            {
                _tags[nameof(yILOnOffLazer)].Is = Convert.ToInt32(value);
            }
        }

        #endregion Properties

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
                _JsonConvertor = new JavaScriptSerializer();

                string jsonString = File.ReadAllText(_filePath);

                Dictionary<string, TagIdentity> readData = _JsonConvertor.Deserialize<Dictionary<string, TagIdentity>>(jsonString);

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
                string allText = _JsonConvertor.Serialize(GetAllData());

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

        public bool SetsyDeviceIL(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            const int error = -1;

            int Common = (isExecute == true) ? 1 : 0;

            syDeviceIL = Common;

            bool isActionBreak = false;
            bool isError = false;
            Action action = () =>
            {
                while (true)
                {
                    Thread.Sleep(5);

                    if (sxDeviceIL.Is == null)
                    {
                        continue;
                    }

                    if (sxDeviceIL.Is.ToString() == Common.ToString())
                    {
                        break;
                    } // else

                    if (sxDeviceIL.Is.ToString() == error.ToString())
                    {
                        isError = true;
                        break;
                    } // else

                    if (isActionBreak == true)
                    {
                        break;
                    }
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
                }

                return true;
            }
        }

        public bool SetyILOnOffLazer(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            const int error = -1;

            int Common = (isExecute == true) ? 1 : 0;

            yILOnOffLazer = Common;

            bool isActionBreak = false;
            bool isError = false;
            Action action = () =>
            {
                while (true)
                {
                    Thread.Sleep(5);

                    if (xILOnOffLazer.Is == null)
                    {
                        continue;
                    }

                    if (xILOnOffLazer.Is.ToString() == Common.ToString())
                    {
                        break;
                    } // else

                    if (xILOnOffLazer.Is.ToString() == error.ToString())
                    {
                        isError = true;
                        break;
                    } // else

                    if (isActionBreak == true)
                    {
                        break;
                    }
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
                }

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