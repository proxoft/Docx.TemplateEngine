using System;

namespace Proxoft.TemplateEngine.Docx.DataModel;

public class ConditionModel(string name, Func<bool> conditionFunc) : Model(name)
{
    private readonly Func<bool> _conditionFunc = conditionFunc;

    public ConditionModel(string name, bool value) : this(name, () => value)
    {
    }

    public bool IsTrue() => _conditionFunc();

    public bool IsFullfilled(string parameter)
    {
        return string.IsNullOrWhiteSpace(parameter) || parameter.ToLower() != "false"
            ? this.IsTrue()
            : !this.IsTrue();
    }

    public override string FormattedValue()
    {
        return _conditionFunc().ToString().ToLower();
    }

    internal override Model Find(ModelExpression expression)
    {
        if (expression.IsFinal && expression.Name == this.Name)
        {
            return this;
        }

        return this.Parent.Find(expression);
    }
}
