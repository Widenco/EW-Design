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
        private string _backgroundColor = "#f5f7fa";
        public string BackgroundColor 
        { 
            get => _backgroundColor; 
            set => SetProperty(ref _backgroundColor, value);
        }
        public ObservableCollection<string> NavbarElementsText { get; set; }

        public string NavBarElementsColor { get; set; } = "#3a3f47";

        public NavBarComponent() 
        { 
            Type = "NavBar"; 
            NavbarElementsText = new ObservableCollection<string>
            {
                "Inicio",
                "Características",
                "Precios",
                "Contacto"
            };
            
        }

    }
}

