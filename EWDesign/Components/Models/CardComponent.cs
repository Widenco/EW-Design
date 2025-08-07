using EWDesign.Components.Views;
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

namespace EWDesign.Components.Models
{
    public class CardComponent : ComponentModel, ICodeGeneratable
    {
        private TextComponent _title;
        public TextComponent Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private TextComponent _body;
        public TextComponent Body
        {
            get => _body;
            set => SetProperty(ref _body, value);
        }

        private Brush _background = new SolidColorBrush((Color) ColorConverter.ConvertFromString("#2A2A40"));

        [EditableProperty("Card Background")]
        public Brush Background
        {
            get => _background;
            set => SetProperty(ref _background, value);
        }

        private double _width = 300;

        [EditableProperty("Card Width")]
        public double Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
        }

        private double _height = 170;

        [EditableProperty("Card Height")]
        public double Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }

        public override void SetSelected(bool s)
        {
            base.SetSelected(s);

            if (!s)
            {
                Title.IsEditing = s;
                Body.IsEditing = s;
            }
        }

        public string HTMLContent()
        {
            return $"<div class='feature-item'>" +
           $"<h2 class='feature-title'>{_title.Text}</h2>\n" +
           $"<p class='feature-text'>{_body.Text}</p>\n" +
           $"</div>";
        }
        public string CSSContent()
        {
            return ".feature-item {\n" +
                $"  background-color: {Background};\n" +
                "  padding: 32px;\n" +
                "  border-radius: 16px;\n" +
                $"  width: {Width};\n" +
                "  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.2);\n" +
                "  transition: transform 0.2s ease;\n}\n\n" +
                ".feature-item:hover {\n" +
                "  transform: translateY(-4px);\n}\n\n" +
                ".feature-title {\n" +
                $"  font-size: {_title.FontSize};\n" +
                $"  font-weight: {_title.FontWeight};\n" +
                $"  margin: {_title.Margin};\n" +
                $"  color: {_title.ForeGround};\n}}\n\n" +
                ".feature-text {\n" +
                $"  font-size: {_body.FontSize};\n" +
                $"  color: {_body.ForeGround};\n" +
                "  line-height: 1.5;\n}\n";
        }

        public CardComponent()
        {
            Type = "Card";
            _title = new TextComponent
            {
                Type = "Title Text",
                Text = "Sample Title",
                TextWrap = TextWrapping.NoWrap,
                Margin = new Thickness(0, 0, 0, 12),
                FontSize = 22,
                FontWeight = FontWeights.SemiBold,
                ForeGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("White")),
                DelegateContextMenu = true,
            };
            _body = new TextComponent
            {
                Type = "Body Text",
                Text = "This is a sample body text, here is where your text goes...",
                Margin = new Thickness(0,0,0,0),
                FontSize = 16,
                ForeGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DADADA")),
                TextWrap = TextWrapping.Wrap,
                DelegateContextMenu = true,
            };

            Children.Add(Title);
            Children.Add(Body);

        }
        public CardComponent(string title, string body)
        {
            Type = "Card";
            _title = new TextComponent
            {
                Type = "Title Text",
                Text = title,
                TextWrap = TextWrapping.NoWrap,
                Margin = new Thickness(0, 0, 0, 12),
                FontSize = 22,
                FontWeight = FontWeights.SemiBold,
                ForeGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("White")),
                DelegateContextMenu = true,
                

            };
         
            _body = new TextComponent
            {
                Type = "Body Text",
                Text = body,
                Margin = new Thickness(0, 0, 0, 0),
                FontSize = 16,
                ForeGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DADADA")),
                TextWrap = TextWrapping.Wrap,
                DelegateContextMenu = true,
                
            };

            Children.Add(Title);
            Children.Add(Body);
        }
       
    }
}
