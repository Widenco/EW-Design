﻿using EWDesign.Components.Models;
using EWDesign.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Core;

namespace EWDesign.Components.Views
{
    /// <summary>
    /// Interaction logic for NavBarView.xaml
    /// </summary>
    public partial class NavBarView : UserControl
    {
        public NavBarComponent Model { get; }

        public NavBarView(NavBarComponent model)
        {
            InitializeComponent();
            Model = model;
            this.DataContext = model;

        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Model.IsEditing = false;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Escape)
            {
                Model.IsEditing = false;
            }
        }

        private void TextBlock_Click(object sender, MouseButtonEventArgs e)
        {
            Model.IsEditing = true;
        }
    }
}
