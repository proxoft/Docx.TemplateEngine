using Proxoft.TemplateEngine.Docx.DataModel;

namespace Proxoft.TemplateEngine.Docx.Tests;

public class ParagraphValueModelTest
{
    private readonly FolderConfig _folderConfig = FolderConfig.Default.Subfolder("Paragraphs_ValueModel");

    private readonly ObjectModel _documentModel = new(
        new ()
        {
            { "xyz", new ValueModel("The real value of XYZ") }
        }
    );

    [Fact]
    public void SimpleModel()
    {
        nameof(SimpleModel)
            .ReplacePlaceholders(_documentModel, _folderConfig)
            .AssertResultAndExpectedInnerTextAreaEqual();
    }

    [Fact]
    public void SimpleModelAsTheOnlyText()
    {
        nameof(SimpleModelAsTheOnlyText)
            .ReplacePlaceholders(_documentModel, _folderConfig)
            .AssertResultAndExpectedInnerTextAreaEqual();
    }

    [Fact]
    public void RepeatedSimpleModel()
    {
        nameof(RepeatedSimpleModel)
            .ReplacePlaceholders(_documentModel, _folderConfig)
            .AssertResultAndExpectedInnerTextAreaEqual();
    }

    [Fact]
    public void SimpleModelStyling()
    {
        nameof(SimpleModelStyling)
            .ReplacePlaceholders(_documentModel, _folderConfig)
            .AssertResultAndExpectedInnerTextAreaEqual();
    }

    [Fact]
    public void SimpleModelInconsistentStyling()
    {
        nameof(SimpleModelInconsistentStyling)
            .ReplacePlaceholders(_documentModel, _folderConfig)
            .AssertResultAndExpectedInnerTextAreaEqual();
    }

    [Fact]
    public void SimpleModelMultipleParagraphs()
    {
        nameof(SimpleModelMultipleParagraphs)
            .ReplacePlaceholders(_documentModel, _folderConfig)
            .AssertResultAndExpectedInnerTextAreaEqual();
    }

    [Fact]
    public void DuplicatedCharsSimpleValues()
    {
        nameof(DuplicatedCharsSimpleValues)
            .ReplacePlaceholders(_documentModel, _folderConfig)
            .AssertResultAndExpectedInnerTextAreaEqual();
    }
}
