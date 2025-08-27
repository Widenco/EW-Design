using EWDesign.Core;
using EWDesign.Interfaces;
using EWDesign.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EWDesign.Components.Models
{
    public class IconComponent: ComponentModel, ICodeGeneratable
    {
        private double _width = 40;
        public double Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
        }
        private double _height = 40;
        public double Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }

        private Brush _accentColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6C63FF"));
        [EditableProperty("Accent Color")]
        public Brush AccentColor
        {
            get => _accentColor;
            set => SetProperty(ref _accentColor, value);
        }

        private string _iconText = "🐦";
        [EditableProperty("Icon")]
        public string IconText
        {
            get => _iconText;
            set => SetProperty(ref _iconText, value);
        }

        private bool _isEditing;
        public bool IsEditing
        {
            get => _isEditing;
            set => SetProperty(ref _isEditing, value);
        }

        public override void SetSelected(bool s)
        {
            base.SetSelected(s);

            if (!s)
            {
                IsEditing = false;
            }
        }

        public string HTMLContent(string className)
        {
            return $"<a href=\"#\" class='{className}'>{IconText}</a>";
        }

        public string CSSContent(string className)
        {
            return $".{className} {{\r\n" +
                "  display: flex;\r\n" +
                "  align-items: center;\r\n" +
                "  justify-content: center;\r\n" +
                "  width: 40px;\r\n" +
                "  height: 40px;\r\n" +
                $"  background: {BrushToHexRGB(AccentColor)};\r\n" +
                "  border-radius: 8px;\r\n" +
                "  color: white;\r\n" +
                "  text-decoration: none;\r\n" +
                "  font-size: 18px;\r\n" +
                "  transition: all 0.3s ease;\r\n" +
                "  cursor: pointer;\r\n}\r\n";
        }

        public IconComponent()
        {
            Type = "Icon";
            DelegateContextMenu = false;
        }
    }
}
