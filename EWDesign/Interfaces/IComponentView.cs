using EWDesign.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWDesign.Interfaces
{
    public interface IComponentView
    {
        ComponentModel Model { get; }
        event EventHandler ComponentRemoveEvent;
    }
}
