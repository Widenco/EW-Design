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
        public static readonly DependencyProperty ModelProperty =
        DependencyProperty.Register("Model", typeof(TextComponent), typeof(TextView),
            new PropertyMetadata(null, OnModelChanged));

        public TextComponent Model
        {
            get => (TextComponent)GetValue(ModelProperty);
            set => SetValue(ModelProperty, value);
        }
        public TextView()
        {
            InitializeComponent();
        }

        private static void OnModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (TextView)d;
            control.DataContext = e.NewValue;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if(Model != null)
                Model.IsEditing = false;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Escape)
            {
                if(Model != null)
                    Model.IsEditing = false;
            }
        }

        private void TextBlock_Click(object sender, MouseButtonEventArgs e)
        {
            if(Model != null)
                Model.IsEditing = true;
        }
    }
}
