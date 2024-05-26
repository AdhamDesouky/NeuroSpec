using NeuroSpecCompanion.Services.OCR_Service;
using NeuroSpecCompanion.Services.PDF_OCR_Service;

namespace NeuroSpecCompanion.Views;

public partial class MedicalHistoryPage : ContentPage
{
    private readonly IPDFOCRService _pdfService;
    private readonly IOCRService _ocrService;

    public MedicalHistoryPage()
	{
		InitializeComponent();
        _pdfService = new PDFOCRService();
        _ocrService = new OCRService();
	}

    private async void OnSelectPdfClicked(object sender, EventArgs e)
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Please select a PDF",
            FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.iOS, new[] { "public.pdf" } }, // or "com.adobe.pdf"
                { DevicePlatform.Android, new[] { "application/pdf" } },
                { DevicePlatform.WinUI, new[] { ".pdf" } },
                { DevicePlatform.MacCatalyst, new[] { "pdf" } }
            })
        });

        if (result != null)
        {
            using var stream = await result.OpenReadAsync();
            var pdfText = await _pdfService.ReadTextFromPDFAsync(stream);
            PdfTextLabel.Text = pdfText;
        }
    }

    private async void OnSelectImageClicked(object sender, EventArgs e)
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Please select an image",
            FileTypes = FilePickerFileType.Images
        });

        if (result != null)
        {
            using var stream = await result.OpenReadAsync();
            SelectedImage.Source = ImageSource.FromStream(() => stream);

            var ocrResult = await _ocrService.ReadTextFromImageAsync(stream);
            PdfTextLabel.Text = ocrResult;
        }

    }
}