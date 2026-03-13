using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/telegram")]
    public class TelegramController : ControllerBase
    {
        private readonly string allowedUserId = "123456789"; // Replace with your wife's Telegram user ID
        private readonly ILogger _logger;
        private readonly ITelegramService _service;

        public TelegramController(ILogger<TelegramController> logger, ITelegramService service)
        {
            _logger = logger; // TODO: Add logging
            _service = service;
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook([FromBody] TelegramDTO dto)
        {
            if (dto?.Message == null)
                return Ok();

            if (dto.AllowedUserId != allowedUserId)
                return Unauthorized();

            return Ok();
        }
    }
}
