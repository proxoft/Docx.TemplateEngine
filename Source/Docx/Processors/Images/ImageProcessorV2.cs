using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Logging;
using Proxoft.TemplateEngine.Docx.Configurations;
using Proxoft.TemplateEngine.Docx.DataModel;
using SkiaSharp;

using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;

namespace Proxoft.TemplateEngine.Docx.Processors.Images;

internal sealed class ImageProcessorV2(MainDocumentPart mainDocumentPart, EngineConfig engineConfig, ILogger logger) : Processor(engineConfig, logger), IImageProcessorV2
{
    private readonly MainDocumentPart _mainDocumentPart = mainDocumentPart;

    public Run AddImage(ImageModel model, string parameters)
    {
        PartTypeInfo imagePartType = model.ImageName.ImagePartTypeFromName();
        ImagePart imagePart = _mainDocumentPart.AddImagePart(imagePartType);

        using (var ms = new MemoryStream(model.Data))
        {
            imagePart.FeedData(ms);
        }

        ImageParameters ip = ImageParameters.FromString(parameters);
        var (width, height) = this.GetImageSizeInEmu(model.Data, ip);
        Run run = this.CreateRun(model.ImageName, _mainDocumentPart.GetIdOfPart(imagePart), width, height);
        return run;
    }

    private Run CreateRun(string imageName, string relationshipId, long imageWidth, long imageHeight)
    {
        Drawing element = new(
         new DW.Inline(
             new DW.Extent() { Cx = imageWidth, Cy = imageHeight },
             new DW.EffectExtent()
             {
                 LeftEdge = 0L,
                 TopEdge = 0L,
                 RightEdge = 0L,
                 BottomEdge = 0L
             },
             new DW.DocProperties()
             {
                 Id = (UInt32Value)1U,
                 Name = "Picture"
             },
             new DW.NonVisualGraphicFrameDrawingProperties(
                 new A.GraphicFrameLocks() { NoChangeAspect = true }),
             new A.Graphic(
                 new A.GraphicData(
                     new PIC.Picture(
                         new PIC.NonVisualPictureProperties(
                             new PIC.NonVisualDrawingProperties()
                             {
                                 Id = 0U,
                                 Name = imageName
                             },
                             new PIC.NonVisualPictureDrawingProperties()),
                         new PIC.BlipFill(
                             new A.Blip(
                                 new A.BlipExtensionList(
                                     new A.BlipExtension()
                                     {
                                         Uri = "{28A0092B-C50C-407E-A947-70E740481C1C}"
                                     })
                             )
                             {
                                 Embed = relationshipId,
                                 CompressionState =
                                 A.BlipCompressionValues.Print
                             },
                             new A.Stretch(
                                 new A.FillRectangle())),
                         new PIC.ShapeProperties(
                             new A.Transform2D(
                                 new A.Offset() { X = 0L, Y = 0L },
                                 new A.Extents() { Cx = imageWidth, Cy = imageHeight }),
                             new A.PresetGeometry(
                                 new A.AdjustValueList()
                             )
                             { Preset = A.ShapeTypeValues.Rectangle }))
                 )
                 { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
         )
         {
             DistanceFromTop = 0U,
             DistanceFromBottom = 0U,
             DistanceFromLeft = 0U,
             DistanceFromRight = 0U,
             EditId = "50D07946"
         });

        Run run = new(element);
        return run;
    }

    private (long width, long height) GetImageSizeInEmu(byte[] data, ImageParameters imageParameters)
    {
        if (data.Length == 0)
        {
            return (0, 0);
        }

        long pixelWidth;
        long pixelHeight;

        SKImage image = SKImage.FromEncodedData(data);

        this.Logger.LogInformation("Image size in pixels: {0}x{1}", image.Width, image.Height);

        pixelWidth = image.Width.PxToEmu();
        pixelHeight = image.Height.PxToEmu();

        this.Logger.LogInformation("Image size in emu: {0}x{1}", pixelWidth, pixelHeight);

        var (width, height) = imageParameters.Scale(pixelWidth, pixelHeight);
        this.Logger.LogInformation("Scaled image size in emu: {0}x{1}", width, height);

        return (width, height);
    }
}
