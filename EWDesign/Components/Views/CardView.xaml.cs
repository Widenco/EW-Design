using EWDesign.Components.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for CardView.xaml
    /// </summary>
    public partial class CardView : UserControl
    {
        public CardComponent Model { get; }
        public CardView(CardComponent model)
        {
            InitializeComponent();
            Model = model;
            this.DataContext = model;
            InitComponents();
        }

        public void InitComponents()
        {
            var CardComponents = new ObservableCollection<TextView>
            {
                new TextView(Model.Title),
                new TextView(Model.Body)
            };

            foreach (var item in CardComponents)
            {
                Card.Children.Add(item);
            }
        }
    }
}
