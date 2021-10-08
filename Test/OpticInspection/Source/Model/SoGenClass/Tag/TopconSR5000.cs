using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Script.Serialization;
using EmWorks.Lib.Core;
using EmWorks.Lib.Common;
using EmWorks.DevDrv.TopconSR5000;

namespace EmWorks.App.OpticInspection
{
    public class TopconSR5000
    {
        #region Fields

        private const int _defaultTime = 5000;

        public static JavaScriptSerializer _jsonConvertor;
        private string _filePath;
        private System.Collections.Generic.Dictionary<string, Tag> _tags;

        private EmTopconSR5000 _device; 

        #endregion Fields

        #region Constructors

        public TopconSR5000(string filePath, bool isSimulationMode)
        {
            _filePath = filePath;

            _tags = new Dictionary<string, Tag>();

            Load();

            _device = new EmTopconSR5000(_tags, isSimulationMode);

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

        ~TopconSR5000()
        {
        }

        #endregion Destructors

        #region Properties
        public Tag sxDeviceSR5000
        {
            get
            {
                return _tags[nameof(sxDeviceSR5000)];
            }
        }
        public object syDeviceSR5000
        {
            get
            {
                return _tags[nameof(syDeviceSR5000)];
            }
            set
            {
                _tags[nameof(syDeviceSR5000)].Is = Convert.ToInt32(value);
            }
        }
        public Tag xSR5000Connect
        {
            get
            {
                return _tags[nameof(xSR5000Connect)];
            }
        }
        public object ySR5000Connect
        {
            get
            {
                return _tags[nameof(ySR5000Connect)];
            }
            set
            {
                _tags[nameof(ySR5000Connect)].Is = Convert.ToInt32(value);
            }
        }
        public Tag xSR5000MeasurementStartResult
        {
            get
            {
                return _tags[nameof(xSR5000MeasurementStartResult)];
            }
        }
        public object ySR5000MeasurementStart
        {
            get
            {
                return _tags[nameof(ySR5000MeasurementStart)];
            }
            set
            {
                _tags[nameof(ySR5000MeasurementStart)].Is = Convert.ToInt32(value);
            }
        }
        public Tag xSR5000MeasurementStopResult
        {
            get
            {
                return _tags[nameof(xSR5000MeasurementStopResult)];
            }
        }
        public object ySR5000MeasurementStop
        {
            get
            {
                return _tags[nameof(ySR5000MeasurementStop)];
            }
            set
            {
                _tags[nameof(ySR5000MeasurementStop)].Is = Convert.ToInt32(value);
            }
        }
        public Tag xSR5000RGBImageResult
        {
            get
            {
                return _tags[nameof(xSR5000RGBImageResult)];
            }
        }
        public object ySR5000RGBImageRequest
        {
            get
            {
                return _tags[nameof(ySR5000RGBImageRequest)];
            }
            set
            {
                _tags[nameof(ySR5000RGBImageRequest)].Is = value;
            }
        }
        public Tag xSR5000PseudoImageResult
        {
            get
            {
                return _tags[nameof(xSR5000PseudoImageResult)];
            }
        }
        public object ySR5000PseudoImageRequest
        {
            get
            {
                return _tags[nameof(ySR5000PseudoImageRequest)];
            }
            set
            {
                _tags[nameof(ySR5000PseudoImageRequest)].Is = value;
            }
        }
        public Tag xSR5000RecipeLoadResult
        {
            get
            {
                return _tags[nameof(xSR5000RecipeLoadResult)];
            }
        }
        public object ySR5000RecipeLoad
        {
            get
            {
                return _tags[nameof(ySR5000RecipeLoad)];
            }
            set
            {
                _tags[nameof(ySR5000RecipeLoad)].Is = Convert.ToInt32(value);
            }
        }
        public Tag xSR5000RecipeSaveResult
        {
            get
            {
                return _tags[nameof(xSR5000RecipeSaveResult)];
            }
        }
        public object ySR5000RecipeSave
        {
            get
            {
                return _tags[nameof(ySR5000RecipeSave)];
            }
            set
            {
                _tags[nameof(ySR5000RecipeSave)].Is = Convert.ToInt32(value);
            }
        }
        public Tag xSR5000MeasurementLoadResult
        {
            get
            {
                return _tags[nameof(xSR5000MeasurementLoadResult)];
            }
        }
        public object ySR5000MeasurementLoad
        {
            get
            {
                return _tags[nameof(ySR5000MeasurementLoad)];
            }
            set
            {
                _tags[nameof(ySR5000MeasurementLoad)].Is = Convert.ToInt32(value);
            }
        }
        public Tag xSR5000MeasurementSaveResult
        {
            get
            {
                return _tags[nameof(xSR5000MeasurementSaveResult)];
            }
        }
        public object ySR5000MeasurementSave
        {
            get
            {
                return _tags[nameof(ySR5000MeasurementSave)];
            }
            set
            {
                _tags[nameof(ySR5000MeasurementSave)].Is = Convert.ToInt32(value);
            }
        }
        public Tag xSR5000MeasurementDestroyResult
        {
            get
            {
                return _tags[nameof(xSR5000MeasurementDestroyResult)];
            }
        }
        public object ySR5000MeasurementDestroy
        {
            get
            {
                return _tags[nameof(ySR5000MeasurementDestroy)];
            }
            set
            {
                _tags[nameof(ySR5000MeasurementDestroy)].Is = Convert.ToInt32(value);
            }
        }
        public Tag xSR5000LiveViewFocusData
        {
            get
            {
                return _tags[nameof(xSR5000LiveViewFocusData)];
            }
        }
        public Tag xSR5000LiveViewImage
        {
            get
            {
                return _tags[nameof(xSR5000LiveViewImage)];
            }
        }
        public object ySR5000LiveViewImageRequest
        {
            get
            {
                return _tags[nameof(ySR5000LiveViewImageRequest)];
            }
            set
            {
                _tags[nameof(ySR5000LiveViewImageRequest)].Is = Convert.ToInt32(value);
            }
        }
        public Tag xSR5000LiveViewStartResult
        {
            get
            {
                return _tags[nameof(xSR5000LiveViewStartResult)];
            }
        }
        public object ySR5000LiveViewStart
        {
            get
            {
                return _tags[nameof(ySR5000LiveViewStart)];
            }
            set
            {
                _tags[nameof(ySR5000LiveViewStart)].Is = Convert.ToInt32(value);
            }
        }
        public Tag xSR5000LiveViewStopResult
        {
            get
            {
                return _tags[nameof(xSR5000LiveViewStopResult)];
            }
        }
        public object ySR5000LiveViewStop
        {
            get
            {
                return _tags[nameof(ySR5000LiveViewStop)];
            }
            set
            {
                _tags[nameof(ySR5000LiveViewStop)].Is = Convert.ToInt32(value);
            }
        }
        public Tag xSR5000ROISearchResult
        {
            get
            {
                return _tags[nameof(xSR5000ROISearchResult)];
            }
        }
        public object ySR5000ROISearch
        {
            get
            {
                return _tags[nameof(ySR5000ROISearch)];
            }
            set
            {
                _tags[nameof(ySR5000ROISearch)].Is = Convert.ToInt32(value);
            }
        }
        public object ySR5000AutoSpotWidth
        {
            get
            {
                return _tags[nameof(ySR5000AutoSpotWidth)];
            }
            set
            {
                _tags[nameof(ySR5000AutoSpotWidth)].Is = Convert.ToDouble(value);
            }
        }
        public object ySR5000AutoSpotHeight
        {
            get
            {
                return _tags[nameof(ySR5000AutoSpotHeight)];
            }
            set
            {
                _tags[nameof(ySR5000AutoSpotHeight)].Is = Convert.ToDouble(value);
            }
        }
        public object ySR5000AutoSpotMin
        {
            get
            {
                return _tags[nameof(ySR5000AutoSpotMin)];
            }
            set
            {
                _tags[nameof(ySR5000AutoSpotMin)].Is = Convert.ToInt32(value);
            }
        }
        public object ySR5000AutoSpotMax
        {
            get
            {
                return _tags[nameof(ySR5000AutoSpotMax)];
            }
            set
            {
                _tags[nameof(ySR5000AutoSpotMax)].Is = Convert.ToInt32(value);
            }
        }
        public object ySR5000AutoSpotYTh
        {
            get
            {
                return _tags[nameof(ySR5000AutoSpotYTh)];
            }
            set
            {
                _tags[nameof(ySR5000AutoSpotYTh)].Is = Convert.ToDouble(value);
            }
        }
        public object ySR5000AutoSpotShapeType
        {
            get
            {
                return _tags[nameof(ySR5000AutoSpotShapeType)];
            }
            set
            {
                _tags[nameof(ySR5000AutoSpotShapeType)].Is = Convert.ToInt32(value);
            }
        }
        public object ySR5000RecipeFilePath
        {
            get
            {
                return _tags[nameof(ySR5000RecipeFilePath)];
            }
            set
            {
                _tags[nameof(ySR5000RecipeFilePath)].Is = value.ToString();
            }
        }
        public object ySR5000MeasurementFilePath
        {
            get
            {
                return _tags[nameof(ySR5000MeasurementFilePath)];
            }
            set
            {
                _tags[nameof(ySR5000MeasurementFilePath)].Is = value.ToString();
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

        public bool SetsyDeviceSR5000(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            syDeviceSR5000 = Common;

            bool isActionBreak = false;
            bool isError = false;
            Action action = () =>
            {
                while (true)
                {
                    if (sxDeviceSR5000.Is == null)
                    {
                        continue;
                    }

                    if (sxDeviceSR5000.Is.ToString() == Common.ToString())
                    {
                        break;
                    } // else

                    if (sxDeviceSR5000.Is.ToString() == OpenClose.Error.ToString())
                    {
                        isError = true;
                        break;
                    } // else

                    Thread.Sleep(5);

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
        public bool SetySR5000Connect(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            ySR5000Connect = Common;

            bool isActionBreak = false;
            bool isError = false;
            Action action = () =>
            {
                while (true)
                {
                    if (xSR5000Connect.Is == null)
                    {
                        continue;
                    }

                    if (xSR5000Connect.Is.ToString() == Common.ToString())
                    {
                        break;
                    } // else

                    if (xSR5000Connect.Is.ToString() == OpenClose.Error.ToString())
                    {
                        isError = true;
                        break;
                    } // else

                    Thread.Sleep(5);

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
        public bool SetySR5000MeasurementStart(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            ySR5000MeasurementStart = Common;

            bool isActionBreak = false;
            bool isError = false;
            Action action = () =>
            {
                while (true)
                {
                    if (xSR5000MeasurementStartResult.Is == null)
                    {
                        continue;
                    }

                    if (xSR5000MeasurementStartResult.Is.ToString() == Common.ToString())
                    {
                        break;
                    } // else

                    if (xSR5000MeasurementStartResult.Is.ToString() == OpenClose.Error.ToString())
                    {
                        isError = true;
                        break;
                    } // else

                    Thread.Sleep(5);

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
        public bool SetySR5000MeasurementStop(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            ySR5000MeasurementStop = Common;

            bool isActionBreak = false;
            bool isError = false;
            Action action = () =>
            {
                while (true)
                {
                    if (xSR5000MeasurementStopResult.Is == null)
                    {
                        continue;
                    }

                    if (xSR5000MeasurementStopResult.Is.ToString() == Common.ToString())
                    {
                        break;
                    } // else

                    if (xSR5000MeasurementStopResult.Is.ToString() == OpenClose.Error.ToString())
                    {
                        isError = true;
                        break;
                    } // else

                    Thread.Sleep(5);

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
        public bool SetySR5000RGBImageRequest(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            ySR5000RGBImageRequest = Common;

            bool isActionBreak = false;
            Action action = () =>
            {
                while (true)
                {
                    Thread.Sleep(5);

                    if (xSR5000RGBImageResult.Is != null)
                    {
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
                return true;
            }
        }
        public bool SetySR5000PseudoImageRequest(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            ySR5000PseudoImageRequest = Common;

            bool isActionBreak = false;
            Action action = () =>
            {
                while (true)
                {
                    Thread.Sleep(5);

                    if (xSR5000PseudoImageResult.Is != null)
                    {
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
                return true;
            }
        }
        public bool SetySR5000RecipeLoad(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            ySR5000RecipeLoad = Common;

            bool isActionBreak = false;
            bool isError = false;
            Action action = () =>
            {
                while (true)
                {
                    if (xSR5000RecipeLoadResult.Is == null)
                    {
                        continue;
                    }

                    if (xSR5000RecipeLoadResult.Is.ToString() == Common.ToString())
                    {
                        break;
                    } // else

                    if (xSR5000RecipeLoadResult.Is.ToString() == OpenClose.Error.ToString())
                    {
                        isError = true;
                        break;
                    } // else

                    Thread.Sleep(5);

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
        public bool SetySR5000RecipeSave(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            ySR5000RecipeSave = Common;

            bool isActionBreak = false;
            bool isError = false;
            Action action = () =>
            {
                while (true)
                {
                    if (xSR5000RecipeSaveResult.Is == null)
                    {
                        continue;
                    }

                    if (xSR5000RecipeSaveResult.Is.ToString() == Common.ToString())
                    {
                        break;
                    } // else

                    if (xSR5000RecipeSaveResult.Is.ToString() == OpenClose.Error.ToString())
                    {
                        isError = true;
                        break;
                    } // else

                    Thread.Sleep(5);

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
        public bool SetySR5000MeasurementLoad(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            ySR5000MeasurementLoad = Common;

            bool isActionBreak = false;
            bool isError = false;
            Action action = () =>
            {
                while (true)
                {
                    if (xSR5000MeasurementLoadResult.Is == null)
                    {
                        continue;
                    }

                    if (xSR5000MeasurementLoadResult.Is.ToString() == Common.ToString())
                    {
                        break;
                    } // else

                    if (xSR5000MeasurementLoadResult.Is.ToString() == OpenClose.Error.ToString())
                    {
                        isError = true;
                        break;
                    } // else

                    Thread.Sleep(5);

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
        public bool SetySR5000MeasurementSave(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            ySR5000MeasurementSave = Common;

            bool isActionBreak = false;
            bool isError = false;
            Action action = () =>
            {
                while (true)
                {
                    if (xSR5000MeasurementSaveResult.Is == null)
                    {
                        continue;
                    }

                    if (xSR5000MeasurementSaveResult.Is.ToString() == Common.ToString())
                    {
                        break;
                    } // else

                    if (xSR5000MeasurementSaveResult.Is.ToString() == OpenClose.Error.ToString())
                    {
                        isError = true;
                        break;
                    } // else

                    Thread.Sleep(5);

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
        public bool SetySR5000MeasurementDestroy(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            ySR5000MeasurementDestroy = Common;

            bool isActionBreak = false;
            bool isError = false;
            Action action = () =>
            {
                while (true)
                {
                    if (xSR5000MeasurementDestroyResult.Is == null)
                    {
                        continue;
                    }

                    if (xSR5000MeasurementDestroyResult.Is.ToString() == Common.ToString())
                    {
                        break;
                    } // else

                    if (xSR5000MeasurementDestroyResult.Is.ToString() == OpenClose.Error.ToString())
                    {
                        isError = true;
                        break;
                    } // else

                    Thread.Sleep(5);

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
        public bool SetySR5000LiveViewImageRequest(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            ySR5000LiveViewImageRequest = Common;

            bool isActionBreak = false;
            Action action = () =>
            {
                while (true)
                {
                    Thread.Sleep(5);

                    if (xSR5000LiveViewImage.Is != null)
                    {
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
                return true;
            }
        }
        public bool SetySR5000LiveViewStart(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            ySR5000LiveViewStart = Common;

            bool isActionBreak = false;
            bool isError = false;
            Action action = () =>
            {
                while (true)
                {
                    if (xSR5000LiveViewStartResult.Is == null)
                    {
                        continue;
                    }

                    if (xSR5000LiveViewStartResult.Is.ToString() == Common.ToString())
                    {
                        break;
                    } // else

                    if (xSR5000LiveViewStartResult.Is.ToString() == OpenClose.Error.ToString())
                    {
                        isError = true;
                        break;
                    } // else

                    Thread.Sleep(5);

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
        public bool SetySR5000LiveViewStop(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            ySR5000LiveViewStop = Common;

            bool isActionBreak = false;
            bool isError = false;
            Action action = () =>
            {
                while (true)
                {
                    if (xSR5000LiveViewStopResult.Is == null)
                    {
                        continue;
                    }

                    if (xSR5000LiveViewStopResult.Is.ToString() == Common.ToString())
                    {
                        break;
                    } // else

                    if (xSR5000LiveViewStopResult.Is.ToString() == OpenClose.Error.ToString())
                    {
                        isError = true;
                        break;
                    } // else

                    Thread.Sleep(5);

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
        public bool SetySR5000ROISearch(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            ySR5000ROISearch = Common;

            bool isActionBreak = false;
            Action action = () =>
            {
                while (true)
                {
                    Thread.Sleep(5);

                    if (xSR5000ROISearchResult.Is != null)
                    {
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
                return true;
            }
        }
        #endregion Methods
    }
}
