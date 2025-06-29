namespace Proxoft.TemplateEngine.Docx.Configurations;

public class PlaceholderConfig : ITemplateConfig
{
    public static readonly PlaceholderConfig Default = new("{", "}", ".", ":");

    public PlaceholderConfig(
        string start,
        string end,
        string delimiter,
        string formatDelimiter)
    {
        this.Start = start;
        this.End = end;
        this.NamesDelimiter = delimiter;
        this.ParametersDelimiter = formatDelimiter;
    }

    public string Start { get; }
    public string End { get; }
    public string NamesDelimiter { get; }
    public string ParametersDelimiter { get; }

    string ITemplateConfig.OpenSuffix => string.Empty;

    string ITemplateConfig.ClosePrefix => string.Empty;
}
