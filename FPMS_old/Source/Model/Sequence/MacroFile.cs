using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPMS.E2105_FS111_121
{
    public class MacroFile
    {
        public bool _todoCheck;
        public string _macroName;
        //public List<MacroMethod> _macro = new List<MacroMethod>();

        public MacroFile(string macroFileName)
        {
            _macroName = macroFileName;
            //_macro = MacroManager.LoadMacroFile(FilePath);
        }
    }
}
