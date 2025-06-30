using System;

namespace Proxoft.TemplateEngine.Docx.DataModel;

public class ImageModel(
    string name,
    string imageName,
    byte[] data) : Model(name)
{
    public ImageModel(
        string name,
        string imageName,
        string base64) : this(name, imageName, ImageSourceToByteArray(base64))
    {
    }

    public string ImageName { get; } = imageName;

    public byte[] Data { get; } = data;

    public override string FormattedValue()
    {
        return Convert.ToBase64String(this.Data);
    }

    internal override Model Find(ModelExpression expression)
    {
        if (expression.IsFinal && expression.Name == this.Name)
        {
            return this;
        }

        return this.Parent.Find(expression);
    }

    private static byte[] ImageSourceToByteArray(string imageData)
    {
        if (string.IsNullOrWhiteSpace(imageData))
        {
            return [];
        }

        var i = imageData.IndexOf(",", StringComparison.InvariantCultureIgnoreCase);
        var base64 = imageData[(i + 1)..];
        var imageByteData = Convert.FromBase64String(base64);
        return imageByteData;
    }
}

public static class ImageModelFactory
{
    public static ImageModel ToImageModel(this byte[] data, string name, string imageName)
    {
        return new ImageModel(name, imageName, data);
    }
}
