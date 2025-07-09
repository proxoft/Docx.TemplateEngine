using System.Collections.Generic;
using System.Linq;

namespace Proxoft.TemplateEngine.Docx.DataModel;

public sealed class ObjectModel(Dictionary<string, Model> properties) : ObjectModelBase(properties)
{
    public static ObjectModel Create(params (string key, Model value)[] properties) =>
       new(properties.ToDictionary(p => p.key, p => p.value));
}
