using DocumentFormat.OpenXml.Wordprocessing;
using Proxoft.Docx.TemplateEngine.DataModel;

namespace Proxoft.Docx.TemplateEngine.Processors
{
    internal interface IImageProcessor
    {
        Run AddImage(ImageModel model, string parameters);
    }
}
