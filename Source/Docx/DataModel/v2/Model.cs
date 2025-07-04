namespace Proxoft.TemplateEngine.Docx.DataModel.v2;

public abstract partial class Model
{
    internal const string ThisChar  = "$";

    internal Model Parent { get; set; } = EmptyModel.Instance;

    internal abstract Model Find(ModelExpression expression);
}
