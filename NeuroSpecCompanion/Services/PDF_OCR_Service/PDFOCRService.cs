using System.Text;
using UglyToad.PdfPig;

namespace NeuroSpecCompanion.Services.PDF_OCR_Service
{
    public class PDFOCRService : IPDFOCRService
    {
        public async Task<string> ReadTextFromPDFAsync(Stream pdfStream)
        {
            var sb = new StringBuilder();

            using (var pdfDocument = PdfDocument.Open(pdfStream))
            {
                foreach (var page in pdfDocument.GetPages())
                {
                    sb.Append(page.Text);
                }
            }

            return await Task.FromResult(sb.ToString());
        }
    }
}
