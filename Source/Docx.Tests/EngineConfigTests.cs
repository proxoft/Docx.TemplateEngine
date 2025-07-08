using Proxoft.TemplateEngine.Docx.Configurations;
using Proxoft.TemplateEngine.Docx.DataModel;

namespace Proxoft.TemplateEngine.Docx.Tests;

public class EngineConfigTests
{
    private readonly FolderConfig _folderConfig = FolderConfig.Default.Subfolder("EngineConfig");

    [Fact]
    public void DoubleQuote()
    {
        EngineConfig config = new(
            new PlaceholderConfig("{{", "}}", ".", ":"),
            ArrayConfig.Default,
            ConditionConfig.Default,
            "$");

        ObjectModel root = ObjectModel.Create(
            ("xyz", new ValueModel("The real value of XYZ"))
        );

        nameof(DoubleQuote).ReplacePlaceholders(root, config, _folderConfig);
    }
}
