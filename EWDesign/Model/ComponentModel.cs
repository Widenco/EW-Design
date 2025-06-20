using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp1.Core;

namespace EWDesign.Model
{
    public abstract class ComponentModel : ObservableObject
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Type { get; set; }
        public virtual bool DelegateContextMenu { get; set; } = false;

        private bool _isSelected = false;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public virtual void SetSelected(bool s)
        {
            IsSelected = s;
        }
    }
}
