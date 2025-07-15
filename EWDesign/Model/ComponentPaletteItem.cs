using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWDesign.Model
{
    public class ComponentPaletteItem
    {
        public string DisplayName { get; set; }
        public string Category { get; set; } 
        public object ComponentFactory { get; set; } 
    }

}
