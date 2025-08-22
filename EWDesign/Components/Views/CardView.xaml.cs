using EWDesign.Components.Models;
using EWDesign.Core.Code_Generator;
using EWDesign.Interfaces;
using EWDesign.Model;
using EWDesign.View;
using EWDesign.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for CardView.xaml
    /// </summary>
    public partial class CardView : UserControl, IComponentView
    {
        public CardComponent CardModel { get; }

        public ComponentModel Model => CardModel;

        public event EventHandler ComponentRemoveEvent;

        public CardView(CardComponent model)
        {
            InitializeComponent();
            CardModel = model;
            this.DataContext = model;
            InitComponents();
        }


        public void InitComponents()
        {
            System.Diagnostics.Debug.WriteLine($"=== DEBUG: CardView.InitComponents ===");
            System.Diagnostics.Debug.WriteLine($"CardModel.Title: {(CardModel.Title != null ? "EXISTS" : "NULL")}");
            System.Diagnostics.Debug.WriteLine($"CardModel.Body: {(CardModel.Body != null ? "EXISTS" : "NULL")}");
            System.Diagnostics.Debug.WriteLine($"CardModel.Children.Count: {CardModel.Children?.Count ?? 0}");
            
            // Limpiar componentes existentes
            Card.Children.Clear();
            
            // Si Title y Body existen, crear vistas para ellos
            if (CardModel.Title != null)
            {
                var titleView = new TextView(CardModel.Title);
                Card.Children.Add(titleView);
                System.Diagnostics.Debug.WriteLine($"Added Title view: {CardModel.Title.Text}");
            }
            
            if (CardModel.Body != null)
            {
                var bodyView = new TextView(CardModel.Body);
                Card.Children.Add(bodyView);
                System.Diagnostics.Debug.WriteLine($"Added Body view: {CardModel.Body.Text}");
            }
            
            // Agregar componentes hijos adicionales si existen
            if (CardModel.Children != null && CardModel.Children.Count > 2)
            {
                System.Diagnostics.Debug.WriteLine($"Adding {CardModel.Children.Count - 2} additional children");
                foreach (var child in CardModel.Children.Skip(2))
                {
                    IComponentView childView = CreateComponentView(child);
                    if (childView != null)
                    {
                        Card.Children.Add((UIElement)childView);
                        System.Diagnostics.Debug.WriteLine($"Added additional child: {child.Type}");
                    }
                }
            }
            
            System.Diagnostics.Debug.WriteLine($"Final Card.Children.Count: {Card.Children.Count}");
        }
        
        // Método auxiliar para crear vistas de componentes hijos
        private IComponentView CreateComponentView(ComponentModel component)
        {
            switch (component.Type?.ToLower())
            {
                case "text":
                case "title text":
                case "body text":
                case "title-text":
                case "subtitle-text":
                    return new TextView((TextComponent)component);
                case "button":
                    return new ButtonView((ButtonComponent)component);
                case "card":
                    return new CardView((CardComponent)component);
                case "menu":
                    return new MenuView((MenuComponent)component);
                default:
                    return null;
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

        private void OpenComponentEditor(ComponentModel model)
        {
            var dialog = new ComponentEditorDialog(model);
            dialog.Owner = Window.GetWindow(this);
            dialog.ShowDialog();
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BuilderViewModel.Instance.SelectedComponent = this.Model;
            e.Handled = true;
        }
    }
}
