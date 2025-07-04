using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Logging;
using Proxoft.TemplateEngine.Docx.Configurations;
using Proxoft.TemplateEngine.Docx.DataModel.v2;
using Proxoft.TemplateEngine.Docx.Processors.Images;
using Proxoft.TemplateEngine.Docx.Processors.Searching;

namespace Proxoft.TemplateEngine.Docx.Processors.Paragraphs;

internal class ParagraphsProcessorV2(ImageProcessorV2 imageProcessor, EngineConfig engineConfig, ILogger logger) : Processor(engineConfig, logger)
{
    private readonly ImageProcessorV2 _imageProcessor = imageProcessor;

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
                case SingleValueTemplate svt:
                    int endOfText = svt.Process(paragraphs, context, _imageProcessor);
                    startTextIndex = endOfText;
                    break;

                case ArrayTemplate at:
                    {
                        (Paragraph? lastParagraph, int textEnd) = at.ProcessArrayTemplate(context, paragraphs, _imageProcessor, this.EngineConfig, this.Logger);
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
                            startTextIndex = textEnd;
                        }
                    }
                    break;
                    //case ConditionTemplate ct:
                    //    {
                    //        var (lastParagraph, textEnd) = this.ProcessTemplate(ct, paragraphs, context);
                    //        startTextIndex = textEnd;
                    //    }
                    //    break;
            }
        } while (template != Template.Empty);
    }

    //private (Paragraph, int) ProcessTemplate(
    //    ConditionTemplate template,
    //    ICollection<Paragraph> bodyParagraphs,
    //    Model context)
    //{
    //    if (!(context.Find(template.Start.ModelDescription.Expression) is ConditionModel conditionModel))
    //    {
    //        return (bodyParagraphs.ElementAt(template.End.Position.RowIndex), template.End.Position.TextIndex);
    //    }

    //    var startParagraph = bodyParagraphs.ElementAt(template.Start.Position.ParagraphIndex);
    //    startParagraph.ReplaceToken(template.Start, Model.Empty, _imageProcessor);

    //    var endParagraph = bodyParagraphs.ElementAt(template.End.Position.ParagraphIndex);
    //    var textEnd = endParagraph.ReplaceToken(template.End, Model.Empty, _imageProcessor);

    //    if (!conditionModel.IsFullfilled(template.Start.ModelDescription.Parameters))
    //    {
    //        bodyParagraphs.RemoveTextAndElementsBetween(template.Start.Position, template.End.Position);
    //    }

    //    return (endParagraph, textEnd);
    //}
}

file static class TempalteOperations
{
    public static int Process(this SingleValueTemplate template, IReadOnlyCollection<Paragraph> bodyParagraphs, Model context, ImageProcessorV2 imageProcessor)
    {
        Paragraph p = bodyParagraphs.ElementAt(template.Token.Position.ParagraphIndex);
        Model model = context.Find(template.Token.ModelDescription.Expression);
        int textEndIndex = p.ReplaceToken(template.Token, model, imageProcessor);
        return textEndIndex;
    }

    public static (Paragraph?, int) ProcessArrayTemplate(
        this ArrayTemplate template,
        Model context,
        IReadOnlyCollection<Paragraph> bodyParagraphs,
        ImageProcessorV2 imageProcessor,
        EngineConfig engineConfig,
        ILogger logger)
    {
        Model model = context.Find(template.Start.ModelDescription.Expression);
        if(model is not CollectionModel collection)
        {
            logger.LogError("Array template for non collection model: {modelName}", template.Start.ModelDescription.Expression);
            return (null, 0);
        }

        CompositeElementProcessorV2 compositeElementProcessor = new (imageProcessor, engineConfig, logger);
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
}
