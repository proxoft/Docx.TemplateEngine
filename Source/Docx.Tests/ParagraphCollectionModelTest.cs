using System.Linq;
using Proxoft.TemplateEngine.Docx.DataModel;

namespace Proxoft.TemplateEngine.Docx.Tests;

public class ParagraphCollectionModelTest
{
    private readonly FolderConfig _folderConfig = FolderConfig.Default.Subfolder("Paragraphs_Collections");

    [Fact]
    public void CollectionModel()
    {
        ObjectModel documentModel = ObjectModel.Create(
            (
                "root",
                new CollectionModel(Enumerable.Range(0, 5).Select(i => new ValueModel($"{i}")))
            )
        );

        nameof(CollectionModel)
            .ReplacePlaceholders(documentModel, _folderConfig)
            .AssertResultAndExpectedInnerTextAreaEqual();
    }

    [Fact]
    public void CollectionOfObjectModel()
    {
        ObjectModel[] childs = [
            ..Enumerable
                .Range(0, 10)
                .Select(i => ObjectModel.Create(
                    ( "value", new ValueModel($"Value of Child {i}") )
                )),
        ];

        ObjectModel documentModel = ObjectModel.Create(
            ( "collection", new CollectionModel(childs))
        );

        nameof(CollectionOfObjectModel).ReplacePlaceholders(documentModel, _folderConfig);
    }

    [Fact(Skip = "Not implemented yet")]
    public void CollectionOfObjectModelWithCondition()
    {
        ObjectModel[] childs = [
            ..Enumerable
                .Range(0, 5)
                .Select(i => ObjectModel.Create(
                    ("value", new ValueModel($"Value {i}")),
                    ("condition", new ConditionModel(i % 2 == 0))
                ))
        ];

        ObjectModel documentModel = ObjectModel.Create(
            ("collection", new CollectionModel(childs))
        );

        nameof(CollectionOfObjectModelWithCondition)
            .ReplacePlaceholders(documentModel, _folderConfig)
            .AssertResultAndExpectedInnerTextAreaEqual();
    }

    [Fact]
    public void CollectionModelParagraphs()
    {
        ObjectModel documentModel = ObjectModel.Create(
            (
                "root",
                new CollectionModel(Enumerable.Range(0, 5).Select(i => new ValueModel($"{i}")))
            )
        );

        nameof(CollectionModelParagraphs).ReplacePlaceholders(documentModel, _folderConfig);
    }

    [Fact]
    public void CollectionModelWithTableInParagraphs()
    {
        ObjectModel documentModel = ObjectModel.Create(
           (
               "root",
               new CollectionModel(Enumerable.Range(0, 5).Select(i => new ValueModel($"{i}")))
           )
       );

        nameof(CollectionModelWithTableInParagraphs).ReplacePlaceholders(documentModel, _folderConfig);
    }
}
