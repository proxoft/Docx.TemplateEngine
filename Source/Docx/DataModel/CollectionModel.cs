using System.Collections.Generic;

namespace Proxoft.TemplateEngine.Docx.DataModel;

public sealed class CollectionModel : Model
{
    private readonly Model[] _items;
    private readonly ValueModel _length;

    public CollectionModel(IEnumerable<Model> items)
    {
        _items = [.. items];
        foreach (var item in _items)
        {
            item.Parent = this;
        }

        _length = new ValueModel($"{_items.Length}");
        _length.Parent = this;
    }

    public IEnumerable<Model> Items => _items;

    internal override Model Find(ModelExpression expression, string thisCharacter)
    {
        if(expression.Root == "length")
        {
            return _length;
        }

        if(expression.Root == thisCharacter)
        {
            if (expression.IsFinal)
            {
                return this;
            }

            return EmptyModel.Instance;
        }

        return this.Parent.Find(expression, thisCharacter);
    }
}
