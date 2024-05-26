using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSpecCompanion.Services.PDF_OCR_Service
{
    public interface IPDFOCRService
    {
        Task<string> ReadTextFromPDFAsync(Stream pdfStream);
    }
}
