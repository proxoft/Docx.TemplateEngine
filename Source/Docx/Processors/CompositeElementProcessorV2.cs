using DocumentFormat.OpenXml;
using Microsoft.Extensions.Logging;
using Proxoft.TemplateEngine.Docx.Configurations;
using Proxoft.TemplateEngine.Docx.DataModel;
using Proxoft.TemplateEngine.Docx.Processors.Images;
using Proxoft.TemplateEngine.Docx.Processors.Paragraphs;
using Proxoft.TemplateEngine.Docx.Processors.Tables;

namespace Proxoft.TemplateEngine.Docx.Processors;

internal class CompositeElementProcessorV2(
    ImageProcessorV2 imageProcessor,
    EngineConfig engineConfig,
    ILogger logger) : Processor(engineConfig, logger)
{
    ParagraphsProcessorV2 _paragraphsProcessor = new(
            imageProcessor,
            engineConfig,
            logger
        );

    TablesProcessorV2 _tablesProcessor = new(
            imageProcessor,
            engineConfig,
            logger
        );

    public void Process(OpenXmlCompositeElement compositeElement, Model context)
    {
        _paragraphsProcessor.Process(compositeElement, context);
        _tablesProcessor.Process(compositeElement, context);
    }
}
