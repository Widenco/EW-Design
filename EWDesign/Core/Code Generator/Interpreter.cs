using EWDesign.Model;
using EWDesign.Components.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace EWDesign.Core.Code_Generator
{
    public class Interpreter
    {

        private readonly JsonSerializerSettings _settings;

        public Interpreter()
        {
            _settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.All,
                //Converters = new List<JsonConverter> { new ComponentConverter() }
            };
        }

        public void SaveProject(ProjectMetadata project, string filePath)
        {
            string json = JsonConvert.SerializeObject(project, _settings);
            File.WriteAllText(filePath, json);
        }

        public ProjectMetadata LoadProject(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<ProjectMetadata>(json, _settings);
        }
    }

}
