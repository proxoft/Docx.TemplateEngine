using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Proxoft.TemplateEngine.Docx.DataModel;

namespace Proxoft.TemplateEngine.Docx.Serialization;

internal class PolymorphicTypeResolver : DefaultJsonTypeInfoResolver
{
    public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        JsonTypeInfo jsonTypeInfo = base.GetTypeInfo(type, options);

        //if (jsonTypeInfo.Type == typeof(Model))
        //{
            //JsonPolymorphismOptions polymorphismOptions = new()
            //{
            //    TypeDiscriminatorPropertyName = "_type",
            //    IgnoreUnrecognizedTypeDiscriminators = true,
            //    UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToBaseType,
            //    DerivedTypes =
            //        {
            //            new JsonDerivedType(typeof(SimpleModel), "value"),
            //            new JsonDerivedType(typeof(ConditionModel), "condition"),
            //            new JsonDerivedType(typeof(ImageModel), "image"),
            //            new JsonDerivedType(typeof(ObjectModel), "object"),
            //            new JsonDerivedType(typeof(CollectionModel), "collection"),
            //        }
            //};

            //jsonTypeInfo.PolymorphismOptions = polymorphismOptions;
        //}

        return jsonTypeInfo;
    }
}
