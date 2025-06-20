using EWDesign.Components.Models;
using EWDesign.Core;
using EWDesign.Interfaces;
using EWDesign.Model;
using EWDesign.View;
using EWDesign.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class TextView : UserControl, IComponentView
    {
        public TextComponent TextModel { get; }

        public ComponentModel Model => TextModel;

        public event EventHandler ComponentRemoveEvent;
        public TextView(TextComponent model)
        {
            InitializeComponent();
            TextModel = model;
            this.DataContext = TextModel;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextModel.IsEditing = false;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Escape)
            {
                TextModel.IsEditing = false;
            }
        }

        private void TextBlock_Click(object sender, MouseButtonEventArgs e)
        {
            TextModel.IsEditing = true;
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

        private void UserControl_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            ContextMenuHelper.ShowParentContextMenu(this, Model);
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!TextModel.DelegateContextMenu)
            {
                BuilderViewModel.Instance.SelectedComponent = this.Model;
            }

            e.Handled = true;
        }
    }
}
