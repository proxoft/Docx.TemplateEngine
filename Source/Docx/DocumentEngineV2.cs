using System.IO;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Proxoft.TemplateEngine.Docx.Configurations;
using Proxoft.TemplateEngine.Docx.DataModel;
using Proxoft.TemplateEngine.Docx.Processors;

namespace Proxoft.TemplateEngine.Docx;

public class DocumentEngineV2(ILogger<DocumentEngineV2> logger)
{
    private readonly ILogger<DocumentEngineV2> _logger = logger;

    public DocumentEngineV2() : this(NullLogger<DocumentEngineV2>.Instance)
    {
    }

    public byte[] Run(Stream docxTemplate, ObjectModel model, EngineConfig engineConfig)
    {
        DocumentProcessorV2 processor = new(engineConfig, _logger);

        using var ms = new MemoryStream();
        docxTemplate.CopyTo(ms);

        using (var docx = WordprocessingDocument.Open(ms, true))
        {
            processor.Process(docx, model);
        }

        return ms.ToArray();
    }
}

public static class DocumentEngineExtensions
{
    public static byte[] Run(this DocumentEngineV2 engine, byte[] docxTemplate, ObjectModel model, EngineConfig engineConfig)
    {
        using var stream = new MemoryStream(docxTemplate);
        return engine.Run(stream, model, engineConfig);
    }

    public static byte[] Run(this DocumentEngineV2 engine, byte[] docxTemplate, ObjectModel model) =>
        engine.Run(docxTemplate, model, EngineConfig.Default);
}
