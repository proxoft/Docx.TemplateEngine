using System.IO;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Proxoft.TemplateEngine.Docx.Configurations;
using Proxoft.TemplateEngine.Docx.DataModel;
using Proxoft.TemplateEngine.Docx.Processors;

namespace Proxoft.TemplateEngine.Docx;

public class DocumentEngine(EngineConfig engineConfig)
{
    private readonly EngineConfig _engineConfig = engineConfig;

    public DocumentEngine() : this(EngineConfig.Default)
    {
    }

    public ILogger Logger { get; set; } = NullLogger.Instance;

    public byte[] Run(Stream docxTemplate, Model model, EngineConfig engineConfig)
    {
        DocumentProcessor processor = new(engineConfig, this.Logger);

        using var ms = new MemoryStream();
        docxTemplate.CopyTo(ms);

        using (var docx = WordprocessingDocument.Open(ms, true))
        {
            processor.Process(docx, model);
        }

        return ms.ToArray();
    }

    public byte[] Run(Stream docxTemplate, Model model)
        => this.Run(docxTemplate, model, _engineConfig);

    public byte[] Run(byte[] docxTemplate, Model model)
        => this.Run(docxTemplate, model, EngineConfig.Default);

    public byte[] Run(byte[] docxTemplate, Model model, EngineConfig engineConfig)
        => this.Run(new MemoryStream(docxTemplate), model, engineConfig);
}
