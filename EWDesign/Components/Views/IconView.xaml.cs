using EWDesign.Components.Models;
using EWDesign.Interfaces;
using EWDesign.Model;
using EWDesign.View;
using EWDesign.ViewModel;
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
    /// Interaction logic for IconView.xaml
    /// </summary>
    public partial class IconView : UserControl, IComponentView
    {
        public IconComponent IconModel { get; }

        public ComponentModel Model => IconModel;

        public IconView(IconComponent model)
        {
            InitializeComponent();
            IconModel = model;
            this.DataContext = model;
        }

        public event EventHandler ComponentRemoveEvent;

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
