using EWDesign.Components.Models;
using EWDesign.Components.Views;
using EWDesign.Core.Code_Generator;
using EWDesign.Interfaces;
using EWDesign.Model;
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
using System.Windows.Shapes;

namespace EWDesign.View
{
    /// <summary>
    /// Interaction logic for BuilderView.xaml
    /// </summary>
    public partial class BuilderView : Window
    {

        public BuilderViewModel Model { get; }
        public BuilderView()
        {
            Model = new BuilderViewModel();
            InitializeComponent();
            this.DataContext = Model;
            this.PreviewMouseLeftButtonDown += BuilderView_PreviewMouseLeftButtonDown;
            
        }

        private void ComponentItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is ComponentPaletteItem item)
            {
                // Puedes pasar solo el tipo, una fábrica o el nombre
                DragDrop.DoDragDrop(border, item, DragDropEffects.Copy);
                e.Handled = true;
            }
        }

        private void DropArea_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(ComponentPaletteItem)) is ComponentPaletteItem item)
            {
                ComponentModel component = null;

                if (item.ComponentFactory is Type type && typeof(ComponentModel).IsAssignableFrom(type))
                {
                    component = (ComponentModel)Activator.CreateInstance(type);
                }

                IComponentView newElement = null;
                switch (component.Type)
                {
                    case "NavBar":
                        newElement = new Components.Views.NavBarView((NavBarComponent)component);
                        newElement.ComponentRemoveEvent += (s, a) => RemoveComponent(newElement);
                        break;
                    case "Body":
                        newElement = new Components.Views.BodyView((BodyComponent)component);
                        newElement.ComponentRemoveEvent += (s, a) => RemoveComponent(newElement);
                        break;
                }
                    if (newElement != null)
                    {
                        var panel = sender as StackPanel;

                    // Verificar duplicados
                    bool alreadyExists = panel.Children
                        .OfType<IComponentView>().Any(c => c.GetType() == newElement.GetType());

                        if (alreadyExists)
                        {
                            MessageBox.Show($"Ya has agregado un componente '{component.Type}'.", "Componente duplicado", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        // Lógica de inserción corregida
                        if (newElement is NavBarView)
                        {
                            panel.Children.Insert(0, (UserControl)newElement);
                            Model.DroppedComponents.Insert(0, newElement);
                            
                        }
                        else
                        {
                            // Buscar la posición del NavBar si existe
                            int navbarIndex = -1;
                            for (int i = 0; i < panel.Children.Count; i++)
                            {
                                if (panel.Children[i] is StackPanel childContainer)
                                {
                                    var childUC = childContainer.Children.OfType<UserControl>().FirstOrDefault();
                                    if (childUC is NavBarView)
                                    {
                                        navbarIndex = i;
                                        break;
                                    }
                                }
                            }

                            // Insertar después del NavBar o al final si no existe
                            if (navbarIndex != -1)
                            {
                                panel.Children.Insert(navbarIndex + 1, (UserControl)newElement);
                                Model.DroppedComponents.Insert(navbarIndex + 1, newElement);
                            }
                            else
                            {
                                panel.Children.Add((UserControl)newElement);
                                Model.DroppedComponents.Add(newElement);
                                
                                
                            }
                        }
                    
                }
            }
        }

        private void RemoveComponent(IComponentView componentView)
        {
            if (DropArea.Children.Contains((UserControl)componentView))
            {
                DropArea.Children.Remove((UserControl)componentView);
                Model.DroppedComponents.Remove(componentView);
            }
        }

        private void BuilderView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Verifica si el clic fue sobre un componente "interactivo"
            var clickedElement = e.OriginalSource as DependencyObject;

            if (clickedElement == null || !IsDescendantOfBuilder(clickedElement))
                return;

            // Busca hacia arriba si clicaste sobre un componente que implementa IComponentView
            var parentComponent = FindParent<IComponentView>(clickedElement);

            if (parentComponent == null)
            {
                // Si no se hizo clic en ningún componente, se deselecciona
                BuilderViewModel.Instance.SelectedComponent = null;
            }
        }

        private bool IsDescendantOfBuilder(DependencyObject element)
        {
            while (element != null)
            {
                if (element is BuilderView)
                    return true;

                try
                {
                    element = VisualTreeHelper.GetParent(element);
                }
                catch
                {
                    return false; // Evita excepciones al acceder al árbol visual de elementos "especiales"
                }
            }
            return false;
        }


        private T FindParent<T>(DependencyObject current) where T : class
        {
            while (current != null)
            {
                if (current is T match)
                    return match;

                current = VisualTreeHelper.GetParent(current);
            }
            return null;
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            Generator gen = new Generator();
            gen.GenerateFiles(Model.DroppedComponents);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
