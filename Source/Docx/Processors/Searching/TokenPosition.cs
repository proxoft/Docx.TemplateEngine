using System.Diagnostics;

namespace Proxoft.TemplateEngine.Docx.Processors.Searching;

[DebuggerDisplay("{ParagraphIndex}|{TextIndex} / {RowIndex}|{CellIndex})")]
internal class TokenPosition(
    int paragraphIndex,
    int textIndex,
    int rowIndex,
    int cellIndex)
{
    public static readonly TokenPosition None = new(-1, -1, -1, -1);

    public TokenPosition(int paragraphIndex, int textIndex): this(paragraphIndex, textIndex, -1 ,-1)
    {
    }

    public int ParagraphIndex { get; } = paragraphIndex;
    public int TextIndex { get; } = textIndex;
    public int RowIndex { get; } = rowIndex;
    public int CellIndex { get; } = cellIndex;

    public bool IsSameRowCell(TokenPosition other)
    {
        return this.RowIndex == other.RowIndex
            && this.CellIndex == other.CellIndex;
    }
}
