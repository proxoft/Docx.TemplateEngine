using System.Collections.Generic;
using System.Linq;

namespace Proxoft.TemplateEngine.Docx.DataModel.v2;

public abstract class ObjectModelBase : Model
{
    private readonly Dictionary<string, Model> _properties;

    protected ObjectModelBase(Dictionary<string, Model> properties)
    {
        _properties = properties.ToDictionary();
        foreach (var property in _properties.Values)
        {
            property.Parent = this;
        }
    }

    internal override sealed Model Find(ModelExpression expression)
    {
        ModelExpression childExpression = expression;
        if (expression.Root == ThisChar)
        {
            childExpression = childExpression.Child();
        }

        if (_properties.TryGetValue(childExpression.Root, out Model? child))
        {
            if (expression.IsFinal)
            {
                return child;
            }

            return child.Find(childExpression.Child());
        }

        return this.Parent.Find(expression);
    }
}
