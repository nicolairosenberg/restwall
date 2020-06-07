﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    [Route("api/boards/{boardId}/topics")]
    [ResponseCache(CacheProfileName = "0SecondsCacheProfile")]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicService _topicService;
        private readonly IMapper _mapper;
        public TopicsController(ITopicService topicService, IMapper mapper)
        {
            _topicService = topicService;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetTopics")]
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

            var links = CreateTopicsLinks(topicsParams, topics.HasPrevious, topics.HasNext);

            //foreach (var item in responseDtos)
            //{
            //    item.Links = links;
            //}

            return Ok(responseDtos);
        }

        [HttpGet("{topicId}", Name = "GetTopic")]
        [HttpHead]
        public async Task<ActionResult<ResponseTopicDto>> GetTopicAsync(Guid boardId, Guid topicId)
        {
            var topicDto = await _topicService.GetTopicAsync(boardId, topicId);

            if (topicDto == null)
            {
                return NotFound();
            }

            var links = CreateTopicLinks(boardId, topicId);

            //topicDto.Links = links;

            return Ok(topicDto);
        }

        [HttpPost(Name = "CreateTopic")]
        public async Task<ActionResult<ResponseTopicDto>> CreateTopicAsync(Guid boardId, [FromBody] RequestTopicDto topic)
        {
            var responseDto = await _topicService.CreateTopicAsync(boardId, topic);

            var links = CreateTopicLinks(boardId, responseDto.Id);

            //responseDto.Links = links;

            return CreatedAtRoute("GetTopic", new { boardId = responseDto.BoardId, topicId = responseDto.Id }, responseDto);
        }

        [HttpPut("{topicId}", Name = "UpdateTopic")]
        public async Task<ActionResult<ResponseTopicDto>> UpdateTopicAsync(Guid boardId, Guid topicId, [FromBody] UpdateTopicDto topic)
        {
            if (!await _topicService.TopicExistsAsync(topicId))
            {
                return NotFound();
            }

            var updatedDto = await _topicService.UpdateTopicAsync(boardId, topicId, topic);

            return Ok(updatedDto);
        }

        [HttpDelete("{topicId}", Name = "DeleteTopic")]
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
        public IActionResult GetTopicsOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST, PUT, DELETE");
            return Ok();
        }

        private string CreateTopicResourceUri(TopicsParams topicsParams, UriTypeEnum uriType)
        {
            switch (uriType)
            {
                case UriTypeEnum.PreviousPage:
                    return Url.Link("GetTopics",
                        new
                        {
                            pageNumber = topicsParams.PageNumber - 1,
                            pageSize = topicsParams.PageSize

                        });
                case UriTypeEnum.NextPage:
                    return Url.Link("GetTopics",
                        new
                        {
                            pageNumber = topicsParams.PageNumber + 1,
                            pageSize = topicsParams.PageSize

                        });
                case UriTypeEnum.Current:
                default:
                    return Url.Link("GetTopics",
                        new
                        {
                            pageNumber = topicsParams.PageNumber,
                            pageSize = topicsParams.PageSize

                        });
            }
        }

        private IEnumerable<LinkDto> CreateTopicLinks(Guid boardId, Guid topicId)
        {
            var links = new List<LinkDto>();

            links.Add(
                new LinkDto(
                    Url.Link("GetTopic", new { boardId, topicId }),
                    "self",
                    "GET"));

            links.Add(
                new LinkDto(
                    Url.Link("GetTopics", new { boardId }),
                    "topics",
                    "GET"));

            links.Add(
                new LinkDto(
                    Url.Link("DeleteTopic", new { boardId, topicId }),
                    "delete_topic",
                    "DELETE"));

            links.Add(
                new LinkDto(
                    Url.Link("CreateMessage", new { boardId, topicId }),
                    "create_message",
                    "POST"));

            links.Add(
                new LinkDto(
                    Url.Link("GetMessages", new { boardId, topicId }),
                    "messages",
                    "GET"));

            return links;
        }

        private IEnumerable<LinkDto> CreateTopicsLinks(TopicsParams topicsParams, bool hasPrevious, bool hasNext)
        {
            var links = new List<LinkDto>();

            links.Add(
                new LinkDto(CreateTopicResourceUri(topicsParams, UriTypeEnum.Current),
                    "self",
                    "GET"));

            if (hasNext)
            {
                links.Add(
                new LinkDto(CreateTopicResourceUri(topicsParams, UriTypeEnum.NextPage),
                "nextPage",
                "GET")
                );
            }

            if (hasPrevious)
            {
                links.Add(
                new LinkDto(CreateTopicResourceUri(topicsParams, UriTypeEnum.PreviousPage),
                "previousPage",
                "GET")
                );
            }


            return links;
        }
    }
}