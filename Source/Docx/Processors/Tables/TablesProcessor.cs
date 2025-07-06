using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Logging;
using Proxoft.TemplateEngine.Docx.Common;
using Proxoft.TemplateEngine.Docx.Configurations;
using Proxoft.TemplateEngine.Docx.DataModel;
using Proxoft.TemplateEngine.Docx.Processors.Images;
using Proxoft.TemplateEngine.Docx.Processors.Searching;

namespace Proxoft.TemplateEngine.Docx.Processors.Tables;

internal sealed class TablesProcessor(ImageProcessor imageProcessor, EngineConfig engineConfig, ILogger logger) : Processor(engineConfig, logger)
{
    private readonly ImageProcessor _imageProcessor = imageProcessor;

    public void Process(OpenXmlCompositeElement parent, Model context)
    {
        foreach (var table in parent.Childs<Table>())
        {
            this.Process(table, context);
        }
    }

    private void Process(Table table, Model context)
    {
        Template template;
        var lastProcessedTableRow = -1;
        do
        {
            template = table.FindNextTemplate(this.EngineConfig);
            switch (template)
            {
                case ArrayTemplate at:
                    this.ProcessRowsBetweenIndeces(table, lastProcessedTableRow, at.Start.Position.RowIndex - 1, context);
                    lastProcessedTableRow = this.ProcessTemplate(at, table, context);
                    break;
                case ConditionTemplate ct:
                    break;
            }
        }
        while (template != Template.Empty);

        this.ProcessRowsBetweenIndeces(
            table,
            lastProcessedTableRow,
            table.Rows().Count(),
            context);
    }

    private void ProcessRowsBetweenIndeces(
        Table table,
        int firstIndex,
        int lastIndex,
        Model context)
    {
        if (lastIndex == -1)
        {
            return;
        }

        var skipCount = firstIndex < 0 ? 0 : firstIndex;
        var takeCount = lastIndex == firstIndex
            ? 1
            : lastIndex - firstIndex;

        foreach (var row in table.Rows().Skip(skipCount).Take(takeCount))
        {
            this.ProcessCellsOfRow(row, context);
        }
    }

    private void ProcessCellsOfRow(TableRow row, Model context)
    {
        CompositeElementProcessor compositeProcessor = new(_imageProcessor, this.EngineConfig, this.Logger);
        foreach (var cell in row.Cells())
        {
            compositeProcessor.Process(cell, context);
        }
    }

    private int ProcessTemplate(ArrayTemplate template, Table table, Model context)
    {
        if (!(context.Find(template.Start.ModelDescription.Expression) is CollectionModel collection))
        {
            return template.End.Position.RowIndex + 1;
        }

        var resultRows = new List<TableRow>();
        foreach (var item in collection.Items)
        {
            foreach (var row in template.OpenXml.Elements.Select(e => e.CloneNode(true)).Cast<TableRow>())
            {
                this.ProcessCellsOfRow(row, item);
                resultRows.Add(row);
            }
        }

        var originalRows = table.Rows()
            .GetTemplateRows(template)
            .ToArray();

        for (var i = 0; i < resultRows.Count; i++)
        {
            originalRows.First().InsertBeforeSelf(resultRows[i]);
        }

        originalRows.RemoveSelfFromParent();

        return template.Start.Position.RowIndex + resultRows.Count;
    }
}
