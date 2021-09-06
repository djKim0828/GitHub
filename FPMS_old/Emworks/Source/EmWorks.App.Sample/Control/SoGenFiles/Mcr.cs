using EmWorks.DevDrv.CognixMcr;
using EmWorks.Lib.Common;
using EmWorks.Lib.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Web.Script.Serialization;

namespace EmWorks.App.Sample
{
    public class Mcr
    {
        #region Fields

        public static JavaScriptSerializer _jsonConvertor;
        private const int _defaultTime = 5000;
        private EmCognixMcr _device;
        private string _filePath;
        private Dictionary<string, Tag> _tags;

        #endregion Fields

        #region Constructors

        public Mcr(string filePath)
        {
            _filePath = filePath;

            _tags = new Dictionary<string, Tag>();

            Load();

            // Sim
            _device = new EmCognixMcr(_tags, false);

            // Real
            //_device = new EmMCR(_data);
        }

        #endregion Constructors

        #region Properties

        public Tag sxDeviceMCR1
        {
            get
            {
                return (Tag)_tags[nameof(sxDeviceMCR1)];
            }
        }

        public Tag sxDeviceMCR2
        {
            get
            {
                return (Tag)_tags[nameof(sxDeviceMCR2)];
            }
        }

        public object syDeviceMCR1
        {
            get
            {
                return _tags[nameof(syDeviceMCR1)].Identity;
            }
            set
            {
                _tags[nameof(syDeviceMCR1)].Is = value;
            }
        }

        public object syDeviceMCR2
        {
            get
            {
                return _tags[nameof(syDeviceMCR2)].Identity;
            }
            set
            {
                _tags[nameof(syDeviceMCR2)].Is = value;
            }
        }

        public Tag xMCR1TriggerOn
        {
            get
            {
                return (Tag)_tags[nameof(xMCR1TriggerOn)];
            }
        }

        public Tag xMCR2TriggerOn
        {
            get
            {
                return (Tag)_tags[nameof(xMCR2TriggerOn)];
            }
        }

        public object yMCR1TriggerOn
        {
            get
            {
                return _tags[nameof(yMCR1TriggerOn)];
            }
            set
            {
                _tags[nameof(yMCR1TriggerOn)].Is = value;
            }
        }

        public object yMCR2TriggerOn
        {
            get
            {
                return _tags[nameof(yMCR2TriggerOn)];
            }
            set
            {
                _tags[nameof(yMCR2TriggerOn)].Is = value;
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

        public bool SetsyDeviceMCR1(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            syDeviceMCR1 = Common;

            Action action = () =>
            {
                while (true)
                {
                    if (sxDeviceMCR1.Is == null)
                    {
                        continue;
                    }

                    if (sxDeviceMCR1.Is.ToString() == Common.ToString())
                    {
                        break;
                    } // else

                    Thread.Sleep(5);
                }
            };

            return WaitResult(timeOut, action);
        }

        public bool SetsyDeviceMCR2(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            syDeviceMCR2 = Common;

            Action action = () =>
            {
                while (true)
                {
                    if (sxDeviceMCR2.Is == null)
                    {
                        continue;
                    }

                    if (sxDeviceMCR2.Is.ToString() == Common.ToString())
                    {
                        break;
                    } // else

                    Thread.Sleep(5);
                }
            };

            return WaitResult(timeOut, action);
        }

        public bool SetyMCR1TriggerOn(bool isExecute, out string data, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            data = string.Empty;
            xMCR1TriggerOn.Is = string.Empty;

            yMCR1TriggerOn = Common;

            Action action = () =>
            {
                while (true)
                {
                    if (xMCR1TriggerOn.Is == null)
                    {
                        continue;
                    }

                    if (xMCR1TriggerOn.Is.ToString() != string.Empty)
                    {
                        break;
                    } // else

                    Thread.Sleep(5);
                }
            };

            if (WaitResult(timeOut, action) == true)
            {
                data = xMCR1TriggerOn.Is.ToString();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SetyMCR2TriggerOn(bool isExecute, out string data, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            data = string.Empty;
            xMCR2TriggerOn.Is = string.Empty;

            yMCR2TriggerOn = Common;            

            Action action = () =>
            {
                while (true)
                {
                    if (xMCR2TriggerOn.Is == null)
                    {
                        continue;
                    }

                    if (xMCR2TriggerOn.Is.ToString() != string.Empty)
                    {
                        break;
                    } // else

                    Thread.Sleep(5);
                }
            };

            if (WaitResult(timeOut, action) == true)
            {
                data = xMCR2TriggerOn.Is.ToString();
                return true;
            }
            else
            {
                return false;
            }
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