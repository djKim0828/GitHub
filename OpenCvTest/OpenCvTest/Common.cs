using OpenCvSharp.CPlusPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenCvTest
{
    public class Spot
    {
        #region Fields

        public Point endPoints;
        public Mat img;
        public Point startPoints;
        public PictureBox pictureBox;
        public Label resultLable;
        public bool isSearch = false;
        public bool isInspection = false;
        public List<Vec3b> ptList;
        #endregion Fields
    }
}
