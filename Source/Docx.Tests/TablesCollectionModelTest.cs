using Proxoft.TemplateEngine.Docx.DataModel.v2;

namespace Proxoft.TemplateEngine.Docx.Tests;

public class TablesCollectionModelTest
{
    private readonly FolderConfig _folderConfig = FolderConfig.Default.Subfolder("Tables_Collections");

    [Fact]
    public void CollectionOfObjectModels()
    {
        ObjectModel documentModel = ObjectModel.Create(
            ("collection", new CollectionModel(
                [
                    ObjectModel.Create(
                        ("prop", new ValueModel("1")),
                        ("otherProp", new ValueModel("a"))),
                    ObjectModel.Create(
                        ("prop", new ValueModel("2")),
                        ("otherProp", new ValueModel("b"))),
                    ObjectModel.Create(
                        ("prop", new ValueModel("3")),
                        ("otherProp", new ValueModel("c"))),
                    ObjectModel.Create(
                        ("prop", new ValueModel("4")),
                        ("otherProp", new ValueModel("d"))),
                ]
            ))
        );

        nameof(CollectionOfObjectModels).ReplacePlaceholders(documentModel, _folderConfig);
    }

    [Fact]
    public void CollectionOfValueModels()
    {
        ObjectModel documentModel = ObjectModel.Create(
            ("collection", new CollectionModel(
                [
                    new ValueModel("1"),
                    new ValueModel("2"),
                    new ValueModel("3"),
                    new ValueModel("4"),
                    new ValueModel("5")
                ]
            ))
        );

        nameof(CollectionOfValueModels).ReplacePlaceholders(documentModel, _folderConfig);
    }

    [Fact]
    public void CollectionOfValueModelsInSingleCell()
    {
        ObjectModel documentModel = ObjectModel.Create(
            ("collection", new CollectionModel(
                [
                    new ValueModel("1"),
                    new ValueModel("2"),
                    new ValueModel("3"),
                    new ValueModel("4"),
                    new ValueModel("5")
                ]
            ))
        );

        nameof(CollectionOfValueModelsInSingleCell).ReplacePlaceholders(documentModel, _folderConfig);
    }

    [Fact]
    public void CollectionModelTableWithNonTemplateRows()
    {
        ObjectModel documentModel = ObjectModel.Create(
            ("collection", new CollectionModel(
                [
                    new ValueModel("1"),
                    new ValueModel("2"),
                    new ValueModel("3"),
                ]
            ))
        );

        nameof(CollectionModelTableWithNonTemplateRows).ReplacePlaceholders(documentModel, _folderConfig);
    }

    [Fact]
    public void RepeatedCollectionModelInSameTableWithNonTemplateRows()
    {
        ObjectModel documentModel = ObjectModel.Create(
            ("collection", new CollectionModel(
                [
                    new ValueModel("1"),
                    new ValueModel("2"),
                    new ValueModel("3"),
                ]
            ))
        );

        nameof(RepeatedCollectionModelInSameTableWithNonTemplateRows).ReplacePlaceholders(documentModel, _folderConfig);
    }

    [Fact]
    public void CollectionModelTableWithMixedTemplates()
    {
        ObjectModel documentModel = ObjectModel.Create(
            ("collection", new CollectionModel(
                [
                    new ValueModel("1"),
                    new ValueModel("2"),
                    new ValueModel("3"),
                ]
            )),
            ("simpleValue", new ValueModel("XYZ content"))
        );

        nameof(CollectionModelTableWithMixedTemplates).ReplacePlaceholders(documentModel, _folderConfig);
    }
}
