using EWDesign.Components.Views;
using EWDesign.Core;
using EWDesign.Model;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.TextFormatting;

namespace EWDesign.Components.Models
{
    public class TextComponent : ComponentModel
    {

        private string _text = "Sample Text";

        [EditableProperty("Text")]
        public string Text 
        { 
            get => _text; 
            set => SetProperty(ref _text, value);
        }

        private string _foreground = "#2a2e35";

        [EditableProperty("Text Color")]
        public string ForeGround
        {
            get => _foreground;
            set => SetProperty(ref _foreground, value);
        }

        private double _fontSize = 24;

        [EditableProperty("Font Size")]
        public double FontSize
        {
            get => _fontSize;
            set => SetProperty(ref _fontSize, value);
        }

        private FontWeight _fontWeight = FontWeights.Black;

        [EditableProperty("Font Weight")]
        public FontWeight FontWeight
        {
            get => _fontWeight;
            set => SetProperty(ref _fontWeight, value);
        }

        private Thickness _margin = new Thickness(20,0,0,0);
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
