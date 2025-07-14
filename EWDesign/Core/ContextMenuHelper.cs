using EWDesign.Model;
using EWDesign.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace EWDesign.Core
{
    class ContextMenuHelper
    {
        public static void ShowParentContextMenu(FrameworkElement origin, ComponentModel model)
        {
            if (!model.DelegateContextMenu)
            {
                if(origin.ContextMenu != null)
                {
                    origin.ContextMenu.PlacementTarget = origin;
                    origin.ContextMenu.IsOpen = true;
                }
                return;
            }


            DependencyObject parent = VisualTreeHelper.GetParent(origin);

            while (parent != null)
            {
                if (parent is FrameworkElement fe &&
                    fe.DataContext is ComponentModel parentModel &&
                    parentModel != model &&
                    !parentModel.DelegateContextMenu)
                {
                    if (fe.ContextMenu != null)
                    {
                        fe.ContextMenu.PlacementTarget = fe;
                        fe.ContextMenu.IsOpen = true;
                        BuilderViewModel.Instance.SelectedComponent = parentModel;
                        return;
                    }
                }

                parent = VisualTreeHelper.GetParent(parent);
            }
        }
    }
}

