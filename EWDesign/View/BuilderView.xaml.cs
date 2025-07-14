using EWDesign.Components.Views;
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
            
        }
        private void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var listBox = sender as ListBox;

            // Obtener el ítem bajo el cursor
            var hit = VisualTreeHelper.HitTest(listBox, e.GetPosition(listBox));
            if (hit?.VisualHit is DependencyObject element)
            {
                // Buscar el ListBoxItem padre
                while (element != null && !(element is ListBoxItem))
                    element = VisualTreeHelper.GetParent(element);

                if (element != null)
                {
                    var listBoxItem = (ListBoxItem)element;
                    listBoxItem.IsSelected = true;  // Forzar la selección
                    listBox.SelectedItem = listBoxItem.DataContext;

                    // Iniciar el DragDrop inmediatamente
                    DragDrop.DoDragDrop(listBox, listBox.SelectedItem, DragDropEffects.Copy);

                    e.Handled = true;  // Opcional: prevenir procesamiento adicional
                }
            }
        }

        private void DropArea_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                string componentType = (string)e.Data.GetData(DataFormats.StringFormat);

                IComponentView newElement = null;
                switch (componentType)
                {
                    case "NavBar":
                        newElement = new Components.Views.NavBarView(new Components.Models.NavBarComponent());
                        newElement.ComponentRemoveEvent += (s, a) => RemoveComponent(newElement);
                        break;
                    case "Body":
                        newElement = new Components.Views.BodyView(new Components.Models.BodyComponent());
                        newElement.ComponentRemoveEvent += (s, a) => RemoveComponent(newElement);
                        break;
                    case "SideBar":
                        newElement = new Components.Views.NavBarView(new Components.Models.NavBarComponent());
                        break;
                    case "Footer":
                        newElement = new Components.Views.NavBarView(new Components.Models.NavBarComponent());
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
                            MessageBox.Show($"Ya has agregado un componente '{componentType}'.", "Componente duplicado", MessageBoxButton.OK, MessageBoxImage.Warning);
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

        private void DeselectAll(object sender, MouseButtonEventArgs e)
        {
            BuilderViewModel.Instance.SelectedComponent = null;
        }
    }
}
