using Proxoft.TemplateEngine.Docx.DataModel.v2;

namespace Proxoft.TemplateEngine.Docx.Tests;

public class ParagraphObjectModelTest
{
    private readonly FolderConfig _folderConfig = FolderConfig.Default.Subfolder("Paragraphs_ObjectModel");

    private readonly ObjectModel _documentModel = new(
       new()
       {
            { "object", new ObjectModel(new() {
                { "a",  new ValueModel("A - value") },
                { "b",  new ValueModel("B - value") },
                { "c",  new ValueModel("C - value") },
            }) }
       }
    );

    private readonly ObjectModel _nestedDocumentModel =ObjectModel.Create(
        ("level1", ObjectModel.Create(
            ("vl1", new ValueModel("value in level1")),
            ("level2", ObjectModel.Create(
                ("vl2", new ValueModel("value in level2")),
                ("level3", ObjectModel.Create(
                    ("vl3", new ValueModel("value in level3"))
                ))
            ))
        ))
    );

    [Fact]
    public void ObjectModelBase()
    {
        nameof(ObjectModelBase).ReplacePlaceholders(_documentModel, _folderConfig);
    }

    [Fact]
    public void ObjectModelMultipleValues()
    {
        nameof(ObjectModelMultipleValues).ReplacePlaceholders(_documentModel, _folderConfig);
    }

    [Fact]
    public void NestedObjectModel()
    {
         nameof(NestedObjectModel).ReplacePlaceholders(_nestedDocumentModel, _folderConfig);
    }

    [Fact]
    public void NestedObjectModelMultipleParagraphs()
    {
        nameof(NestedObjectModelMultipleParagraphs).ReplacePlaceholders(_nestedDocumentModel, _folderConfig);
    }
}
