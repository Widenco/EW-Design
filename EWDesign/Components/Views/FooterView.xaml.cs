using EWDesign.Components.Models;
using EWDesign.Interfaces;
using EWDesign.Model;
using EWDesign.View;
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

namespace EWDesign.Components.Views
{
    /// <summary>
    /// Interaction logic for FooterView.xaml
    /// </summary>
    public partial class FooterView : UserControl, IComponentView
    {
        public FooterComponent FooterModel { get; }
        public ComponentModel Model => FooterModel;
        public event EventHandler ComponentRemoveEvent;

        public FooterView(FooterComponent model, bool isImporting = false)
        {
            InitializeComponent();
            FooterModel = model;
            this.DataContext = model;

            if (!isImporting)
            {
                InitTemplateComponents();
            }else
            {
                LoadImportedComponents();
            }
        }
        public void InitTemplateComponents()
        {
            var footerLogo = new TextView(FooterModel.Logo);
            footerLogo.ComponentRemoveEvent += (s, e) => RemoveComponent(footerLogo);
            LogoDropArea.Children.Add(footerLogo);

            var copyrightText = new TextView(FooterModel.Copyright);
            copyrightText.ComponentRemoveEvent += (s, e) => RemoveComponent(copyrightText);
            CopyrightDropArea.Children.Add(copyrightText);

            var footerLinks = new MenuView(FooterModel.Links);
            footerLinks.ComponentRemoveEvent += (s, e) => RemoveComponent(footerLinks);
            LinksDropArea.Children.Add(footerLinks);
        }
        public void LoadImportedComponents()
        {
            LogoDropArea.Children.Clear();
            CopyrightDropArea.Children.Clear();
            LinksDropArea.Children.Clear();

            foreach (var child in Model.Children)
            {
                IComponentView componentView = null;

                switch (child.Type?.ToLower())
                {
                    case "footer-title-text":
                    case "text":
                    case "copyright-text":
                        componentView = new TextView((TextComponent)child);
                        break;
                    case "footer-menu":
                    case "menu":
                        componentView = new MenuView((MenuComponent)child);
                        break;
                    

                }

                if (componentView != null)
                {
                    componentView.ComponentRemoveEvent += (s, e) => RemoveComponent(componentView);

                    if (child.Type?.ToLower() == "copyright-text")
                    {
                        CopyrightDropArea.Children.Add((UserControl)componentView);
                    }
                    else if (child.Type?.ToLower() == "footer-title-text")
                    {
                        LogoDropArea.Children.Add((UserControl)componentView);
                    }else if (child.Type?.ToLower() == "footer-menu")
                    {
                        LinksDropArea.Children.Add((UserControl)componentView);
                    }
                }
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
        public void RemoveComponent(IComponentView componentView)
        {
            LogoDropArea.Children.Remove((UserControl)componentView);
            FooterModel.RemoveChild(componentView.Model);
        }
        private void FooterDropArea_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(ComponentPaletteItem)) is ComponentPaletteItem item)
            {
                ComponentModel component = null;

                if (item.ComponentFactory is Type type && typeof(ComponentModel).IsAssignableFrom(type))
                {
                    component = (ComponentModel)Activator.CreateInstance(type);
                    if (item.DisplayName == "Title Text")
                    {
                        component.Type = "Footer-Title-Text";
                    }else if(item.DisplayName == "Menu")
                    {
                        component.Type = "Footer-Menu";
                    }
                        
                }

                IComponentView newElement = null;

                if (component.Type.ToLower() == "footer-title-text")
                {
                    newElement = new TextView((TextComponent)component);
                    newElement.ComponentRemoveEvent += (s, a) => RemoveComponent(newElement);
                }else if(component.Type.ToLower() == "footer-menu")
                {
                    newElement = new MenuView((MenuComponent)component);
                    newElement.ComponentRemoveEvent += (s, a) => RemoveComponent(newElement);
                }
                else
                {
                    MessageBox.Show("No se puede insertar otro componente que no sea un Titulo o links.", "Componente no soportado", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (component != null)
                {
                    var panel = sender as StackPanel;
                    bool duplicated = false;

                    foreach (var child in Model.Children)
                    {
                        if (child.Type == component.Type)
                            duplicated = true;
                    }

                    if (duplicated)
                    {
                        MessageBox.Show("Solo se puede colocar un titulo o una lista de links", "Demasiados Componentes", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    panel.Children.Add((UIElement)newElement);
                    Model.AddChild(newElement.Model);
                }
            }
        }
        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BuilderViewModel.Instance.SelectedComponent = this.Model;
        }
    }
}
