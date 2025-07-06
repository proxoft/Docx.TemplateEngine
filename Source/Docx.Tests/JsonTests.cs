namespace Proxoft.TemplateEngine.Docx.Tests;

public class JsonTests
{
    private const string NONAME_JSON = "{\"$$_rootName\": \"\", \"$$_type\": \"ObjectModel\", \"simple\":{\"$$_type\": \"SimpleModel\", \"$$_value\": \"1\"}}";
    private const string JSON = "{\"$$_rootName\": \"root\", \"$$_type\": \"ObjectModel\", \"simple\":{\"$$_type\": \"SimpleModel\", \"$$_value\": \"1\"}, \"conditionTrue\":{\"$$_type\": \"ConditionModel\", \"$$_value\": \"true\"}, \"conditionFalse\":{\"$$_type\": \"ConditionModel\", \"$$_value\": \"false\"}, \"collection\":{\"$$_type\": \"CollectionModel\", \"$$_items\": [{\"$$_type\": \"SimpleModel\", \"$$_value\": \"1\"}, {\"$$_type\": \"SimpleModel\", \"$$_value\": \"2\"}, {\"$$_type\": \"SimpleModel\", \"$$_value\": \"3\"}], \"$$_itemName\": \"$c\"}, \"image\":{\"$$_type\": \"ImageModel\", \"$$_name\": \"image.png\", \"$$_value\": \"AQIDBA==\"}}";

    private const string EXPECTED_JSON = @"{
    ""_type"": ""DocumentModel""
    ""simple"": {
        ""_type"": ""SimpleModel"",
        ""value"": ""1""
    },
    ""conditionTrue"": {
        ""_type"": ""ConditionModel"",
        ""value"": true
    },
    ""conditionFalse"": {
        ""_type"": ""ConditionModel"",
        ""value"": false
    },
    ""collection"": {
        ""_type"": ""CollectionModel"",
        ""items"": [
            {
                ""_type"": ""SimpleModel"",
                ""Value"": ""1""
            },
            {
                ""_type"": ""SimpleModel"",
                ""Value"": ""2""
            },
            {
                ""_type"": ""SimpleModel"",
                ""Value"": ""3""
            }
        ],
    },
    ""image"": {
        ""_type"": ""ImageModel"",
        ""name"": ""image.png"",
        ""value"": ""AQIDBA==""
    }
}";

    [Fact(Skip = "not working")]
    public void Serialize()
    {
        //ObjectModel root = new(
        //    "",
        //    new SimpleModel("simple", "1"),
        //    new ConditionModel("conditionTrue", () => true),
        //    new ConditionModel("conditionFalse", () => false),
        //    new CollectionModel(
        //        "collection",
        //        [
        //            new SimpleModel("$c", "1"),
        //            new SimpleModel("$c", "2"),
        //            new SimpleModel("$c", "3"),
        //        ],
        //        []
        //    ),
        //    new ImageModel("image", "image.png", [1 ,2, 3, 4])
        //);

        //string json = Serialization.Serializer.Serialize(root);

        //Assert.Equal(
        //    EXPECTED_JSON,
        //    json);
    }

    [Fact(Skip = "not working")]
    public void SerializeModelWithoutName()
    {
        //var root = new ObjectModel(
        //    "",
        //    new SimpleModel("simple", "1")
        //);


        //string json = Serialization.Serializer.Serialize(root);

        //Assert.Equal(
        //    NONAME_JSON,
        //    json);
    }

    [Fact(Skip = "not working")]
    public void Deserialize()
    {
        //ObjectModel? root = Serialization.Serializer.Deserialize(JSON) as ObjectModel;
        //Assert.NotNull(root);
    }
}
