using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;

// using Newtonsoft.Json.Linq;
using Proxoft.TemplateEngine.Docx.DataModel;

namespace Proxoft.TemplateEngine.Docx.Serialization;

internal static class Serializer
{
    private static readonly System.Text.Json.JsonSerializerOptions _jsonSerializerOptions = new()
    {
        WriteIndented = true,
        TypeInfoResolver = new PolymorphicTypeResolver(),
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
    };

    static Serializer()
    {
        // Register custom converters
        //_jsonSerializerOptions.Converters.Add(new Converters.ObjectModelJsonConverter());
        //_jsonSerializerOptions.Converters.Add(new Converters.SimpleModelJsonConverter());
        //_jsonSerializerOptions.Converters.Add(new Converters.ConditionModelJsonConverter());
        //_jsonSerializerOptions.Converters.Add(new Converters.CollectionModelJsonConverter());
        //_jsonSerializerOptions.Converters.Add(new Converters.ImageModelJsonConverter());
    }

    //public static string Serialize(Model root)
    //{
    //    string json = System.Text.Json.JsonSerializer.Serialize(root, _jsonSerializerOptions);
    //    return json;
    //}

    //public static Model? Deserialize(string json)
    //{
    //    throw new NotImplementedException("Deserialization is not implemented yet.");
    //    //var jObject = JObject.Parse(json);

    //    //var name = jObject.Children<JProperty>().SingleOrDefault(p => p.Name == Constants.RootNameProperty);
    //    //var model = jObject.ToModel(name.Value.ToString());

    //    //return model;
    //}
}
