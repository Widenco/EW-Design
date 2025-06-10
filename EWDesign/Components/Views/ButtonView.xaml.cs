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
    /// Interaction logic for ButtonView.xaml
    /// </summary>
    public partial class ButtonView : UserControl
    {
        public ButtonComponent Model { get; }
        public ButtonView(ButtonComponent model)
        {
            InitializeComponent();
            Model = model;
            this.DataContext = model;
            InitComponents();
        }

        public void InitComponents()
        {
            var Text = new TextView(Model.TextContent);
            ButtonText.Children.Add(Text);
        }
    }
}
