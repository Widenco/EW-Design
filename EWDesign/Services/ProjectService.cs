using EWDesign.Components.Models;
using EWDesign.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EWDesign.Services
{
    public class ProjectService
    {
        private static ProjectService _instance;
        public static ProjectService Instance => _instance ??= new ProjectService();

        private ProjectService() { }

        public bool ExportProject(NavBarComponent navbar, BodyComponent body, FooterComponent footer, string filePath)
        {
            try
            {
                // Validar que los componentes no sean null
                if (navbar == null || body == null || footer == null)
                {
                    MessageBox.Show("Error: Los componentes NavBar, Body y Footer son requeridos para exportar.", 
                        "Error de Exportación", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                // Crear el objeto de datos del proyecto
                var projectData = new ProjectData
                {
                    ProjectName = Path.GetFileNameWithoutExtension(filePath),
                    ExportDate = DateTime.Now,
                    Version = "1.0",
                    NavBar = ConvertNavBarToData(navbar),
                    Body = ConvertBodyToData(body),
                    Footer = ConvertFooterToData(footer)
                };

                // Serializar a JSON
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(projectData, Newtonsoft.Json.Formatting.Indented);
                
                // Escribir al archivo
                File.WriteAllText(filePath, json, Encoding.UTF8);
                
                MessageBox.Show($"Proyecto exportado exitosamente a:\n{filePath}", 
                    "Exportación Exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
                
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar el proyecto:\n{ex.Message}", 
                    "Error de Exportación", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public (NavBarComponent navbar, BodyComponent body, FooterComponent footer) ImportProject(string filePath)
        {
            try
            {
                // Validar que el archivo existe
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("El archivo especificado no existe.", 
                        "Error de Importación", MessageBoxButton.OK, MessageBoxImage.Error);
                    return (null, null, null);
                }

                // Leer el archivo JSON
                string json = File.ReadAllText(filePath, Encoding.UTF8);
                
                // Deserializar
                var projectData = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectData>(json);
                
                // Validar la estructura del proyecto
                if (projectData == null || projectData.NavBar == null || projectData.Body == null || projectData.Footer == null)
                {
                    MessageBox.Show("El archivo no contiene un proyecto válido.", 
                        "Error de Importación", MessageBoxButton.OK, MessageBoxImage.Error);
                    return (null, null, null);
                }

                // Convertir de vuelta a componentes
                var navbar = ConvertDataToNavBar(projectData.NavBar);
                var body = ConvertDataToBody(projectData.Body);
                var footer = ConvertDataToFooter(projectData.Footer);

                MessageBox.Show($"Proyecto importado exitosamente:\n{projectData.ProjectName}\nExportado: {projectData.ExportDate:dd/MM/yyyy HH:mm}", 
                    "Importación Exitosa", MessageBoxButton.OK, MessageBoxImage.Information);

                return (navbar, body, footer);
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                MessageBox.Show($"Error al leer el archivo JSON:\n{ex.Message}", 
                    "Error de Importación", MessageBoxButton.OK, MessageBoxImage.Error);
                return (null, null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al importar el proyecto:\n{ex.Message}", 
                    "Error de Importación", MessageBoxButton.OK, MessageBoxImage.Error);
                return (null, null, null);
            }
        }

        private NavBarData ConvertNavBarToData(NavBarComponent navbar)
        {
            return new NavBarData
            {
                Type = navbar.Type,
                Title = navbar.Title?.Text ?? "Mi Producto",
                BackgroundColor = navbar.BrushToHexRGB(navbar.BackgroundColor) ?? "#f5f7fa",
                NavBarElements = navbar.NavbarElementsText?.ToList() ?? new List<string>(),
                Children = ConvertComponentsToData(navbar.Children)
            };
        }

        private BodyData ConvertBodyToData(BodyComponent body)
        {
            System.Diagnostics.Debug.WriteLine("=== DEBUG: ConvertBodyToData ===");
            System.Diagnostics.Debug.WriteLine($"FeatureSectionComponents count: {body.FeatureSectionComponents?.Count ?? 0}");
            System.Diagnostics.Debug.WriteLine($"Children count: {body.Children?.Count ?? 0}");
            
            // DEBUG: Mostrar detalles de los componentes que se van a exportar
            if (body.FeatureSectionComponents != null && body.FeatureSectionComponents.Count > 0)
            {
                System.Diagnostics.Debug.WriteLine("=== FeatureSectionComponents to Export ===");
                for (int i = 0; i < body.FeatureSectionComponents.Count; i++)
                {
                    var comp = body.FeatureSectionComponents[i];
                    System.Diagnostics.Debug.WriteLine($"  Component {i}: Type={comp.Type}, Id={comp.Id}");
                    if (comp is CardComponent card)
                    {
                        System.Diagnostics.Debug.WriteLine($"    Card Title: {card.Title?.Text ?? "NULL"}");
                    }
                }
            }
            
            // NUEVA LÓGICA: Solo exportar componentes de Children que NO estén ya en FeatureSectionComponents
            // Esto evita duplicados en la exportación
            var additionalChildren = new List<ComponentData>();
            if (body.Children != null && body.Children.Count > 0)
            {
                var featureSectionIds = body.FeatureSectionComponents?.Select(c => c.Id).ToHashSet() ?? new HashSet<Guid>();
                var heroSectionIds = body.HeroSectionComponents?.Select(c => c.Id).ToHashSet() ?? new HashSet<Guid>();
                
                foreach (var child in body.Children)
                {
                    if (!featureSectionIds.Contains(child.Id) && !heroSectionIds.Contains(child.Id))
                    {
                        System.Diagnostics.Debug.WriteLine($"Adding additional child to export: {child.Type} with ID: {child.Id}");
                        var childData = ConvertComponentToData(child);
                        additionalChildren.Add(childData);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"Skipping duplicate child: {child.Type} with ID: {child.Id}");
                    }
                }
            }
            
            return new BodyData
            {
                Type = body.Type,
                BackgroundColor = body.BrushToHexRGB(body.BodyBackgroundColor) ?? "#1E1E2F",
                HeroSectionText = body.HeroSectionText?.ToList() ?? new List<string>(),
                FeatureSectionText = body.FeatureSectionText?.ToList() ?? new List<string>(),
                HeroSectionComponents = ConvertComponentsToData(body.HeroSectionComponents),
                FeatureSectionComponents = ConvertComponentsToData(body.FeatureSectionComponents),
                Children = additionalChildren
            };
        }
        private FooterData ConvertFooterToData(FooterComponent footer)
        {
            List<string> footerLinks = new List<string>();

            foreach(var item in footer.Links.MenuItems)
            {
                footerLinks.Add(item.Text);
            }

            return new FooterData
            {
                Type = footer.Type,
                Logo = footer.Logo?.Text ?? "Mi Producto",
                Copyright = footer.Copyright?.Text ?? "© 2025 MiProducto. Todos los derechos reservados.",
                FooterLinks = footerLinks ?? new List<string>(),
                BackgroundColor = footer.Background.ToString() ?? "#FFFFFF",
                Foreground = footer.Foreground.ToString() ?? "#2a2a40",
                Children = ConvertComponentsToData(footer.Children)
            };

        }

        private List<ComponentData> ConvertOnlyModifiedFeatureComponents(ObservableCollection<ComponentModel> components)
        {
            var result = new List<ComponentData>();
            
            if (components == null || components.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("=== DEBUG: ConvertOnlyModifiedFeatureComponents ===");
                System.Diagnostics.Debug.WriteLine("Components is null or empty, returning empty list");
                return result;
            }

            System.Diagnostics.Debug.WriteLine($"=== DEBUG: ConvertOnlyModifiedFeatureComponents ===");
            System.Diagnostics.Debug.WriteLine($"Total components: {components.Count}");
            System.Diagnostics.Debug.WriteLine($"All are CardComponents: {components.All(c => c is CardComponent)}");

            // EXPORTAR TODOS los componentes que están presentes en el lienzo
            // No importa si son por defecto o modificados, exportar todo lo que está visible
            System.Diagnostics.Debug.WriteLine($"Exporting ALL {components.Count} components from the canvas");
            foreach (var component in components)
            {
                if (component is CardComponent card)
                {
                    System.Diagnostics.Debug.WriteLine($"Exporting CardComponent: {card.Title?.Text ?? "NULL"}");
                    var componentData = ConvertCardComponentToData(card);
                    result.Add(componentData);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Exporting non-CardComponent: {component.Type}");
                    var componentData = ConvertComponentToData(component);
                    result.Add(componentData);
                }
            }
            
            return result;
        }
        
        private ComponentData ConvertComponentToData(ComponentModel component)
        {
            var componentData = new ComponentData
            {
                Id = component.Id,
                Type = component.Type,
                Children = ConvertComponentsToData(component.Children)
            };

            // Convertir propiedades específicas según el tipo de componente
            if (component is TextComponent textComponent)
            {
                componentData.Text = textComponent.Text;
                componentData.ForegroundColor = textComponent.BrushToHexRGB(textComponent.ForeGround);
                componentData.FontSize = textComponent.FontSize.ToString();
                componentData.FontWeight = textComponent.FontWeight.ToString();
                componentData.TextAlignment = textComponent.TextAlignment.ToString();
                componentData.TextWrap = textComponent.TextWrap.ToString();
                componentData.Margin = textComponent.Margin.ToString();
            }
            else if (component is ButtonComponent buttonComponent)
            {
                componentData.Text = buttonComponent.TextContent?.Text ?? "";
                componentData.BackgroundColor = buttonComponent.BrushToHexRGB(buttonComponent.Background);
                componentData.ForegroundColor = buttonComponent.BrushToHexRGB(buttonComponent.Foreground);
                componentData.FontSize = buttonComponent.FontSize.ToString();
                componentData.FontWeight = buttonComponent.FontWeight.ToString();
            }
            else if (component is CardComponent cardComponent)
            {
                return ConvertCardComponentToData(cardComponent);
            }

            return componentData;
        }
        
        private ComponentData ConvertCardComponentToData(CardComponent card)
        {
            var componentData = new ComponentData
            {
                Id = card.Id,
                Type = card.Type,
                Children = new List<ComponentData>() // Inicializar lista vacía
            };

            // Convertir propiedades específicas del CardComponent
            componentData.BackgroundColor = card.BrushToHexRGB(card.Background);
            componentData.Width = card.Width;
            componentData.Height = card.Height;
            
            // Agregar SOLO los componentes hijos del Card (Title y Body)
            if (card.Title != null)
            {
                var titleData = new ComponentData
                {
                    Id = card.Title.Id,
                    Type = card.Title.Type,
                    Text = card.Title.Text,
                    TextWrap = card.Title.TextWrap.ToString(),
                    TextAlignment = card.Title.TextAlignment.ToString(),
                    ForegroundColor = card.Title.BrushToHexRGB(card.Title.ForeGround),
                    FontSize = card.Title.FontSize.ToString(),
                    FontWeight = card.Title.FontWeight.ToString(),
                    Margin = card.Title.Margin.ToString(),
                    Children = new List<ComponentData>()
                };
                componentData.Children.Add(titleData);
            }
            
            if (card.Body != null)
            {
                var bodyData = new ComponentData
                {
                    Id = card.Body.Id,
                    Type = card.Body.Type,
                    Text = card.Body.Text,
                    TextWrap = card.Body.TextWrap.ToString(),
                    TextAlignment = card.Body.TextAlignment.ToString(),
                    ForegroundColor = card.Body.BrushToHexRGB(card.Body.ForeGround),
                    FontSize = card.Body.FontSize.ToString(),
                    FontWeight = card.Body.FontWeight.ToString(),
                    Margin = card.Body.Margin.ToString(),
                    Children = new List<ComponentData>()
                };
                componentData.Children.Add(bodyData);
            }
            
            // NO agregar los componentes de card.Children aquí
            // porque ya agregamos Title y Body arriba
            
            return componentData;
        }

        private List<ComponentData> ConvertComponentsToData(ObservableCollection<ComponentModel> components)
        {
            var result = new List<ComponentData>();
            
            if (components == null)
                return result;
                
            foreach (var component in components)
            {
                var componentData = new ComponentData
                {
                    Id = component.Id,
                    Type = component.Type,
                    Children = new List<ComponentData>() // Inicializar lista vacía
                };

                // Convertir propiedades específicas según el tipo
                switch (component)
                {
                    case TextComponent textComponent:
                        componentData.Text = textComponent.Text;
                        componentData.BackgroundColor = null; // TextComponent no tiene BackgroundColor
                        componentData.ForegroundColor = textComponent.BrushToHexRGB(textComponent.ForeGround);
                        componentData.FontSize = textComponent.FontSize.ToString();
                        componentData.FontWeight = textComponent.FontWeight.ToString();
                        componentData.TextAlignment = textComponent.TextAlignment.ToString();
                        componentData.TextWrap = textComponent.TextWrap.ToString();
                        componentData.Margin = textComponent.Margin.ToString();
                        break;

                    case ButtonComponent buttonComponent:
                        componentData.Text = buttonComponent.TextContent?.Text ?? "";
                        componentData.BackgroundColor = buttonComponent.BrushToHexRGB(buttonComponent.Background);
                        componentData.ForegroundColor = buttonComponent.BrushToHexRGB(buttonComponent.Foreground);
                        componentData.FontSize = buttonComponent.FontSize.ToString();
                        componentData.FontWeight = buttonComponent.FontWeight.ToString();
                        break;

                    case CardComponent cardComponent:
                        componentData.BackgroundColor = cardComponent.BrushToHexRGB(cardComponent.Background);
                        componentData.Width = cardComponent.Width;
                        componentData.Height = cardComponent.Height;
                        
                        // Agregar SOLO los componentes hijos del Card (Title y Body)
                        if (cardComponent.Title != null)
                        {
                            var titleData = new ComponentData
                            {
                                Id = cardComponent.Title.Id,
                                Type = cardComponent.Title.Type,
                                Text = cardComponent.Title.Text,
                                TextWrap = cardComponent.Title.TextWrap.ToString(),
                                TextAlignment = cardComponent.Title.TextAlignment.ToString(),
                                ForegroundColor = cardComponent.Title.BrushToHexRGB(cardComponent.Title.ForeGround),
                                FontSize = cardComponent.Title.FontSize.ToString(),
                                FontWeight = cardComponent.Title.FontWeight.ToString(),
                                Margin = cardComponent.Title.Margin.ToString(),
                                Children = new List<ComponentData>()
                            };
                            componentData.Children.Add(titleData);
                        }
                        
                        if (cardComponent.Body != null)
                        {
                            var bodyData = new ComponentData
                            {
                                Id = cardComponent.Body.Id,
                                Type = cardComponent.Body.Type,
                                Text = cardComponent.Body.Text,
                                TextWrap = cardComponent.Body.TextWrap.ToString(),
                                TextAlignment = cardComponent.Body.TextAlignment.ToString(),
                                ForegroundColor = cardComponent.Body.BrushToHexRGB(cardComponent.Body.ForeGround),
                                FontSize = cardComponent.Body.FontSize.ToString(),
                                FontWeight = cardComponent.Body.FontWeight.ToString(),
                                Margin = cardComponent.Body.Margin.ToString(),
                                Children = new List<ComponentData>()
                            };
                            componentData.Children.Add(bodyData);
                        }
                        
                        // NO agregar los componentes de cardComponent.Children aquí
                        // porque ya agregamos Title y Body arriba
                        break;

                    case MenuComponent menuComponent:
                        componentData.ForegroundColor = menuComponent.BrushToHexRGB(menuComponent.ForeGround);
                        break;
                }

                result.Add(componentData);
            }

            return result;
        }

        private NavBarComponent ConvertDataToNavBar(NavBarData data)
        {
            var navbar = new NavBarComponent();
            
            if (data.Title != null)
            {
                navbar.Title.Text = data.Title;
            }
            
            if (data.BackgroundColor != null)
            {
                navbar.BackgroundColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString(data.BackgroundColor));
            }
            
            if (data.NavBarElements != null)
            {
                navbar.NavbarElementsText = new ObservableCollection<string>(data.NavBarElements);
            }
            
            // Cargar los componentes hijos que se arrastraron al NavBar
            if (data.Children != null)
            {
                navbar.Children = ConvertDataToComponents(data.Children);
            }

            return navbar;
        }

        private BodyComponent ConvertDataToBody(BodyData data)
        {
            System.Diagnostics.Debug.WriteLine("=== DEBUG: ConvertDataToBody (Import) ===");
            System.Diagnostics.Debug.WriteLine($"Importing HeroSectionComponents count: {data.HeroSectionComponents?.Count ?? 0}");
            System.Diagnostics.Debug.WriteLine($"Importing FeatureSectionComponents count: {data.FeatureSectionComponents?.Count ?? 0}");
            System.Diagnostics.Debug.WriteLine($"Importing Children count: {data.Children?.Count ?? 0}");
            
            // DEBUG: Mostrar detalles de los FeatureSectionComponents
            if (data.FeatureSectionComponents != null && data.FeatureSectionComponents.Count > 0)
            {
                System.Diagnostics.Debug.WriteLine("=== FeatureSectionComponents Details ===");
                for (int i = 0; i < data.FeatureSectionComponents.Count; i++)
                {
                    var comp = data.FeatureSectionComponents[i];
                    System.Diagnostics.Debug.WriteLine($"  Component {i}: Type={comp.Type}, Id={comp.Id}");
                    if (comp.Children != null && comp.Children.Count > 0)
                    {
                        System.Diagnostics.Debug.WriteLine($"    Children count: {comp.Children.Count}");
                        foreach (var child in comp.Children)
                        {
                            System.Diagnostics.Debug.WriteLine($"      Child: Type={child.Type}, Text={child.Text}");
                        }
                    }
                }
            }
            
            var body = new BodyComponent();
            
            if (data.BackgroundColor != null)
            {
                body.BodyBackgroundColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString(data.BackgroundColor));
            }
            
            if (data.HeroSectionText != null)
            {
                body.HeroSectionText = new ObservableCollection<string>(data.HeroSectionText);
            }
            
            if (data.FeatureSectionText != null)
            {
                body.FeatureSectionText = new ObservableCollection<string>(data.FeatureSectionText);
            }
            
            if (data.HeroSectionComponents != null)
            {
                body.HeroSectionComponents = ConvertDataToComponents(data.HeroSectionComponents);
                System.Diagnostics.Debug.WriteLine($"Imported HeroSectionComponents: {body.HeroSectionComponents.Count}");
                
                // También agregar los componentes de hero a la colección Children
                foreach (var component in body.HeroSectionComponents)
                {
                    body.Children.Add(component);
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No HeroSectionComponents to import");
            }
            
            if (data.FeatureSectionComponents != null)
            {
                body.FeatureSectionComponents = ConvertDataToComponents(data.FeatureSectionComponents);
                System.Diagnostics.Debug.WriteLine($"Imported FeatureSectionComponents: {body.FeatureSectionComponents.Count}");
                
                // DEBUG: Verificar que los CardComponents se crearon correctamente
                foreach (var component in body.FeatureSectionComponents)
                {
                    if (component is CardComponent card)
                    {
                        System.Diagnostics.Debug.WriteLine($"CardComponent created: Title='{card.Title?.Text ?? "NULL"}', Body='{card.Body?.Text ?? "NULL"}'");
                    }
                }
                
                // También agregar los componentes de features a la colección Children
                foreach (var component in body.FeatureSectionComponents)
                {
                    body.Children.Add(component);
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No FeatureSectionComponents to import");
            }
            
            // Cargar los componentes hijos que se arrastraron al Body
            if (data.Children != null)
            {
                var additionalChildren = ConvertDataToComponents(data.Children);
                System.Diagnostics.Debug.WriteLine($"Imported additional Children: {additionalChildren.Count}");
                foreach (var component in additionalChildren)
                {
                    body.Children.Add(component);
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No additional Children to import");
            }

            System.Diagnostics.Debug.WriteLine($"Final BodyComponent state:");
            System.Diagnostics.Debug.WriteLine($"  HeroSectionComponents: {body.HeroSectionComponents?.Count ?? 0}");
            System.Diagnostics.Debug.WriteLine($"  FeatureSectionComponents: {body.FeatureSectionComponents?.Count ?? 0}");
            System.Diagnostics.Debug.WriteLine($"  Children: {body.Children?.Count ?? 0}");

            return body;
        }
        private FooterComponent ConvertDataToFooter(FooterData data)
        {
            var footer = new FooterComponent();

            if(data.Logo != null)
            {
                footer.Logo.Text = data.Logo;
            }

            if(data.Copyright != null)
            {
                footer.Copyright.Text = data.Copyright;
            }

            if(data.Foreground != null)
            {
                footer.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(data.Foreground));
            }

            if(data.BackgroundColor != null)
            {
                footer.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(data.BackgroundColor));
            }

            if (data.FooterLinks != null)
            {
                footer.Links = new MenuComponent(data.FooterLinks.ToArray(), false, footer.Foreground);
            }

            if(data.Children != null)
            {
                footer.Children = ConvertDataToComponents(data.Children);
            }

            return footer;
        }

        private ObservableCollection<ComponentModel> ConvertDataToComponents(List<ComponentData> componentsData)
        {
            var components = new ObservableCollection<ComponentModel>();
            
            foreach (var data in componentsData)
            {
                ComponentModel component = null;
                
                switch (data.Type?.ToLower())
                {
                    case "text":
                    case "title text":
                    case "body text":
                    case "title-text":
                    case "subtitle-text":
                    case "navbar-title-text":
                    case "footer-title-text":
                    case "copyright-text":
                        component = new TextComponent();
                        if (component is TextComponent textComponent)
                        {
                            textComponent.Text = data.Text ?? "";
                            if (data.TextWrap != null)
                            {
                                switch (data.TextWrap.ToLower())
                                {
                                    case "wrap": textComponent.TextWrap = TextWrapping.Wrap; break;
                                    case "nowrap": textComponent.TextWrap = TextWrapping.NoWrap; break;
                                }
                            }
                            if (data.TextAlignment != null)
                            {
                                switch (data.TextAlignment.ToLower())
                                {
                                    case "left": textComponent.TextAlignment = TextAlignment.Left; break;
                                    case "center": textComponent.TextAlignment = TextAlignment.Center; break;
                                    case "right": textComponent.TextAlignment = TextAlignment.Right; break;
                                }
                            }
                            if (data.ForegroundColor != null)
                                textComponent.ForeGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString(data.ForegroundColor));
                            if (data.FontSize != null && double.TryParse(data.FontSize, out double fontSize))
                                textComponent.FontSize = fontSize;
                            if (data.FontWeight != null)
                            {
                                switch (data.FontWeight.ToLower())
                                {
                                    case "black": textComponent.FontWeight = FontWeights.Black; break;
                                    case "bold": textComponent.FontWeight = FontWeights.Bold; break;
                                    case "semibold": textComponent.FontWeight = FontWeights.SemiBold; break;
                                    case "normal": textComponent.FontWeight = FontWeights.Normal; break;
                                    default: textComponent.FontWeight = FontWeights.Normal; break;
                                }
                            }
                            if (data.Margin != null)
                            {
                                var margins = data.Margin.Split(',');
                                double marginTop = Double.Parse(margins[1]);
                                double marginBottom = Double.Parse(margins[3]);
                                double marginLeft = Double.Parse(margins[0]);
                                double marginRight = Double.Parse(margins[2]);
                                textComponent.Margin = new Thickness(marginLeft, marginTop, marginRight, marginBottom);
                            }
                        }
                        break;

                    case "button":
                        component = new ButtonComponent();
                        if (component is ButtonComponent buttonComponent)
                        {
                            if (data.Text != null)
                                buttonComponent.TextContent.Text = data.Text;
                            if (data.BackgroundColor != null)
                                buttonComponent.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(data.BackgroundColor));
                            if (data.ForegroundColor != null)
                                buttonComponent.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(data.ForegroundColor));
                            if (data.FontSize != null && double.TryParse(data.FontSize, out double fontSize))
                                buttonComponent.FontSize = fontSize;
                            if (data.FontWeight != null)
                            {
                                switch (data.FontWeight.ToLower())
                                {
                                    case "black": buttonComponent.FontWeight = FontWeights.Black; break;
                                    case "bold": buttonComponent.FontWeight = FontWeights.Bold; break;
                                    case "semibold": buttonComponent.FontWeight = FontWeights.SemiBold; break;
                                    case "normal": buttonComponent.FontWeight = FontWeights.Normal; break;
                                    default: buttonComponent.FontWeight = FontWeights.SemiBold; break;
                                }
                            }
                        }
                        break;

                    case "card":
                        component = new CardComponent();
                        if (component is CardComponent cardComponent)
                        {
                            // Restaurar propiedades básicas del Card
                            if (data.BackgroundColor != null)
                                cardComponent.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(data.BackgroundColor));
                            if (data.Width > 0)
                                cardComponent.Width = data.Width;
                            if (data.Height > 0)
                                cardComponent.Height = data.Height;
                            
                            // Restaurar los componentes hijos del Card (Title y Body)
                            if (data.Children != null && data.Children.Count > 0)
                            {
                                // Buscar el Title por tipo específico
                                var titleData = data.Children.FirstOrDefault(c => 
                                    c.Type?.ToLower() == "title text" || 
                                    c.Type?.ToLower() == "title-text" ||
                                    c.Type?.ToLower().Contains("title") == true);
                                
                                if (titleData != null)
                                {
                                    cardComponent.Title.Text = titleData.Text ?? "Sample Title";
                                    if (titleData.TextWrap != null)
                                    {
                                        switch (titleData.TextWrap.ToLower())
                                        {
                                            case "wrap": cardComponent.Title.TextWrap = TextWrapping.Wrap; break;
                                            case "nowrap": cardComponent.Title.TextWrap = TextWrapping.NoWrap; break;
                                        }
                                    }
                                    if (titleData.TextAlignment != null)
                                    {
                                        switch (titleData.TextAlignment.ToLower())
                                        {
                                            case "left": cardComponent.Title.TextAlignment = TextAlignment.Left; break;
                                            case "center": cardComponent.Title.TextAlignment = TextAlignment.Center; break;
                                            case "right": cardComponent.Title.TextAlignment = TextAlignment.Right; break;
                                        }
                                    }
                                    if (titleData.ForegroundColor != null)
                                        cardComponent.Title.ForeGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString(titleData.ForegroundColor));
                                    if (titleData.FontSize != null && double.TryParse(titleData.FontSize, out double titleFontSize))
                                        cardComponent.Title.FontSize = titleFontSize;
                                    if (titleData.FontWeight != null)
                                    {
                                        switch (titleData.FontWeight.ToLower())
                                        {
                                            case "black": cardComponent.Title.FontWeight = FontWeights.Black; break;
                                            case "bold": cardComponent.Title.FontWeight = FontWeights.Bold; break;
                                            case "semibold": cardComponent.Title.FontWeight = FontWeights.SemiBold; break;
                                            case "normal": cardComponent.Title.FontWeight = FontWeights.Normal; break;
                                            default: cardComponent.Title.FontWeight = FontWeights.SemiBold; break;
                                        }
                                    }
                                    if (titleData.Margin != null)
                                    {
                                        var margins = titleData.Margin.Split(',');
                                        double marginTop = Double.Parse(margins[1]);
                                        double marginBottom = Double.Parse(margins[3]);
                                        double marginLeft = Double.Parse(margins[0]);
                                        double marginRight = Double.Parse(margins[2]);
                                        cardComponent.Title.Margin = new Thickness(marginLeft, marginTop, marginRight, marginBottom);
                                    }
                                }
                                
                                // Buscar el Body por tipo específico
                                var bodyData = data.Children.FirstOrDefault(c => 
                                    c.Type?.ToLower() == "body text" || 
                                    c.Type?.ToLower() == "body-text" ||
                                    c.Type?.ToLower().Contains("body") == true);
                                
                                if (bodyData != null)
                                {
                                    cardComponent.Body.Text = bodyData.Text ?? "This is a sample body text, here is where your text goes...";
                                    if (bodyData.TextWrap != null)
                                    {
                                        switch (bodyData.TextWrap.ToLower())
                                        {
                                            case "wrap": cardComponent.Body.TextWrap = TextWrapping.Wrap; break;
                                            case "nowrap": cardComponent.Body.TextWrap = TextWrapping.NoWrap; break;
                                        }
                                    }
                                    if (bodyData.TextAlignment != null)
                                    {
                                        switch (bodyData.TextAlignment.ToLower())
                                        {
                                            case "left": cardComponent.Body.TextAlignment = TextAlignment.Left; break;
                                            case "center": cardComponent.Body.TextAlignment = TextAlignment.Center; break;
                                            case "right": cardComponent.Body.TextAlignment = TextAlignment.Right; break;
                                        }
                                    }
                                    if (bodyData.ForegroundColor != null)
                                        cardComponent.Body.ForeGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString(bodyData.ForegroundColor));
                                    if (bodyData.FontSize != null && double.TryParse(bodyData.FontSize, out double bodyFontSize))
                                        cardComponent.Body.FontSize = bodyFontSize;
                                    if (bodyData.FontWeight != null)
                                    {
                                        switch (bodyData.FontWeight.ToLower())
                                        {
                                            case "black": cardComponent.Body.FontWeight = FontWeights.Black; break;
                                            case "bold": cardComponent.Body.FontWeight = FontWeights.Bold; break;
                                            case "semibold": cardComponent.Body.FontWeight = FontWeights.SemiBold; break;
                                            case "normal": cardComponent.Body.FontWeight = FontWeights.Normal; break;
                                            default: cardComponent.Body.FontWeight = FontWeights.Normal; break;
                                        }
                                    }
                                    if (bodyData.Margin != null)
                                    {
                                        var margins = bodyData.Margin.Split(',');
                                        double marginTop = Double.Parse(margins[1]);
                                        double marginBottom = Double.Parse(margins[3]);
                                        double marginLeft = Double.Parse(margins[0]);
                                        double marginRight = Double.Parse(margins[2]);
                                        cardComponent.Body.Margin = new Thickness(marginLeft, marginTop, marginRight, marginBottom);
                                    }
                                }
                                
                                // Restaurar componentes hijos adicionales del Card (si los hay)
                                var additionalChildren = data.Children.Where(c => 
                                    c.Type?.ToLower() != "title text" && 
                                    c.Type?.ToLower() != "title-text" &&
                                    c.Type?.ToLower() != "body text" && 
                                    c.Type?.ToLower() != "body-text" &&
                                    !c.Type?.ToLower().Contains("title") == true &&
                                    !c.Type?.ToLower().Contains("body") == true).ToList();
                                
                                if (additionalChildren.Any())
                                {
                                    var additionalComponents = ConvertDataToComponents(additionalChildren);
                                    foreach (var additionalComponent in additionalComponents)
                                    {
                                        cardComponent.Children.Add(additionalComponent);
                                    }
                                }
                            }
                        }
                        break;

                    case "menu":
                    case "footer-menu":
                        component = new MenuComponent();
                        if (component is MenuComponent menuComponent)
                        {
                            if (data.ForegroundColor != null)
                                menuComponent.ForeGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString(data.ForegroundColor));
                        }
                        break;
                }

                if (component != null)
                {
                    component.Id = data.Id;
                    component.Type = data.Type;
                    
                    // Convertir hijos recursivamente
                    if (data.Children != null)
                    {
                        component.Children = ConvertDataToComponents(data.Children);
                    }
                    
                    components.Add(component);
                }
            }

            return components;
        }
    }
}
