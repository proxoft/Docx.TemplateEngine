namespace Proxoft.TemplateEngine.Docx.DataModel;

public abstract class ValueModelBase : Model
{
    internal override sealed Model Find(ModelExpression expression, string thisCharacter)
    {
        if(expression.IsEmpty
            || expression.Root == thisCharacter && expression.Child().IsEmpty)
        {
            return this;
        }

        return this.Parent.Find(expression, thisCharacter);
    }
}
