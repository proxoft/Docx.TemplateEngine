using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Logging;
using Proxoft.TemplateEngine.Docx.Configurations;
using Proxoft.TemplateEngine.Docx.DataModel;
using Proxoft.TemplateEngine.Docx.Processors.Images;
using Proxoft.TemplateEngine.Docx.Processors.Searching;

namespace Proxoft.TemplateEngine.Docx.Processors.Paragraphs;

internal class ParagraphsProcessor(
    ImageProcessor imageProcessor,
    EngineConfig engineConfig,
    ILogger logger) : Processor(engineConfig, logger)
{
    private readonly ImageProcessor _imageProcessor = imageProcessor;

    public void Process(OpenXmlCompositeElement parent, Model context)
    {
        Template template;
        int startTextIndex = 0;
        do
        {
            Paragraph[] paragraphs = [
                ..parent
                    .ChildElements
                    .OfType<Paragraph>()
            ];

            template = paragraphs.FindNextTemplate(startTextIndex, this.EngineConfig);

            switch (template)
            {
                case ValueTemplate svt:
                    int endOfText = svt.Process(paragraphs, context, _imageProcessor, this.EngineConfig);
                    startTextIndex = endOfText;
                    break;

                case ArrayTemplate at:
                    {
                        (Paragraph? lastParagraph, int continueTextEnd) = at.ProcessArrayTemplate(context, paragraphs, _imageProcessor, this.EngineConfig, this.Logger);
                        if(lastParagraph is null)
                        {
                            paragraphs = [.. paragraphs.Skip(at.End.Position.ParagraphIndex + 1)];
                            startTextIndex = 0;
                        }
                        else
                        {
                            paragraphs = [
                                ..parent
                                    .ChildElements
                                    .OfType<Paragraph>()
                                    .SkipWhile(p => p != lastParagraph)
                            ];
                            startTextIndex = continueTextEnd;
                        }
                    }
                    break;
                case ConditionTemplate ct:
                    {
                        (Paragraph lastParagraph, int continueTextEnd) = ct.ProcessConditionTemplate(context, paragraphs, _imageProcessor, this.EngineConfig, this.Logger);
                        startTextIndex = continueTextEnd;
                    }
                    break;
            }
        } while (template != Template.Empty);
    }
}

file static class TempalteOperations
{
    public static int Process(this ValueTemplate template, IReadOnlyCollection<Paragraph> bodyParagraphs, Model context, ImageProcessor imageProcessor, EngineConfig engineConfig)
    {
        Paragraph p = bodyParagraphs.ElementAt(template.Token.Position.ParagraphIndex);
        Model model = context.Find(template.Token.ModelDescription.Expression, engineConfig.ThisCharacter);
        int textEndIndex = p.ReplaceToken(template.Token, model, imageProcessor);
        return textEndIndex;
    }

    public static (Paragraph?, int) ProcessArrayTemplate(
        this ArrayTemplate template,
        Model context,
        IReadOnlyCollection<Paragraph> bodyParagraphs,
        ImageProcessor imageProcessor,
        EngineConfig engineConfig,
        ILogger logger)
    {
        Model model = context.Find(template.Start.ModelDescription.Expression, engineConfig.ThisCharacter);
        if(model is not CollectionModel collection)
        {
            logger.LogError("Array template for non collection model: {modelName}", template.Start.ModelDescription.OriginalText);
            return (null, 0);
        }

        CompositeElementProcessor compositeElementProcessor = new (imageProcessor, engineConfig, logger);
        OpenXmlElement[] result = [];

        foreach (var item in collection.Items)
        {
            Body itemBody = template.OpenXml.CreateBody();
            compositeElementProcessor.Process(itemBody, item);

            result = [
                ..result,
                ..itemBody.ChildElements.Select(e => e.CloneNode(true))
            ];
        }

        Paragraph startParagraph = bodyParagraphs.ElementAt(template.Start.Position.ParagraphIndex);
        Paragraph endParagraph = bodyParagraphs.ElementAt(template.End.Position.ParagraphIndex);

        if (startParagraph != endParagraph)
        {
            OpenXmlElement? s = startParagraph.NextSibling();
            while (s != endParagraph)
            {
                OpenXmlElement? t = s;
                s = t?.NextSibling();
                t?.Remove();
            }
        }

        startParagraph.ReplaceToken(template.Start, EmptyModel.Instance, imageProcessor);
        int textEnd = endParagraph.ReplaceToken(template.End, EmptyModel.Instance, imageProcessor);

        foreach (var e in result)
        {
            endParagraph.InsertBeforeSelf(e);
        }

        return (endParagraph, textEnd);
    }

    public static (Paragraph, int) ProcessConditionTemplate(
        this ConditionTemplate template,
        Model context,
        ICollection<Paragraph> bodyParagraphs,
        ImageProcessor imageProcessor,
        EngineConfig engineConfig,
        ILogger logger)
    {
        ConditionModel? conditionModel = context.Find(template.Start.ModelDescription.Expression, engineConfig.ThisCharacter) as ConditionModel;
        if (conditionModel is null)
        {
            logger.LogError("ConditionModel is missing or Condition template found for non condition model: {modelName}", template.Start.ModelDescription.OriginalText);
        }

        Paragraph startParagraph = bodyParagraphs.ElementAt(template.Start.Position.ParagraphIndex);
        int _ = startParagraph.ReplaceToken(template.Start, EmptyModel.Instance, imageProcessor);

        Paragraph endParagraph = bodyParagraphs.ElementAt(template.End.Position.ParagraphIndex);
        int textEnd = template.Start.Position.TextIndex;

        bool suttisfied = conditionModel?.Evaluate(template.Start.ModelDescription.Parameters) ?? false;
        if (!suttisfied)
        {
            bodyParagraphs.RemoveTextAndElementsBetween(template.Start.Position, template.End.Position);
        }

        return(endParagraph, textEnd);
    }
}
