using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavesSystemTuner.Model
{
    // IMod is an interface for modifications to the Windows OS
    internal interface IMod
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public bool IsApplied { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsRelogRequired { get; set; }
    }
}
