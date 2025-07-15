using EWDesign.Components.Models;
using EWDesign.Interfaces;
using EWDesign.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Core;

namespace EWDesign.ViewModel
{
    public class BuilderViewModel: ObservableObject
    {
        public static BuilderViewModel Instance { get; private set; }

        public ObservableCollection<ComponentPaletteItem> AllComponents { get; }
        public IEnumerable<IGrouping<string, ComponentPaletteItem>> GroupedComponents =>
            AllComponents.GroupBy(c => c.Category);
        public ObservableCollection<string> Components { get; set; }

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
                DisplayName = "Text",
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

            Components = new ObservableCollection<string>{
                "NavBar", "Body", "SideBar", "Footer"
            };
            DroppedComponents = new ObservableCollection<IComponentView>();
        }
    }
}
