using EWDesign.Model;
using EWDesign.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using EWDesign.Core;

namespace EWDesign.ViewModel
{
    class NewViewModel:ObservableObject
    {
        public ObservableCollection<IconModel> Icons { get; set; }
        public ICommand OpenBuilderCommand { get; }
        public ICommand OpenTemplateCommand { get; }
        public event Action OpenBuilder;
        public event Action OpenTemplate;

        public NewViewModel()
        {
            OpenBuilderCommand = new RelayCommand(o => OpenBuilder?.Invoke());
            OpenTemplateCommand = new RelayCommand(o => OpenTemplate?.Invoke());

            Icons = new ObservableCollection<IconModel> {
                new IconModel
                {
                    IconPath = "pack://application:,,,/Assets/newPageIcon.png",
                    Text = "New",
                    Tooltip = "New Page",
                    Command = OpenBuilderCommand
                },
               /* new IconModel
                {
                    IconPath = "pack://application:,,,/Assets/newPageIcon.png",
                    Text = "Template 1",
                    Tooltip = "New Template 1",
                    Command = OpenTemplateCommand
                },
                new IconModel
                {
                    IconPath = "pack://application:,,,/Assets/newPageIcon.png",
                    Text = "Template 2",
                    Tooltip = "New Template 2",
                    Command = new RelayCommand(o => NewTemplate(2))
                }*/
            };

            
        }

    }
}
