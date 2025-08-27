using EWDesign.Components.Views;
using EWDesign.Core;
using EWDesign.Interfaces;
using EWDesign.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;

namespace EWDesign.Components.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TextComponent : ComponentModel, ICodeGeneratable
    {
        private string _text = "Sample Text";

        [EditableProperty("Text")]
        [JsonProperty]
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private Brush _foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2a2e35"));

        [EditableProperty("Text Color")]
        [JsonProperty]
        public Brush ForeGround
        {
            get => _foreground;
            set => SetProperty(ref _foreground, value);
        }

        private double _fontSize = 24;

        [EditableProperty("Font Size")]
        [JsonProperty]
        public double FontSize
        {
            get => _fontSize;
            set => SetProperty(ref _fontSize, value);
        }

        private FontWeight _fontWeight = FontWeights.Black;
        public FontWeight FontWeight
        {
            get => _fontWeight;
            set => SetProperty(ref _fontWeight, value);
        }

        private Thickness _margin = new Thickness(20, 0, 0, 0);
        public Thickness Margin
        {
            get => _margin;
            set => SetProperty(ref _margin, value);
        }

        private TextWrapping _textWrap = TextWrapping.Wrap;
        public TextWrapping TextWrap
        {
            get => _textWrap;
            set => SetProperty(ref _textWrap, value);
        }

        private TextAlignment _textAlignment = TextAlignment.Left;

        [EditableProperty("Text Alignment")]
        [JsonProperty]
        public TextAlignment TextAlignment
        {
            get => _textAlignment;
            set => SetProperty(ref _textAlignment, value);
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
            if (this.Type == "Navbar-Title-Text")
            {
                return $"<div class='{className}'>{Text}</div>";
            }
            else if (this.Type == "Footer-Title-Text")
            {
                return $"<div class='{className}'>{Text}</div>";
            }
            else if (this.Type == "Footer-Description-Text")
            {
                return $"<p class='{className}'>{Text}</p>";
            }
            else if (this.Type == "Title-Text")
            {
                return $"<h1 class='{className}'>{Text}</h1>";
            }
            else
            {
                return $"<p class=\"{className}\">{Text}</p>";
            }
        }
        public string CSSContent(string className)
        {

            if (this.Type == "Navbar-Title-Text" || this.Type == "Footer-Title-Text")
            {

                return $".{className} {{\r\n" +
                    $"  font-size: {FontSize}px;\r\n" +
                    "  font-weight: 600;\r\n" +
                    $"  color: {BrushToHexRGB(ForeGround)};\r\n}}\n";
            }
            else if (this.Type == "Footer-Description-Text")
            {
                return $".{className} {{\r\n" +
                    $"  font-size: {FontSize}px;\r\n" +
                    $"  color: {BrushToHexRGB(ForeGround)};\r\n" +
                    "  line-height: 1.6;\r\n" +
                    "  margin: 0;\r\n}\r\n";
            }
            else if (this.Type == "Title-Text")
            {
                return $".{className} {{\r\n" +
                    $"  font-size: {FontSize}px;\r\n" +
                    "  font-weight: 700;\r\n" +
                    $"  color: {BrushToHexRGB(ForeGround)};\r\n" +
                    "  line-height: 1.2;\r\n" +
                    $"  margin: {Margin};\r\n}}";
            }
            else
            {
                return $".{className} {{\r\n" +
                    $"  font-size: {FontSize}px;\r\n" +
                    $"  color: {BrushToHexRGB(ForeGround)};\r\n" +
                    $"  margin-bottom: {Margin};\r\n}}";
            }
        }

        public TextComponent()
        {
            Type = "Text";
        }

        public TextComponent(bool c)
        {
            Type = "Text";
            DelegateContextMenu = c;
        }

    }
}
