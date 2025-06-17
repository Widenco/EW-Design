using EWDesign.Components.Views;
using EWDesign.Core;
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

        [EditableProperty("Button Width")]
        public double Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
        }

        private double _heigth = 40;

        [EditableProperty("Button Height")]
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

        private string _background = "#6C63FF";

        [EditableProperty("Button Background Color")]
        public string Background
        {
            get => _background;
            set => SetProperty(ref _background, value);
        }

        private double _fontSize = 18;

        [EditableProperty("Text Font Size")]
        public double FontSize
        {
            get => _fontSize;
            set => SetProperty(ref _fontSize, value);
        }

        private FontWeight _fontWeigh = FontWeights.SemiBold;

        [EditableProperty("Text Font Weight")]
        public FontWeight FontWeight
        {
            get => _fontWeigh;
            set => SetProperty(ref _fontWeigh, value);
        }

        private string _foreground = "White";

        [EditableProperty("Text Color")]
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
