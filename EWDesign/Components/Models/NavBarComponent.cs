using EWDesign.Components.Views;
using EWDesign.Core;
using EWDesign.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace EWDesign.Components.Models
{
    public class NavBarComponent : ComponentModel
    {
        private TextComponent _title;
        public TextComponent Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private MenuComponent _menu;

        public MenuComponent Menu
        {
            get => _menu;
            set => SetProperty(ref _menu, value);
        }

        private Brush _backgroundColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f5f7fa"));

        [EditableProperty("Background Color")]
        public Brush BackgroundColor 
        { 
            get => _backgroundColor; 
            set => SetProperty(ref _backgroundColor, value);
        }
        public ObservableCollection<string> NavbarElementsText { get; set; }

        public string NavBarElementsColor { get; set; } = "#3a3f47";

        public NavBarComponent() 
        { 
            Type = "NavBar";
            Title = new TextComponent
            {
                Type = "Navbar-Title-Text",
                Text = "Mi Producto",
                DelegateContextMenu = false

            };

            Menu = new MenuComponent { DelegateContextMenu = false };

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

