using EWDesign.Components.Views;
using EWDesign.Model;
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
    public class ButtonComponent : ComponentModel
    {
        private TextComponent _textContent;
        public TextComponent TextContent
        {
            get => _textContent;
            set => SetProperty(ref _textContent, value);
        }

        private double _width = 200;
        public double Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
        }

        private double _heigth = 40;
        public double Height
        {
            get => _heigth;
            set => SetProperty(ref _heigth, value);
        }

        private TextAlignment _horizontalAlignment = TextAlignment.Center;
        public TextAlignment HorizontalAlignment
        {
            get => _horizontalAlignment;
            set => SetProperty(ref _horizontalAlignment, value);
        }

        private string _background = "#6C63FF";
        public string Background
        {
            get => _background;
            set => SetProperty(ref _background, value);
        }

        private double _fontSize = 18;
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

        private string _foreground = "White";
        public string Foreground
        {
            get => _foreground;
            set => SetProperty(ref _foreground, value);
        }

        public ButtonComponent()
        {
            Type = "Button";
            _textContent = new TextComponent
            {
                Text = "Sample Text",
                FontSize = _fontSize,
                FontWeight = _fontWeigh,
                ForeGround = _foreground,
                TextAlignment = HorizontalAlignment
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
                TextAlignment = HorizontalAlignment
            };
        }
    }
}
