using EWDesign.Components.Views;
using EWDesign.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EWDesign.Components.Models
{
    public class NavBarComponent : ComponentModel
    {
        public string Title { get; set; } = "Mi Producto";
        public string BackgroundColor { get; set; } = "#f5f7fa";

        public ObservableCollection<string> NavbarElements { get; set; }

        public string NavBarElementsColor { get; set; } = "#3a3f47";

        public NavBarComponent() 
        { 
            Type = "NavBar"; 
            NavbarElements = new ObservableCollection<string>
            {
                "Inicio",
                "Características",
                "Precios",
                "Contacto"
            };
        }

        public override UserControl GetView()
        {
            return new NavBarView(this);
        }

    }
}

