using Proxoft.TemplateEngine.Docx.DataModel.v2;

namespace Proxoft.TemplateEngine.Docx.Tests;

public class HeadersFootersTests
{
    private readonly FolderConfig _folderConfig = FolderConfig.Default.Subfolder("HeadersFooters");

    [Fact]
    public void SimpleHeader()
    {
        ObjectModel documentModel = ObjectModel.Create(
            ("xyz",new ValueModel("The real value of XYZ"))
        );

        nameof(SimpleHeader).ReplacePlaceholders(documentModel, _folderConfig);
    }

    [Fact]
    public void SimpleFooter()
    {
        ObjectModel documentModel = ObjectModel.Create(
            ("xyz", new ValueModel("The real value of XYZ"))
        );

        nameof(SimpleFooter).ReplacePlaceholders(documentModel, _folderConfig);
    }
}
