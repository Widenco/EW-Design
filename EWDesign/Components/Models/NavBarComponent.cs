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
        public TextComponent TextModel { get; set; }

        private string _backgroundColor = "#f5f7fa";
        public string BackgroundColor 
        { 
            get => _backgroundColor; 
            set => SetProperty(ref _backgroundColor, value);
        }
        public ObservableCollection<TextComponent> NavbarElements { get; set; }

        public string NavBarElementsColor { get; set; } = "#3a3f47";

        public NavBarComponent() 
        { 
            Type = "NavBar"; 
            NavbarElements = new ObservableCollection<TextComponent>
            {
                new TextComponent{ Text = "Inicio" },
                new TextComponent { Text = "Características" },
                new TextComponent{ Text = "Precios" },
                new TextComponent { Text = "Contacto" }
            };
            TextModel = new TextComponent();
        }

    }
}

