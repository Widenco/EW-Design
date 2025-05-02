using EWDesign.Components.Views;
using EWDesign.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EWDesign.Components.Models
{
    public class NavBarComponent : ComponentModel
    {
        public string Title { get; set; } = "My NavBar";
        public string BackgroundColor { get; set; } = "#2C3E50";

        public NavBarComponent() { Type = "NavBar"; }

        public override UserControl GetView()
        {
            return new NavBarView(this);
        }
    }
}

