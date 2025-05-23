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
        private string _title = "Mi Producto";
        public string Title 
        { 
            get => _title; 
            set => SetProperty(ref _title, value); 
        }
        private string _backgroundColor = "#f5f7fa";
        public string BackgroundColor 
        { 
            get => _backgroundColor; 
            set => SetProperty(ref _backgroundColor, value);
        }

        private bool _isEditing;
        public bool IsEditing
        {
            get => _isEditing;
            set => SetProperty(ref _isEditing, value);
        }

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

        public void Prueba()
        {
            Console.WriteLine("Click");
        }

    }
}

