namespace Proxoft.TemplateEngine.Docx.DataModel.v2;

internal class EmptyModel : Model
{
    public static readonly EmptyModel Instance = new();

    internal override Model Find(ModelExpression expression)
    {
        return Instance;
    }
}
