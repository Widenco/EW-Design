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

        private bool _isEditing;
        public bool IsEditing
        {
            get => _isEditing;
            set => SetProperty(ref _isEditing, value);
        }

        public override UserControl GetView()
        {
            return new TextView(this);
        }
    }
}
