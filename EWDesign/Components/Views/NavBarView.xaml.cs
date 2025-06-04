using EWDesign.Components.Models;
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
    public partial class NavBarView : UserControl
    {
        public NavBarComponent Model { get; }

        public NavBarView(NavBarComponent model)
        {
            InitializeComponent();
            Model = model;
            this.DataContext = model;
            InitTemplateComponents();
             
        }
        
        //Inicializando y añadiendo componentes del NavBar por codigo
        public void InitTemplateComponents()
        {
            var TitleText = new TextView( new TextComponent { Text = "Mi Producto" });
            TitleDropArea.Children.Add(TitleText);

            foreach (var item in Model.NavbarElementsText)
            {
                var menuItem = new TextView(new TextComponent { Text = item, Margin = new Thickness(16, 0, 0, 0),
                FontSize = "16", ForeGround = "#3A3F47"});
                MenuItemsDropArea.Children.Add(menuItem);
            }
        }
    }
}
