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

public static class SimpleModelFactory
{
    public static SimpleModel ToSimpleModel(this int value, string name, string format = "")
    {
        return new SimpleModel(name, value.ToString(format));
    }

    public static SimpleModel ToSimpleModel(this string value, string name)
    {
        return new SimpleModel(name, value);
    }

    public static SimpleModel ToSimpleModel(this DateTime value, string name, string format = "")
    {
        return new SimpleModel(name, value.ToString(format));
    }

    public static SimpleModel ToSimpleModel(this decimal value, string name, string format = "")
    {
        return new SimpleModel(name, value.ToString(format));
    }
}