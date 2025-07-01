using System.Collections.Generic;
using System.Linq;

namespace Proxoft.TemplateEngine.Docx.DataModel;

public class CollectionModel : ObjectModel
{
    public CollectionModel(
        string name,
        IEnumerable<Model> items,
        IEnumerable<Model> childModels) : base(name, childModels)
    {
        var ai = items.ToArray();
        this.Items = ai;
        this.SetSelfAsParent(ai);
    }

    public IReadOnlyCollection<Model> Items { get; }
}
