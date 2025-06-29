using System;
using System.Diagnostics;

namespace Proxoft.TemplateEngine.Docx.DataModel;

[DebuggerDisplay("{Name}: {FormattedValue()}")]
public class SimpleModel(string name, Func<string> formattedValueFunc) : Model(name)
{
    private readonly Func<string> _formattedValueFunc = formattedValueFunc;

    public SimpleModel(string name, string formattedValue) : this(name, () => formattedValue)
    {
    }

    public override string FormattedValue()
    {
        return _formattedValueFunc();
    }

    internal override Model Find(ModelExpression expression)
    {
        if(expression.IsFinal && expression.Name == this.Name)
        {
            return this;
        }

        return this.Parent.Find(expression);
    }
}
