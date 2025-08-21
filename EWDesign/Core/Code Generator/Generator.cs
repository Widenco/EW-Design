using EWDesign.Components.Models;
using EWDesign.Interfaces;
using EWDesign.Model;
using EWDesign.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace EWDesign.Core.Code_Generator
{
    public class Generator
    {

        private Dictionary<string, string> _styleToClassMap = new Dictionary<string, string>();
        private StringBuilder _cssBuilder = new StringBuilder();
        private int _classCounter = 1;

        public string BuildComponentCode(ComponentModel component)
        {
            var sb = new StringBuilder();

            if (component.Type.ToLower() == "navbar")
            {
                var navbar = component as NavBarComponent;

                sb.AppendLine("<nav class='navbar'>\n" + "<div class='navbar-container'>");

                var navbarCSS = ".navbar {\r\n" +
                    $"  background-color: {navbar.BrushToHexRGB(navbar.BackgroundColor)};\r\n" +
                    "  height: 72px;\r\n" +
                    "  width: 100%;\r\n" +
                    "  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.05);\r\n" +
                    "  position: fixed;\r\n" +
                    "  top: 0;\r\n" +
                    "  z-index: 1000;\r\n}\r\n" +
                    ".navbar-container {\r\n" +
                    "  max-width: 1200px;\r\n" +
                    "  height: 100%;\r\n  margin: 0 auto;\r\n" +
                    "  padding: 0 24px;\r\n" +
                    "  display: flex;\r\n" +
                    "  align-items: center;\r\n" +
                    "  justify-content: space-between;\r\n}\r\n";

                _cssBuilder.AppendLine(navbarCSS);

                if (component.Children.Count > 0)
                {
                    foreach (var child in component.Children)
                    {
                        sb.AppendLine(BuildComponentCode(child));
                    }
                }

                sb.AppendLine("</div>\n" + "</nav>");

            }
            else if (component.Type.ToLower() == "body")
            {
                var body = component as BodyComponent;

                sb.AppendLine("<main class='landing-body'>");

                var bodyCSS = "body {\r\n" +
                    "  margin: 0;\r\n" +
                    "  font-family: \"Inter\", sans-serif;\r\n" +
                    $"  background-color: {body.BrushToHexRGB(body.BodyBackgroundColor)};\r\n}}\r\n" +
                    ".landing-body {\r\n" +
                    "  padding-top: 72px;\r\n" +
                    "  display: flex;\r\n" +
                    "  flex-direction: column;\r\n" +
                    "  align-items: center;\r\n" +
                    "  padding: 72px 24px;\r\n}\r\n";

                _cssBuilder.AppendLine(bodyCSS);

                if (component.Children.Count > 0)
                {
                    if(body.HeroSectionComponents.Count > 0)
                    {
                        _cssBuilder.AppendLine(
                            ".hero {\r\n" +
                            "  text-align: center;\r\n" +
                            "  max-width: 800px;\r\n" +
                            "  margin-bottom: 96px;\r\n}\r\n");

                        sb.AppendLine("<section class=\"hero\">");

                        foreach(var heroComponent in body.HeroSectionComponents)
                        {
                            sb.AppendLine(BuildComponentCode(heroComponent));
                        }

                        sb.AppendLine("</section>");

                    }

                    if(body.FeatureSectionComponents.Count > 0)
                    {
                        _cssBuilder.AppendLine(
                            ".features {\r\n" +
                            "  display: flex;\r\n" +
                            "  flex-direction: row;\r\n" +
                            "  gap: 48px;\r\n" +
                            "  max-width: 1000px;\r\n" +
                            "  justify-content: center;\r\n" +
                            "  flex-wrap: wrap;\r\n}\r\n");

                        sb.AppendLine("<section class=\"features\">");

                        foreach (var featuresComponent in body.FeatureSectionComponents)
                        {
                            sb.AppendLine(BuildComponentCode(featuresComponent));
                        }

                        sb.AppendLine("</section>");

                    }
                    /*foreach (var child in component.Children)
                    {
                        sb.AppendLine(BuildComponentCode(child));
                    }*/
                }

                sb.AppendLine("</main>");
            }
            else if (component is ICodeGeneratable)
            {
                var className = GetOrCreateClassName(component);
                var gen = component as ICodeGeneratable;
                sb.AppendLine(gen.HTMLContent(className.ToLower()));
            }

            return sb.ToString();
        }

        public string BuildHTMLPageCode(ObservableCollection<IComponentView> page)
        {
            var html = new StringBuilder();
            bool hasBody = false;
            _cssBuilder.AppendLine("@import url(\"https://fonts.googleapis.com/css2?family=Inter:wght@400;600&display=swap\");");

            html.AppendLine("<!DOCTYPE html>\r\n" +
                "<html>\r\n" +
                "  <head>\r\n" +
                "    <meta charset=\"utf-8\" />\r\n" +
                "    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" />\r\n" +
                "    <title>Page Title</title>\r\n" +
                "    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" />\r\n" +
                "    <link rel=\"stylesheet\" type=\"text/css\" media=\"screen\" href=\"style.css\" />\r\n" +
                "  </head>\r\n" +
                "  <body>\r\n");

            foreach (var item in page)
            {
                if (item.Model.Type == "Body")
                    hasBody = true;

                html.AppendLine(BuildComponentCode(item.Model));
            }

            html.AppendLine("  </body>\r\n</html>");

            if(!hasBody)
                _cssBuilder.AppendLine(
                        "body {\r\n" +
                        "  margin: 0;\r\n" +
                        "  font-family: \"Inter\", sans-serif;\r\n");

            return html.ToString();
        }

        public string BuildCSSPageCode()
        {
            if (_cssBuilder != null)
            {
                return _cssBuilder.ToString();
            }
            else
            {
                throw new NullReferenceException("No se ha construido el codigo CSS");
            }
        }

        private string GetOrCreateClassName(ComponentModel component)
        {
            // Serializamos las propiedades relevantes para identificar estilos
            string styleKey = SerializeStyles(component);

            if(styleKey != null)
            {
                // Si ya existe este estilo, usamos la misma clase
                if (_styleToClassMap.TryGetValue(styleKey, out string existingClass))
                    return existingClass;

                // Usamos la propiedad Type como base
                string baseClass = component.Type;

                // Si ya existe otra clase de este tipo pero con estilo distinto, generamos sufijo
                if (_styleToClassMap.Values.Any(v => v == baseClass))
                {
                    baseClass = $"{component.Type}-{_classCounter++}";
                }

                // Guardamos el mapeo y generamos el CSS
                _styleToClassMap[styleKey] = baseClass;
                var componentCSS = component as ICodeGeneratable;
                _cssBuilder.AppendLine(componentCSS.CSSContent(baseClass.ToLower()));

                return baseClass;
            }

            return null;
        }
    
        private string SerializeStyles(ComponentModel component)
        {
            switch (component.Type)
            {
                case "Navbar-Title-Text":
                case "Title-Text":
                case "Subtitle-Text":

                    TextComponent textModel = component as TextComponent;
                    return $"FontSize={textModel.FontSize};" +
                   $"Foreground={textModel.ForeGround};" +
                   $"FontWeigh={textModel.FontWeight};";

                case "Button":
                    ButtonComponent buttonModel = component as ButtonComponent;
                    return $"FontSize={buttonModel.FontSize};" +
                   $"Foreground={buttonModel.Foreground};" +
                   $"Background={buttonModel.Background};" +
                   $"FontWeigh={buttonModel.FontWeight};";

                case "Card":
                    CardComponent cardModel = component as CardComponent;
                    return $"Background={cardModel.Background};" +
                   $"Width={cardModel.Width};";

                case "Menu":
                    MenuComponent menuModel = component as MenuComponent;
                    return $"FontSize={menuModel.FontSize};" +
                   $"Foreground={menuModel.ForeGround};";
            }

            return null;
        }

        public void GenerateFiles(ObservableCollection<IComponentView> page)
        {
            var browser = new System.Windows.Forms.FolderBrowserDialog();
            browser.Description = "Seleccione la ruta de exportacion";
            var result = browser.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var htmlContent = BuildHTMLPageCode(page);
                var cssContent = BuildCSSPageCode();
                var folderName = browser.SelectedPath;

                try
                {
                    File.WriteAllText(folderName + "/index.html", htmlContent);
                    File.WriteAllText(folderName + "/style.css", cssContent);
                    MessageBox.Show("Proyecto exportado correctamente", "Proyecto Exportado", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (DirectoryNotFoundException ex)
                {
                    MessageBox.Show($"La ruta proporcionada no se ha encontrado. \n {ex.Message}", "Error al crear archivos", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        public void SaveProject(ObservableCollection<IComponentView> page)
        {
            var interpreter = new Interpreter();
            var browser = new System.Windows.Forms.SaveFileDialog();
            browser.Title = "Save Project";
            browser.Filter = "Json files (*.json)|*.json";
            var result = browser.ShowDialog();
            var components = new ObservableCollection<ComponentModel>();

            foreach (var item in page)
            {
                components.Add(item.Model);
            }

            if(result == System.Windows.Forms.DialogResult.OK)
            {
                var projectName = Path.GetFileNameWithoutExtension(browser.FileName);
                var project = new ProjectMetadata
                {
                    ProjectName = projectName,
                    Components = components.ToList()
                };
                
                interpreter.SaveProject(project, browser.FileName);
            }
        }
    }
}
