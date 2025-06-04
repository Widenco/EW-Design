using EWDesign.Components.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EWDesign.Components.Views
{
    /// <summary>
    /// Interaction logic for TextView.xaml
    /// </summary>
    public partial class TextView : UserControl
    {
        public TextComponent Model { get; }
        public TextView(TextComponent model)
        {
            InitializeComponent();
            Model = model;
            this.DataContext = Model;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Model.IsEditing = false;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Escape)
            {
                Model.IsEditing = false;
            }
        }

        private void TextBlock_Click(object sender, MouseButtonEventArgs e)
        {
            Model.IsEditing = true;
        }
    }
}
