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

        public bool ExportProject(NavBarComponent navbar, BodyComponent body, string filePath)
        {
            try
            {
                // Validar que los componentes no sean null
                if (navbar == null || body == null)
                {
                    MessageBox.Show("Error: Los componentes NavBar y Body son requeridos para exportar.", 
                        "Error de Exportación", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                // Crear el objeto de datos del proyecto
                var projectData = new ProjectData
                {
                    ProjectName = "Mi Proyecto",
                    ExportDate = DateTime.Now,
                    Version = "1.0",
                    NavBar = ConvertNavBarToData(navbar),
                    Body = ConvertBodyToData(body)
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

        public (NavBarComponent navbar, BodyComponent body) ImportProject(string filePath)
        {
            try
            {
                // Validar que el archivo existe
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("El archivo especificado no existe.", 
                        "Error de Importación", MessageBoxButton.OK, MessageBoxImage.Error);
                    return (null, null);
                }

                // Leer el archivo JSON
                string json = File.ReadAllText(filePath, Encoding.UTF8);
                
                // Deserializar
                var projectData = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectData>(json);
                
                // Validar la estructura del proyecto
                if (projectData == null || projectData.NavBar == null || projectData.Body == null)
                {
                    MessageBox.Show("El archivo no contiene un proyecto válido.", 
                        "Error de Importación", MessageBoxButton.OK, MessageBoxImage.Error);
                    return (null, null);
                }

                // Convertir de vuelta a componentes
                var navbar = ConvertDataToNavBar(projectData.NavBar);
                var body = ConvertDataToBody(projectData.Body);

                MessageBox.Show($"Proyecto importado exitosamente:\n{projectData.ProjectName}\nExportado: {projectData.ExportDate:dd/MM/yyyy HH:mm}", 
                    "Importación Exitosa", MessageBoxButton.OK, MessageBoxImage.Information);

                return (navbar, body);
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                MessageBox.Show($"Error al leer el archivo JSON:\n{ex.Message}", 
                    "Error de Importación", MessageBoxButton.OK, MessageBoxImage.Error);
                return (null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al importar el proyecto:\n{ex.Message}", 
                    "Error de Importación", MessageBoxButton.OK, MessageBoxImage.Error);
                return (null, null);
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
                NavBarElementsColor = navbar.NavBarElementsColor ?? "#3a3f47",
                // Agregar los componentes hijos que se arrastraron al NavBar
                Children = ConvertComponentsToData(navbar.Children)
            };
        }

        private BodyData ConvertBodyToData(BodyComponent body)
        {
            return new BodyData
            {
                Type = body.Type,
                BackgroundColor = body.BrushToHexRGB(body.BodyBackgroundColor) ?? "#1E1E2F",
                HeroSectionText = body.HeroSectionText?.ToList() ?? new List<string>(),
                FeatureSectionText = body.FeatureSectionText?.ToList() ?? new List<string>(),
                HeroSectionComponents = ConvertComponentsToData(body.HeroSectionComponents),
                FeatureSectionComponents = ConvertComponentsToData(body.FeatureSectionComponents),
                // Agregar los componentes hijos que se arrastraron al Body
                Children = ConvertComponentsToData(body.Children)
            };
        }

        private List<ComponentData> ConvertComponentsToData(ObservableCollection<ComponentModel> components)
        {
            var result = new List<ComponentData>();
            
            foreach (var component in components)
            {
                var componentData = new ComponentData
                {
                    Id = component.Id,
                    Type = component.Type,
                    Children = ConvertComponentsToData(component.Children)
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
                        
                        // Agregar los componentes hijos del Card (Title y Body)
                        if (cardComponent.Title != null)
                        {
                            var titleData = new ComponentData
                            {
                                Id = cardComponent.Title.Id,
                                Type = cardComponent.Title.Type,
                                Text = cardComponent.Title.Text,
                                ForegroundColor = cardComponent.Title.BrushToHexRGB(cardComponent.Title.ForeGround),
                                FontSize = cardComponent.Title.FontSize.ToString(),
                                FontWeight = cardComponent.Title.FontWeight.ToString(),
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
                                ForegroundColor = cardComponent.Body.BrushToHexRGB(cardComponent.Body.ForeGround),
                                FontSize = cardComponent.Body.FontSize.ToString(),
                                FontWeight = cardComponent.Body.FontWeight.ToString(),
                                Children = new List<ComponentData>()
                            };
                            componentData.Children.Add(bodyData);
                        }
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
            
            if (data.NavBarElementsColor != null)
            {
                navbar.NavBarElementsColor = data.NavBarElementsColor;
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
            }
            
            if (data.FeatureSectionComponents != null)
            {
                body.FeatureSectionComponents = ConvertDataToComponents(data.FeatureSectionComponents);
            }
            
            // Cargar los componentes hijos que se arrastraron al Body
            if (data.Children != null)
            {
                body.Children = ConvertDataToComponents(data.Children);
            }

            return body;
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
                        component = new TextComponent();
                        if (component is TextComponent textComponent)
                        {
                            textComponent.Text = data.Text ?? "";
                            // TextComponent no tiene BackgroundColor, se omite
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
                            if (data.BackgroundColor != null)
                                cardComponent.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(data.BackgroundColor));
                            cardComponent.Width = data.Width;
                            cardComponent.Height = data.Height;
                            
                            // Restaurar los componentes hijos del Card (Title y Body)
                            if (data.Children != null && data.Children.Count >= 2)
                            {
                                // El primer hijo es el Title
                                var titleData = data.Children.FirstOrDefault(c => c.Type?.ToLower().Contains("title") == true);
                                if (titleData != null)
                                {
                                    cardComponent.Title.Text = titleData.Text ?? "Sample Title";
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
                                }
                                
                                // El segundo hijo es el Body
                                var bodyData = data.Children.FirstOrDefault(c => c.Type?.ToLower().Contains("body") == true);
                                if (bodyData != null)
                                {
                                    cardComponent.Body.Text = bodyData.Text ?? "This is a sample body text, here is where your text goes...";
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
                                }
                            }
                        }
                        break;

                    case "menu":
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
