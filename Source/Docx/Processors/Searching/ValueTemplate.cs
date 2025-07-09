using System.Diagnostics;

namespace Proxoft.TemplateEngine.Docx.Processors.Searching;

[DebuggerDisplay("{GetType().Name}: {Token}")]
internal class ValueTemplate(Token token) : Template
{
    public override bool IsComplete => true;

    public Token Token { get; } = token;
}
