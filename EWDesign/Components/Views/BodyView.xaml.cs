using EWDesign.Components.Models;
using EWDesign.Core.Code_Generator;
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
    /// Interaction logic for BodyView.xaml
    /// </summary>
    public partial class BodyView : UserControl, IComponentView
    {
        public BodyComponent BodyModel { get; }

        public ComponentModel Model => BodyModel;

        public event EventHandler ComponentRemoveEvent;

        public BodyView(BodyComponent model, bool isImporting = false)
        {
            InitializeComponent();
            BodyModel = model;
            DataContext = model;
            
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
            
            this.PreviewMouseLeftButtonDown += UserControl_MouseLeftButtonDown;
        }

        //Inicializando y añadiendo componentes a la plantilla por codigo
        public void InitTemplateComponents()
        {
            var HeroSectionItems = new ObservableCollection<IComponentView>
            {

                new TextView(new TextComponent
                {
                    Type = "Title-Text",
                    Text = BodyModel.HeroSectionText[0],
                    Margin = new Thickness(0, 0, 0, 24),
                    FontSize = 48,
                    ForeGround = new SolidColorBrush((Color) ColorConverter.ConvertFromString("White")),
                    TextWrap = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Center
                }),

                new TextView(new TextComponent
                {
                    Type = "Subtitle-Text",
                    Text = BodyModel.HeroSectionText[1],
                    Margin = new Thickness(0, 0, 0, 40),
                    FontSize = 20,
                    ForeGround = new SolidColorBrush((Color) ColorConverter.ConvertFromString("#DADADA")),
                    TextWrap = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Center
                }),

                new ButtonView(new ButtonComponent(BodyModel.HeroSectionText[2]))
            };

            var FeatureSectionItems = new ObservableCollection<IComponentView>
            {
                new CardView(new CardComponent(BodyModel.FeatureSectionText[0], BodyModel.FeatureSectionText[1])),
                new CardView(new CardComponent(BodyModel.FeatureSectionText[2], BodyModel.FeatureSectionText[3])),
                new CardView(new CardComponent(BodyModel.FeatureSectionText[4], BodyModel.FeatureSectionText[5]))
            };

            foreach (var item in HeroSectionItems)
            {
                item.ComponentRemoveEvent += (s, e) => RemoveComponent(item);
                HeroSectionDropArea.Children.Add((UIElement)item);
                Model.AddChild(item.Model);
                BodyModel.HeroSectionComponents.Add(item.Model);
            }

            foreach (var item in FeatureSectionItems)
            {
                item.ComponentRemoveEvent += (s, e) => RemoveComponent(item);
                FeatureSectionDropArea.Children.Add((UIElement)item);
                Model.AddChild(item.Model);
                BodyModel.FeatureSectionComponents.Add(item.Model);
            }

        }

        // Cargar componentes importados
        private void LoadImportedComponents()
        {
            // Limpiar áreas de drop
            HeroSectionDropArea.Children.Clear();
            FeatureSectionDropArea.Children.Clear();

            // Crear un conjunto para rastrear componentes ya cargados (por ID)
            var loadedComponentIds = new HashSet<Guid>();

            // Cargar componentes de HeroSection
            if (BodyModel.HeroSectionComponents != null)
            {
                foreach (var component in BodyModel.HeroSectionComponents)
                {
                    if (!loadedComponentIds.Contains(component.Id))
                    {
                        IComponentView componentView = CreateComponentView(component);
                        if (componentView != null)
                        {
                            componentView.ComponentRemoveEvent += (s, e) => RemoveComponent(componentView);
                            HeroSectionDropArea.Children.Add((UIElement)componentView);
                            loadedComponentIds.Add(component.Id);
                        }
                    }
                }
            }

            // Cargar componentes de FeatureSection
            if (BodyModel.FeatureSectionComponents != null)
            {
                foreach (var component in BodyModel.FeatureSectionComponents)
                {
                    if (!loadedComponentIds.Contains(component.Id))
                    {
                        IComponentView componentView = CreateComponentView(component);
                        if (componentView != null)
                        {
                            componentView.ComponentRemoveEvent += (s, e) => RemoveComponent(componentView);
                            FeatureSectionDropArea.Children.Add((UIElement)componentView);
                            loadedComponentIds.Add(component.Id);
                        }
                    }
                }
            }

            // Cargar componentes hijos adicionales que se arrastraron al Body
            // Solo cargar los que no están ya en HeroSection o FeatureSection
            foreach (var child in Model.Children)
            {
                if (!loadedComponentIds.Contains(child.Id))
                {
                    IComponentView componentView = CreateComponentView(child);
                    if (componentView != null)
                    {
                        componentView.ComponentRemoveEvent += (s, e) => RemoveComponent(componentView);
                        
                        // Determinar en qué sección agregar el componente basado en su tipo
                        if (child.Type?.ToLower().Contains("title") == true || 
                            child.Type?.ToLower().Contains("subtitle") == true ||
                            child.Type?.ToLower() == "button")
                        {
                            HeroSectionDropArea.Children.Add((UIElement)componentView);
                        }
                        else if (child.Type?.ToLower() == "card")
                        {
                            FeatureSectionDropArea.Children.Add((UIElement)componentView);
                        }
                        else
                        {
                            // Por defecto, agregar a HeroSection
                            HeroSectionDropArea.Children.Add((UIElement)componentView);
                        }
                        
                        loadedComponentIds.Add(child.Id);
                    }
                }
            }
        }

        // Método auxiliar para crear vistas de componentes
        private IComponentView CreateComponentView(ComponentModel component)
        {
            switch (component.Type?.ToLower())
            {
                case "text":
                case "title-text":
                case "subtitle-text":
                case "title text":
                case "body text":
                    return new TextView((TextComponent)component);
                case "button":
                    return new ButtonView((ButtonComponent)component);
                case "card":
                    return new CardView((CardComponent)component);
                case "menu":
                    return new MenuView((MenuComponent)component);
                default:
                    return null;
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

        public void RemoveComponent(IComponentView componentView)
        {
            if (HeroSectionDropArea.Children.Contains((UserControl)componentView))
            {
                HeroSectionDropArea.Children.Remove((UserControl)componentView);
                Model.RemoveChild(componentView.Model);
            }
            else
            {
                FeatureSectionDropArea.Children.Remove((UserControl)componentView);
                Model.RemoveChild(componentView.Model);
            }
        }

        private void OpenComponentEditor(ComponentModel model)
        {
            var dialog = new ComponentEditorDialog(model);
            dialog.Owner = Window.GetWindow(this);
            dialog.ShowDialog();
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var original = e.OriginalSource as DependencyObject;

            // Detectamos si se hizo clic directamente sobre el Body o sobre alguno de sus subcomponentes
            var clickedSubcomponent = FindParent<IComponentView>(original);

            // Si se hizo clic fuera de cualquier subcomponente (o justo en el body), selecciona el body
            if (clickedSubcomponent == this)
            {
                BuilderViewModel.Instance.SelectedComponent = this.Model;
                // No ponemos e.Handled = true para no bloquear subcomponentes
            }
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

        private void HeroSectionDropArea_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(ComponentPaletteItem)) is ComponentPaletteItem item)
            {
                ComponentModel component = null;

                if (item.ComponentFactory is Type type && typeof(ComponentModel).IsAssignableFrom(type))
                {
                    component = (ComponentModel)Activator.CreateInstance(type);
                }

                IComponentView newElement = null;

                switch (component.Type.ToLower())
                {
                    case "text":

                        if (item.DisplayName == "Title-Text")
                        {
                            component.Type = item.DisplayName;
                        }
                        else if (item.DisplayName == "Subtitle-Text")
                        {
                            component.Type = item.DisplayName;
                        }

                        newElement = new TextView((TextComponent)component);
                        newElement.ComponentRemoveEvent += (s, a) => RemoveComponent(newElement);
                        break;
                    case "button":
                        newElement = new ButtonView((ButtonComponent)component);
                        newElement.ComponentRemoveEvent += (s, a) => RemoveComponent(newElement);
                        break;
                    case "card":
                        newElement = new CardView((CardComponent)component);
                        newElement.ComponentRemoveEvent += (s, a) => RemoveComponent(newElement);
                        break;
                    default:
                        MessageBox.Show("No se puede insertar este componente en el contenedor.", "Componente no soportado", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                }

                if(newElement != null)
                {
                    var panel = sender as StackPanel;
                    panel.Children.Add((UIElement)newElement);
                    Model.AddChild(newElement.Model);
                }
        }
        }

        private void FeatureSectionDropArea_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(ComponentPaletteItem)) is ComponentPaletteItem item)
            {
                ComponentModel component = null;

                if (item.ComponentFactory is Type type && typeof(ComponentModel).IsAssignableFrom(type))
                {
                    component = (ComponentModel)Activator.CreateInstance(type);
                }

                IComponentView newElement = null;

                switch (component.Type.ToLower())
                {
                    case "text":

                        if (item.DisplayName == "Title-Text")
                        {
                            component.Type = item.DisplayName;
                        }
                        else if (item.DisplayName == "Subtitle-Text")
                        {
                            component.Type = item.DisplayName;
                        }

                        newElement = new TextView((TextComponent)component);
                        newElement.ComponentRemoveEvent += (s, a) => RemoveComponent(newElement);
                        break;
                    case "button":
                        newElement = new ButtonView((ButtonComponent)component);
                        newElement.ComponentRemoveEvent += (s, a) => RemoveComponent(newElement);
                        break;
                    case "card":
                        newElement = new CardView((CardComponent)component);
                        newElement.ComponentRemoveEvent += (s, a) => RemoveComponent(newElement);
                        break;
                    default:
                        MessageBox.Show("No se puede insertar este componente en el contenedor.", "Componente no soportado", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                }

                if (newElement != null)
                {
                    var panel = sender as WrapPanel;
                    panel.Children.Add((UIElement)newElement);
                    Model.AddChild(newElement.Model);
                }
            }
        }
    }
}
