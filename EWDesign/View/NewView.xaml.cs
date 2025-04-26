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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EWDesign.View
{
    /// <summary>
    /// Interaction logic for NewView.xaml
    /// </summary>
    public partial class NewView : UserControl
    {
        public NewView()
        {
            InitializeComponent();
            var viewModel = new NewViewModel();

            viewModel.OpenBuilder += OnOpenBuilder;

            DataContext = viewModel;
        }

        private void OnOpenBuilder()
        {
            var main = Application.Current.MainWindow;
            main.Hide();

            var BuilderWindow = new BuilderView();
            BuilderWindow.ShowDialog();

            main.Show();
        }
    }
}
