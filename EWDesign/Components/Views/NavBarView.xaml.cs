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
using EWDesign.Core;

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

        public NavBarView(NavBarComponent model, bool isImporting = false)
        {
            InitializeComponent();
            NavBarModel = model;
            this.DataContext = model;
            
            // Solo inicializar componentes por defecto si no se está importando
            if (!isImporting)
            {
                InitTemplateComponents();
            }
            else
            {
                // Si se está importando, mostrar los componentes hijos que ya están cargados
                LoadImportedComponents();
            }
             
        }

        //Inicializando y añadiendo componentes del NavBar por codigo
        public void InitTemplateComponents()
        {
            var TitleText = new TextView(NavBarModel.Title);
            TitleText.ComponentRemoveEvent += (s, e) => RemoveComponent(TitleText);
            TitleDropArea.Children.Add(TitleText);
            Model.AddChild(TitleText.Model);

            var Menu = new MenuView(NavBarModel.Menu);
            Menu.ComponentRemoveEvent += (s, e) => RemoveComponent(Menu);
            MenuDropArea.Children.Add(Menu);
            Model.AddChild(Menu.Model);
        }

        // Cargar componentes importados
        private void LoadImportedComponents()
        {
            // Limpiar áreas de drop
            TitleDropArea.Children.Clear();
            MenuDropArea.Children.Clear();

            // Cargar componentes hijos que ya están en el modelo
            foreach (var child in Model.Children)
            {
                IComponentView componentView = null;

                switch (child.Type?.ToLower())
                {
                    case "navbar-title-text":
                    case "text":
                        componentView = new TextView((TextComponent)child);
                        break;
                    case "menu":
                        componentView = new MenuView((MenuComponent)child);
                        break;
                }

                if (componentView != null)
                {
                    componentView.ComponentRemoveEvent += (s, e) => RemoveComponent(componentView);
                    
                    // Determinar en qué área agregar el componente
                    if (child.Type?.ToLower() == "navbar-title-text" || child.Type?.ToLower() == "text")
                    {
                        TitleDropArea.Children.Add((UserControl)componentView);
                    }
                    else if (child.Type?.ToLower() == "menu")
                    {
                        MenuDropArea.Children.Add((UserControl)componentView);
                    }
                }
            }
        }

        private void RemoveComponent(IComponentView componentView)
        {
            //Comprobando cual de los dos paneles solicita remover el componente
            if (TitleDropArea.Children.Contains((UserControl)componentView))
            {
                TitleDropArea.Children.Remove((UserControl)componentView);
                Model.RemoveChild(componentView.Model);
                
            }else
            {
                MenuDropArea.Children.Remove((UserControl)componentView);
                Model.RemoveChild(componentView.Model);
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

        private void DropArea_Drop(object sender, DragEventArgs e)
        {
            if(e.Data.GetData(typeof(ComponentPaletteItem)) is ComponentPaletteItem item)
            {
                ComponentModel component = null;

                if (item.ComponentFactory is Type type && typeof(ComponentModel).IsAssignableFrom(type))
                {
                    component = (ComponentModel)Activator.CreateInstance(type);
                    if (item.DisplayName == "Title Text")
                        component.Type = "Navbar-Title-Text";
                }

                IComponentView newElement = null;

                if(component.Type.ToLower() == "navbar-title-text")
                {
                    newElement = new TextView((TextComponent)component);
                    newElement.ComponentRemoveEvent += (s, a) => RemoveComponent(newElement);
                }
                else if (component.Type.ToLower() == "menu")
                {
                    newElement = new MenuView((MenuComponent)component);
                    newElement.ComponentRemoveEvent += (s, a) => RemoveComponent(newElement);
                }
                else
                {
                    MessageBox.Show("No se puede insertar otro componente que no sea un Titulo o un Menu.", "Componente no soportado", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (component != null)
                {
                    var panel = sender as StackPanel;
                    bool duplicated = false;

                    foreach (var child in Model.Children)
                    {
                        if (child.Type == component.Type)
                            duplicated = true;
                    }

                    if (duplicated)
                     {
                         MessageBox.Show("Solo se puede colocar un titulo o un menu", "Demasiados Componentes", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                     }

                    panel.Children.Add((UIElement)newElement);
                    Model.AddChild(newElement.Model);
                }
            }
        }
    }
}
