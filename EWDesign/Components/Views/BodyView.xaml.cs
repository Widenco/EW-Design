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
            
            // Inicializar componentes por defecto solo si no se está importando
            if (!isImporting)
            {
                InitTemplateComponents();
            }
            else
            {
                // Si se está importando, cargar solo los componentes exportados
                LoadImportedComponents();
            }
            
            this.PreviewMouseLeftButtonDown += UserControl_MouseLeftButtonDown;
        }

        //Inicializando y añadiendo componentes a la plantilla por codigo
        public void InitTemplateComponents()
        {
            // Crear componentes hero por defecto
            CreateDefaultHeroComponents();
            
            // Crear componentes feature por defecto (3 CardComponents)
            CreateDefaultFeatureComponents();
        }

        // Cargar componentes importados
        private void LoadImportedComponents()
        {
            // DEBUG: Mostrar información de depuración
            System.Diagnostics.Debug.WriteLine($"=== DEBUG: LoadImportedComponents ===");
            System.Diagnostics.Debug.WriteLine($"HeroSectionComponents count: {BodyModel.HeroSectionComponents?.Count ?? 0}");
            System.Diagnostics.Debug.WriteLine($"FeatureSectionComponents count: {BodyModel.FeatureSectionComponents?.Count ?? 0}");
            System.Diagnostics.Debug.WriteLine($"Is FeatureSectionComponents null? {BodyModel.FeatureSectionComponents == null}");
            System.Diagnostics.Debug.WriteLine($"Is FeatureSectionComponents empty? {BodyModel.FeatureSectionComponents?.Count == 0}");

            // Limpiar áreas de drop
            HeroSectionDropArea.Children.Clear();
            FeatureSectionDropArea.Children.Clear();

            // Crear un conjunto para rastrear componentes ya cargados (por ID)
            var loadedComponentIds = new HashSet<Guid>();

            // Cargar componentes de HeroSection
            if (BodyModel.HeroSectionComponents != null && BodyModel.HeroSectionComponents.Count > 0)
            {
                System.Diagnostics.Debug.WriteLine($"Loading {BodyModel.HeroSectionComponents.Count} HeroSection components");
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
                            System.Diagnostics.Debug.WriteLine($"Loaded HeroSection component: {component.Type}");
                        }
                    }
                }
            }

            // Cargar componentes de FeatureSection
            if (BodyModel.FeatureSectionComponents != null && BodyModel.FeatureSectionComponents.Count > 0)
            {
                System.Diagnostics.Debug.WriteLine($"Loading {BodyModel.FeatureSectionComponents.Count} FeatureSection components");
                foreach (var component in BodyModel.FeatureSectionComponents)
                {
                    System.Diagnostics.Debug.WriteLine($"Processing FeatureSection component: {component.Type} with ID: {component.Id}");
                    if (!loadedComponentIds.Contains(component.Id))
                    {
                        IComponentView componentView = CreateComponentView(component);
                        if (componentView != null)
                        {
                            componentView.ComponentRemoveEvent += (s, e) => RemoveComponent(componentView);
                            FeatureSectionDropArea.Children.Add((UIElement)componentView);
                            loadedComponentIds.Add(component.Id);
                            System.Diagnostics.Debug.WriteLine($"Successfully loaded FeatureSection component: {component.Type} - UIElement added to FeatureSectionDropArea");
                            
                                                         // DEBUG: Verificar que el componente se agregó correctamente
                             System.Diagnostics.Debug.WriteLine($"FeatureSectionDropArea.Children.Count after adding: {FeatureSectionDropArea.Children.Count}");
                             System.Diagnostics.Debug.WriteLine($"Component view type: {componentView.GetType().Name}");
                             System.Diagnostics.Debug.WriteLine($"Component view visibility: {((UIElement)componentView).Visibility}");
                             
                             // Verificar si el componente tiene Width y Height (FrameworkElement)
                             if (componentView is FrameworkElement fe)
                             {
                                 System.Diagnostics.Debug.WriteLine($"Component view width: {fe.Width}");
                                 System.Diagnostics.Debug.WriteLine($"Component view height: {fe.Height}");
                             }
                             else
                             {
                                 System.Diagnostics.Debug.WriteLine("Component view is not FrameworkElement - no Width/Height properties");
                             }
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"Failed to create component view for: {component.Type}");
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"Component {component.Id} already loaded, skipping");
                    }
                }
                System.Diagnostics.Debug.WriteLine($"FeatureSectionDropArea.Children.Count after loading: {FeatureSectionDropArea.Children.Count}");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No FeatureSectionComponents to load");
            }

            // NUEVA LÓGICA: Solo cargar componentes de Children que NO estén ya en FeatureSectionComponents
            // Esto evita duplicados cuando un componente está tanto en FeatureSectionComponents como en Children
            if (BodyModel.Children != null && BodyModel.Children.Count > 0)
            {
                System.Diagnostics.Debug.WriteLine($"Checking {BodyModel.Children.Count} additional Children for duplicates");
                foreach (var component in BodyModel.Children)
                {
                    if (!loadedComponentIds.Contains(component.Id))
                    {
                        System.Diagnostics.Debug.WriteLine($"Loading additional component: {component.Type} with ID: {component.Id}");
                        IComponentView componentView = CreateComponentView(component);
                        if (componentView != null)
                        {
                            componentView.ComponentRemoveEvent += (s, e) => RemoveComponent(componentView);
                            FeatureSectionDropArea.Children.Add((UIElement)componentView);
                            loadedComponentIds.Add(component.Id);
                            System.Diagnostics.Debug.WriteLine($"Loaded additional component: {component.Type}");
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"Skipping duplicate component: {component.Type} with ID: {component.Id}");
                    }
                }
            }

            // Si no hay componentes exportados, crear componentes por defecto
            if (BodyModel.HeroSectionComponents == null || BodyModel.HeroSectionComponents.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("Creating default HeroSection components");
                CreateDefaultHeroComponents();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("HeroSection components already loaded, skipping defaults");
            }

            // NUEVA LÓGICA: Si es una importación, NO crear componentes por defecto
            // Una lista vacía en importación significa "no exportar CardComponents por defecto"
            // Solo crear por defecto si realmente no hay datos de importación
            if (BodyModel.FeatureSectionComponents == null)
            {
                System.Diagnostics.Debug.WriteLine("FeatureSectionComponents is null - creating default components");
                CreateDefaultFeatureComponents();
            }
            else if (BodyModel.FeatureSectionComponents.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("FeatureSectionComponents is empty (imported) - NOT creating default components");
                System.Diagnostics.Debug.WriteLine("This means the project was exported with 0 CardComponents (all were default)");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"FeatureSection components already loaded ({BodyModel.FeatureSectionComponents.Count} components), skipping defaults");
            }

            // NOTA: No cargar desde Model.Children porque esos componentes ya están incluidos
            // en HeroSectionComponents y FeatureSectionComponents durante la importación
            // Esto evita duplicados y asegura que solo se carguen los componentes exportados
            
            // DEBUG: Estado final después de cargar componentes
            System.Diagnostics.Debug.WriteLine($"=== FINAL STATE AFTER LOADING ===");
            System.Diagnostics.Debug.WriteLine($"HeroSectionDropArea.Children.Count: {HeroSectionDropArea.Children.Count}");
            System.Diagnostics.Debug.WriteLine($"FeatureSectionDropArea.Children.Count: {FeatureSectionDropArea.Children.Count}");
            System.Diagnostics.Debug.WriteLine($"BodyModel.HeroSectionComponents.Count: {BodyModel.HeroSectionComponents?.Count ?? 0}");
            System.Diagnostics.Debug.WriteLine($"BodyModel.FeatureSectionComponents.Count: {BodyModel.FeatureSectionComponents?.Count ?? 0}");
            System.Diagnostics.Debug.WriteLine($"BodyModel.Children.Count: {BodyModel.Children?.Count ?? 0}");
        }

        // Método auxiliar para crear vistas de componentes
        private IComponentView CreateComponentView(ComponentModel component)
        {
            System.Diagnostics.Debug.WriteLine($"CreateComponentView called for component: {component.Type} with ID: {component.Id}");
            
            IComponentView result = null;
            switch (component.Type?.ToLower())
            {
                case "text":
                case "title-text":
                case "subtitle-text":
                case "title text":
                case "body text":
                    result = new TextView((TextComponent)component);
                    System.Diagnostics.Debug.WriteLine($"Created TextView for TextComponent");
                    break;
                case "button":
                    result = new ButtonView((ButtonComponent)component);
                    System.Diagnostics.Debug.WriteLine($"Created ButtonView for ButtonComponent");
                    break;
                case "card":
                    result = new CardView((CardComponent)component);
                    System.Diagnostics.Debug.WriteLine($"Created CardView for CardComponent");
                    break;
                case "menu":
                    result = new MenuView((MenuComponent)component);
                    System.Diagnostics.Debug.WriteLine($"Created MenuView for MenuComponent");
                    break;
                default:
                    System.Diagnostics.Debug.WriteLine($"Unknown component type: {component.Type}");
                    result = null;
                    break;
            }
            
            System.Diagnostics.Debug.WriteLine($"CreateComponentView result: {(result != null ? "SUCCESS" : "FAILED")}");
            return result;
        }

        // Crear componentes hero por defecto
        private void CreateDefaultHeroComponents()
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

            foreach (var item in HeroSectionItems)
            {
                item.ComponentRemoveEvent += (s, e) => RemoveComponent(item);
                HeroSectionDropArea.Children.Add((UIElement)item);
                // SOLO agregar a HeroSectionComponents, NO a Model.Children para evitar duplicados
                BodyModel.HeroSectionComponents.Add(item.Model);
            }
        }

        // Crear componentes feature por defecto
        private void CreateDefaultFeatureComponents()
        {
            var FeatureSectionItems = new ObservableCollection<IComponentView>
            {
                new CardView(new CardComponent(BodyModel.FeatureSectionText[0], BodyModel.FeatureSectionText[1])),
                new CardView(new CardComponent(BodyModel.FeatureSectionText[2], BodyModel.FeatureSectionText[3])),
                new CardView(new CardComponent(BodyModel.FeatureSectionText[4], BodyModel.FeatureSectionText[5]))
            };

            foreach (var item in FeatureSectionItems)
            {
                item.ComponentRemoveEvent += (s, e) => RemoveComponent(item);
                FeatureSectionDropArea.Children.Add((UIElement)item);
                // SOLO agregar a FeatureSectionComponents, NO a Model.Children para evitar duplicados
                BodyModel.FeatureSectionComponents.Add(item.Model);
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
            System.Diagnostics.Debug.WriteLine($"=== DEBUG: RemoveComponent ===");
            System.Diagnostics.Debug.WriteLine($"Removing component: {componentView.Model.Type} with ID: {componentView.Model.Id}");
            
            if (HeroSectionDropArea.Children.Contains((UserControl)componentView))
            {
                System.Diagnostics.Debug.WriteLine("Removing from HeroSectionDropArea");
                HeroSectionDropArea.Children.Remove((UserControl)componentView);
                Model.RemoveChild(componentView.Model);
                
                // También eliminar de la colección específica
                if (BodyModel.HeroSectionComponents.Contains(componentView.Model))
                {
                    BodyModel.HeroSectionComponents.Remove(componentView.Model);
                    System.Diagnostics.Debug.WriteLine($"Removed from HeroSectionComponents. Count now: {BodyModel.HeroSectionComponents.Count}");
                }
            }
            else if (FeatureSectionDropArea.Children.Contains((UserControl)componentView))
            {
                System.Diagnostics.Debug.WriteLine("Removing from FeatureSectionDropArea");
                FeatureSectionDropArea.Children.Remove((UserControl)componentView);
                Model.RemoveChild(componentView.Model);
                
                // También eliminar de la colección específica
                if (BodyModel.FeatureSectionComponents.Contains(componentView.Model))
                {
                    BodyModel.FeatureSectionComponents.Remove(componentView.Model);
                    System.Diagnostics.Debug.WriteLine($"Removed from FeatureSectionComponents. Count now: {BodyModel.FeatureSectionComponents.Count}");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Component not found in any drop area");
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
                }

                if(newElement != null)
                {
                    var panel = sender as StackPanel;
                    panel.Children.Add((UIElement)newElement);
                    Model.AddChild(newElement.Model);
                    
                    // También agregar a la colección específica
                    BodyModel.HeroSectionComponents.Add(newElement.Model);
                    System.Diagnostics.Debug.WriteLine($"Added to HeroSectionComponents. Count now: {BodyModel.HeroSectionComponents.Count}");
                }
        }
        }

        private void FeatureSectionDropArea_Drop(object sender, DragEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("=== DEBUG: FeatureSectionDropArea_Drop ===");
            
            if (e.Data.GetData(typeof(ComponentPaletteItem)) is ComponentPaletteItem item)
            {
                System.Diagnostics.Debug.WriteLine($"Dropping component: {item.DisplayName}");
                
                ComponentModel component = null;

                if (item.ComponentFactory is Type type && typeof(ComponentModel).IsAssignableFrom(type))
                {
                    component = (ComponentModel)Activator.CreateInstance(type);
                    System.Diagnostics.Debug.WriteLine($"Created component: {component.Type}");
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
                }

                if (newElement != null)
                {
                    var panel = sender as WrapPanel;
                    panel.Children.Add((UIElement)newElement);
                    Model.AddChild(newElement.Model);
                    
                    // También agregar a la colección específica
                    BodyModel.FeatureSectionComponents.Add(newElement.Model);
                    System.Diagnostics.Debug.WriteLine($"Added to FeatureSectionComponents. Count now: {BodyModel.FeatureSectionComponents.Count}");
                    System.Diagnostics.Debug.WriteLine($"FeatureSectionDropArea.Children.Count: {FeatureSectionDropArea.Children.Count}");
                    
                    // Verificar que el componente se agregó correctamente
                    System.Diagnostics.Debug.WriteLine($"Component added: {newElement.Model.Type} with ID: {newElement.Model.Id}");
                    if (newElement.Model is CardComponent card)
                    {
                        System.Diagnostics.Debug.WriteLine($"Card Title: {card.Title?.Text ?? "NULL"}");
                    }
                }
            }
        }
    }
}
