namespace Proxoft.TemplateEngine.Docx.Configurations;

public sealed class ArrayConfig : ITemplateConfig
{
    public static readonly ArrayConfig Default = new("[", "]");

    public ArrayConfig(
        string open,
        string close)
    {
        this.Open = open;
        this.Close = close;
    }

    public string Open { get; }
    public string Close { get; }

    string ITemplateConfig.OpenSuffix => this.Open;

    string ITemplateConfig.ClosePrefix => this.Close;
}
