using Microsoft.AspNetCore.Mvc;

namespace NeuroSpecBackend.Controllers
{
    public class FHIRPatientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
