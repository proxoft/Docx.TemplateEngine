using System.Diagnostics;
using Proxoft.TemplateEngine.Docx.DataModel;

namespace Proxoft.TemplateEngine.Docx.Processors.Searching;

[DebuggerDisplay("{ModelDescription}({Position})")]
internal class Token
{
    public static readonly Token None = new(TokenType.None, ModelDescription.Empty, TokenPosition.None);

    private Token(
        TokenType tokenType,
        ModelDescription modelDescription,
        TokenPosition position)
    {
        this.TokenType = tokenType;
        this.ModelDescription = modelDescription;
        this.Position = position;
    }

    public TokenType TokenType { get; }
    public ModelDescription ModelDescription { get; }
    public TokenPosition Position { get; }

    public static Token SingleValue(ModelDescription modelDescription, TokenPosition position) =>
        new(TokenType.SingleValue, modelDescription, position);

    public static Token CollectionBegin(ModelDescription modelDescription, TokenPosition position) =>
        new(TokenType.CollectionBegin, modelDescription, position);

    public static Token CollectionEnd(ModelDescription modelDescription, TokenPosition position) =>
        new(TokenType.CollectionEnd, modelDescription, position);

    public static Token ConditionBegin(ModelDescription modelDescription, TokenPosition position)=>
        new(TokenType.ConditionBegin, modelDescription, position);

    public static Token ConditionEnd(ModelDescription modelDescription, TokenPosition position) =>
        new(TokenType.ConditionEnd, modelDescription, position);
}
