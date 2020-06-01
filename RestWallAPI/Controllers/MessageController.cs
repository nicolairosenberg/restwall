using System;
using System.Collections.Generic;
using System.Linq;
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
        private DataContext _context;

        public MessageController(ILogger<MessageController> logger, DataContext dataContext)
        {
            _logger = logger;
            _context = dataContext;
        }

        [HttpGet("messages/{userGuid}")]
        public async Task<List<Message>> GetMessagesAsync(Guid userGuid)
        {
            return _context.Messages.ToList();
            //return new List<Message>();
        }

        [HttpGet("{controller}/{messageGuid}")]
        public async Task<IActionResult> GetMessageAsync(Guid userGuid, Guid messageGuid)
        {
            return Ok(new Message());
        }

        [HttpPost("{controller}/{userGuid}")]
        public async Task<IActionResult> CreateMessageAsync(Guid userGuid, [FromBody] Message message)
        {
            return Ok(message);
        }

        [HttpPut("{controller}/{userGuid}")]
        public async Task<IActionResult> UpdateMessageAsync(Guid userGuid, [FromBody] Message message)
        {
            return Ok(message);
        }

        [HttpDelete("{controller}/{userGuid}")]
        public async Task<IActionResult> DeleteMessageAsync(Guid userGuid, [FromBody] Message message)
        {
            return Ok(message);
        }
    }
}
