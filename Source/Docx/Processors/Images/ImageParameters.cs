using System;

namespace Proxoft.TemplateEngine.Docx.Processors.Images;

internal class ImageParameters
{
    private const string parameterSeparator = ";";

    private readonly long? _maxWidth;
    private readonly long? _maxHeight;

    private ImageParameters(long? maxWidth, long? maxHeight)
    {
        _maxWidth = maxWidth;
        _maxHeight = maxHeight;
    }

    public (long width, long height) Scale(long imageWidth, long imageHeight)
    {
        var factor = this.CalculateFactor(imageWidth, imageHeight);
        var iw = (long)Math.Round(factor * imageWidth);
        var ih = (long)Math.Round(factor * imageHeight);
        return (iw, ih);
    }

    public static ImageParameters FromString(string parameters)
    {
        if (string.IsNullOrWhiteSpace(parameters))
        {
            return new ImageParameters(null, null);
        }

        string[] parts = parameters.Split(parameterSeparator);
        long? maxWidth = parts.WidthInEmu();
        long? maxHeight = parts.HeightInEmu();
        return new ImageParameters(maxWidth, maxHeight);
    }

    private double CalculateFactor(long imageWidth, long imageHeight)
    {
        if (_maxWidth == null && _maxHeight == null)
        {
            return 1;
        }

        var wf = CalculateFactor(_maxWidth, imageWidth);
        var hf = CalculateFactor(_maxHeight, imageHeight);
        return Math.Min(wf, hf);
    }

    private static double CalculateFactor(long? maximum, long current)
    {
        return maximum == null || maximum > current
            ? 1d
            : 1d * maximum.Value / current;
    }

    public override string ToString()
    {
        return $"{_maxWidth}x{_maxHeight}";
    }
}
