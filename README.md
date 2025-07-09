# Docx.TemplateEngine
Docx TemplateEngine is a tool for processing Docx templates. The user create a docx document with custom placeholders e.g. {order.totalPrice}.
It is necessary to create a corresponding Template model tree in the code:

```cs
ObjectModel documentModel = ObjectModel.Create(
    ("order", ObjectModel.Create(("totalPrice", "256.00")))
);
```

And process the template:

```cs
using FileStream templateStream = File.Open(inputFileName, FileMode.Open, FileAccess.Read);

DocumentEngine engine = new(NullLogger<DocumentEngine>.Instance);
byte[] docx = engine.Run(templateStream, documentModel, config);

File.WriteAllBytes("C:/Temp/processedDocument.docx", docx);
```

## Example

```cs
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
```

The word template:
![screenshotTemplate](./TemplateDocx.png)

Filled document:
![filledDocument](./FilledDocx.png)

