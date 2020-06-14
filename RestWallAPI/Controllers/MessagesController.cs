using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Helpers;
using RestLib.Infrastructure.Models.V1;
using RestLib.Infrastructure.Models.V1.Messages;
using RestLib.Infrastructure.Services.Interfaces;

namespace RestWallAPI.Controllers
{
    [ApiController]
    [Route("v1/boards/{boardId}/topics/{topicId}/messages")]
    public class MessagesController : ControllerBase
    {
        private readonly ILogger<MessagesController> _logger;
        private readonly IMessageService _messageService;

        public MessagesController(ILogger<MessagesController> logger, IMessageService messageService)
        {
            _logger = logger;
            _messageService = messageService;
        }

        [HttpGet(Name = "GetMessages")]
        public async Task<IActionResult> GetMessagesAsync(Guid boardId, Guid topicId)
        {
            var responseDtos = await _messageService.GetMessagesAsync(topicId);

            if (responseDtos == null)
            {
                return NotFound();
            }

            return Ok(responseDtos);
        }

        [HttpGet("{messageId}", Name = "GetMessageAsync")]
        public async Task<IActionResult> GetMessageAsync(Guid topicId, Guid messageId)
        {
            var messageDto = await _messageService.GetMessageAsync(topicId, messageId);

            if (messageDto == null)
            {
                return NotFound();
            }

            return Ok(messageDto);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateMessageAsync(Guid boardId, Guid topicId, [FromBody] RequestMessageDto message)
        {
            var messageDto = await _messageService.CreateMessageAsync(topicId, message);

            if (messageDto == null)
            {
                return NotFound();
            }

            return CreatedAtRoute("GetMessageAsync", new { boardId, topicId, messageId = messageDto.Id }, messageDto);
        }

        [HttpPut("{messageId}")]
        public async Task<IActionResult> UpdateMessageAsync(Guid topicId, Guid messageId, [FromBody] UpdateMessageDto message)
        {

            var messageDto = await _messageService.UpdateMessageAsync(topicId, messageId, message);

            if (messageDto == null)
            {
                return NotFound();
            }

            return Ok(messageDto);
        }

        [HttpDelete("{messageId}")]
        public async Task<IActionResult> DeleteMessageAsync(Guid topicId, Guid messageId)
        {

            if (!await _messageService.MessageExistsAsync(topicId))
            {
                return NotFound();
            }

            var messageDto = await _messageService.GetMessageAsync(topicId, messageId);

            if (messageDto == null)
            {
                return NoContent();
            }

            var deletedTopicDto = await _messageService.DeleteMessageAsync(topicId, messageDto);

            return Ok(deletedTopicDto);
        }
    }
}
