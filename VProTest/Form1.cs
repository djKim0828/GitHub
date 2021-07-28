using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.VisionPro;
using Cognex.VisionPro.ImageFile;
using Cognex.VisionPro.Exceptions;
using Cognex.VisionPro.ImageProcessing;

namespace VProTest
{
    public partial class Form1 : Form
    {
        CogImageFileTool mImageFileTool;

        public Form1()
        {
            InitializeComponent();

            mImageFileTool = new CogImageFileTool();

            tools = new CogIPOneImageTool();

            flip = new CogIPOneImageFlipRotate();

            tools.Operators.Add(flip);

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            

            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            //openFileDialog.InitialDirectory = ProjectMainDir() + @"\Config";
            openFileDialog.Filter = "Bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            string filePath = string.Empty;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Get the path of specified file
                filePath = openFileDialog.FileName;

                mImageFileTool.Operator.Open(filePath, CogImageFileModeConstants.Read);
                mImageFileTool.Run();

                tools.InputImage = (CogImage24PlanarColor)mImageFileTool.OutputImage;

                cogDisplay1.Image = tools.InputImage;
                
            }
        }

        private CogIPOneImageTool tools;
        private CogIPOneImageFlipRotate flip;


        private void btnRotate_Click(object sender, EventArgs e)
        {
            flip.OperationInPixelSpace = CogIPOneImageFlipRotateOperationConstants.Rotate90Deg;

            tools.Run();

            tools.InputImage = tools.OutputImage;
            cogDisplay1.Image = tools.OutputImage;

        }

        private void btnDrawLine_Click(object sender, EventArgs e)
        {
            //Cognex.VisionPro.CogCopyShapeConstants
            CogLine cogline = new CogLine();
            cogline.X = 100;
            cogline.Y = 100;
            //cogline.Rotation = 90;

            cogline.Dragging += Cogline_Dragging;
            cogDisplay1.InteractiveGraphics.Add(cogline, "cl1", false);
            //cogDisplay1.Image = cogline;
        }

        private void Cogline_Dragging(object sender, CogDraggingEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CogLine cogline = new CogLine();
            cogline.X = 100;
            cogline.Y = 50;
            cogline.Rotation = 180;

            cogDisplay1.InteractiveGraphics.Add(cogline, "cl2", false);
        }
    }
}
