using EWDesign.Components.Models;
using EWDesign.Interfaces;
using EWDesign.Model;
using EWDesign.View;
using EWDesign.ViewModel;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
            }
            else
            {
                LoadImportedComponents();
            }
        }

        public void InitTemplateComponents()
        {
            CreateDefaultComponentsForSection("Description");
            CreateDefaultComponentsForSection("Links");
            CreateDefaultComponentsForSection("Contact");
            CreateDefaultComponentsForSection("Social");
        }

        public void LoadImportedComponents()
        {
            LogoDropArea.Children.Clear();
            DescriptionDropArea.Children.Clear();
            ContactTitleDropArea.Children.Clear();
            ContactDropArea.Children.Clear();
            SocialTitleDropArea.Children.Clear();
            IconsDropArea.Children.Clear();
            LinksTitleDropArea.Children.Clear();
            LinksDropArea.Children.Clear();

            // Cargando Componentes por seccion

            // Seccion de Descripcion
            foreach (var component in FooterModel.DescriptionSection)
            {
                if(component.Type.ToLower() == "footer-title-text")
                {
                    var componentView = new TextView((TextComponent)component);
                    LogoDropArea.Children.Add((UIElement)componentView);
                }

                if(component.Type.ToLower() == "footer-description-text")
                {
                    var componentView = new TextView((TextComponent)component);
                    DescriptionDropArea.Children.Add((UIElement)componentView);
                }
            }

            // Seccion de Links
            foreach (var component in FooterModel.LinksSection)
            {
                if (component.Type.ToLower() == "footer-title-text")
                {
                    var componentView = new TextView((TextComponent)component);
                    LinksTitleDropArea.Children.Add((UIElement)componentView);
                }

                if (component.Type.ToLower() == "footer-menu")
                {
                    var componentView = new MenuView((MenuComponent)component);
                    LinksDropArea.Children.Add((UIElement)componentView);
                }

                if(component.Type.ToLower() == "footer-description-text")
                {
                    var componentView = new TextView((TextComponent)component);
                    LinksDropArea.Children.Add((UIElement)componentView);
                }
            }

            // Seccion de Contacto
            foreach (var component in FooterModel.ContactSection)
            {
                if (component.Type.ToLower() == "footer-title-text")
                {
                    var componentView = new TextView((TextComponent)component);
                    ContactTitleDropArea.Children.Add((UIElement)componentView);
                }

                if (component.Type.ToLower() == "footer-description-text")
                {
                    var componentView = new TextView((TextComponent)component);
                    ContactDropArea.Children.Add((UIElement)componentView);
                }
            }

            // Seccion de Redes Sociales
            foreach (var component in FooterModel.IconsSection)
            {
                if (component.Type.ToLower() == "footer-title-text")
                {
                    var componentView = new TextView((TextComponent)component);
                    SocialTitleDropArea.Children.Add((UIElement)componentView);
                }

                if (component.Type.ToLower() == "footer-icon" || component.Type.ToLower() == "icon")
                {
                    var componentView = new IconView((IconComponent)component);
                    IconsDropArea.Children.Add((UIElement)componentView);
                }
            }

            // DESPUÉS, si hay componentes personalizados en Children, agregarlos
            /* if (Model.Children.Any())
             {
                 foreach (var child in Model.Children)
                 {
                     IComponentView componentView = null;

                     switch (child.Type?.ToLower())
                     {
                         case "footer-title-text":
                         case "text":
                         case "footer-description-text":
                             componentView = new TextView((TextComponent)child);
                             break;
                         case "footer-menu":
                         case "menu":
                             componentView = new MenuView((MenuComponent)child);
                             break;
                         case "icon":
                         case "footer-icon":
                             componentView = new IconView((IconComponent)child);
                             break;
                     }

                     if (componentView != null)
                     {
                         componentView.ComponentRemoveEvent += (s, e) => RemoveComponent(componentView);

                         // Distribuir componentes en las áreas correspondientes
                         switch (child.Type?.ToLower())
                         {
                             case "footer-title-text":
                                 LogoDropArea.Children.Add((UserControl)componentView);
                                 break;
                             case "footer-description-text":
                                 DescriptionDropArea.Children.Add((UserControl)componentView);
                                 break;
                             case "footer-contact-title-text":
                                 ContactTitleDropArea.Children.Add((UserControl)componentView);
                                 break;
                             case "footer-contact-text":
                                 ContactDropArea.Children.Add((UserControl)componentView);
                                 break;
                             case "footer-social-title-text":
                                 SocialTitleDropArea.Children.Add((UserControl)componentView);
                                 break;
                             case "footer-links-title-text":
                                 LinksTitleDropArea.Children.Add((UserControl)componentView);
                                 break;
                             case "footer-menu":
                                 LinksDropArea.Children.Add((UserControl)componentView);
                                 break;
                             case "footer-icon":
                                 IconsDropArea.Children.Add((UserControl)componentView);
                                 break;
                         }
                     }
                 }
             }*/
        }

        private void CreateDefaultComponentsForSection(string section)
        {
            switch (section)
            {
                case "Description":
                    {
                        // Logo
                        var footerLogo = new TextView(new TextComponent
                        {
                            Type = "Footer-Title-Text",
                            Text = "MiProducto",
                            FontSize = 28,
                            ForeGround = FooterModel.Foreground,
                            FontWeight = FontWeights.Bold,
                            Margin = new Thickness(0, 0, 0, 16)
                        });
                        footerLogo.ComponentRemoveEvent += (s, e) => RemoveComponent(footerLogo);
                        LogoDropArea.Children.Add(footerLogo);
                        FooterModel.DescriptionSection.Add(footerLogo.Model);
                        FooterModel.AddChild(footerLogo.Model);

                        // Descripción
                        var descriptionText = new TextView(new TextComponent
                        {
                            Type = "Footer-Description-Text",
                            Text = "Transformando ideas en experiencias digitales excepcionales. Creamos soluciones innovadoras que conectan con tu audiencia.",
                            FontSize = 14,
                            ForeGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B0B0B0")),
                            Margin = new Thickness(0, 0, 0, 24),
                            TextWrap = TextWrapping.Wrap
                        });
                        descriptionText.ComponentRemoveEvent += (s, e) => RemoveComponent(descriptionText);
                        DescriptionDropArea.Children.Add(descriptionText);
                        FooterModel.DescriptionSection.Add(descriptionText.Model);
                        FooterModel.AddChild(descriptionText.Model);
                    }
                    break;
                case "Links":
                    {
                        // Titulo del menu
                        var footerLinksTitle = new TextView(new TextComponent 
                        {
                            Type = "Footer-Title-Text",
                            Text = "Enlaces Rapidos",
                            FontSize = 16,
                            ForeGround = FooterModel.Foreground,
                            FontWeight = FontWeights.SemiBold,
                            Margin = new Thickness(0, 0, 0, 12)
                        });
                        footerLinksTitle.ComponentRemoveEvent += (s, e) => RemoveComponent(footerLinksTitle);
                        LinksTitleDropArea.Children.Add(footerLinksTitle);
                        FooterModel.LinksSection.Add(footerLinksTitle.Model);
                        FooterModel.AddChild(footerLinksTitle.Model);

                        // Links del menú
                        var footerLinks = new MenuView(new MenuComponent(FooterModel.Foreground, false, "Footer-Menu", Orientation.Vertical));
                        footerLinks.ComponentRemoveEvent += (s, e) => RemoveComponent(footerLinks);
                        LinksDropArea.Children.Add(footerLinks);
                        FooterModel.LinksSection.Add(footerLinks.Model);
                        FooterModel.AddChild(footerLinks.Model);
                    }
                    break;
                case "Contact":
                    {
                        // Titulo de contacto
                        var contactTitle = new TextView(new TextComponent 
                        {
                            Type = "Footer-Title-Text",
                            Text = "Contacto",
                            FontSize = 16,
                            ForeGround = FooterModel.Foreground,
                            FontWeight = FontWeights.SemiBold,
                            Margin = new Thickness(0, 0, 0, 12)
                        });
                        contactTitle.ComponentRemoveEvent += (s, e) => RemoveComponent(contactTitle);
                        ContactTitleDropArea.Children.Add(contactTitle);
                        FooterModel.ContactSection.Add(contactTitle.Model);
                        FooterModel.AddChild(contactTitle.Model);

                        // Información de contacto
                        var contactText = new TextView(new TextComponent 
                        {
                            Type = "Footer-Description-Text",
                            Text = "📧 info@miproducto.com\n📞 +1 (555) 123-4567\n📍 123 Calle Principal, Ciudad",
                            FontSize = 14,
                            ForeGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B0B0B0")),
                            Margin = new Thickness(0, 0, 0, 16),
                            TextWrap = TextWrapping.Wrap
                        });
                        contactText.ComponentRemoveEvent += (s, e) => RemoveComponent(contactText);
                        ContactDropArea.Children.Add(contactText);
                        FooterModel.ContactSection.Add(contactText.Model);
                        FooterModel.AddChild(contactText.Model);
                    }
                    break;
                case "Social":
                    {
                        // Título de redes sociales
                        var socialTitleText = new TextView(new TextComponent
                        {
                            Type = "Footer-Title-Text",
                            Text = "Síguenos",
                            FontSize = 16,
                            ForeGround = FooterModel.Foreground ,
                            FontWeight = FontWeights.SemiBold,
                            Margin = new Thickness(0, 0, 0, 12)
                        });
                        socialTitleText.ComponentRemoveEvent += (s, e) => RemoveComponent(socialTitleText);
                        SocialTitleDropArea.Children.Add(socialTitleText);
                        FooterModel.IconsSection.Add(socialTitleText.Model);
                        FooterModel.AddChild(socialTitleText.Model);

                        string[] icons = {"📘", "🐦", "📷" };

                        //Iconos de redes sociales
                        foreach (var icon in icons)
                        {
                            var socialIcon = new IconView(new IconComponent
                            {
                                Type = "Footer-Icon",
                                IconText = icon
                            });
                            socialIcon.ComponentRemoveEvent += (s, e) => RemoveComponent(socialIcon);
                            IconsDropArea.Children.Add(socialIcon);
                            FooterModel.IconsSection.Add(socialIcon.Model);
                            FooterModel.AddChild(socialIcon.Model);
                        }
                    }
                    break;
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
            // Remover de todas las áreas posibles
            LogoDropArea.Children.Remove((UserControl)componentView);
            DescriptionDropArea.Children.Remove((UserControl)componentView);
            LinksTitleDropArea.Children.Remove((UserControl)componentView);
            LinksDropArea.Children.Remove((UserControl)componentView);
            ContactTitleDropArea.Children.Remove((UserControl)componentView);
            ContactDropArea.Children.Remove((UserControl)componentView);
            SocialTitleDropArea.Children.Remove((UserControl)componentView);
            IconsDropArea.Children.Remove((UserControl)componentView);

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

                    // Asignar tipos específicos según el área de drop
                    var dropArea = sender as StackPanel;
                    if (dropArea == LogoDropArea)
                    {
                        component.Type = "Footer-Title-Text";
                    }
                    else if (dropArea == DescriptionDropArea)
                    {
                        component.Type = "Footer-Description-Text";
                    }
                    else if (dropArea == ContactTitleDropArea)
                    {
                        component.Type = "Footer-Title-Text";
                    }
                    else if (dropArea == ContactDropArea)
                    {
                        component.Type = "Footer-Description-Text";
                    }
                    else if (dropArea == SocialTitleDropArea)
                    {
                        component.Type = "Footer-Title-Text";
                    }
                    else if(dropArea == LinksTitleDropArea)
                    {
                        component.Type = "Footer-Title-Text";
                    }
                    else if (dropArea == LinksDropArea)
                    {
                        component.Type = "Footer-Menu";
                    }
                }

                IComponentView newElement = null;

                if (component.Type.ToLower().Contains("text"))
                {
                    newElement = new TextView((TextComponent)component);
                    newElement.ComponentRemoveEvent += (s, a) => RemoveComponent(newElement);
                }
                else if (component.Type.ToLower().Contains("menu"))
                {
                    newElement = new MenuView((MenuComponent)component);
                    newElement.ComponentRemoveEvent += (s, a) => RemoveComponent(newElement);
                }
                else
                {
                    MessageBox.Show("Solo se pueden insertar componentes de texto o menú en el footer.", "Componente no soportado", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (component != null)
                {
                    var panel = sender as StackPanel;
                    bool duplicated = false;

                    // Verificar duplicados por tipo
                    foreach (var child in Model.Children)
                    {
                        if (child.Type == component.Type)
                            duplicated = true;
                    }

                    if (duplicated)
                    {
                        MessageBox.Show("Ya existe un componente de este tipo en el footer.", "Componente duplicado", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    panel.Children.Add((UIElement)newElement);
                    AddComponentToSection(newElement, panel);
                    Model.AddChild(newElement.Model);
                }
            }
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BuilderViewModel.Instance.SelectedComponent = this.Model;
        }

        private void AddComponentToSection(IComponentView component, StackPanel section)
        {
            if(section == LogoDropArea || section == DescriptionDropArea)
            {
                FooterModel.DescriptionSection.Add(component.Model);
            }
            if(section == LinksTitleDropArea || section == LinksDropArea)
            {
                FooterModel.LinksSection.Add(component.Model);
            }
            if(section == ContactTitleDropArea || section == ContactDropArea)
            {
                FooterModel.ContactSection.Add(component.Model);
            }
            if(section == SocialTitleDropArea || section == IconsDropArea)
            {
                FooterModel.IconsSection.Add(component.Model);
            }
        }

        private void IconsDropArea_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(ComponentPaletteItem)) is ComponentPaletteItem item)
            {
                ComponentModel component = null;

                if (item.ComponentFactory is Type type && typeof(ComponentModel).IsAssignableFrom(type))
                {
                    component = (ComponentModel)Activator.CreateInstance(type);

                    var dropArea = sender as StackPanel;
                    if (dropArea == IconsDropArea)
                    {
                        component.Type = "Footer-Icon";
                    }

                    IComponentView newElement = null;

                    if (component.Type.ToLower().Contains("icon"))
                    {
                        newElement = new IconView((IconComponent)component);
                        newElement.ComponentRemoveEvent += (s, a) => RemoveComponent(newElement);
                    }
                    else
                    {
                        MessageBox.Show("Solo se pueden insertar iconos.", "Componente no soportado", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (component != null)
                    {
                        var panel = sender as StackPanel;
                        panel.Children.Add((UIElement)newElement);
                        AddComponentToSection(newElement, panel);
                        Model.AddChild(newElement.Model);
                    }

                }

            }
        }
    }
}
