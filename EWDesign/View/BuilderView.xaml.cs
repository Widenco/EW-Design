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

                UIElement newElement = null;
                switch (componentType)
                {
                    case "NavBar":
                        newElement = new TextBlock { Text = "NavBar", Foreground = Brushes.White, FontSize = 24 };
                        break;
                    case "Body":
                        newElement = new TextBlock { Text = "Body", Foreground = Brushes.White, FontSize = 24 };
                        break;
                    case "SideBar":
                        newElement = new TextBlock { Text = "SideBar", Foreground = Brushes.White, FontSize = 24 };
                        break;
                    case "Footer":
                        newElement = new TextBlock { Text = "Footer", Foreground = Brushes.White, FontSize = 24 };
                        break;
                }

                if (newElement != null)
                {
                    var panel = sender as StackPanel;
                    panel.Children.Add(newElement);
                }
            }
        }
    }
}
