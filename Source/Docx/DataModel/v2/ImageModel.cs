namespace Proxoft.TemplateEngine.Docx.DataModel.v2;

public sealed class ImageModel(string imageName, byte[] data) : ValueModelBase
{
    public string ImageName { get; } = imageName;

    public byte[] Data { get; } = data;
}
