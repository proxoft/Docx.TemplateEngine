namespace Proxoft.TemplateEngine.Docx.Tests;

internal record FolderConfig
{
    public static readonly FolderConfig Default = new();

    public string SamplesFolder { get; init; } = "../../../../../Source/Samples";

    public string OutputFolder { get; init; } = "../../../../TestOutputs";

    public FolderConfig Subfolder(string subfolder) =>
        this with
        {
            SamplesFolder = $"{this.SamplesFolder}/{subfolder}",
            OutputFolder = $"{this.OutputFolder}/{subfolder}"
        };
}
