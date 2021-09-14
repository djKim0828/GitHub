using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basler.Pylon;
using System.Threading;

namespace PreCameraTestor
{
    public partial class Form1 : Form
    {
        private bool _isOpen = false;

        private Camera _camera;

        public Form1()
        {
            InitializeComponent();

            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenCamera();
        }

        private bool OpenCamera()
        {            
            try
            {
                if (_isOpen == true)
                {
                    return true;
                } // else

                if(_camera == null)
                {
                    _camera = new Camera();

                    // Print the model name of the camera.
                    WriteLog("Using camera : " + _camera.CameraInfo[CameraInfoKey.ModelName].ToString());

                    // Set the acquisition mode to free running continuous acquisition when the camera is opened.
                    _camera.CameraOpened += Configuration.AcquireContinuous;

                    // For demonstration purposes, only add an event handler for connection loss.
                    _camera.ConnectionLost += OnConnectionLost;
                } //else
                
                // Open the connection to the camera device.
                _camera.Open();

                WriteLog("Complete Open camera");

                ChangeStatus(true);

                return true;
            }
            catch (Exception e)
            {
                WriteLog("Exception : "+ e.Message);
                return false;
            }            
        }

        private void OnConnectionLost(Object sender, EventArgs e)
        {            
            WriteLog("OnConnectionLost has been called.");
            ChangeStatus(false);
        }

        private void WriteLog(string message)
        {
            rcbOutput.Invoke(new MethodInvoker(() =>
            {
                String nowTime = "[" + System.DateTime.Now.ToString() + "] ";

                rcbOutput.AppendText(nowTime + message + "\r\n");
                rcbOutput.SelectionStart = rcbOutput.Text.Length;
                rcbOutput.ScrollToCaret();
            }));
        }

        private void ChangeStatus(bool isOpen)
        {
            _isOpen = isOpen;

            pnlStatus.Invoke(new MethodInvoker(() =>
            {
                if (isOpen == true)
                {
                    pnlStatus.BackColor = Color.Green;
                }
                else
                {
                    pnlStatus.BackColor = Color.Red;
                }                
            }));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseCamera();
        }

        private void CloseCamera()
        {
            if (_isOpen != true)
            {
                return;
            }// else

            _camera.Close();

            ChangeStatus(false);

            WriteLog("Complete close camera");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseCamera();
        }

        bool _isGrabStop = false;

        private void startGrabProc(object tag)
        {
            try
            {
                int width = Convert.ToInt16(txtWidth.Text);
                int height = Convert.ToInt16(txtHeight.Text);

                ///////////////// Don't single step beyond this line when using GigE cameras (see comments above) ///////////////////////////////
                // Before testing the callbacks, we manually set the heartbeat timeout to a short value when using GigE cameras.
                // For debug versions, the heartbeat timeout has been set to 5 minutes, so it would take up to 5 minutes
                // until device removal is detected.
                _camera.Parameters[PLTransportLayer.HeartbeatTimeout].TrySetValue(1000, IntegerValueCorrection.Nearest);  // 1000 ms timeout
                _camera.Parameters[PLCamera.Width].SetValue(width, IntegerValueCorrection.Nearest);
                _camera.Parameters[PLCamera.Height].SetValue(height, IntegerValueCorrection.Nearest);

                // Start the grabbing.
                _camera.StreamGrabber.Start();

                while(_isGrabStop == false)
                {
                    // Wait for an image and then retrieve it. A timeout of 10000 ms is used.
                    IGrabResult grabResult = _camera.StreamGrabber.RetrieveResult(10000, TimeoutHandling.ThrowException);
                    using (grabResult)
                    {
                        // Image grabbed successfully?
                        if (grabResult.GrabSucceeded)
                        {
                            byte[] buffer = grabResult.PixelData as byte[];
                            Bitmap temp = ImageMngr.BytTo8bitBitmap(buffer, width, height);
                            pictureBox.Image = temp;

                            // Display the grabbed image.
                            //ImageWindow.DisplayImage(0, grabResult);
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
            finally
            {
                _camera.StreamGrabber.Stop();
            }
        }

        Thread grabthd;

        private void btnStart_Click(object sender, EventArgs e)
        {
            grabthd = new Thread(startGrabProc);
            _isGrabStop = false;
            grabthd.Start();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                WriteLog(pictureBox.Image.PixelFormat.ToString());
            }
            
        }

        private void btnGrab_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                pictureBox.Image.Save(Application.StartupPath + @"1.bmp");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            //openFileDialog.InitialDirectory = OutputPath;
            openFileDialog.Filter = "Bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            string filePath = string.Empty;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Get the path of specified file
                filePath = openFileDialog.FileName;

                pictureBox.Image = Image.FromFile(filePath);
            }
            else
            {
                return;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _isGrabStop = true;
        }
    }
}
