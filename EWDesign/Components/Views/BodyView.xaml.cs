using EWDesign.Components.Models;
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
    public partial class BodyView : UserControl
    {
        public BodyComponent Model { get; } 
        public BodyView(BodyComponent model)
        {
            InitializeComponent();
            Model = model;
            DataContext = model;
            InitTemplateComponents();
        }

        //Inicializando y añadiendo componentes a la plantilla por codigo
        public void InitTemplateComponents()
        {
            var HeroSectionItems = new ObservableCollection<UserControl>
            {

                new TextView(new TextComponent
                {
                    Text = Model.HeroSectionText[0],
                    Margin = new Thickness(0, 0, 0, 24),
                    FontSize = 48,
                    ForeGround = "White",
                    TextWrap = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Center
                }),

                new TextView(new TextComponent
                {
                    Text = Model.HeroSectionText[1],
                    Margin = new Thickness(0, 0, 0, 40),
                    FontSize = 20,
                    ForeGround = "#DADADA",
                    TextWrap = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Center
                }),

                new ButtonView(new ButtonComponent(Model.HeroSectionText[2]))
            };

            var FeatureSectionItems = new ObservableCollection<UIElement>
            {
                new CardView(new CardComponent(Model.FeatureSectionText[0], Model.FeatureSectionText[1])),
                new CardView(new CardComponent(Model.FeatureSectionText[2], Model.FeatureSectionText[3])),
                new CardView(new CardComponent(Model.FeatureSectionText[4], Model.FeatureSectionText[5]))
            };

            foreach (var item in HeroSectionItems)
            {
                HeroSectionDropArea.Children.Add(item);
            }

            foreach (var item in FeatureSectionItems)
            {
                FeatureSectionDropArea.Children.Add(item);
            }

        }
    }
}
