using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp1.Core;

namespace EWDesign.Model
{
    public abstract class ComponentModel : ObservableObject
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Type { get; set; }
    }
}
