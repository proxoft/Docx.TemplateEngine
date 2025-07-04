﻿using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Logging;
using Proxoft.TemplateEngine.Docx.Configurations;
using Proxoft.TemplateEngine.Docx.DataModel;
using Proxoft.TemplateEngine.Docx.Processors.Images;
using Proxoft.TemplateEngine.Docx.Processors.Searching;

namespace Proxoft.TemplateEngine.Docx.Processors.Paragraphs;

internal class ParagraphsProcessor
{
    private readonly EngineConfig _engineConfig;
    private readonly IImageProcessor _imageProcessor;
    private readonly ILogger _logger;

    public ParagraphsProcessor(EngineConfig engineConfig, IImageProcessor imageProcessor, ILogger logger)
    {
        _engineConfig = engineConfig;
        _imageProcessor = imageProcessor;
        _logger = logger;
    }

    public void Process(OpenXmlCompositeElement parent, Model context)
    {
        Template template;
        int startTextIndex = 0;
        do
        {
            var paragraphs = parent
                .ChildElements
                .OfType<Paragraph>()
                .ToArray();

            template = paragraphs.FindNextTemplate(startTextIndex, _engineConfig);

            switch (template)
            {
                case SingleValueTemplate svt:
                    {
                        var endOfText = this.ProcessTemplate(svt, paragraphs, context);

                        paragraphs = paragraphs
                            .Skip(svt.Token.Position.ParagraphIndex)
                            .ToArray();

                        startTextIndex = endOfText;
                    }
                    break;

                case ArrayTemplate at:
                    {
                        var (lastParagraph, textEnd) = this.ProcessTemplate(at, paragraphs, context);
                        paragraphs = parent
                            .ChildElements
                            .OfType<Paragraph>()
                            .SkipWhile(p => p != lastParagraph)
                            .ToArray();
                        
                        startTextIndex = textEnd;
                    }
                    break;
                case ConditionTemplate ct:
                    {
                        var (lastParagraph, textEnd) = this.ProcessTemplate(ct, paragraphs, context);
                        startTextIndex = textEnd;
                    }
                    break;
            }
        } while (template != Template.Empty);
    }

    private int ProcessTemplate(SingleValueTemplate template, IReadOnlyCollection<Paragraph> bodyParagraphs, Model context)
    {
        var p = bodyParagraphs.ElementAt(template.Token.Position.ParagraphIndex);
        var model = context.Find(template.Token.ModelDescription.Expression);

        var textEndIndex = p.ReplaceToken(template.Token, model, _imageProcessor);
        return textEndIndex;
    }

    private (Paragraph, int) ProcessTemplate(
        ArrayTemplate template,
        IReadOnlyCollection<Paragraph> bodyParagraphs,
        Model context)
    {
        if (!(context.Find(template.Start.ModelDescription.Expression) is CollectionModel collection))
        {
            return (null, 0);
        }

        var startParagraph = bodyParagraphs.ElementAt(template.Start.Position.ParagraphIndex);
        var endParagraph = bodyParagraphs.ElementAt(template.End.Position.ParagraphIndex);

        if (startParagraph != endParagraph)
        {
            var s = startParagraph.NextSibling();
            while(s != endParagraph)
            {
                var t = s;
                s = t.NextSibling();
                t.Remove();
            }
        }

        var result = new List<OpenXmlElement>();
        var compositeElementProcessor = new CompositeElementProcessor(_engineConfig, _imageProcessor, _logger);

        foreach (var item in collection.Items)
        {
            var itemBody = template.OpenXml.CreateBody();
            compositeElementProcessor.Process(itemBody, item);

            result.AddRange(itemBody.ChildElements.Select(e => e.CloneNode(true)));
        }

        startParagraph.ReplaceToken(template.Start, Model.Empty, _imageProcessor);
        var textEnd = endParagraph.ReplaceToken(template.End, Model.Empty, _imageProcessor);

        foreach (var e in result)
        {
            endParagraph.InsertBeforeSelf(e);
        }

        return (endParagraph, textEnd);
    }

    private (Paragraph, int) ProcessTemplate(
        ConditionTemplate template,
        ICollection<Paragraph> bodyParagraphs,
        Model context)
    {
        if (!(context.Find(template.Start.ModelDescription.Expression) is ConditionModel conditionModel))
        {
            return (bodyParagraphs.ElementAt(template.End.Position.RowIndex), template.End.Position.TextIndex);
        }

        var startParagraph = bodyParagraphs.ElementAt(template.Start.Position.ParagraphIndex);
        startParagraph.ReplaceToken(template.Start, Model.Empty, _imageProcessor);

        var endParagraph = bodyParagraphs.ElementAt(template.End.Position.ParagraphIndex);
        var textEnd = endParagraph.ReplaceToken(template.End, Model.Empty, _imageProcessor);

        if (!conditionModel.IsFullfilled(template.Start.ModelDescription.Parameters))
        {
            bodyParagraphs.RemoveTextAndElementsBetween(template.Start.Position, template.End.Position);
        }

        return (endParagraph, textEnd);
    }
}
