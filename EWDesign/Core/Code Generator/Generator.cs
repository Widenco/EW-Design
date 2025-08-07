using EWDesign.Components.Models;
using EWDesign.Interfaces;
using EWDesign.Model;
using EWDesign.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWDesign.Core.Code_Generator
{
    public class Generator
    {
        public string BuildComponentCode(ComponentModel component)
        {
            var sb = new StringBuilder();

            if(component.Type.ToLower() == "navbar")
            {
                
                sb.AppendLine("<nav class='navbar'>\n" + "<div class='navbar-container'>");

                if(component.Children.Count > 0)
                {
                    foreach (var child in component.Children)
                    {
                        sb.AppendLine(BuildComponentCode(child));
                    }
                }

                sb.AppendLine("</div>\n" + "</nav>");
            }
            else if(component.Type.ToLower() == "body")
            {
                sb.AppendLine("<main class='landing-body'>");

                if(component.Children.Count > 0)
                {
                    foreach (var child in component.Children)
                    {
                        sb.AppendLine(BuildComponentCode(child));
                    }
                }

                sb.AppendLine("</main>");
            }
            else if(component is ICodeGeneratable gen)
            {
                sb.AppendLine(gen.HTMLContent());   
            }

            return sb.ToString();
        }

        public string BuildComponentCSS(ComponentModel component)
        {
            var sb = new StringBuilder();

            if(component.Type.ToLower() == "navbar")
            {
                var navbarComponent = component as NavBarComponent;

                string navbarCSS = ".navbar {\r\n" +
                    $"  background-color: #{navbarComponent.BackgroundColor};\r\n" +
                    "  height: 72px;\r\n" +
                    "  width: 100%;\r\n" +
                    "  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.05);\r\n" +
                    "  position: fixed;\r\n" +
                    "  top: 0;\r\n" +
                    "  z-index: 1000;\r\n}" +
                    "\n.navbar-container {\r\n" +
                    "  max-width: 1200px;\r\n" +
                    "  height: 100%;\r\n" +
                    "  margin: 0 auto;\r\n" +
                    "  padding: 0 24px;\r\n" +
                    "  display: flex;\r\n" +
                    "  align-items: center;\r\n" +
                    "  justify-content: space-between;\r\n}\n";

                sb.AppendLine(navbarCSS);

                if (component.Children.Count > 0)
                {
                    foreach (var child in component.Children)
                    {
                        sb.AppendLine(BuildComponentCSS(child));
                    }
                }
            }
            else if (component.Type.ToLower() == "body")
            {
                var bodyComponent = component as BodyComponent;

                string bodyCSS = "\nbody {\r\n" +
                    "  margin: 0;\r\n" +
                    "  font-family: \"Inter\", sans-serif;\r\n" +
                    $"  background-color: {bodyComponent.BodyBackgroundColor};\r\n}}\n";

                bodyCSS += ".landing-body {\n" +
                "  padding-top: 72px;\n" +
                "  display: flex;\n" +
                "  flex-direction: column;\n" +
                "  align-items: center;\n" +
                "  padding: 72px 24px;\n}\n";

                sb.AppendLine(bodyCSS);

                if (component.Children.Count > 0)
                {
                    foreach (var child in component.Children)
                    {
                        sb.AppendLine(BuildComponentCSS(child));
                    }
                }

            }
            else if (component is ICodeGeneratable gen)
            {
                sb.AppendLine(gen.CSSContent());
            }

            return sb.ToString();
        }

        public string BuildPageCode(ObservableCollection<IComponentView> page)
        {
            var html = new StringBuilder();
            var css = new StringBuilder();
            string pageCode = "";

            foreach (var item in page)
            {
                html.AppendLine(BuildComponentCode(item.Model));
                css.AppendLine(BuildComponentCSS(item.Model));
            }

            pageCode += html.ToString();
            pageCode += css.ToString();
            return pageCode;
        }
    }

}
