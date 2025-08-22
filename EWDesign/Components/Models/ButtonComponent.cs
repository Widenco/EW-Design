using EWDesign.Components.Views;
using EWDesign.Core;
using EWDesign.Interfaces;
using EWDesign.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EWDesign.Components.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ButtonComponent : ComponentModel, ICodeGeneratable
    {
        private TextComponent _textContent;
        public TextComponent TextContent
        {
            get => _textContent;
            set => SetProperty(ref _textContent, value);
        }

        private double _width = 200;

        [EditableProperty("Button Width")]
        [JsonProperty]
        public double Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
        }

        private double _heigth = 40;

        [EditableProperty("Button Height")]
        [JsonProperty]
        public double Height
        {
            get => _heigth;
            set => SetProperty(ref _heigth, value);
        }

        private HorizontalAlignment _horizontalAlignment = System.Windows.HorizontalAlignment.Center;

        public HorizontalAlignment HorizontalAlignment
        {
            get => _horizontalAlignment;
            set => SetProperty(ref _horizontalAlignment, value);
        }

        private Brush _background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6C63FF"));

        [EditableProperty("Button Background Color")]
        [JsonProperty]
        public Brush Background
        {
            get => _background;
            set => SetProperty(ref _background, value);
        }

        private double _fontSize = 18;

        [EditableProperty("Text Font Size")]
        [JsonProperty]
        public double FontSize
        {
            get => _fontSize;
            set => SetProperty(ref _fontSize, value);
        }

        private FontWeight _fontWeigh = FontWeights.SemiBold;
        public FontWeight FontWeight
        {
            get => _fontWeigh;
            set => SetProperty(ref _fontWeigh, value);
        }

        private Brush _foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("White"));
        public Brush Foreground
        {
            get => _foreground;
            set => SetProperty(ref _foreground, value);
        }

        public override void SetSelected(bool s)
        {
            base.SetSelected(s);

            if (!s)
            {
                TextContent.IsEditing = s;
            }
        }

        public string HTMLContent(string className)
        {
            return $"<button class='{className}'>{TextContent.Text}</button>";
        }
        public string CSSContent(string className)
        {
            return $".{className} {{\r\n" +
                $"  background-color: {BrushToHexRGB(Background)};\r\n" +
                $"  color: {BrushToHexRGB(Foreground)};\r\n" +
                $"  font-size: {FontSize}px;\r\n" +
                "  font-weight: 600;\r\n" +
                "  padding: 14px 32px;\r\n" +
                "  border: none;\r\n" +
                "  border-radius: 8px;\r\n" +
                "  cursor: pointer;\r\n" +
                "  transition: background-color 0.3s ease;\r\n}\n";
        }
        public ButtonComponent()
        {
            Type = "Button";
            _textContent = new TextComponent
            {
                Margin = new Thickness(0, 0, 0, 0),
                Text = "Sample Text",
                FontSize = _fontSize,
                FontWeight = _fontWeigh,
                ForeGround = _foreground,
                TextAlignment = TextAlignment.Center,
                DelegateContextMenu = true
            };
        }

        public ButtonComponent(string content)
        {
            Type = "Button";
            _textContent = new TextComponent
            {
                Margin = new Thickness(0,0,0,0),
                Text = content,
                FontSize = _fontSize,
                FontWeight = _fontWeigh,
                ForeGround = _foreground,
                TextAlignment = TextAlignment.Center,
                DelegateContextMenu = true
            };
        }
    }
}
