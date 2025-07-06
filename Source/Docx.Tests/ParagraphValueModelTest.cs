using Proxoft.TemplateEngine.Docx.DataModel;

namespace Proxoft.TemplateEngine.Docx.Tests;

public class ParagraphValueModelTest
{
    private readonly FolderConfig _folderConfig = FolderConfig.Default.Subfolder("Paragraphs");

    private readonly ObjectModel _documentModel = new(
        new ()
        {
            { "xyz", new ValueModel("The real value of XYZ") }
        }
    );

    [Fact]
    public void SimpleModel()
    {
        nameof(SimpleModel).ReplacePlaceholders(_documentModel, _folderConfig);
    }

    [Fact]
    public void SimpleModelAsTheOnlyText()
    {
        nameof(SimpleModelAsTheOnlyText).ReplacePlaceholders(_documentModel, _folderConfig);
    }

    [Fact]
    public void RepeatedSimpleModel()
    {
        nameof(RepeatedSimpleModel).ReplacePlaceholders(_documentModel, _folderConfig);
    }

    [Fact]
    public void SimpleModelStyling()
    {
        nameof(SimpleModelStyling).ReplacePlaceholders(_documentModel, _folderConfig);
    }

    [Fact]
    public void SimpleModelInconsistentStyling()
    {
        nameof(SimpleModelInconsistentStyling).ReplacePlaceholders(_documentModel, _folderConfig);
    }

    [Fact]
    public void SimpleModelMultipleParagraphs()
    {
        nameof(SimpleModelMultipleParagraphs).ReplacePlaceholders(_documentModel, _folderConfig);
    }

    [Fact]
    public void DuplicatedCharsSimpleValues()
    {
        nameof(DuplicatedCharsSimpleValues).ReplacePlaceholders(_documentModel, _folderConfig);
    }
}
