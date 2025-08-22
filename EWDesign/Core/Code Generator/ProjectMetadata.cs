using EWDesign.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWDesign.Core.Code_Generator
{
    public class ProjectMetadata
    {
        public string ProjectName { get; set; } = "Nuevo Proyecto";
        public List<ComponentModel> Components { get; set; } = new List<ComponentModel>();
    }
}
