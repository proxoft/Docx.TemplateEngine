using Proxoft.TemplateEngine.Docx.DataModel.v2;

namespace Proxoft.TemplateEngine.Docx.Tests;

public class TablesValueModelTest
{
    private readonly FolderConfig _folderConfig = FolderConfig.Default.Subfolder("Tables_ValueModel");

    [Fact]
    public void ValueModel()
    {
        ObjectModel documentModel = ObjectModel.Create(
            ( "xyz", new ValueModel("The real value of XYZ") )
        );

        nameof(ValueModel).ReplacePlaceholders(documentModel, _folderConfig);
    }
}
