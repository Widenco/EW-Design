using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp1.Core;
using Newtonsoft.Json;

namespace EWDesign.Model
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class ComponentModel : ObservableObject
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonProperty]
        public string Type { get; set; }
        public virtual bool DelegateContextMenu { get; set; } = false;

        [JsonProperty]
        public ObservableCollection<ComponentModel> Children = new ObservableCollection<ComponentModel>();

        public void AddChild(ComponentModel child)
        {
            Children.Add(child);
        }

        public void RemoveChild(ComponentModel child)
        {
            if (Children.Contains(child))
            {
                Children.Remove(child);
            }
        }

        public void MoveChild(int oldIndex, int newIndex)
        {
            if (oldIndex < 0 || oldIndex >= Children.Count || newIndex < 0 || newIndex >= Children.Count)
                return;

            var item = Children[oldIndex];
            Children.RemoveAt(oldIndex);
            Children.Insert(newIndex, item);
        }

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

        public string BrushToHexRGB(Brush brush)
        {
            if (brush is SolidColorBrush solidColorBrush)
            {
                var color = solidColorBrush.Color;
                return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
            }
            return null;
        }
    }
}
