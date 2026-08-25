using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Service.Domain.Entities;
using Service.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/telegram")]
    public class TelegramController : ControllerBase
    {
        private const string SecretTokenHeader = "X-Telegram-Bot-Api-Secret-Token";

        private readonly ILogger _logger;
        private readonly ITelegramService _service;
        private readonly IConfiguration _config;

        public TelegramController(
            ILogger<TelegramController> logger,
            ITelegramService service,
            IConfiguration config)
        {
            _logger = logger;
            _service = service;
            _config = config;
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook([FromBody] JsonElement dto)
        {
            var expectedSecretToken = _config["Telegram:WebhookSecretToken"];

            if (string.IsNullOrWhiteSpace(expectedSecretToken))
            {
                _logger.LogError(
                    "{ConfigKey} is not configured; all webhook requests will be rejected",
                    "Telegram:WebhookSecretToken");

                return Unauthorized();
            }

            var providedSecretToken = Request.Headers[SecretTokenHeader].ToString();

            if (providedSecretToken != expectedSecretToken)
            {
                _logger.LogWarning(
                    "Ignoring Telegram webhook call with a missing or invalid {Header} header",
                    SecretTokenHeader);

                // Return 200 so Telegram does not keep retrying the update.
                return Ok();
            }

            try
            {
                var update = dto.Deserialize<TelegramRequest>();

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
