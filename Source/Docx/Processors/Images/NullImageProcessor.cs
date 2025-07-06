using DocumentFormat.OpenXml.Wordprocessing;
using Proxoft.TemplateEngine.Docx.DataModel.v2;

namespace Proxoft.TemplateEngine.Docx.Processors.Images;

internal sealed class NullImageProcessor : IImageProcessorV2
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
