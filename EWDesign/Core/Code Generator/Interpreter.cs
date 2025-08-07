using EWDesign.Model;
using EWDesign.Components.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWDesign.Core.Code_Generator
{
    public static class Interpreter
    {
        public static ComponentModel CreateModelFromRaw(RawComponentData raw)
        {
            ComponentModel model = null;
            
            switch(raw.Type.ToLower())
            {
                case "text":
                    model = new TextComponent
                    {
                        Text = raw.Properties["Content"]?.ToString(),
                        FontSize = Convert.ToInt32(raw.Properties["FontSize"])
                    };
                    break;
                case "navbar":
                    model = new NavBarComponent { Id = raw.Id }; // Y así sucesivamente
                    break;
                default:
                    throw new NotSupportedException($"Tipo no soportado: {raw.Type}");
            };

            foreach (var childRaw in raw.Children)
            {
                var child = CreateModelFromRaw(childRaw);
                model.Children.Append(child);
            }

            return model;
        }
    }

}
