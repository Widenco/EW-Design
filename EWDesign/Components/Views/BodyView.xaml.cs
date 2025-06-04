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
            var HeroSectionItems = new ObservableCollection<TextView>
            {
                new TextView(new TextComponent
                {
                    Text = Model.HeroSectionText[1], //Orden invertido para evitar mal orden de UI
                    Margin = new Thickness(0, 0, 0, 40),
                    FontSize = "20",
                    ForeGround = "#DADADA",
                    TextWrap = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Center
                }),

                new TextView(new TextComponent
                {
                    Text = Model.HeroSectionText[0],
                    Margin = new Thickness(0, 0, 0, 24),
                    FontSize = "48",
                    ForeGround = "White",
                    TextWrap = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Center
                })
            };

            foreach (var item in HeroSectionItems)
            {
                //Insert en vez de Add para mantener el orden por ahora
                HeroSectionDropArea.Children.Insert(0,item); 
            }

        }
    }
}
