using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroSpecCompanion.Services.OCR_Service
{
    public interface IOCRService
    {
        Task<string> ReadTextFromImageAsync(Stream imageStream);
    }
}
