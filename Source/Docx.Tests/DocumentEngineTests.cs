using Proxoft.TemplateEngine.Docx.DataModel;

namespace Proxoft.TemplateEngine.Docx.Tests;

public class DocumentEngineTests : TestBase
{
    public DocumentEngineTests() : base("DocumentEngine")
    {
    }

    [Fact]
    public void HelloWorld()
    {
        this.Process(nameof(HelloWorld), Model.Empty);
    }

    [Fact]
    public void AdHoc()
    {
        this.Process("Template", Model.Empty);
    }
}
