﻿using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Proxoft.Docx.TemplateEngine
{
    internal static class OpenXmlExtensions
    {
        public static IEnumerable<T> Childs<T>(this OpenXmlCompositeElement parent) => parent
                .ChildElements
                .OfType<T>();

        public static IEnumerable<Paragraph> Paragraphs(this OpenXmlCompositeElement parent) => parent
                .Childs<Paragraph>();

        public static IEnumerable<Run> Runs(this Paragraph paragraph) => paragraph
                .Childs<Run>();

        public static IEnumerable<TableRow> Rows(this Table table) => table
                .Childs<TableRow>();

        public static IEnumerable<TableCell> Cells(this TableRow row) => row
                .Childs<TableCell>();

        public static IEnumerable<T> ItemsAfter<T>(this IEnumerable<T> source, T afterItem)
        {
            return source
                .SkipWhile(i => i.Equals(afterItem))
                .Skip(1);
        }

        public static void InsertSelfAfter<T>(this IEnumerable<T> children, T afterItem)
            where T : OpenXmlElement
        {
            foreach(var c in children.Reverse())
            {
                afterItem.InsertAfterSelf(c);
            }
        }

        public static void RemoveSelfFromParent<T>(this IEnumerable<T> children)
            where T: OpenXmlElement
        {
            foreach(var c in children)
            {
                c.Remove();
            }
        }
    }
}
