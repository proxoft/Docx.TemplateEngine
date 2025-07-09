namespace Proxoft.TemplateEngine.Docx.DataModel;

internal class EmptyModel : Model
{
    public static readonly EmptyModel Instance = new();

    internal override Model Find(ModelExpression expression, string thisCharacter)
    {
        return Instance;
    }
}
