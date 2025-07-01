using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Proxoft.TemplateEngine.Docx.DataModel;

namespace Proxoft.TemplateEngine.Docx.Serialization.Converters;

internal class ObjectModelJsonConverter : JsonConverter<ObjectModel>
{
    public override ObjectModel? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return null;
    }

    public override void Write(Utf8JsonWriter writer, ObjectModel value, JsonSerializerOptions options)
    {
        foreach (Model item in value.Childs)
        {
            writer.WriteStartObject();
            writer.WriteString("_type", item.GetType().Name); // the real name
            writer.WriteString("_name", item.Name);
            writer.WriteEndObject();
        }
    }
}
