using System.Text.Json;
using API.DTOs;
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
            //if (dto?.Message == null)
            //    return Ok();

            try
            {
                //await _service.Webhook(dto);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
