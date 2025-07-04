using DocumentFormat.OpenXml.Packaging;
using Microsoft.Extensions.Logging;
using Proxoft.TemplateEngine.Docx.Configurations;
using Proxoft.TemplateEngine.Docx.Processors.Images;
using Proxoft.TemplateEngine.Docx.Processors.Paragraphs;

namespace Proxoft.TemplateEngine.Docx.Processors;

internal class DocumentProcessorV2(EngineConfig engineConfig, ILogger logger) : Processor(engineConfig, logger)
{
    public void Process(WordprocessingDocument document, DataModel.v2.ObjectModel documentModel)
    {
        MainDocumentPart? mainPart = document.MainDocumentPart;
        if(mainPart?.Document?.Body is null)
        {
            this.Logger.LogError("MainDocumentPart or underlying Body is null. Cannot process the document.");
            return;
        }

        CompositeElementProcessorV2 processor = new(
            new ImageProcessorV2(mainPart, this.EngineConfig, this.Logger),
            this.EngineConfig,
            this.Logger
        );

        processor.Process(mainPart.Document.Body, documentModel);
    }
}
