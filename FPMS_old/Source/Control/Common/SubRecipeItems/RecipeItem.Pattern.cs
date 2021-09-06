namespace FPMS.E2105_FS111_121
{
    public partial class RecipeItem
    {
        #region Enums

        public enum EColor
        {
            W,
            R,
            G,
            B,
        }

        #endregion Enums

        #region Methods

        [IsItem(true)]
        [IsComboBox("Color", typeof(EColor))]
        public bool Pattern_CoolorLevel(EColor Color, int Level)
        {
            //Global.Global.DrawColorDisplay(Color.ToString(), Level);
            return true;
        }

        [IsItem(true)]
        public bool Pattern_FileName(string FileName)
        {
            //Global.Global.DrawFromImgFile(FileName);
            return true;
        }

        [IsItem(true)]
        public bool Pattern_RGBLevel(int Red, int Green, int Blue)
        {
            //Global.Global.DrawColorDisplay(Red, Green, Blue);
            return true;
        }

        #endregion Methods
    }
}