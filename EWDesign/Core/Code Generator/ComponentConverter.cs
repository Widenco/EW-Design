using EWDesign.Components.Models;
using EWDesign.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

public class ComponentConverter : JsonConverter<ComponentModel>
{
    private static readonly Dictionary<string, Type> _map = new Dictionary<string, Type>()
    {
        { "NavBar", typeof(NavBarComponent) },
        { "Body", typeof(BodyComponent)},
        { "Button", typeof(ButtonComponent) },
        { "Card", typeof(CardComponent) },
        { "Menu", typeof(MenuComponent) },
        { "Text", typeof(TextComponent) },
        { "Navbar-Title-Text", typeof(TextComponent) },
        { "Title-Text", typeof(TextComponent) },
        { "Subtitle-Text", typeof(TextComponent) }
    };

    public override void WriteJson(JsonWriter writer, ComponentModel value, JsonSerializer serializer)
    {
        var jo = JObject.FromObject(value, serializer);
        jo.AddFirst(new JProperty("ComponentType", value.GetType().Name.Replace("Component", "")));
        jo.WriteTo(writer);
    }

    public override ComponentModel ReadJson(JsonReader reader, Type objectType, ComponentModel existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var jo = JObject.Load(reader);
        var typeName = jo["ComponentType"]?.ToString();

        if (typeName != null && _map.TryGetValue(typeName, out var type))
        {
            return (ComponentModel)jo.ToObject(type, serializer);
        }

        throw new JsonSerializationException($"Tipo de componente desconocido: {typeName}");
    }
}
