namespace Proxoft.TemplateEngine.Docx.DataModel;

public abstract class ValueModelBase : Model
{
    internal override sealed Model Find(ModelExpression expression)
    {
        if(expression.IsEmpty
            || expression.Root == ThisChar && expression.Child().IsEmpty)
        {
            return this;
        }

        return this.Parent.Find(expression);
    }
}
