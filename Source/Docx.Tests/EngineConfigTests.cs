using Proxoft.Docx.TemplateEngine.DataModel;
using Proxoft.TemplateEngine.Docx.Configurations;
using Proxoft.TemplateEngine.Docx.Tests;
using Xunit;

namespace Proxoft.Docx.TemplateEngine.Tests
{
    public class EngineConfigTests : TestBase
    {
        public EngineConfigTests() : base("EngineConfig")
        {
        }

        [Fact]
        public void DoubleQuote()
        {
            var config = new EngineConfig(
                new PlaceholderConfig("{{", "}}", ".", ":"),
                ArrayConfig.Default,
                ConditionConfig.Default);

            this.Process(nameof(DoubleQuote), new SimpleModel("xyz", "The real value of XYZ"), config);
        }
    }
}
