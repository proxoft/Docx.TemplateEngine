﻿using DocumentFormat.OpenXml.Packaging;
using Microsoft.Extensions.Logging;
using Proxoft.TemplateEngine.Docx.Configurations;
using Proxoft.TemplateEngine.Docx.DataModel;
using Proxoft.TemplateEngine.Docx.Processors.Images;

namespace Proxoft.TemplateEngine.Docx.Processors;

internal class DocumentProcessor(EngineConfig engineConfig, ILogger logger) : Processor(engineConfig, logger)
{
    public void Process(WordprocessingDocument document, ObjectModel documentModel)
    {
        MainDocumentPart? mainPart = document.MainDocumentPart;
        if(mainPart?.Document?.Body is null)
        {
            this.Logger.LogError("MainDocumentPart or underlying Body is null. Cannot process the document.");
            return;
        }

        CompositeElementProcessor processor = new(
            new ImageProcessor(mainPart, this.EngineConfig, this.Logger),
            this.EngineConfig,
            this.Logger
        );

        processor.Process(mainPart.Document.Body, documentModel);

        foreach (var hp in mainPart.HeaderParts)
        {
            processor.Process(hp.Header, documentModel);
        }

        foreach (var fp in mainPart.FooterParts)
        {
            processor.Process(fp.Footer, documentModel);
        }
    }
}
