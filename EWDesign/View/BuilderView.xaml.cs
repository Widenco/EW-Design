﻿using EWDesign.Components.Views;
using EWDesign.ViewModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace EWDesign.View
{
    /// <summary>
    /// Interaction logic for BuilderView.xaml
    /// </summary>
    public partial class BuilderView : Window
    {
        public BuilderView()
        {
            InitializeComponent();
            this.DataContext = new BuilderViewModel();
        }
        private void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var listBox = sender as ListBox;
            var selectedItem = listBox.SelectedItem;
            if(selectedItem != null)
            {
                DragDrop.DoDragDrop(listBox, selectedItem, DragDropEffects.Copy);
            }
        }

        private void DropArea_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                string componentType = (string)e.Data.GetData(DataFormats.StringFormat);

                UserControl newElement = null;
                switch (componentType)
                {
                    case "NavBar":
                        newElement = new Components.Views.NavBarView(new Components.Models.NavBarComponent());
                        break;
                    case "Body":
                        newElement = new Components.Views.BodyView(new Components.Models.BodyComponent());
                        break;
                    case "SideBar":
                        newElement = new Components.Views.NavBarView(new Components.Models.NavBarComponent());
                        break;
                    case "Footer":
                        newElement = new Components.Views.NavBarView(new Components.Models.NavBarComponent());
                        break;
                }

                if (newElement != null)
                {
                    var panel = sender as StackPanel;
                    bool canAdd = false;

                    if(panel.Children.Count == 0)
                    {
                        canAdd = true;
                    } else
                    {

                        if (panel.Children[0] is NavBarView)
                        {
                            foreach (var component in panel.Children)
                            {
                                if (newElement.GetType() == component.GetType())
                                {
                                    canAdd = false;
                                    break;
                                }
                                else
                                {
                                    canAdd = true;
                                }
                            }
                        }
                        else
                        {
                         if(newElement is NavBarView)
                            {
                                canAdd = false;
                            }   
                        }

                    }

                    if (canAdd)
                    {
                        panel.Children.Add(newElement);
                    }
                    
                }
            }
        }
    }
}
