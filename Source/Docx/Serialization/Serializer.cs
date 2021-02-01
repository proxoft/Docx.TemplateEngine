using System.Linq;
using Proxoft.Docx.TemplateEngine.DataModel;

using Newtonsoft.Json.Linq;

namespace Proxoft.Docx.TemplateEngine.Serialization
{
    public static class Serializer
    {
        public static string Serialize(Model root)
        {
            var json = root.ToJson(NameSerialization.AsProperty);
            return json;
        }

        public static Model Deserialize(string json)
        {
            var jObject = JObject.Parse(json);

            var name = jObject.Children<JProperty>().SingleOrDefault(p => p.Name == Constants.RootNameProperty);
            var model = jObject.ToModel(name.Value.ToString());

            return model;
        }
    }
}
