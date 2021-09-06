using EmWorks.Lib.Common;
using HsApiNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmWorks.DevDrv.TopconSR5000
{
    public partial class EmTopconSR5000
    {

        public bool ParsingSpotData(List<SpotData> sdList, out List<SpotStatisticsData> dataList)
        {
            try
            {
                dataList = new List<SpotStatisticsData>();

                foreach (SpotData sd in sdList)
                {
                    SpotStatisticsData temp = new SpotStatisticsData();

                    temp.SpotNo = sd.spot_id;

                    temp.LocationX = sd.location.x;
                    temp.LocationY = sd.location.y;

                    temp.R = sd.statistics[(int)StatisticsIndex.R].average;
                    temp.G = sd.statistics[(int)StatisticsIndex.G].average;
                    temp.B = sd.statistics[(int)StatisticsIndex.B].average;

                    temp.CIE_U = sd.statistics[(int)StatisticsIndex.CIE_u_].average;
                    temp.CIE_V = sd.statistics[(int)StatisticsIndex.CIE_v_].average;
                    temp.CIE_X = sd.statistics[(int)StatisticsIndex.CIE_x].average;
                    temp.CIE_Y = sd.statistics[(int)StatisticsIndex.CIE_y].average;
                    temp.Dominant_Wavelength = sd.statistics[(int)StatisticsIndex.Dominant_Wavelength].average;
                    temp.Trsistimuls_X = sd.statistics[(int)StatisticsIndex.TrsistimulsX].average;
                    temp.Trsistimuls_Y_Luminance = sd.statistics[(int)StatisticsIndex.TrsistimulsY_Luminance].average;
                    temp.Trsistimuls_Z = sd.statistics[(int)StatisticsIndex.TrsistimulsZ].average;

                    temp.Spectral = new List<double>();
                    foreach (Statistics data in sd.spectral_statistics)
                    {
                        temp.Spectral.Add(data.average);
                    }

                    dataList.Add(temp);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                Logger.Exception(ex);

                dataList = null;
                return false;
            }
        }

        public enum StatisticsIndex
        {
            TrsistimulsX = 0,
            TrsistimulsY_Luminance,
            TrsistimulsZ,
            CIE_x,
            CIE_y,
            CIE_u_,
            CIE_v_,
            CCT,
            Deviation,
            Dominant_Wavelength,
            ExcitationPurityValue,
            LAB_L, //L_Indicateds_Lightness,
            LAB_a, //a_red_grren_coordinate,
            LAB_b, //b_yellow_blue_Coordinate,
            Choroma,
            Hue,
            R,
            G,
            B,
            LocationX,
            LocationY,
        }

        public class SpotStatisticsData
        {
            #region Properties

            public int SpotNo { get; set; }
            public double CIE_U { get; set; }
            public double CIE_V { get; set; }
            public double CIE_X { get; set; }
            public double CIE_Y { get; set; }
            public double Dominant_Wavelength { get; set; }
            public double G { get; set; }
            public double R { get; set; }
            public double B { get; set; }
            public double LUV { get; set; }
            public List<double> Spectral { get; set; }
            public double Trsistimuls_X { get; set; }
            public double Trsistimuls_Y_Luminance { get; set; }
            public double Trsistimuls_Z { get; set; }

            public int LocationX { get; set; }
            public int LocationY { get; set; }

            #endregion Properties
        }


    }
}
