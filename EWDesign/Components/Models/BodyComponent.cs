using EWDesign.Components.Views;
using EWDesign.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EWDesign.Components.Models
{
    public class BodyComponent : ComponentModel
    {
        public ObservableCollection<string> HeroSectionText { get; set; }
        public ObservableCollection<string> FeatureSectionText { get; set; }
        public string BodyBackgroundColor { get; set; } = "#1E1E2F";

        public BodyComponent()
        {
            Type = "Body";
            HeroSectionText = new ObservableCollection<string>
            {
                "Impulsa tu productividad con MiProducto",
                "Una solución simple, poderosa y accesible para optimizar tu flujo de trabajo.",
                "Comienza ahora"
            };
            FeatureSectionText = new ObservableCollection<string>
            {
                "Fácil de usar", 
                "Diseñado pensando en la simplicidad y eficiencia, sin curva de aprendizaje.", 
                "Altamente personalizable",
                "Adapta la herramienta a tus necesidades sin complicaciones.", 
                "Soporte dedicado", 
                "Te acompañamos en cada paso con asistencia rápida y confiable."
            };
        }

    }
}
