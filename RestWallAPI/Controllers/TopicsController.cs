using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Helpers;
using RestLib.Infrastructure.Models.V1;
using RestLib.Infrastructure.Models.V1.Topics;
using RestLib.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestWallAPI.Controllers
{

    [ApiController]
    [Route("v1/topics")]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicService _topicService;
        private readonly IMapper _mapper;
        public TopicsController(ITopicService topicService, IMapper mapper)
        {
            _topicService = topicService;
            _mapper = mapper;
        }

        [HttpGet("{boardId}")]
        public async Task<ActionResult<IEnumerable<ResponseTopicDto>>> GetTopicsAsync(Guid boardId)
        {
            var responseDtos = await _topicService.GetTopicsAsync(boardId);

            if (responseDtos == null)
            {
                return NotFound();
            }

            return Ok(responseDtos);
        }

        //[HttpGet("{topicId}", Name = "GetTopicAsync")]
        //public async Task<ActionResult<ResponseTopicDto>> GetTopicAsync(Guid boardId, Guid topicId)
        //{
        //    var topicDto = await _topicService.GetTopicAsync(boardId, topicId);

        //    if (topicDto == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(topicDto);
        //}

        [HttpPost]

        public async Task<ActionResult<ResponseTopicDto>> CreateTopicAsync(Guid boardId, [FromBody] RequestTopicDto topic)
        {
            var topicDto = await _topicService.CreateTopicAsync(boardId, topic);

            if (topicDto == null)
            {
                return NotFound();
            }

            return CreatedAtRoute("GetTopicAsync", new { boardId = topicDto.BoardId, topicId = topicDto.Id }, topicDto);
        }

        [HttpPut("{topicId}")]
        public async Task<ActionResult<ResponseTopicDto>> UpdateTopicAsync(Guid boardId, Guid topicId, [FromBody] UpdateTopicDto topic)
        {

            if (!await _topicService.TopicExistsAsync(topicId))
            {
                return NotFound();
            }

            var topicDto = await _topicService.UpdateTopicAsync(topicId, topic);

            return Ok(topicDto);
        }

        [HttpDelete("{topicId}")]
        public async Task<ActionResult<ResponseTopicDto>> DeleteTopicAsync(Guid boardId, Guid topicId)
        {
            if (!await _topicService.TopicExistsAsync(topicId))
            {
                return NotFound();
            }

            var topicDto = await _topicService.GetTopicAsync(topicId);

            if (topicDto == null)
            {
                return NoContent();
            }

            return Ok(topicDto);
        }
    }
}