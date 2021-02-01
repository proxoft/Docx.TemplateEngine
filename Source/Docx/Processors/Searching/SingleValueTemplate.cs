namespace Proxoft.Docx.TemplateEngine.Processors.Searching
{
    internal class SingleValueTemplate : Template
    {
        public SingleValueTemplate(Token token)
        {
            this.Token = token;
        }

        public override bool IsComplete => true;

        public Token Token { get; }
    }
}
