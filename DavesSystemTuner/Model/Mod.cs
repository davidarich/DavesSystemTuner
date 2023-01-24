using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavesSystemTuner.Model
{

    // Mod is the base class to represent a modification to the Windows OS
    internal abstract class Mod : IMod
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool IsApplied { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsRelogRequired { get; set; }

        public Mod()
        {
            Name = string.Empty;
            IsApplied = false;
            IsEnabled = true;
            Description = string.Empty;
            Category = string.Empty;
        }
    }
}
