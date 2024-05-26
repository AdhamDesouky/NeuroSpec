using IronOcr;

namespace NeuroSpecCompanion.Services.OCR_Service
{
    public class OCRService: IOCRService
    {
        
        public async Task<string> ReadTextFromImageAsync(Stream imageStream)
        {
            IronOcr.License.LicenseKey = "IRONSUITE.ABDELRAHMANSALEHPRO.GMAIL.COM.4806-6342698CC4-A6MJQETJ4VIPJ6UX-LFTTCCDGIJZT-L7JG2VDNZV65-EI7NKB6SRMH3-BIVU2RSJINGV-2DGSHLGJREIK-NUTPZY-TEWYNME72SOMUA-DEPLOYMENT.TRIAL-RHDI6K.TRIAL.EXPIRES.24.JUN.2024";
            // Create a temporary file path
            var tempFilePath = Path.Combine(Path.GetTempPath(), $"{Path.GetRandomFileName()}.tmp");

            // Save the stream to the temporary file
            using (var fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
            {
                await imageStream.CopyToAsync(fileStream);
            }

            try
            {
                var ocr = new IronTesseract();

                using (var ocrInput = new OcrInput())
                {
                    ocrInput.LoadImage(tempFilePath);
                    //ocrInput.LoadPdf("document.pdf");

                    // Optionally Apply Filters if needed:
                    // ocrInput.Deskew();  // use only if image not straight
                    // ocrInput.DeNoise(); // use only if image contains digital noise

                    var ocrResult = ocr.Read(ocrInput);
                    return await Task.FromResult(ocrResult.Text);

                }

            }
            finally
            {
                // Clean up the temporary file
                if (File.Exists(tempFilePath))
                {
                    File.Delete(tempFilePath);
                }
            }
        }
    }
}
