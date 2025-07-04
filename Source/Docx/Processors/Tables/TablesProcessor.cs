﻿using System.Collections.Generic;
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

internal class TablesProcessor
{
    private readonly EngineConfig _engineConfig;
    private readonly IImageProcessor _imageProcessor;
    private readonly ILogger _logger;

    public TablesProcessor(EngineConfig engineConfig, IImageProcessor imageProcessor, ILogger logger)
    {
        _engineConfig = engineConfig;
        _imageProcessor = imageProcessor;
        _logger = logger;
    }

    public void Process(OpenXmlCompositeElement parent, Model context)
    {
        var tables = parent
            .Childs<Table>();

        foreach(var table in tables)
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
            template = table.FindNextTemplate(_engineConfig);
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

    private int ProcessTemplate(ArrayTemplate template, Table table, Model context)
    {
        if (!(context.Find(template.Start.ModelDescription.Expression) is CollectionModel collection))
        {
            return template.End.Position.RowIndex + 1;
        }

        var resultRows = new List<TableRow>();
        foreach(var item in collection.Items)
        {
            foreach(var row in template.OpenXml.Elements.Select(e => e.CloneNode(true)).Cast<TableRow>())
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

    private void ProcessRowsBetweenIndeces(
        Table table,
        int firstIndex,
        int lastIndex,
        Model context)
    {
        if(lastIndex == -1)
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
        var compositeElementProcessor = new CompositeElementProcessor(_engineConfig, _imageProcessor, _logger);

        foreach (var cell in row.Cells())
        {
            compositeElementProcessor.Process(cell, context);
        }
    }
}
