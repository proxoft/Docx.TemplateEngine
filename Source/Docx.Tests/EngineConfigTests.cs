﻿using Proxoft.TemplateEngine.Docx.Configurations;
using Proxoft.TemplateEngine.Docx.DataModel;

namespace Proxoft.TemplateEngine.Docx.Tests;

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
