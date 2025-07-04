using System;

namespace Proxoft.TemplateEngine.Docx.DataModel.v2;

public sealed class ValueModel(Func<string> value) : ValueModelBase
{
    private readonly Func<string> _value = value;

    public ValueModel(string value) : this(() => value)
    {
    }

    public string Value => _value();
}
