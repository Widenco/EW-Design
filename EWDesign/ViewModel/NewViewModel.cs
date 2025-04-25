using EWDesign.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Core;

namespace EWDesign.ViewModel
{
    class NewViewModel:ObservableObject
    {
        public ObservableCollection<IconModel> Icons { get; set; }

        public NewViewModel()
        {
            Icons = new ObservableCollection<IconModel> {
                new IconModel
                {
                    IconPath = "pack://application:,,,/Assets/newPageIcon.png",
                    Text = "New",
                    Tooltip = "New Page",
                    Command = new RelayCommand(o => NewPage())
                },
                new IconModel
                {
                    IconPath = "pack://application:,,,/Assets/newPageIcon.png",
                    Text = "Template 1",
                    Tooltip = "New Template 1",
                    Command = new RelayCommand(o => NewTemplate(1))
                },
                new IconModel
                {
                    IconPath = "pack://application:,,,/Assets/newPageIcon.png",
                    Text = "Template 2",
                    Tooltip = "New Template 2",
                    Command = new RelayCommand(o => NewTemplate(2))
                }
            };
        }

        private void NewPage()
        {

        }

        private void NewTemplate(int templateID)
        {

        }
    }
}
