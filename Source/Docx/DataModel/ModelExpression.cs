﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Proxoft.Docx.TemplateEngine.DataModel
{
    [DebuggerDisplay("{ToExpressionString()}")]
    internal class ModelExpression
    {
        private readonly string[] _segments;

        public ModelExpression(IEnumerable<string> segments)
        {
            _segments = segments.ToArray();
        }

        public string Root => _segments.FirstOrDefault() ?? string.Empty;
        public string Name => _segments.LastOrDefault() ?? string.Empty;

        public bool IsFinal => _segments.Length <= 1;

        public ModelExpression Child()
        {
            return new ModelExpression(_segments.Skip(1));
        }

        public string ToExpressionString(string nameSeparator = ".")
        {
            return string.Join(nameSeparator, _segments);
        }
    }
}
