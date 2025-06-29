using Proxoft.Docx.TemplateEngine.DataModel;

namespace Proxoft.TemplateEngine.Docx.Tests;

public class ParagraphConditionsTests : TestBase
{
    public ParagraphConditionsTests() : base("Paragraphs_Conditions")
    {
    }

    [Fact]
    public void ConditionModel()
    {
        var model = new ObjectModel("",
            new ConditionModel("trueCondition", true),
            new ConditionModel("falseCondition", false)
        );

        this.Process(nameof(ConditionModel), model);
    }

    [Fact]
    public void ConditionModelInOneLine()
    {
        var model = new ObjectModel("",
            new ConditionModel("isTrue", true),
            new ConditionModel("isFalse", false)
        );

        this.Process(nameof(ConditionModelInOneLine), model);
    }

    [Fact]
    public void ConditionModelMultipleParagraphs()
    {
        var model = new ObjectModel("",
            new ConditionModel("isTrue", true),
            new ConditionModel("isFalse", false)
        );

        this.Process(nameof(ConditionModelMultipleParagraphs), model);
    }

    [Fact]
    public void ConditionModelWithFalseParameter()
    {
        var model = new ObjectModel("",
            new ConditionModel("theCondition", false)
        );

        this.Process(nameof(ConditionModelWithFalseParameter), model);
    }

    [Fact]
    public void WhenFalse_And_ClosingTokenOnNewLine()
    {
        var model = new ObjectModel("",
            new ConditionModel("theCondition", false)
        );

        this.Process(nameof(WhenFalse_And_ClosingTokenOnNewLine), model);
    }

    [Fact]
    public void WhenFalse_And_ContainsMultipleParagraphs()
    {
        var model = new ObjectModel("",
            new ConditionModel("theCondition", false)
        );

        this.Process(nameof(WhenFalse_And_ContainsMultipleParagraphs), model);
    }

    [Fact]
    public void WhenTrue_And_ContainsTable()
    {
        var model = new ObjectModel("",
            new ConditionModel("theCondition", true)
        );

        this.Process(nameof(WhenTrue_And_ContainsTable), model);
    }

    [Fact]
    public void WhenFalse_And_ContainsTable()
    {
        var model = new ObjectModel("",
            new ConditionModel("theCondition", false)
        );

        this.Process(nameof(WhenFalse_And_ContainsTable), model);
    }
}
