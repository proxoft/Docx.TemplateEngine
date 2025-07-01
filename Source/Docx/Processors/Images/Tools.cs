using DocumentFormat.OpenXml.Packaging;

namespace Proxoft.TemplateEngine.Docx.Processors.Images;

internal static class Tools
{
    public static PartTypeInfo ImagePartTypeFromName(this string imageName)
    {
        int index = imageName.LastIndexOf('.');
        if (index == -1)
        {
            return ImagePartType.Bmp;
        }

        string extension = imageName[(index + 1)..].ToLower();
        return extension switch
        { 
            "png" => ImagePartType.Png,
            "jpg" => ImagePartType.Jpeg,
            "jpeg" => ImagePartType.Jpeg,
            "bmp" => ImagePartType.Bmp,
            "emf" => ImagePartType.Emf,
            "gif" => ImagePartType.Gif,
            "ico" => ImagePartType.Icon,
            "tiff" => ImagePartType.Tiff,
            "wmf" => ImagePartType.Wmf,
            "pcx" => ImagePartType.Pcx,
            _ => ImagePartType.Bmp
        };
    }
}
