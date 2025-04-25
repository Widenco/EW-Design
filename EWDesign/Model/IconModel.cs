using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EWDesign.Model
{
    class IconModel
    {
        public string IconPath { get; set; }
        public string Text { get; set; }
        public string Tooltip { get; set; }
        public ICommand Command { get; set; }
    }
}
