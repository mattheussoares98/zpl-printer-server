using System.Collections.Generic;
using System.IO;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;

public static class PdfMerger
{
    public static byte[] Merge(IEnumerable<byte[]> pdfPages)
    {
        var output = new PdfDocument();
        foreach (var pageBytes in pdfPages)
        {
            using var ms = new MemoryStream(pageBytes);
            // Import the single-page PDF
            var input = PdfReader.Open(ms, PdfDocumentOpenMode.Import);
            for (int i = 0; i < input.PageCount; i++)
            {
                output.AddPage(input.Pages[i]);
            }
        }
        using var outMs = new MemoryStream();
        // Save without incremental updates
        output.Save(outMs, false);
        return outMs.ToArray();
    }
}
