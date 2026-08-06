using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Service.Domain.Entities;
using Service.Services;

namespace API.Controllers
{
    public class TelegramController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ITelegramService _service;

        public TelegramController(ILogger<TelegramController> logger, ITelegramService service)
        {
            _logger = logger; // TODO: Add logging
            _service = service;
        }

        [HttpPost("/api/telegram/webhook")]
        public async Task<IActionResult> Webhook([FromBody] JsonElement dto)
        {
            try
            {
                var update = dto.Deserialize<TelegramRequest>(new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (update is null)
                {
                    return Ok();
                }

                await _service.Webhook(update);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error processing Telegram webhook");
                return Problem(title: "Telegram webhook processing failed.");
            }
        }
    }
}
