using System.IO;
using Proxoft.TemplateEngine.Docx.DataModel;

namespace Proxoft.TemplateEngine.Docx.Tests;

public class ImageModelTest
{
    private readonly FolderConfig _folderConfig = FolderConfig.Default.Subfolder("Images");

    [Fact]
    public void Image()
    {
        ObjectModel root = this.CreateDocumentModel("sample.jpeg");
        nameof(Image).ReplacePlaceholders(root, _folderConfig);
    }

    [Fact]
    public void ImageInTheMiddleOfRun()
    {
        ObjectModel model = this.CreateDocumentModel("sample.jpeg");
        nameof(ImageInTheMiddleOfRun).ReplacePlaceholders(model, _folderConfig);
    }

    [Fact]
    public void ImageSizeParameters()
    {
        ObjectModel model = this.CreateDocumentModel("sample.jpeg");
        nameof(ImageSizeParameters).ReplacePlaceholders(model, _folderConfig);
    }

    [Fact]
    public void ImageSizeParametersWidth()
    {
        ObjectModel model = this.CreateDocumentModel("sample.jpeg");
        nameof(ImageSizeParametersWidth).ReplacePlaceholders(model, _folderConfig);
    }

    [Fact]
    public void ImageSizeParametersHeight()
    {
        ObjectModel model = this.CreateDocumentModel("sample.jpeg");
        nameof(ImageSizeParametersHeight).ReplacePlaceholders(model, _folderConfig);
    }

    [Fact]
    public void ImageSizeParametersInch()
    {
        ObjectModel model = this.CreateDocumentModel("sample.jpeg");
        nameof(ImageSizeParametersInch).ReplacePlaceholders(model, _folderConfig);
    }

    [Fact]
    public void ImageInTable()
    {
        ObjectModel root = this.CreateDocumentModel("sample.jpeg");
        nameof(ImageInTable).ReplacePlaceholders(root, _folderConfig);
    }

    [Fact]
    public void EmptyImage()
    {
        ObjectModel root = this.CreateDocumentModel(new ImageModel("empty.jpeg", []));
        nameof(EmptyImage).ReplacePlaceholders(root, _folderConfig);
    }

    private ObjectModel CreateDocumentModel(string imageName)
    {
        var imageModel = this.LoadFromFile(imageName);
        return this.CreateDocumentModel(imageModel);
    }

    private ObjectModel CreateDocumentModel(ImageModel imageModel) =>
        ObjectModel.Create(
            ("image", imageModel)
        );

    private ImageModel LoadFromFile(string imageName)
    {
        var data = File.ReadAllBytes(_folderConfig.SamplesFolder  + "/" + imageName);
        return new ImageModel(imageName, data);
    }
}
