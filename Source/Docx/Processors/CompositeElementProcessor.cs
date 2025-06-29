using DocumentFormat.OpenXml;
using Microsoft.Extensions.Logging;
using Proxoft.TemplateEngine.Docx.Configurations;
using Proxoft.TemplateEngine.Docx.DataModel;
using Proxoft.TemplateEngine.Docx.Processors.Images;
using Proxoft.TemplateEngine.Docx.Processors.Paragraphs;
using Proxoft.TemplateEngine.Docx.Processors.Tables;

namespace Proxoft.TemplateEngine.Docx.Processors;

internal class CompositeElementProcessor(EngineConfig engineConfig, IImageProcessor imageProcessor, ILogger logger)
{
    private ParagraphsProcessor _paragraphsProcessor = new ParagraphsProcessor(engineConfig, imageProcessor, logger);
    private TablesProcessor _tablesProcessor = new TablesProcessor(engineConfig, imageProcessor, logger);

    public void Process(OpenXmlCompositeElement compositeElement, Model context)
    {
        _paragraphsProcessor.Process(compositeElement, context);
        _tablesProcessor.Process(compositeElement, context);
    }
}
