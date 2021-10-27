using EmWorks.Lib.Common;
using System;
using System.Collections.Generic;

namespace EmWorks.DevDrv.LiveView
{
    public class EmLiveView

    {
        #region Fields

        public List<EmLiveViewUserControl> IPCamera = new List<EmLiveViewUserControl>();
        private EmLiveViewModel _configLiveView;

        #endregion Fields

        #region Constructors

        public EmLiveView(int cameraCount)
        {
            Initialize(cameraCount);
        }

        #endregion Constructors

        #region Destructors

        ~EmLiveView()
        {
        }

        #endregion Destructors

        #region Properties

        public EmLiveViewModel ConfigLiveView
        {
            get
            {
                return _configLiveView;
            }
        }

        #endregion Properties

        #region Methods

        public void InitialConfig(string filePath)
        {
            _configLiveView = new EmLiveViewModel(filePath);
            _configLiveView.Load(ref _configLiveView);
            _configLiveView.Save();
        }

        public bool Open()
        {
            foreach (var item in IPCamera)
            {
                if (item.OpenDevice() != true)
                {
                    Logger.Error(item + "Open Error");

                    return false;
                }//else
            }

            return true;
        }

        private void Initialize(int cameracount)
        {
            InitialConfig(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)) + @"\Config\EmLiveViewModel.json");

            if (_configLiveView.CameraIP.Count == 0)
            {
                _configLiveView.CameraIP.Add("192.168.150.201");
                _configLiveView.CameraIP.Add("192.168.150.202");
                _configLiveView.Save();
            }//else

            if (_configLiveView.CameraIP.Count < cameracount)
            {
                Logger.Error("Please Check The Config of CameraIP");
            }
            else
            {
                for (int i = 0; i < cameracount; i++)
                {
                    IPCamera.Add(new EmLiveViewUserControl(_configLiveView.CameraIP[i]));
                }
            }
        }

        #endregion Methods
    }
}