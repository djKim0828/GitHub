using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AppWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        public IntPtr _memory;

        private ImageBrush _loadImagebrush;

        private string _memoryKeyName = "MMF_IMG";

        private uint _memorySize = 2000 * 2000;

        #endregion Fields

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        public BitmapSource ConvertImage(int width, int height, Byte[] imageBytes)
        {
            BitmapSource bmps = BitmapSource.Create(width, height, 96, 96,
                System.Windows.Media.PixelFormats.Gray8, null, imageBytes, width);

            return bmps;
        }

        public bool LoadImage(byte[] imageBuffer)
        {
            try
            {
                BitmapSource bitmapSource = ConvertImage(2000, 2000, imageBuffer);

                _loadImagebrush = new ImageBrush();
                _loadImagebrush.ImageSource = bitmapSource;
                _loadImagebrush.Stretch = Stretch.Fill;

                this.grdImage.Background = _loadImagebrush;

                return true;
            }
            catch (System.Exception ex)
            {
                WriteLog(ex.ToString());
                return false;
            }
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WndProc);
        }

        private void btnKill_Click(object sender, RoutedEventArgs e)
        {
            if (Func.KillProcess("CubiControl") == true)
            {
                WriteLog("Complete Run Command");
            }
            else
            {
                WriteLog("Failed Run Command");
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            _memory = new IntPtr();

            WriteLog("Create");

            Func.CreateMemory(ref _memory, _memoryKeyName, _memorySize);

            WriteLog("MemoryAddress : " + _memory.ToString());

            string args = "";

            IntPtr mainWindowHandle = new WindowInteropHelper(this).Handle;

            if (Func.RunCubiControl(args, mainWindowHandle, _memoryKeyName, txtPixelValue.Text) == true)
            {
                WriteLog("Complete Run Command");
            }
            else
            {
                WriteLog("Failed Run Command");
            }
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            // Handle messages...
            switch (msg)
            {
                case MessageMngr.WM_CUBIEVENT1:
                    WriteLog("Receive " + msg.ToString());
                    WriteLog("Receive w=" + wParam.ToString());

                    WriteLog("Receive l=" + lParam.ToString());

                    IntPtr hMemorySrc;
                    IntPtr pMemorySrc;
                    UIntPtr uBytes = new UIntPtr(_memorySize);

                    hMemorySrc = Func.OpenFileMapping((uint)Func.FileMapAccess.FileMapAllAccess, true, _memoryKeyName);
                    pMemorySrc = Func.MapViewOfFile(hMemorySrc, Func.FileMapAccess.FileMapAllAccess, 0, 0, uBytes);

                    byte[] managedArray = new byte[_memorySize];

                    Marshal.Copy(pMemorySrc, managedArray, 0, (int)_memorySize);

                    WriteLog(managedArray[0].ToString());

                    LoadImage(managedArray);

                    Func.UnmapViewOfFile(pMemorySrc);
                    Func.CloseHandle(hMemorySrc);

                    break;
            }

            return IntPtr.Zero;
        }

        private void WriteLog(string message)
        {
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            txtResult.AppendText(now + " : " + message + "\n");
            txtResult.ScrollToEnd();
        }

        #endregion Methods
    }
}