using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using NeuroSpecCompanion.Services;
using NeuroSpecCompanion.Services.FHIR_Base;
using NeuroSpecCompanion.Services.OCR_Service;
using NeuroSpecCompanion.Services.PDF_OCR_Service;

//using NeuroSpecCompanion.Services;

namespace NeuroSpecCompanion
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>().UseMauiCommunityToolkitMediaElement()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddSingleton<ChatbotService>();
            builder.Services.AddSingleton<FirebaseService>();
            builder.Services.AddSingleton<IOCRService, OCRService>();
            builder.Services.AddSingleton<IPDFOCRService, PDFOCRService>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
