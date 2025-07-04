using System.IO;
using Microsoft.Extensions.Logging.Abstractions;
using Proxoft.TemplateEngine.Docx.Configurations;
using Proxoft.TemplateEngine.Docx.DataModel.v2;

namespace Proxoft.TemplateEngine.Docx.Tests;

internal static class TemplateOperators
{
    //private const string _samplesFolder = "../../../../../Source/Samples/Paragraphs";
    //private const string _outputFolder = "../../../../TestOutputs/Paragraphs";

    public static void ReplacePlaceholders(this string docxSampleFileName, ObjectModel model, FolderConfig folderConfig) =>
        docxSampleFileName.ReplacePlaceholders(model, EngineConfig.Default, folderConfig);

    public static void ReplacePlaceholders(this string docxSampleFileName, ObjectModel model, EngineConfig config, FolderConfig folderConfig)
    {
        if (!Directory.Exists(folderConfig.OutputFolder))
        {
            Directory.CreateDirectory(folderConfig.OutputFolder);
        }

        var outputFileName = $"{folderConfig.OutputFolder}/{docxSampleFileName}.docx";
        if (File.Exists(outputFileName))
        {
            File.Delete(outputFileName);
        }

        var inputFileName = $"{folderConfig.SamplesFolder}/{docxSampleFileName}.docx";
        using var templateStream = File.Open(inputFileName, FileMode.Open, FileAccess.Read);

        DocumentEngineV2 engine = new(NullLogger<DocumentEngineV2>.Instance);
        var docx = engine.Run(templateStream, model, config);

        File.WriteAllBytes(outputFileName, docx);
    }
}
