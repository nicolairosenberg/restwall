using Microsoft.AspNetCore.Mvc;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Models.V1;
using RestLib.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestWallAPI.Controllers
{
    
    [ApiController]
    [Route("boards/{boardId}/topics")]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicService _topicService;

        public TopicsController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        [HttpGet]
        [HttpHead]
        public async Task<ActionResult<IEnumerable<ResponseTopicDto>>> GetTopicsAsync(Guid boardId)
        {
            var responseDtos = await _topicService.GetTopicsAsync(boardId);
            
            if(responseDtos == null)
            {
                return NotFound();
            }

            return Ok(responseDtos);
        }


        [HttpGet("{topicId}", Name = "GetTopic")]
        [HttpHead]
        public async Task<ActionResult<ResponseTopicDto>> GetTopicAsync(Guid boardId, Guid topicId)
        {
            return await _topicService.GetTopicAsync(boardId, topicId);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseTopicDto>> CreateTopicAsync(Guid boardId, [FromBody] RequestTopicDto topic)
        {
            var responseDto = await _topicService.CreateTopicAsync(boardId, topic);

            return CreatedAtRoute("GetTopic", new { boardId = responseDto.BoardId, topicId = responseDto.Id }, responseDto);
        }


        [HttpPut("{topicId}")]
        public async Task<ActionResult<ResponseTopicDto>> UpdateTopicAsync(Guid boardId, Guid topicId, [FromBody] ResponseTopicDto topic)
        {
            return null;
        }

        [HttpDelete("{topicId}")]
        public async Task<ActionResult<ResponseTopicDto>> DeleteTopicAsync(Guid boardId, Guid topicId, [FromBody] ResponseTopicDto topic)
        {
            return null;
        }
    }
}