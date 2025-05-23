﻿using EWDesign.Components.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EWDesign.Components.Views
{
    /// <summary>
    /// Interaction logic for BodyView.xaml
    /// </summary>
    public partial class BodyView : UserControl
    {
        public BodyComponent Model { get; } 
        public BodyView(BodyComponent model)
        {
            InitializeComponent();
            Model = model;
            DataContext = model;
        }
    }
}
