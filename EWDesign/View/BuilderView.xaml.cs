using EWDesign.Components.Views;
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
        public BuilderView()
        {
            InitializeComponent();
            this.DataContext = new BuilderViewModel();
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

                UserControl newElement = null;
                switch (componentType)
                {
                    case "NavBar":
                        newElement = new Components.Views.NavBarView(new Components.Models.NavBarComponent());
                        break;
                    case "Body":
                        newElement = new Components.Views.BodyView(new Components.Models.BodyComponent());
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
                            .OfType<StackPanel>()
                            .Select(sp => sp.Children.OfType<UserControl>().FirstOrDefault())
                            .Any(uc => uc?.GetType() == newElement.GetType());

                        if (alreadyExists)
                        {
                            MessageBox.Show($"Ya has agregado un componente '{componentType}'.", "Componente duplicado", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        var container = new StackPanel
                        {
                            Margin = new Thickness(5),
                            Background = Brushes.Transparent
                        };

                        container.PreviewMouseLeftButtonDown += (s, args) =>
                        {
                            if (args.ClickCount == 2)
                            {
                                panel.Children.Remove(container);
                                args.Handled = true;
                            }
                        };

                        container.Children.Add(newElement);

                        // Lógica de inserción corregida
                        if (newElement is NavBarView)
                        {
                            panel.Children.Insert(0, container);
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
                                panel.Children.Insert(navbarIndex + 1, container);
                            }
                            else
                            {
                                panel.Children.Add(container);
                            }
                        }
                    
                }
            }
        }
    }
}
