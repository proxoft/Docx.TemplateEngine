using Proxoft.TemplateEngine.Docx.DataModel;

namespace Proxoft.TemplateEngine.Docx.Tests;

public class ParagraphConditionModelTest
{
    private readonly FolderConfig _folderConfig = FolderConfig.Default.Subfolder("Paragraphs_Conditions");

    private readonly ObjectModel _documentModel = ObjectModel.Create(
        ("trueCondition", new ConditionModel(true)),
        ("falseCondition", new ConditionModel(false)),
        ("xyz", new ValueModel("the value of XYZ"))
    );

    [Fact]
    public void ConditionModel()
    {
        nameof(ConditionModel).ReplacePlaceholders(_documentModel, _folderConfig);
    }

    [Fact]
    public void ConditionModelInOneLine()
    {
        nameof(ConditionModelInOneLine).ReplacePlaceholders(_documentModel, _folderConfig);
    }

    [Fact]
    public void ConditionModelMultipleParagraphs()
    {
        nameof(ConditionModelMultipleParagraphs).ReplacePlaceholders(_documentModel, _folderConfig);
    }

    [Fact]
    public void ConditionModelWithFalseParameter()
    {
        nameof(ConditionModelWithFalseParameter).ReplacePlaceholders(_documentModel, _folderConfig);
    }

    [Fact]
    public void WhenFalse_And_ClosingTokenOnNewLine()
    {
        nameof(WhenFalse_And_ClosingTokenOnNewLine).ReplacePlaceholders(_documentModel, _folderConfig);
    }

    [Fact]
    public void WhenFalse_And_ContainsMultipleParagraphs()
    {
        nameof(WhenFalse_And_ContainsMultipleParagraphs).ReplacePlaceholders(_documentModel, _folderConfig);
    }

    [Fact]
    public void WhenTrue_And_ContainsTable()
    {
        nameof(WhenTrue_And_ContainsTable).ReplacePlaceholders(_documentModel, _folderConfig);
    }

    [Fact]
    public void WhenFalse_And_ContainsTable()
    {
        nameof(WhenFalse_And_ContainsTable).ReplacePlaceholders(_documentModel, _folderConfig);
    }

    [Fact]
    public void PlaceholderInsideOfCondition()
    {
        nameof(PlaceholderInsideOfCondition).ReplacePlaceholders(_documentModel, _folderConfig);
    }
}
