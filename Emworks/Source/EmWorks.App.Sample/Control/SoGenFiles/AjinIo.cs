using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Script.Serialization;
using EmWorks.Lib.Core;
using EmWorks.Lib.Common;
using EmWorks.DevDrv.AjinMotionIo;

namespace EmWorks.App.Sample
{

    public class AjinIo
    {
        #region Fields

        private const int _defaultTime = 5000;

        public static JavaScriptSerializer _jsonConvertor;
        private string _filePath;
        private System.Collections.Generic.Dictionary<string, Tag> _tags;

        private DriverBase _device;

        #endregion Fields

        #region Constructors

        public AjinIo(string filePath, bool isSimulationMode)
        {
            _filePath = filePath;

            _tags = new Dictionary<string, Tag>();

            Load();

            _device = new EmAjinIo(_tags, isSimulationMode); 

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

        ~AjinIo()
        {
        }

        #endregion Destructors

        #region Properties
        public Tag sxDeviceIO
        {
            get
            {
                return _tags[nameof(sxDeviceIO)];
            }
        }
        public object syDeviceIO
        {
            get
            {
                return _tags[nameof(syDeviceIO)];
            }
            set
            {
                _tags[nameof(syDeviceIO)].Is = value;
            }
        }
        public Tag xDeviceLibraryVersion
        {
            get
            {
                return _tags[nameof(xDeviceLibraryVersion)];
            }
        }
        public object ySystemAnalog1Out
        {
            get
            {
                return _tags[nameof(ySystemAnalog1Out)];
            }
            set
            {
                _tags[nameof(ySystemAnalog1Out)].Is = value;
            }
        }
        public object yInRobotAnallog2Out
        {
            get
            {
                return _tags[nameof(yInRobotAnallog2Out)];
            }
            set
            {
                _tags[nameof(yInRobotAnallog2Out)].Is = value;
            }
        }
        public object yInRobotAnallog3Out
        {
            get
            {
                return _tags[nameof(yInRobotAnallog3Out)];
            }
            set
            {
                _tags[nameof(yInRobotAnallog3Out)].Is = value;
            }
        }
        public Tag xSystemAnalog1Input
        {
            get
            {
                return _tags[nameof(xSystemAnalog1Input)];
            }
        }
        public Tag xInRobotAnallog2Input
        {
            get
            {
                return _tags[nameof(xInRobotAnallog2Input)];
            }
        }
        public Tag xInRobotAnallog3Input
        {
            get
            {
                return _tags[nameof(xInRobotAnallog3Input)];
            }
        }
        public Tag xSystemCda
        {
            get
            {
                return _tags[nameof(xSystemCda)];
            }
        }
        public Tag xSystemVac
        {
            get
            {
                return _tags[nameof(xSystemVac)];
            }
        }
        public Tag xInShuttleVac1
        {
            get
            {
                return _tags[nameof(xInShuttleVac1)];
            }
        }
        public Tag xInShuttleVac2
        {
            get
            {
                return _tags[nameof(xInShuttleVac2)];
            }
        }
        public Tag xInRobotPadVac1
        {
            get
            {
                return _tags[nameof(xInRobotPadVac1)];
            }
        }
        public Tag xInRobotPadVac2
        {
            get
            {
                return _tags[nameof(xInRobotPadVac2)];
            }
        }
        public Tag xOutRobotPadVac1
        {
            get
            {
                return _tags[nameof(xOutRobotPadVac1)];
            }
        }
        public Tag xOutRobotPadVac2
        {
            get
            {
                return _tags[nameof(xOutRobotPadVac2)];
            }
        }
        public Tag xInRobotPad1Down
        {
            get
            {
                return _tags[nameof(xInRobotPad1Down)];
            }
        }
        public Tag xInRobotPad1Up
        {
            get
            {
                return _tags[nameof(xInRobotPad1Up)];
            }
        }
        public Tag xInRobotPad2Down
        {
            get
            {
                return _tags[nameof(xInRobotPad2Down)];
            }
        }
        public Tag xInRobotPad2Up
        {
            get
            {
                return _tags[nameof(xInRobotPad2Up)];
            }
        }
        public Tag xOutRobotPad1Turn
        {
            get
            {
                return _tags[nameof(xOutRobotPad1Turn)];
            }
        }
        public Tag xOutRobotPad1Return
        {
            get
            {
                return _tags[nameof(xOutRobotPad1Return)];
            }
        }
        public Tag xOutRobotPad2Turn
        {
            get
            {
                return _tags[nameof(xOutRobotPad2Turn)];
            }
        }
        public Tag xOutRobotPad2Return
        {
            get
            {
                return _tags[nameof(xOutRobotPad2Return)];
            }
        }
        public Tag xOutCvDetector
        {
            get
            {
                return _tags[nameof(xOutCvDetector)];
            }
        }
        public Tag xOutCvDetector1
        {
            get
            {
                return _tags[nameof(xOutCvDetector1)];
            }
        }
        public Tag xOutCvDetector2
        {
            get
            {
                return _tags[nameof(xOutCvDetector2)];
            }
        }
        public object yInShuttleVac1On
        {
            get
            {
                return _tags[nameof(yInShuttleVac1On)];
            }
            set
            {
                _tags[nameof(yInShuttleVac1On)].Is = value;
            }
        }
        public object yInShuttleVac2On
        {
            get
            {
                return _tags[nameof(yInShuttleVac2On)];
            }
            set
            {
                _tags[nameof(yInShuttleVac2On)].Is = value;
            }
        }
        public object yInShuttleBlow1On
        {
            get
            {
                return _tags[nameof(yInShuttleBlow1On)];
            }
            set
            {
                _tags[nameof(yInShuttleBlow1On)].Is = value;
            }
        }
        public object yInShuttleBlow2On
        {
            get
            {
                return _tags[nameof(yInShuttleBlow2On)];
            }
            set
            {
                _tags[nameof(yInShuttleBlow2On)].Is = value;
            }
        }
        public object yInRobotPadVac1On
        {
            get
            {
                return _tags[nameof(yInRobotPadVac1On)];
            }
            set
            {
                _tags[nameof(yInRobotPadVac1On)].Is = value;
            }
        }
        public object yInRobotPadVBlow1On
        {
            get
            {
                return _tags[nameof(yInRobotPadVBlow1On)];
            }
            set
            {
                _tags[nameof(yInRobotPadVBlow1On)].Is = value;
            }
        }
        public object yInRobotPadVac2On
        {
            get
            {
                return _tags[nameof(yInRobotPadVac2On)];
            }
            set
            {
                _tags[nameof(yInRobotPadVac2On)].Is = value;
            }
        }
        public object yInRobotPadVBlow2On
        {
            get
            {
                return _tags[nameof(yInRobotPadVBlow2On)];
            }
            set
            {
                _tags[nameof(yInRobotPadVBlow2On)].Is = value;
            }
        }
        public object yInRobotPad1Down
        {
            get
            {
                return _tags[nameof(yInRobotPad1Down)];
            }
            set
            {
                _tags[nameof(yInRobotPad1Down)].Is = value;
            }
        }
        public object yInRobotPad1Up
        {
            get
            {
                return _tags[nameof(yInRobotPad1Up)];
            }
            set
            {
                _tags[nameof(yInRobotPad1Up)].Is = value;
            }
        }
        public object yInRobotPad2Down
        {
            get
            {
                return _tags[nameof(yInRobotPad2Down)];
            }
            set
            {
                _tags[nameof(yInRobotPad2Down)].Is = value;
            }
        }
        public object yInRobotPad2Up
        {
            get
            {
                return _tags[nameof(yInRobotPad2Up)];
            }
            set
            {
                _tags[nameof(yInRobotPad2Up)].Is = value;
            }
        }
        public object yOutRobotPadVac1On
        {
            get
            {
                return _tags[nameof(yOutRobotPadVac1On)];
            }
            set
            {
                _tags[nameof(yOutRobotPadVac1On)].Is = value;
            }
        }
        public object yOutRobotPadVBlow1On
        {
            get
            {
                return _tags[nameof(yOutRobotPadVBlow1On)];
            }
            set
            {
                _tags[nameof(yOutRobotPadVBlow1On)].Is = value;
            }
        }
        public object yOutRobotPadVac2On
        {
            get
            {
                return _tags[nameof(yOutRobotPadVac2On)];
            }
            set
            {
                _tags[nameof(yOutRobotPadVac2On)].Is = value;
            }
        }
        public object yOutRobotPadVBlow2On
        {
            get
            {
                return _tags[nameof(yOutRobotPadVBlow2On)];
            }
            set
            {
                _tags[nameof(yOutRobotPadVBlow2On)].Is = value;
            }
        }
        public object yOutRobotPad1Turn
        {
            get
            {
                return _tags[nameof(yOutRobotPad1Turn)];
            }
            set
            {
                _tags[nameof(yOutRobotPad1Turn)].Is = value;
            }
        }
        public object yOutRobotPad1Return
        {
            get
            {
                return _tags[nameof(yOutRobotPad1Return)];
            }
            set
            {
                _tags[nameof(yOutRobotPad1Return)].Is = value;
            }
        }
        public object yOutRobotPad2Turn
        {
            get
            {
                return _tags[nameof(yOutRobotPad2Turn)];
            }
            set
            {
                _tags[nameof(yOutRobotPad2Turn)].Is = value;
            }
        }
        public object yOutRobotPad2Return
        {
            get
            {
                return _tags[nameof(yOutRobotPad2Return)];
            }
            set
            {
                _tags[nameof(yOutRobotPad2Return)].Is = value;
            }
        }
        public object yTowerRedOn
        {
            get
            {
                return _tags[nameof(yTowerRedOn)];
            }
            set
            {
                _tags[nameof(yTowerRedOn)].Is = value;
            }
        }
        public object yTowerYellowOn
        {
            get
            {
                return _tags[nameof(yTowerYellowOn)];
            }
            set
            {
                _tags[nameof(yTowerYellowOn)].Is = value;
            }
        }
        public object yTowerGreenOn
        {
            get
            {
                return _tags[nameof(yTowerGreenOn)];
            }
            set
            {
                _tags[nameof(yTowerGreenOn)].Is = value;
            }
        }
        public object ySystemBuzzer01On
        {
            get
            {
                return _tags[nameof(ySystemBuzzer01On)];
            }
            set
            {
                _tags[nameof(ySystemBuzzer01On)].Is = value;
            }
        }
        public object ySystemBuzzer02On
        {
            get
            {
                return _tags[nameof(ySystemBuzzer02On)];
            }
            set
            {
                _tags[nameof(ySystemBuzzer02On)].Is = value;
            }
        }
        public object ySystemBuzzer03On
        {
            get
            {
                return _tags[nameof(ySystemBuzzer03On)];
            }
            set
            {
                _tags[nameof(ySystemBuzzer03On)].Is = value;
            }
        }
        public object ySystemBuzzer04On
        {
            get
            {
                return _tags[nameof(ySystemBuzzer04On)];
            }
            set
            {
                _tags[nameof(ySystemBuzzer04On)].Is = value;
            }
        }
        public object ySystemBuzzer05On
        {
            get
            {
                return _tags[nameof(ySystemBuzzer05On)];
            }
            set
            {
                _tags[nameof(ySystemBuzzer05On)].Is = value;
            }
        }
        public object ySystemMagnetContact
        {
            get
            {
                return _tags[nameof(ySystemMagnetContact)];
            }
            set
            {
                _tags[nameof(ySystemMagnetContact)].Is = value;
            }
        }
        public object yOnBtnFrontLampOn
        {
            get
            {
                return _tags[nameof(yOnBtnFrontLampOn)];
            }
            set
            {
                _tags[nameof(yOnBtnFrontLampOn)].Is = value;
            }
        }
        public object yOnBtnRearLampOn
        {
            get
            {
                return _tags[nameof(yOnBtnRearLampOn)];
            }
            set
            {
                _tags[nameof(yOnBtnRearLampOn)].Is = value;
            }
        }
        public object yOffBntFrontLampOn
        {
            get
            {
                return _tags[nameof(yOffBntFrontLampOn)];
            }
            set
            {
                _tags[nameof(yOffBntFrontLampOn)].Is = value;
            }
        }
        public object yOffBntRearLampOn
        {
            get
            {
                return _tags[nameof(yOffBntRearLampOn)];
            }
            set
            {
                _tags[nameof(yOffBntRearLampOn)].Is = value;
            }
        }
        public object yDoorFront01On
        {
            get
            {
                return _tags[nameof(yDoorFront01On)];
            }
            set
            {
                _tags[nameof(yDoorFront01On)].Is = value;
            }
        }
        public object yDoorFront02On
        {
            get
            {
                return _tags[nameof(yDoorFront02On)];
            }
            set
            {
                _tags[nameof(yDoorFront02On)].Is = value;
            }
        }
        public object yDoorRearOn
        {
            get
            {
                return _tags[nameof(yDoorRearOn)];
            }
            set
            {
                _tags[nameof(yDoorRearOn)].Is = value;
            }
        }
        public object yKeyFrontOn
        {
            get
            {
                return _tags[nameof(yKeyFrontOn)];
            }
            set
            {
                _tags[nameof(yKeyFrontOn)].Is = value;
            }
        }
        public object yKeyRearOn
        {
            get
            {
                return _tags[nameof(yKeyRearOn)];
            }
            set
            {
                _tags[nameof(yKeyRearOn)].Is = value;
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

        public bool SetsyDeviceIO(bool isExecute, int timeOut = _defaultTime)
        {
#warning Interlock check 할 것.

            int Common = (isExecute == true) ? 1 : 0;

            syDeviceIO = Common;

            Action action = () =>
            {
                while (true)
                {
                    if (sxDeviceIO.Is == null)
                    {
                        continue;
                    }

                    if (sxDeviceIO.Is.ToString() == Common.ToString())
                    {
                        break;
                    } // else

                    Thread.Sleep(5);
                }
            };

            return WaitResult(timeOut, action);
        }
        #endregion Methods
    }
}
