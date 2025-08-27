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
        public ObservableCollection<ComponentModel> DescriptionSection { get; set; }
        public ObservableCollection<ComponentModel> LinksSection { get; set; }
        public ObservableCollection<ComponentModel> ContactSection { get; set; }
        public ObservableCollection<ComponentModel> IconsSection { get; set; }
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

            DescriptionSection = new ObservableCollection<ComponentModel>();
            LinksSection = new ObservableCollection<ComponentModel>();
            ContactSection = new ObservableCollection<ComponentModel>();
            IconsSection = new ObservableCollection<ComponentModel>();
        }
    }

}
