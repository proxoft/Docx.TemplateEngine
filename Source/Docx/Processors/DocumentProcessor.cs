using DocumentFormat.OpenXml.Packaging;
using Microsoft.Extensions.Logging;
using Proxoft.TemplateEngine.Docx.Configurations;
using Proxoft.TemplateEngine.Docx.DataModel;
using Proxoft.TemplateEngine.Docx.Processors.Images;

namespace Proxoft.TemplateEngine.Docx.Processors;

internal class DocumentProcessor(EngineConfig engineConfig, ILogger logger)
{
    private readonly EngineConfig _engineConfig = engineConfig;

    public ILogger Logger { get; } = logger;

    public void Process(WordprocessingDocument document, Model documentModel)
    {
        var mainPart = document.MainDocumentPart;
        var imageProcessor = new ImageProcessor(mainPart, this.Logger);
        var compositeElementProcessor = new CompositeElementProcessor(_engineConfig, imageProcessor, this.Logger);

        compositeElementProcessor.Process(mainPart.Document.Body, documentModel);

        foreach (var hp in mainPart.HeaderParts)
        {
            compositeElementProcessor.Process(hp.Header, documentModel);
        }

        foreach (var fp in mainPart.FooterParts)
        {
            compositeElementProcessor.Process(fp.Footer, documentModel);
        }
    }
}
