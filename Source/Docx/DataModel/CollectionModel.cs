using System.Collections.Generic;
using System.Linq;

namespace Proxoft.TemplateEngine.Docx.DataModel;

public sealed class CollectionModel: ObjectModelBase
{
    private readonly Model[] _items;

    public CollectionModel(IEnumerable<Model> items) : this(items, [])
    {
    }

    public CollectionModel(IEnumerable<Model> items, Dictionary<string, Model> properties) : base(properties)
    {
        _items = [.. items];
        foreach (var item in _items)
        {
            item.Parent = this;
        }
    }

    public IEnumerable<Model> Items => _items;

    public static CollectionModel Create(Model[] items, params (string key, Model value)[] properties)
    {
        Dictionary<string, Model> props = properties.ToDictionary(p => p.key, p => p.value);
        return new CollectionModel(items, props);
    }
}
