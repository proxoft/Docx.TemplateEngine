using DocumentFormat.OpenXml.Wordprocessing;
using Proxoft.TemplateEngine.Docx.DataModel;

namespace Proxoft.Docx.TemplateEngine.Processors
{
    internal interface IImageProcessor
    {
        Run AddImage(ImageModel model, string parameters);
    }
}
