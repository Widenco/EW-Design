using EWDesign.Components.Models;
using EWDesign.Interfaces;
using EWDesign.Model;
using EWDesign.View;
using EWDesign.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using WpfApp1.Core;

namespace EWDesign.Components.Views
{
    /// <summary>
    /// Interaction logic for NavBarView.xaml
    /// </summary>
    public partial class NavBarView : UserControl, IComponentView
    {
        public NavBarComponent NavBarModel { get; }

        public ComponentModel Model => NavBarModel;

        public event EventHandler ComponentRemoveEvent;

        public NavBarView(NavBarComponent model)
        {
            InitializeComponent();
            NavBarModel = model;
            this.DataContext = model;
            InitTemplateComponents();
             
        }

        //Inicializando y añadiendo componentes del NavBar por codigo
        public void InitTemplateComponents()
        {
            var TitleText = new TextView( new TextComponent { Text = "Mi Producto", DelegateContextMenu = false});
            TitleText.ComponentRemoveEvent += (s, e) => RemoveComponent(TitleText);
            TitleDropArea.Children.Add(TitleText);

            foreach (var item in NavBarModel.NavbarElementsText)
            {
                var menuItem = new TextView(new TextComponent { Text = item, Margin = new Thickness(16, 0, 0, 0),
                FontSize = 16, ForeGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3A3F47")), DelegateContextMenu = false});
                menuItem.ComponentRemoveEvent += (s, e) => RemoveComponent(menuItem);
                MenuItemsDropArea.Children.Add(menuItem);
            }
        }

        private void RemoveComponent(IComponentView componentView)
        {
            //Comprobando cual de los dos paneles solicita remover el componente
            if (TitleDropArea.Children.Contains((UserControl)componentView))
            {
                TitleDropArea.Children.Remove((UserControl)componentView);
            }else
            {
                MenuItemsDropArea.Children.Remove((UserControl)componentView);
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
        }
    }
}
