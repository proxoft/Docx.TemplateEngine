using System.IO;
using System.Linq;
using Proxoft.TemplateEngine.Docx.DataModel;

namespace Proxoft.TemplateEngine.Docx.Tests;

public class ExamplesTest
{
    private readonly FolderConfig _folderConfig = FolderConfig.Default.Subfolder("Examples");

    [Fact]
    public void Example()
    {
        ObjectModel root = ObjectModel.Create(
            ( "title" , new ValueModel("Example Document") ),
            ( "description", new ValueModel("very short description")),
            ( "list",
               new CollectionModel(
                    Enumerable.Range(0, 5).Select(i => ObjectModel.Create(
                        ("name", new ValueModel($"Name {i * 10}")),
                        ("value", new ValueModel($"Value {i}"))
                    ))
                )
            ),
            (
                "nested",
                ObjectModel.Create(
                    ("text", new ValueModel("Nested value"))
                )
            ),
            (
                "condition",
                new ConditionModel(true)
            ),
            (
                "image",
                this.LoadFromFile("sample.jpeg")
            )
        );

        nameof(Example)
            .ReplacePlaceholders(root, _folderConfig);
    }

    private ImageModel LoadFromFile(string imageName)
    {
        var data = File.ReadAllBytes(_folderConfig.SamplesFolder + "/" + imageName);
        return new ImageModel(imageName, data);
    }
}
