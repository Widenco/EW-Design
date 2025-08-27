using EWDesign.Components.Models;
using EWDesign.Components.Views;
using EWDesign.Interfaces;
using EWDesign.Model;
using EWDesign.Services;
using EWDesign.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using EWDesign.View;
using System.IO;

namespace EWDesign.ViewModel
{
    public class BuilderViewModel: ObservableObject
    {
        public static BuilderViewModel Instance { get; private set; }

        public ObservableCollection<ComponentPaletteItem> AllComponents { get; }
        public IEnumerable<IGrouping<string, ComponentPaletteItem>> GroupedComponents =>
    AllComponents
        .Where(c => string.IsNullOrWhiteSpace(SearchText) ||
                    c.DisplayName?.ToLower().Contains(SearchText?.ToLower()) == true)
        .GroupBy(c => c.Category);

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(GroupedComponents)); // Refresca la agrupación cuando cambia el texto
            }
        }

        private ComponentModel _selectedComponent;
        public ComponentModel SelectedComponent
        {
            get => _selectedComponent;
            set
            {
                if (_selectedComponent != value)
                {
                    _selectedComponent?.SetSelected(false);  // ← Deseleccionar el anterior
                    _selectedComponent = value;
                    _selectedComponent?.SetSelected(true);   // ← Seleccionar el nuevo
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<IComponentView> DroppedComponents { get; set; }

        // Propiedades para los componentes principales
        public NavBarComponent CurrentNavBar { get; set; }
        public BodyComponent CurrentBody { get; set; }

        public FooterComponent CurrentFooter { get; set; }

        // Comandos para exportar e importar
        public RelayCommand ExportProjectCommand { get; set; }
        public RelayCommand ImportProjectCommand { get; set; }
        public RelayCommand ClearCanvasCommand { get; set; }

        // Evento para notificar cuando se importan componentes
        public event Action OnComponentsImported;

        public BuilderViewModel()
        {
            Instance = this;
            AllComponents = new ObservableCollection<ComponentPaletteItem>();

            AllComponents.Add(new ComponentPaletteItem
            {
                DisplayName = "Button",
                Category = "Design Components",
                ComponentFactory = typeof(ButtonComponent)
            });
            AllComponents.Add(new ComponentPaletteItem
            {
                DisplayName = "Title Text",
                Category = "Design Components",
                ComponentFactory = typeof(TextComponent)
            });
            AllComponents.Add(new ComponentPaletteItem
            {
                DisplayName = "SubTitle Text",
                Category = "Design Components",
                ComponentFactory = typeof(TextComponent)
            });
            AllComponents.Add(new ComponentPaletteItem
            {
                DisplayName = "Menu Item",
                Category = "Design Components",
                ComponentFactory = typeof(TextComponent)
            });
            AllComponents.Add(new ComponentPaletteItem
            {
                DisplayName = "Card",
                Category = "Design Components",
                ComponentFactory = typeof(CardComponent)
            });
            AllComponents.Add(new ComponentPaletteItem
            {
                DisplayName = "Menu",
                Category = "Design Components",
                ComponentFactory = typeof(MenuComponent)
            });
            AllComponents.Add(new ComponentPaletteItem
            {
                DisplayName = "Icon",
                Category = "Design Components",
                ComponentFactory = typeof(IconComponent)
            });
            AllComponents.Add(new ComponentPaletteItem
            {
                DisplayName = "Navbar",
                Category = "Layout Components",
                ComponentFactory = typeof(NavBarComponent)
            });
            AllComponents.Add(new ComponentPaletteItem
            {
                DisplayName = "Body",
                Category = "Layout Components",
                ComponentFactory = typeof(BodyComponent)
            });
            AllComponents.Add(new ComponentPaletteItem
            {
                DisplayName = "Footer",
                Category = "Layout Components",
                ComponentFactory = typeof(FooterComponent)
            });

            DroppedComponents = new ObservableCollection<IComponentView>();

            // Inicializar comandos
            ExportProjectCommand = new RelayCommand(ExportProject);
            ImportProjectCommand = new RelayCommand(ImportProject);
            ClearCanvasCommand = new RelayCommand(ClearCanvas);
        }

        private void ExportProject(object parameter)
        {
            try
            {
                // Verificar que tenemos los componentes necesarios
                if (CurrentNavBar == null || CurrentBody == null || CurrentFooter == null)
                {
                    MessageBox.Show("Debe tener un NavBar, un Body y un Footer en el lienzo para exportar el proyecto.", 
                        "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Crear diálogo para guardar archivo
                var saveFileDialog = new Microsoft.Win32.SaveFileDialog
                {
                    Filter = "Archivos JSON (*.json)|*.json|Todos los archivos (*.*)|*.*",
                    DefaultExt = "json",
                    FileName = $"Proyecto_{DateTime.Now:yyyyMMdd_HHmmss}.json"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    bool success = ProjectService.Instance.ExportProject(CurrentNavBar, CurrentBody, CurrentFooter, saveFileDialog.FileName);
                    if (success)
                    {
                        BuilderView.Instance.Title = Path.GetFileNameWithoutExtension(saveFileDialog.FileName);
                        MessageBox.Show("Proyecto exportado exitosamente.", 
                            "Exportación Completada", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar el proyecto: {ex.Message}", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ImportProject(object parameter)
        {
            try
            {
                // Crear diálogo para abrir archivo
                var openFileDialog = new Microsoft.Win32.OpenFileDialog
                {
                    Filter = "Archivos JSON (*.json)|*.json|Todos los archivos (*.*)|*.*",
                    DefaultExt = "json"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    var (navbar, body, footer) = ProjectService.Instance.ImportProject(openFileDialog.FileName);
                    
                    if (navbar != null && body != null && footer != null)
                    {
                        // Limpiar el lienzo actual sin mostrar confirmación
                        ClearCanvasSilently();
                        
                        // Cargar los componentes importados
                        CurrentNavBar = navbar;
                        CurrentBody = body;
                        CurrentFooter = footer;
                        
                        // Crear las vistas visuales para los componentes importados
                        var navbarView = new EWDesign.Components.Views.NavBarView(navbar, true); // isImporting = true
                        var bodyView = new EWDesign.Components.Views.BodyView(body, true); // isImporting = true
                        var footerView = new EWDesign.Components.Views.FooterView(footer, true); // isImporting = true
                        
                        // Configurar eventos de eliminación
                        navbarView.ComponentRemoveEvent += (s, e) => RemoveComponentFromCanvas(navbarView);
                        bodyView.ComponentRemoveEvent += (s, e) => RemoveComponentFromCanvas(bodyView);
                        footerView.ComponentRemoveEvent += (s, e) => RemoveComponentFromCanvas(footerView);

                        // Agregar a la colección de componentes
                        DroppedComponents.Clear();
                        DroppedComponents.Add(navbarView);
                        DroppedComponents.Add(bodyView);
                        DroppedComponents.Add(footerView);
                        
                        // Notificar al BuilderView que debe actualizar la interfaz
                        NotifyCanvasUpdate(navbarView, bodyView, footerView);
                        BuilderView.Instance.Title = Path.GetFileNameWithoutExtension(openFileDialog.FileName);

                        MessageBox.Show("Proyecto importado exitosamente. Los componentes están listos para usar.", 
                            "Importación Exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al importar el proyecto: {ex.Message}", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearCanvas(object parameter)
        {
            try
            {
                var result = MessageBox.Show("¿Está seguro de que desea limpiar el lienzo? Esta acción no se puede deshacer.", 
                    "Confirmar Limpieza", MessageBoxButton.YesNo, MessageBoxImage.Question);
                
                if (result == MessageBoxResult.Yes)
                {
                    ClearCanvasSilently();
                    BuilderView.Instance.Title = "Untitled Project";

                    MessageBox.Show("Lienzo limpiado exitosamente.", 
                        "Limpieza Completada", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al limpiar el lienzo: {ex.Message}", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearCanvasSilently()
        {
            CurrentNavBar = null;
            CurrentBody = null;
            DroppedComponents.Clear();
            SelectedComponent = null;
            
            // Notificar al View que debe limpiar el lienzo visual
            OnPropertyChanged(nameof(DroppedComponents));
            OnComponentsImported?.Invoke();
        }

        private void RemoveComponentFromCanvas(IComponentView componentView)
        {
            DroppedComponents.Remove(componentView);
            
            if (componentView is EWDesign.Components.Views.NavBarView)
            {
                CurrentNavBar = null;
            }
            else if (componentView is EWDesign.Components.Views.BodyView)
            {
                CurrentBody = null;
            }
        }

        private void NotifyCanvasUpdate(IComponentView navbarView, IComponentView bodyView, IComponentView footerView)
        {
            // Notificar que se han importado componentes
            OnPropertyChanged(nameof(DroppedComponents));
            OnComponentsImported?.Invoke();
        }
    }
}
