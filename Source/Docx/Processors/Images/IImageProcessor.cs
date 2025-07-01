using DocumentFormat.OpenXml.Wordprocessing;
using Proxoft.TemplateEngine.Docx.DataModel;

namespace Proxoft.TemplateEngine.Docx.Processors.Images;

internal interface IImageProcessor
{
    Run AddImage(ImageModel model, string parameters);
}
