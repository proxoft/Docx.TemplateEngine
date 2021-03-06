﻿using System.IO;
using DocumentFormat.OpenXml.Packaging;
using Proxoft.Docx.TemplateEngine.DataModel;
using Proxoft.Docx.TemplateEngine.Processors;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Proxoft.Docx.TemplateEngine
{
    public class DocumentEngine
    {
        private EngineConfig _engineConfig;

        public DocumentEngine() : this(EngineConfig.Default)
        {
        }

        public DocumentEngine(EngineConfig engineConfig)
        {
            _engineConfig = engineConfig;
        }

        public ILogger Logger { get; set; } = NullLogger.Instance;

        public byte[] Run(Stream docxTemplate, Model model)
            => this.Run(docxTemplate, model, _engineConfig);

        public byte[] Run(Stream docxTemplate, Model model, EngineConfig engineConfig)
        {
            var processor = new DocumentProcessor(engineConfig);
            processor.Logger = this.Logger ?? NullLogger.Instance;

            using (var ms = new MemoryStream())
            {
                docxTemplate.CopyTo(ms);

                using (var docx = WordprocessingDocument.Open(ms, true))
                {
                    processor.Process(docx, model);
                }

                return ms.ToArray();
            }
        }

        public byte[] Run(byte[] docxTemplate, Model model)
            => this.Run(docxTemplate, model, EngineConfig.Default);

        public byte[] Run(byte[] docxTemplate, Model model, EngineConfig engineConfig)
            => this.Run(new MemoryStream(docxTemplate), model, engineConfig);
    }
}
