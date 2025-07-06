using Proxoft.TemplateEngine.Docx.DataModel;

namespace Proxoft.TemplateEngine.Docx.DataModel;

public abstract partial class Model
{
    internal const string ThisChar  = "$";

    internal Model Parent { get; set; } = EmptyModel.Instance;

    internal abstract Model Find(ModelExpression expression);
}
