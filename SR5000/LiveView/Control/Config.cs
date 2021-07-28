using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveView
{
    public class Config
    {
        public string _RecipeFilePath;
        public string _AutoSpotSelectType;
        public string _AutoSpotWidth;
        public string _AutoSpotHeigth;
        public string _AutoSpotMin;
        public string _AutoSpotMax;
        public string _AutoSpotYth;

        public Config()
        {
            _RecipeFilePath = string.Empty;
            _AutoSpotSelectType = string.Empty;
            _AutoSpotWidth = _AutoSpotHeigth = string.Empty;
            _AutoSpotMin = _AutoSpotMax = string.Empty;
        }

        ~Config()
        {

        }        
    }
}
