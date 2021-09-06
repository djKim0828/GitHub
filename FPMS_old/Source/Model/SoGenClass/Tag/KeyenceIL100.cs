using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Script.Serialization;
using EmWorks.Lib.Core;
using EmWorks.Lib.Common;
using EmWorks.DevDrv.KeyenceIL100;

namespace FPMS.E2105_FS111_121
{
    public class KeyenceIL100
    {
        #region Fields

        private const int _defaultTime = 5000;

        public static JavaScriptSerializer _JsonConvertor;
        private string _filePath;
        private System.Collections.Generic.Dictionary<string, Tag> _tags;

        private EmKeyenceIL100 _device;

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
         public Tag sxDeviceIL100
         {
             get
             {
                 return _tags[nameof(sxDeviceIL100)];
             }
         }
         public object syDeviceIL100
         {
             get
             {
                 return _tags[nameof(syDeviceIL100)];
             }
             set
             {
                 _tags[nameof(syDeviceIL100)].Is = Convert.ToInt32(value);
             }
         }
         public Tag xIL100OnOffLazer
         {
             get
             {
                 return _tags[nameof(xIL100OnOffLazer)];
             }
         }
         public object yIL100OnOffLazer
         {
             get
             {
                 return _tags[nameof(yIL100OnOffLazer)];
             }
             set
             {
                 _tags[nameof(yIL100OnOffLazer)].Is = Convert.ToInt32(value);
             }
         }
         public Tag xIL100GetDistance
         {
             get
             {
                 return _tags[nameof(xIL100GetDistance)];
             }
         }
         public object yIL100Comport
         {
             get
             {
                 return _tags[nameof(yIL100Comport)];
             }
             set
             {
                 _tags[nameof(yIL100Comport)].Is = value;
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

        private Tag GetpropertyName(string propertyName)
        {
            return _tags.FirstOrDefault(x => x.Value.Name == propertyName).Value;
        }
        
        public bool SetsyDeviceIL100(bool isExecute, out string data, int timeOut = _defaultTime)
        {
            #warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            syDeviceIL100 = Common; 

            data = string.Empty;
            sxDeviceIL100.Is = string.Empty;

            bool isActionBreak = false;
            Action action = () =>
            {
                while (true)
                {
                    Thread.Sleep(5);

                    if (sxDeviceIL100.Is == null)
                    {
                        continue;
                    }

                    if (sxDeviceIL100.Is.ToString() != string.Empty)
                    {
                        break;
                    } // else

                    if (isActionBreak == true)
                    {
                        break;
                    }

                }
            };

            if (WaitResult(timeOut, action) == true)
            {
                data = sxDeviceIL100.Is.ToString();
                return true;
            }
            else
            {
                isActionBreak = true;
                return false;
            }

        }
        public bool SetyIL100OnOffLazer(bool isExecute, int timeOut = _defaultTime)
        {
            #warning Interlock check 할 것.

            const int error = -1; 

            int Common = (isExecute == true) ? 1 : 0;

            yIL100OnOffLazer = Common; 

            bool isActionBreak = false;
            bool isError = false;
            Action action = () =>
            {
                while (true)
                {
                    Thread.Sleep(5);

                    if (xIL100OnOffLazer.Is == null)
                    {
                        continue;
                    }

                    if (xIL100OnOffLazer.Is.ToString() == Common.ToString())
                    {
                        break;
                    } // else

                    if (xIL100OnOffLazer.Is.ToString() == error.ToString())
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
       #endregion Methods
    }
}
