using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Services.Interfaces;

namespace RestWallAPI.Controllers
{
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly ILogger<MessagesController> _logger;
        private readonly IMessageService _messageService;

        public MessagesController(ILogger<MessagesController> logger, IMessageService messageService)
        {
            _logger = logger;
            _messageService = messageService;
        }

        [HttpGet("messages/{userGuid}/{boardGuid}")]
        public async Task<IActionResult> GetMessagesAsync(Guid userGuid, Guid? boardGuid = null)
        {
            var messages = await _messageService.GetMessagesAsync(userGuid, boardGuid);
            return Ok(messages);
        }

        [HttpGet("{controller}/{messageGuid}")]
        public async Task<IActionResult> GetMessageAsync(Guid userGuid, Guid messageGuid)
        {
            // auth user, check if they have access to resource.

            var message = await _messageService.GetMessageAsync(messageGuid);
            if (message.User.Id == userGuid)
            {
                return Ok(message);
            }
            else
            {
                return BadRequest();
            }

        }

        // HUSK RETURN 201 NÅR CREATED
        [HttpPost("{controller}/{userGuid}/{boardGuid}")]
        public async Task<IActionResult> CreateMessageAsync(Guid userGuid, Guid boardGuid, [FromBody] Message message)
        {
            var createdMessage = await _messageService.CreateMessageAsync(userGuid, boardGuid, message);

            if (createdMessage != null)
            {
                return Created("", createdMessage);
            }
            else
            {
                return BadRequest(createdMessage);
            }

        }

        [HttpPut("{controller}/{userGuid}")]
        public async Task<IActionResult> UpdateMessageAsync(Guid userGuid, [FromBody] Message message)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{controller}/{userGuid}")]
        public async Task<IActionResult> DeleteMessageAsync(Guid userGuid, [FromBody] Message message)
        {
            throw new NotImplementedException();
        }
    }
}
