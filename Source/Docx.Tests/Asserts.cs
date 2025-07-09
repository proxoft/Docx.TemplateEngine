using System;
using DocumentFormat.OpenXml.Packaging;

namespace Proxoft.TemplateEngine.Docx.Tests;

internal static class Asserts
{
    public static void AssertResultAndExpectedInnerTextAreaEqual(this string wordOutputFileName)
    {
        using WordprocessingDocument docx = WordprocessingDocument.Open(wordOutputFileName, false);

        string innerText = docx.MainDocumentPart?.Document?.Body?.InnerText ?? "";

        Assert.NotEmpty(innerText);

        int i1 = innerText.IndexOf('=');
        int i2 = innerText.LastIndexOf('=');

        if (i1 == -1)
            Assert.Fail("Document does not contain RESULT / EXPECTED delimiter \"=...=\")");

        string result = innerText[..i1]
            .Trim()
            .Replace("RESULT", "");

        string expected = innerText[(i2+1)..]
            .Trim()
            .Replace("EXPECTED", "");

        Assert.Equal(expected, result, StringComparer.OrdinalIgnoreCase);
    }
}
