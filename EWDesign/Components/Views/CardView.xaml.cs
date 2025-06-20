using EWDesign.Components.Models;
using EWDesign.Interfaces;
using EWDesign.Model;
using EWDesign.View;
using EWDesign.ViewModel;
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
    public partial class CardView : UserControl, IComponentView
    {
        public CardComponent CardModel { get; }

        public ComponentModel Model => CardModel;

        public event EventHandler ComponentRemoveEvent;

        public CardView(CardComponent model)
        {
            InitializeComponent();
            CardModel = model;
            this.DataContext = model;
            InitComponents();
        }

        public void InitComponents()
        {
            var CardComponents = new ObservableCollection<TextView>
            {
                new TextView(CardModel.Title),
                new TextView(CardModel.Body)
            };

            foreach (var item in CardComponents)
            {
                Card.Children.Add(item);
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            OpenComponentEditor(this.Model);
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            ComponentRemoveEvent?.Invoke(this, EventArgs.Empty);
        }

        private void OpenComponentEditor(ComponentModel model)
        {
            var dialog = new ComponentEditorDialog(model);
            dialog.Owner = Window.GetWindow(this);
            dialog.ShowDialog();
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BuilderViewModel.Instance.SelectedComponent = this.Model;
            e.Handled = true;
        }
    }
}
