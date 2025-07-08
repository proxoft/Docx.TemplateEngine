namespace Proxoft.TemplateEngine.Docx.DataModel;

public abstract partial class Model
{
    internal Model Parent { get; set; } = EmptyModel.Instance;

    internal abstract Model Find(ModelExpression expression, string thisCharacter);
}
