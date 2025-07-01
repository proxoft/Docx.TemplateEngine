namespace Proxoft.TemplateEngine.Docx.Processors.Searching;

internal class SingleValueTemplate(Token token) : Template
{
    public override bool IsComplete => true;

    public Token Token { get; } = token;
}
