using System.Collections.Generic;
using System.Diagnostics;

namespace Proxoft.TemplateEngine.Docx.DataModel;

[DebuggerDisplay("{Expression}")]
internal class ModelDescription(
    IEnumerable<string> segments,
    string parameters,
    string originalText)
{
    public static readonly ModelDescription Empty = new([], string.Empty, string.Empty);

    public ModelDescription(
        IEnumerable<string> segments,
        string originalText) : this(segments, string.Empty, originalText)
    {
    }

    public ModelExpression Expression { get; } = new ModelExpression(segments);

    public string Parameters { get; } = parameters;

    public string OriginalText { get; } = originalText;
}
