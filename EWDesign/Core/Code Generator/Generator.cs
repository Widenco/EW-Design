using EWDesign.Components.Models;
using EWDesign.Interfaces;
using EWDesign.Model;
using EWDesign.View;
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

                    if (body.HeroSectionComponents.Count > 0)
                    {
                        _cssBuilder.AppendLine(
                            ".hero {\r\n" +
                            "  text-align: center;\r\n" +
                            "  max-width: 800px;\r\n" +
                            "  margin-bottom: 96px;\r\n}\r\n");

                        sb.AppendLine("<section class=\"hero\">");

                        foreach (var heroComponent in body.HeroSectionComponents)
                        {
                            sb.AppendLine(BuildComponentCode(heroComponent));
                        }

                        sb.AppendLine("</section>");

                    }

                    if (body.FeatureSectionComponents.Count > 0)
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

                sb.AppendLine("</main>");
            }
            else if (component.Type.ToLower() == "footer")
            {
                var footer = component as FooterComponent;

                sb.AppendLine("<footer class=\"footer\">\r\n" +
                    "      <div class=\"footer-container\">\r\n" +
                    "        <div class=\"footer-main\">\r\n");

                var footerCSS = ".footer {\r\n" +
                    $"  background-color: {footer.BrushToHexRGB(footer.Background)};\r\n" +
                    $"  color: {footer.BrushToHexRGB(footer.Foreground)};\r\n" +
                    "  padding: 0;\r\n" +
                    "  margin-top: 96px;\r\n" +
                    "  font-family: \"Inter\", sans-serif;\r\n}\r\n\r\n" +
                    ".footer-container {\r\n" +
                    "  max-width: 1200px;\r\n" +
                    "  margin: 0 auto;\r\n" +
                    "  padding: 40px;\r\n}\r\n\r\n" +
                    ".footer-main {\r\n" +
                    "  display: grid;\r\n" +
                    "  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));\r\n" +
                    "  gap: 40px;\r\n" +
                    "  margin-bottom: 32px;\r\n}\r\n\r\n" +
                    ".footer-section {\r\n" +
                    "  display: flex;\r\n" +
                    "  flex-direction: column;\r\n" +
                    "  gap: 16px;\r\n}\r\n\r\n" +
                    ".footer-brand {\r\n  gap: 24px;\r\n}\r\n" +
                    ".footer-logo {\r\n" +
                    "  margin-bottom: 16px;\r\n}\r\n\r\n" +
                    ".footer-description {\r\n" +
                    "  line-height: 1.6;\r\n}\r\n" +
                    ".social-icons {\r\n" +
                    "  display: flex;\r\n" +
                    "  gap: 12px;\r\n" +
                    "  margin-top: 12px;\r\n}\r\n" +
                    "@media (max-width: 768px) {\r\n" +
                    "  .footer-main {\r\n" +
                    "    grid-template-columns: 1fr;\r\n" +
                    "    gap: 24px;\r\n  }\r\n" +
                    "  .footer-container {\r\n" +
                    "    padding: 24px 16px;\r\n  }\r\n\r\n}";

                _cssBuilder.AppendLine(footerCSS);

                // Generando codigo por seccion

                // Seccion de Descripcion
                if(footer.DescriptionSection.Count > 0)
                {
                    sb.AppendLine("<div class=\"footer-section footer-brand\">\r\n");
                    var logo = footer.DescriptionSection.FirstOrDefault(c => c.Type == "Footer-Title-Text");
                    var logoDescription = footer.DescriptionSection.FirstOrDefault(c => c.Type == "Footer-Description-Text");

                    if(logo != null)
                    {
                        sb.AppendLine("<div class=\"footer-logo\">\r\n");
                        sb.AppendLine(BuildComponentCode(logo));
                        sb.AppendLine("\r\n</div>");
                    }

                    if (logoDescription != null)
                    {
                        sb.AppendLine("<div class=\"footer-description\">\r\n");
                        sb.AppendLine(BuildComponentCode(logoDescription));
                        sb.AppendLine("\r\n</div>");
                    }

                    sb.AppendLine("\r\n</div>");

                }

                // Seccion de Links
                if(footer.LinksSection.Count > 0)
                {
                    sb.AppendLine("<div class=\"footer-section footer-links\">\r\n");
                    var linksTitle = footer.LinksSection.FirstOrDefault(c => c.Type == "Footer-Title-Text");
                    var links = footer.LinksSection.FirstOrDefault(c => c.Type == "Footer-Menu" || c.Type == "Menu");

                    if(linksTitle != null)
                    {
                        sb.AppendLine(BuildComponentCode(linksTitle));
                    }
                    if(links != null)
                    {
                        sb.AppendLine(BuildComponentCode(links));
                    }

                    sb.AppendLine("\r\n</div>");
                }

                // Seccion de Contacto
                if(footer.ContactSection.Count > 0)
                {
                    sb.AppendLine("<div class=\"footer-section footer-contact\">\r\n");
                    var contactTitle = footer.ContactSection.FirstOrDefault(c => c.Type == "Footer-Title-Text");
                    var contactText = footer.ContactSection.FirstOrDefault(c => c.Type == "Footer-Description-Text");

                    if (contactTitle != null)
                    {
                        sb.AppendLine(BuildComponentCode(contactTitle));
                    }
                    if(contactText != null)
                    {
                        sb.AppendLine(BuildComponentCode(contactText));
                    }

                    sb.AppendLine("\r\n</div>");
                }

                // Seccion de Redes Sociales
                if(footer.IconsSection.Count > 0)
                {
                    sb.AppendLine("<div class=\"footer-section footer-social\">\r\n");
                    var iconsText = footer.IconsSection.FirstOrDefault(c => c.Type == "Footer-Title-Text");

                    if (iconsText != null)
                    {
                        sb.AppendLine(BuildComponentCode(iconsText));
                    }

                    var icons = footer.IconsSection.Where(c => c.Type == "Footer-Icon" || c.Type == "Icon") ?? null;
                    if (icons != null)
                    {
                        sb.AppendLine("<div class=\"social-icons\">\r\n");
                        foreach (var item in icons)
                        {
                            sb.AppendLine(BuildComponentCode(item));
                        }
                        sb.AppendLine("\r\n</div>");
                    }
                }

                sb.AppendLine("        </div>\r\n" +
                    "      </div>\r\n" +
                    "    </footer>");
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
                $"    <title>{BuilderView.Instance.Title}</title>\r\n" +
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

            if (!hasBody)
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

            if (styleKey != null)
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
                case "Footer-Title-Text":
                case "Footer-Description-Text":
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
                case "Footer-Menu":
                    MenuComponent menuModel = component as MenuComponent;
                    return $"FontSize={menuModel.FontSize};" +
                   $"Foreground={menuModel.ForeGround};";
                case "Icon":
                case "Footer-Icon":
                    IconComponent iconModel = component as IconComponent;
                    return $"Background={iconModel.AccentColor};";
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
    }
}
