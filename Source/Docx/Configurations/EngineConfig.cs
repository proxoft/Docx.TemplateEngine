using System;

namespace Proxoft.TemplateEngine.Docx.Configurations;

public class EngineConfig
{
    public static readonly EngineConfig Default = new(PlaceholderConfig.Default, ArrayConfig.Default, ConditionConfig.Default, "$");

    public EngineConfig(
        PlaceholderConfig placeholder,
        ArrayConfig arrayConfig,
        ConditionConfig condition,
        string thisCharacter)
    {
        this.Placeholder = placeholder;
        this.Array = arrayConfig;
        this.Condition = condition;
        this.ThisCharacter = thisCharacter.Contains('.')
            ? throw new ArgumentOutOfRangeException(nameof(thisCharacter), "cannot contain '.'")
            : thisCharacter;
    }

    public PlaceholderConfig Placeholder { get; }
    public ArrayConfig Array { get; }
    public ConditionConfig Condition { get; }
    public string ThisCharacter { get; }
}
