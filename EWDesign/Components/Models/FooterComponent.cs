using EWDesign.Core;
using EWDesign.Interfaces;
using EWDesign.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;

namespace EWDesign.Components.Models
{
    public class FooterComponent : ComponentModel
    {
        private MenuComponent _links;
        public MenuComponent Links 
        {
            get => _links;
            set => SetProperty(ref _links, value); 
        }

        private TextComponent _logo;
        public TextComponent Logo 
        {
            get => _logo;
            set => SetProperty(ref _logo, value);
        }

        private TextComponent _copyright;
        public TextComponent Copyright 
        { 
            get => _copyright; 
            set => SetProperty(ref _copyright, value); 
        }

        public Brush _background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2a2a40"));
        [EditableProperty("Footer Background")]
        public Brush Background 
        { 
            get => _background; 
            set => SetProperty(ref _background, value); 
        }

        private Brush _foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
        public Brush Foreground 
        { 
            get => _foreground;
            set => SetProperty(ref _foreground, value); 
        }
        public FooterComponent()
        {
            Type = "Footer";
            Links = new MenuComponent(_foreground, false);
            Links.Type = "Footer-Menu";

            Logo = new TextComponent
            {
                Type = "Footer-Title-Text",
                Text = "MiProducto",
                FontSize = 24,
                ForeGround = Foreground,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0,0,0,12)
            };
            

            Copyright = new TextComponent
            {
                Type = "Copyright-Text",
                Text = "© 2025 MiProducto. Todos los derechos reservados.",
                FontSize = 14,
                ForeGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#888")),
                Margin = new Thickness(0,24,0,0)
            };

            Children.Add(Logo);
            Children.Add(Links);
            Children.Add(Copyright);
        }
    }

}
