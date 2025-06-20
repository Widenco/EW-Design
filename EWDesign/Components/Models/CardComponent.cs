using EWDesign.Core;
using EWDesign.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EWDesign.Components.Models
{
    public class CardComponent : ComponentModel
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

        private string _background = "#2A2A40";

        [EditableProperty("Card Background")]
        public string Background
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

        public CardComponent()
        {
            Type = "Card";
            _title = new TextComponent
            {
                Text = "Sample Title",
                TextWrap = TextWrapping.NoWrap,
                Margin = new Thickness(0, 0, 0, 12),
                FontSize = 22,
                FontWeight = FontWeights.SemiBold,
                ForeGround = "White",
                DelegateContextMenu = true

            };
            _body = new TextComponent
            {
                Text = "This is a sample body text, here is where your text goes...",
                Margin = new Thickness(0,0,0,0),
                FontSize = 16,
                ForeGround = "#DADADA",
                TextWrap = TextWrapping.Wrap,
                DelegateContextMenu = true
            };
        }
        public CardComponent(string title, string body)
        {
            Type = "Card";
            _title = new TextComponent
            {
                Text = title,
                TextWrap = TextWrapping.NoWrap,
                Margin = new Thickness(0, 0, 0, 12),
                FontSize = 22,
                FontWeight = FontWeights.SemiBold,
                ForeGround = "White",
                DelegateContextMenu = true

            };
            _body = new TextComponent
            {
                Text = body,
                Margin = new Thickness(0, 0, 0, 0),
                FontSize = 16,
                ForeGround = "#DADADA",
                TextWrap = TextWrapping.Wrap,
                DelegateContextMenu = true
            };
        }
    }
}
