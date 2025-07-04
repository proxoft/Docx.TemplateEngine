using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Proxoft.TemplateEngine.Docx.DataModel;

[DebuggerDisplay("{ToExpressionString()}")]
internal class ModelExpression(IEnumerable<string> segments)
{
    private readonly string[] _segments = [.. segments];

    public string Root => _segments.FirstOrDefault() ?? string.Empty;

    public string Name => _segments.LastOrDefault() ?? string.Empty;

    public bool IsFinal => _segments.Length <= 1;

    public bool IsEmpty => _segments.Length == 0;

    public ModelExpression Child() =>
        new(_segments.Skip(1));

    public string ToExpressionString(string nameSeparator = ".") =>
        string.Join(nameSeparator, _segments);
}
