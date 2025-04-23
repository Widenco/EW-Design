using EWDesign.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Core;

namespace EWDesign.ViewModel
{
	class MainViewModel : ObservableObject
    {
        public RelayCommand RecentRC { get; set; }
        public RelayCommand NewRC { get; set; }

        public RecentViewModel RecentVM { get; set; }
        public NewViewModel NewVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            RecentVM = new RecentViewModel();
            NewVM = new NewViewModel();

            CurrentView = RecentVM;

            RecentRC = new RelayCommand(o => 
            {
                CurrentView = RecentVM;
            });

            NewRC = new RelayCommand(o =>
            {
                CurrentView = NewVM;
            });
        }
    }
}
