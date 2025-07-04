using Microsoft.Extensions.Logging;
using Proxoft.TemplateEngine.Docx.Configurations;

namespace Proxoft.TemplateEngine.Docx.Processors;

internal abstract class Processor(EngineConfig engineConfig, ILogger logger)
{
    protected EngineConfig EngineConfig { get; } = engineConfig;

    protected ILogger Logger { get; } = logger;
}
