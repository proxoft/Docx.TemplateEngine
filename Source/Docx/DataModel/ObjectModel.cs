using System.Collections.Generic;
using System.Linq;

namespace Proxoft.TemplateEngine.Docx.DataModel;

public sealed class ObjectModel : Model
{
    private readonly Dictionary<string, Model> _properties;

    public ObjectModel(Dictionary<string, Model> properties)
    {
        _properties = properties.ToDictionary();
        foreach (var property in _properties.Values)
        {
            property.Parent = this;
        }
    }

    internal override sealed Model Find(ModelExpression expression, string thisCharacter)
    {
        ModelExpression childExpression = expression;
        if (expression.Root == thisCharacter)
        {
            childExpression = childExpression.Child();
        }

        if (_properties.TryGetValue(childExpression.Root, out Model? child))
        {
            if (expression.IsFinal)
            {
                return child;
            }

            return child.Find(childExpression.Child(), thisCharacter);
        }

        return this.Parent.Find(expression, thisCharacter);
    }

    public static ObjectModel Create(params (string key, Model value)[] properties) =>
       new(properties.ToDictionary(p => p.key, p => p.value));
}
