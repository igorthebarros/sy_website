using System.Security.Cryptography;
using System.Text;
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
            if (!IsSecretTokenValid())
            {
                _logger.LogWarning("Rejected Telegram webhook call with missing or invalid secret token");
                return Unauthorized();
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

        private bool IsSecretTokenValid()
        {
            var expectedToken = _config["Telegram:WebhookSecretToken"];

            if (string.IsNullOrWhiteSpace(expectedToken))
            {
                _logger.LogWarning(
                    "Telegram:WebhookSecretToken is not configured; webhook secret verification is disabled");
                return true;
            }

            var providedToken = Request.Headers[SecretTokenHeader].ToString();

            if (string.IsNullOrEmpty(providedToken))
            {
                return false;
            }

            return CryptographicOperations.FixedTimeEquals(
                Encoding.UTF8.GetBytes(providedToken),
                Encoding.UTF8.GetBytes(expectedToken));
        }
    }
}
