using DocumentFormat.OpenXml.Wordprocessing;
using Proxoft.TemplateEngine.Docx.DataModel;

namespace Proxoft.TemplateEngine.Docx.Processors.Images;

internal interface IImageProcessorV2
{
    Run AddImage(ImageModel model, string parameters);
}