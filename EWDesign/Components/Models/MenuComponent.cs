using EWDesign.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using EWDesign.Core;
using System.Windows.Data;
using EWDesign.Interfaces;

namespace EWDesign.Components.Models
{
    public class MenuComponent: ComponentModel, ICodeGeneratable
    {
        private ObservableCollection<TextComponent> _menuItems;
        [EditableProperty("Menu Items Text")]
        public ObservableCollection<TextComponent> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }

        private readonly string[] _menuItemsText;

        private double _fontSize = 16;

        [EditableProperty("Font Size")]
        public double FontSize
        {
            get => _fontSize;
            set 
            { 
                SetProperty(ref _fontSize, value);
                SyncFontSizeToTextComponents();
            }
        }

        private Brush _foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3A3F47"));

        [EditableProperty("Text Color")]
        public Brush ForeGround
        {
            get => _foreground;
            set => SetProperty(ref _foreground, value);
        }

        private Thickness _margin = new Thickness(16, 0, 0, 0);
        public Thickness Margin
        {
            get => _margin;
            set => SetProperty(ref _margin, value);
        }

        public override void SetSelected(bool s)
        {
            base.SetSelected(s);

            if (!s)
            {
                if(_menuItems.Count > 0) 
                    foreach (var item in _menuItems)
                    {
                        item.IsEditing = s;
                    }
            }
        }

        private void SyncFontSizeToTextComponents()
        {
            foreach (var item in MenuItems)
            {
                item.FontSize = this.FontSize;
            }
        }

        public string HTMLContent()
        {
            var code = $"<ul class=\"{Type.ToLower()}\">\n";
            foreach(var item in _menuItems)
            {
                code += $"<li><a href=\"#{item.Text}\">{item.Text}</a></li>\r\n";
            }

            code += "</ul>";

            return code;
        }

        public string CSSContent()
        {
            return $".{Type.ToLower()} {{\r\n" +
                "  list-style: none;\r\n" +
                "  display: flex;\r\n" +
                "  gap: 32px;\r\n" +
                "  margin: 0;\r\n" +
                "  padding: 0;\r\n}" +
                $"\n.{Type.ToLower()} li a {{\r\n" +
                "  text-decoration: none;\r\n" +
                $"  color: #{ForeGround};\r\n" +
                "  font-weight: 500;\r\n" +
                $"  font-size: {FontSize}px;\r\n" +
                "  transition: color 0.3s ease;\r\n}";
        }

        public MenuComponent()
        {
            Type = "Menu";

            _menuItemsText = new string[] 
            {
                "Inicio",
                "Caracteristicas",
                "Precio",
                "Contacto"
            };

            _menuItems = new ObservableCollection<TextComponent>();

            foreach (var item in _menuItemsText)
            {
                var menuItem = new TextComponent
                {
                    Text = item,
                    Margin = Margin,
                    FontSize = FontSize,
                    ForeGround = ForeGround,
                    DelegateContextMenu = true
                };

                _menuItems.Add(menuItem);
            }  
        }
    }
}
