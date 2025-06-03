using EWDesign.Components.Views;
using EWDesign.Model;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EWDesign.Components.Models
{
    public class TextComponent : ComponentModel
    {
        private string _text = "Mi Producto";
        public string Text 
        { 
            get => _text; 
            set => SetProperty(ref _text, value);
        }

        private string _foreground = "#2a2e35";
        public string ForeGround
        {
            get => _foreground;
            set => SetProperty(ref _foreground, value);
        }

        private string _fontSize = "24";

        public string FontSize
        {
            get => _fontSize;
            set => SetProperty(ref _fontSize, value);
        }

        private string[] _margin = { "24", "0", "0", "0" };
        public string[] Margin
        {
            get => _margin;
            set => SetProperty(ref _margin, value);
        }
        private bool _isEditing;
        public bool IsEditing
        {
            get => _isEditing;
            set => SetProperty(ref _isEditing, value);
        }

    }
}
