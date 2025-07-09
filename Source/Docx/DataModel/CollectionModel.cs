using System.Collections.Generic;

namespace Proxoft.TemplateEngine.Docx.DataModel;

public sealed class CollectionModel : Model
{
    private readonly Model[] _items;
    private readonly ValueModel _length;
    private readonly ConditionModel _empty;
    private readonly ConditionModel _notEmpty;

    public CollectionModel(IEnumerable<Model> items)
    {
        _items = [.. items];
        foreach (var item in _items)
        {
            item.Parent = this;
        }

        _length = new ValueModel($"{_items.Length}")
        {
            Parent = this
        };

        _empty = new ConditionModel(_items.Length == 0)
        {
            Parent = this
        };

        _notEmpty = new ConditionModel(_items.Length > 0)
        {
            Parent = this
        };
    }

    public IEnumerable<Model> Items => _items;

    internal override Model Find(ModelExpression expression, string thisCharacter)
    {
        ModelExpression childExpression = expression;
        if (expression.Root == thisCharacter)
        {
            childExpression = childExpression.Child();
        }

        switch (childExpression.Root)
        {
            case "length":
                return _length;
            case "empty":
                return _empty;
            case "notEmpty":
                return _notEmpty;
        }

        if (childExpression.IsFinal)
        {
            return EmptyModel.Instance;
        }

        return this.Parent.Find(expression, thisCharacter);
    }
}
