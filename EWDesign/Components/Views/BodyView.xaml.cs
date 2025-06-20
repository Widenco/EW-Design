using EWDesign.Components.Models;
using EWDesign.Interfaces;
using EWDesign.Model;
using EWDesign.View;
using EWDesign.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class BodyView : UserControl, IComponentView
    {
        public BodyComponent BodyModel { get; }

        public ComponentModel Model => BodyModel;

        public event EventHandler ComponentRemoveEvent;

        public BodyView(BodyComponent model)
        {
            InitializeComponent();
            BodyModel = model;
            DataContext = model;
            InitTemplateComponents();
        }

        //Inicializando y añadiendo componentes a la plantilla por codigo
        public void InitTemplateComponents()
        {
            var HeroSectionItems = new ObservableCollection<IComponentView>
            {

                new TextView(new TextComponent
                {
                    Text = BodyModel.HeroSectionText[0],
                    Margin = new Thickness(0, 0, 0, 24),
                    FontSize = 48,
                    ForeGround = "White",
                    TextWrap = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Center
                }),

                new TextView(new TextComponent
                {
                    Text = BodyModel.HeroSectionText[1],
                    Margin = new Thickness(0, 0, 0, 40),
                    FontSize = 20,
                    ForeGround = "#DADADA",
                    TextWrap = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Center
                }),

                new ButtonView(new ButtonComponent(BodyModel.HeroSectionText[2]))
            };

            var FeatureSectionItems = new ObservableCollection<IComponentView>
            {
                new CardView(new CardComponent(BodyModel.FeatureSectionText[0], BodyModel.FeatureSectionText[1])),
                new CardView(new CardComponent(BodyModel.FeatureSectionText[2], BodyModel.FeatureSectionText[3])),
                new CardView(new CardComponent(BodyModel.FeatureSectionText[4], BodyModel.FeatureSectionText[5]))
            };

            foreach (var item in HeroSectionItems)
            {
                item.ComponentRemoveEvent += (s, e) => RemoveComponent(item);
                HeroSectionDropArea.Children.Add((UIElement)item);
            }

            foreach (var item in FeatureSectionItems)
            {
                item.ComponentRemoveEvent += (s, e) => RemoveComponent(item);
                FeatureSectionDropArea.Children.Add((UIElement)item);
            }

        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            OpenComponentEditor(this.Model);
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            ComponentRemoveEvent?.Invoke(this, EventArgs.Empty);
        }

        public void RemoveComponent(IComponentView componentView)
        {
            if (HeroSectionDropArea.Children.Contains((UserControl)componentView))
            {
                HeroSectionDropArea.Children.Remove((UserControl)componentView);
            }
            else
            {
                FeatureSectionDropArea.Children.Remove((UserControl)componentView);
            }
        }

        private void OpenComponentEditor(ComponentModel model)
        {
            var dialog = new ComponentEditorDialog(model);
            dialog.Owner = Window.GetWindow(this);
            dialog.ShowDialog();
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BuilderViewModel.Instance.SelectedComponent = null;
            e.Handled = true;
        }
    }
}
