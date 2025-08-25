using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace EWDesign.Model
{
    [Serializable]
    public class ProjectData
    {
        public string ProjectName { get; set; } = "Mi Proyecto";
        public DateTime ExportDate { get; set; } = DateTime.Now;
        public string Version { get; set; } = "1.0";
        public NavBarData NavBar { get; set; }
        public BodyData Body { get; set; }
        public FooterData Footer { get; set; }
    }

    [Serializable]
    public class NavBarData
    {
        public string Type { get; set; } = "NavBar";
        public string Title { get; set; } = "Mi Producto";
        public string BackgroundColor { get; set; } = "#f5f7fa";
                        public List<string> NavBarElements { get; set; } = new List<string>
                {
                    "Inicio",
                    "Características",
                    "Precios",
                    "Contacto"
                };
                // Agregar propiedad para los componentes hijos arrastrados al NavBar
                public List<ComponentData> Children { get; set; } = new List<ComponentData>();
    }

    [Serializable]
    public class BodyData
    {
        public string Type { get; set; } = "Body";
        public string BackgroundColor { get; set; } = "#1E1E2F";
                        public List<string> HeroSectionText { get; set; } = new List<string>
                {
                    "Impulsa tu productividad con MiProducto",
                    "Una solución simple, poderosa y accesible para optimizar tu flujo de trabajo.",
                    "Comienza ahora"
                };
                public List<string> FeatureSectionText { get; set; } = new List<string>
                {
                    "Fácil de usar",
                    "Diseñado pensando en la simplicidad y eficiencia, sin curva de aprendizaje.",
                    "Altamente personalizable",
                    "Adapta la herramienta a tus necesidades sin complicaciones.",
                    "Soporte dedicado",
                    "Te acompañamos en cada paso con asistencia rápida y confiable."
                };
                public List<ComponentData> HeroSectionComponents { get; set; } = new List<ComponentData>();
                public List<ComponentData> FeatureSectionComponents { get; set; } = new List<ComponentData>();
                // Agregar propiedad para los componentes hijos arrastrados al Body
                public List<ComponentData> Children { get; set; } = new List<ComponentData>();
    }

    [Serializable]
    public class FooterData
    {
        public string Type { get; set; } = "Footer";
        public string Logo { get; set; } = "Mi Producto";
        public string Copyright { get; set; } = "© 2025 MiProducto. Todos los derechos reservados.";
        public List<string> FooterLinks { get; set; } = new List<string>
                {
                    "Inicio",
                    "Características",
                    "Precios",
                    "Contacto"
                };
        public string BackgroundColor { get; set; } = "#2a2a40";
        public string Foreground { get; set; } = "#FFFFFF";
        public List<ComponentData> Children { get; set; } = new List<ComponentData>();
    }

    [Serializable]
    public class ComponentData
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Type { get; set; }
        public string Text { get; set; }
        public string BackgroundColor { get; set; }
        public string ForegroundColor { get; set; }
        public string FontSize { get; set; }
        public string FontWeight { get; set; }
        public string TextAlignment { get; set; }
        public string TextWrap { get; set; }
        public string Margin { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public List<ComponentData> Children { get; set; } = new List<ComponentData>();
    }
}
