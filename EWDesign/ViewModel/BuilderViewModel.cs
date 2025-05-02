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
    class BuilderViewModel: ObservableObject
    {
        public ObservableCollection<string> Components { get; set; }
        public String SelectedComponent { get; set; }
        public ObservableCollection<UserControl> DroppedComponents { get; set; }

        public BuilderViewModel()
        {
            Components = new ObservableCollection<string>{
                "NavBar", "Body", "SideBar", "Footer"
            };
            DroppedComponents = new ObservableCollection<UserControl>();
        }
    }
}
