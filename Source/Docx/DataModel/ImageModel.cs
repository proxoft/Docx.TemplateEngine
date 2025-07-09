namespace Proxoft.TemplateEngine.Docx.DataModel;

public sealed class ImageModel(string imageName, byte[] data) : ValueModelBase
{
    public string ImageName { get; } = imageName;

    public byte[] Data { get; } = data;
}
