using System;

namespace Proxoft.TemplateEngine.Docx.DataModel;

public sealed class ConditionModel(Func<bool> statement) : ValueModelBase
{
    private readonly Func<bool> _statement = statement;

    public ConditionModel(bool value) : this(() => value)
    {
    }

    public bool Evaluate() => _statement();

    public bool Evaluate(string parameter) =>
        string.IsNullOrWhiteSpace(parameter) || parameter.ToLower() != "false"
            ? _statement()
            : !_statement();
}
