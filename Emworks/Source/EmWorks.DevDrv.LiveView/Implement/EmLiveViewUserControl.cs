using EmWorks.Lib.Common;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace EmWorks.DevDrv.LiveView
{
    /// <summary>
    /// Copyright @ ENC Technology Co.,Ltd.
    /// Class Name : <see cref="EmLiveViewUserControl"/>
    /// Author : Andrew, Yoon
    /// Description : object detail description.
    /// </summary>
    public class EmLiveViewUserControl : EmLiveViewControl
    {
        #region Fields

        private bool _bisopen;
        private string _ipadress = string.Empty;
        private bool _isLoop;
        private int _locale;
        private int _loopInterval;
        private string[] _mediaOptions;
        private DirectoryInfo _vlcLibDirectory;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Author : Andrew, Yoon
        /// Description :  objcet constructor.
        /// </summary>
        public EmLiveViewUserControl(string ipadress)
        {
            _ipadress = ipadress;
            Initialize();
        }

        #endregion Constructors

        #region Destructors

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description :  object destructor.
        /// </summary>
        ~EmLiveViewUserControl()
        {
            _isLoop = false;

            if (_bisopen == true)
            {
                CloseDevice();
            }//else
        }

        #endregion Destructors

        #region Properties

        public bool IsInitialled { get; protected set; }

        #endregion Properties

        #region Methods

        public bool CloseDevice()
        {
            if (this.vlcPlayer.SourceProvider.MediaPlayer == null)
            {
                return true;
            }//else

            if (_bisopen == true)
            {
                try
                {
                    this.vlcPlayer.SourceProvider.MediaPlayer.Stop();
                }
                catch (Exception ex)
                {
                    Logger.Exception(ex);
                }
            }//else

            if (this.vlcPlayer.SourceProvider.MediaPlayer.IsPlaying())
            {
                return false;
            }//else

            _bisopen = false;

            return true;
        }

        public bool OpenDevice()
        {
            if (_bisopen == true)
            {
                return true;
            }//else

            this.vlcPlayer.SourceProvider.MediaPlayer.SetMedia(new Uri("rtsp://admin:enc7000123@" + _ipadress + ":554"), _mediaOptions);

            this.vlcPlayer.SourceProvider.MediaPlayer.Play();

            Task.Delay(500);

            _bisopen = true;

            return true;
        }

        public bool SetIp_adr()
        {
            if (_bisopen == true)
            {
                CloseDevice();
            }//else

            return true;
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description : EmWorks base thread.
        /// </summary>
        private void EmWorksProc(object state)
        {
            while (_isLoop)
            {
                Thread.Sleep(_loopInterval);
            }
        }

        /// <summary>
        /// Author : Andrew, Yoon
        /// Description : controls initialization.
        /// </summary>
        private bool InitControls()
        {
            this.vlcPlayer.SourceProvider.CreatePlayer(this._vlcLibDirectory);

            return true;
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description :  object initialization.
        /// </summary>
        private void Initialize()
        {
            var resultInstance = InitInstance();
            var resultControls = InitControls();
            var resultEvent = RegistEvents();

            if (resultInstance == true &&
                resultControls == true &&
                resultEvent == true)
            {
                IsInitialled = true;
                ThreadPool.QueueUserWorkItem(EmWorksProc);
            }
            else
            {
                IsInitialled = false;
            }
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description :  Instance initialization.
        /// </summary>
        private bool InitInstance()
        {
            IsInitialled = false;
            _loopInterval = 5;
            _isLoop = false;

            _vlcLibDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\libvlc\win-x64");
            _mediaOptions = new string[] { ":network-caching=200" };

            return true;
        }

        /// <summary>
        /// Author :  Andrew, Yoon
        /// Description : regist events
        /// </summary>
        private bool RegistEvents()
        {
            return true;
        }

        #endregion Methods
    }
}