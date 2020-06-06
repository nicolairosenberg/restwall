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
    [Route("boards/{boardId}/topics/{topicId}/messages")]
    public class MessagesController : ControllerBase
    {
        private readonly ILogger<MessagesController> _logger;
        private readonly IMessageService _messageService;

        public MessagesController(ILogger<MessagesController> logger, IMessageService messageService)
        {
            _logger = logger;
            _messageService = messageService;
        }

        [HttpGet(Name = "GetMessagesAsync")]
        public async Task<IActionResult> GetMessagesAsync(Guid boardId, Guid topicId)
        {
            return null;
        }

        [HttpPost(Name = "CreateMessageAsync")]
        public async Task<IActionResult> CreateMessageAsync(Guid boardId, Guid topicId, [FromBody] Message message)
        {
            return null;

            // createdAtRoute GetTopicAsync
        }

        [HttpGet("{messageId}")]
        public async Task<IActionResult> GetMessageAsync(Guid boardId, Guid topicId, Guid messageId)
        {
            return null;
        }

        [HttpPut("{messageId}")]
        public async Task<IActionResult> UpdateMessageAsync(Guid boardId, Guid topicId, Guid messageId, [FromBody] Message message)
        {
            return null;
        }

        [HttpDelete("{messageId}")]
        public async Task<IActionResult> DeleteMessageAsync(Guid boardId, Guid topicId, Guid messageId, [FromBody] Message message)
        {
            return null;
        }

        [HttpOptions]
        public IActionResult GetTopicOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST, PUT, DELETE");
            return Ok();
        }
    }
}
