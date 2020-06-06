using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Helpers;
using RestLib.Infrastructure.Models.V1;
using RestLib.Infrastructure.Parameters;
using RestLib.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestWallAPI.Controllers
{

    [ApiController]
    [Route("boards/{boardId}/topics")]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicService _topicService;
        private readonly IMapper _mapper;
        public TopicsController(ITopicService topicService, IMapper mapper)
        {
            _topicService = topicService;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetTopicsAsync")]
        [HttpHead]
        public async Task<ActionResult<IEnumerable<ResponseTopicDto>>> GetTopicsAsync(Guid boardId, [FromQuery] TopicsParams topicsParams)
        {
            PagedList<Topic> topics = await _topicService.GetTopicsAsync(boardId, topicsParams);

            if (topics == null)
            {
                return NotFound();
            }

            var previousPageLink = topics.HasPrevious ? CreateTopicResourceUri(topicsParams, UriTypeEnum.PreviousPage) : null;
            var nextPageLink = topics.HasNext ? CreateTopicResourceUri(topicsParams, UriTypeEnum.NextPage) : null;

            var paginationMetaData = new
            {
                totalCount = topics.TotalCount,
                pageSize = topics.PageSize,
                currentPage = topics.CurrentPage,
                totalPages = topics.TotalPages,
                previousPageLink,
                nextPageLink
            };

            var paginationMetaDataJson = JsonSerializer.Serialize(paginationMetaData);

            Response.Headers.Add("X-Pagination", paginationMetaDataJson);

            var responseDtos = _mapper.Map<IEnumerable<ResponseTopicDto>>(topics);
            //var responseDtos = _mapp

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
        public async Task<ActionResult<ResponseTopicDto>> UpdateTopicAsync(Guid boardId, Guid topicId, [FromBody] UpdateTopicDto topic)
        {
            if (!await _topicService.TopicExistsAsync(topicId))
            {
                return NotFound();
            }

            var updatedDto = await _topicService.UpdateTopicAsync(boardId, topicId, topic);

            return Ok(updatedDto);
        }

        [HttpDelete("{topicId}")]
        public async Task<ActionResult<ResponseTopicDto>> DeleteTopicAsync(Guid boardId, Guid topicId)
        {
            if (!await _topicService.TopicExistsAsync(topicId))
            {
                return NotFound();
            }

            var topic = await _topicService.GetTopicAsync(boardId, topicId);

            if (topic == null)
            {
                return NoContent();
            }

            return await _topicService.DeleteTopicAsync(boardId, topic);
        }

        [HttpOptions]
        public IActionResult GetTopicOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST, PUT, DELETE");
            return Ok();
        }

        private string CreateTopicResourceUri(TopicsParams topicsParams, UriTypeEnum uriType)
        {
            switch (uriType)
            {
                case UriTypeEnum.PreviousPage:
                    return Url.Link("GetTopicsAsync",
                        new
                        {
                            pageNumber = topicsParams.PageNumber - 1,
                            pageSize = topicsParams.PageSize

                        });
                case UriTypeEnum.NextPage:
                    return Url.Link("GetTopicsAsync",
                        new
                        {
                            pageNumber = topicsParams.PageNumber + 1,
                            pageSize = topicsParams.PageSize

                        });
                default:
                    return Url.Link("GetTopicsAsync",
                        new
                        {
                            pageNumber = topicsParams.PageNumber,
                            pageSize = topicsParams.PageSize

                        });
            }
        }
    }
}