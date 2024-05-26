using Microsoft.AspNetCore.Mvc;
using NeuroSpecBackend.Services;

namespace NeuroSpecBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatbotController : Controller
    {
        private readonly ChatbotService _chatbotService;
        public ChatbotController(ChatbotService chatbotService)
        {
            _chatbotService = chatbotService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string question)
        {
            string response = await _chatbotService.ProcessMessageAsync(question);

            return Ok(response);
        }
    }
}
