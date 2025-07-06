using DocumentFormat.OpenXml.Wordprocessing;
using Proxoft.TemplateEngine.Docx.DataModel;

namespace Proxoft.TemplateEngine.Docx.Processors.Images;

internal sealed class NullImageProcessor : IImageProcessor
{
    public static readonly NullImageProcessor Instance = new();

    private NullImageProcessor()
    {
    }

    public Run AddImage(ImageModel model, string parameters)
    {
        return new Run();
    }
}
