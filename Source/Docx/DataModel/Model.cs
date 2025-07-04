﻿using System.Diagnostics;

namespace Proxoft.TemplateEngine.Docx.DataModel;

[DebuggerDisplay("{Name}")]
public abstract class Model(string name)
{
    public static readonly Model Empty = new EmptyModel();
    public static readonly Model Exception = new ExceptionModel();

    protected Model Parent { get; private set; } = Empty;

    public string Name { get; set; } = name;

    public abstract string FormattedValue();

    internal abstract Model Find(ModelExpression expression);

    internal void SetParent(Model context)
    {
        this.Parent = context;
    }

    private class EmptyModel : Model
    {
        public EmptyModel() : base(string.Empty)
        {
        }

        public override string FormattedValue()
        {
            return string.Empty;
        }

        internal override Model Find(ModelExpression expression)
        {
            return this;
        }
    }

    private class ExceptionModel : Model
    {
        public ExceptionModel() : base(string.Empty)
        {
        }

        public override string FormattedValue()
        {
            throw new System.Exception("Exception model");
        }

        internal override Model Find(ModelExpression expression)
        {
            throw new System.Exception("Exception model");
        }
    }
}
