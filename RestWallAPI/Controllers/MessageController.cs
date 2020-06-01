using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestLib.Infrastructure.Entities;

namespace RestWallAPI.Controllers
{
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<MessageController> _logger;

        public MessageController(ILogger<MessageController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{controller}/{userGuid}")]
        public async Task<List<Message>> GetMessagesAsync(Guid userGuid)
        {
            return new List<Message>();
        }

        [HttpGet("messages/{messageGuid}")]
        public async Task<IActionResult> GetMessageAsync(Guid messageGuid)
        {
            return Ok(new Message());
        }

        [HttpPost("{controller}")]
        public async Task<IActionResult> CreateMessageAsync([FromBody] Message message)
        {
            return Ok(message);
        }

        [HttpPut("{controller}")]
        public async Task<IActionResult> UpdateMessageAsync([FromBody] Message message)
        {
            return Ok(message);
        }

        [HttpDelete("{controller}")]
        public async Task<IActionResult> DeleteMessageAsync([FromBody] Message message)
        {
            return Ok(message);
        }
    }
}
