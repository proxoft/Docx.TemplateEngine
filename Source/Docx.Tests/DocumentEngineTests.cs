using Proxoft.Docx.TemplateEngine.DataModel;
using Proxoft.TemplateEngine.Docx.Tests;
using Xunit;

namespace Proxoft.Docx.TemplateEngine.Tests
{
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
}
