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
    [Route("api/boards/{boardId}/topics")]
    [ResponseCache(CacheProfileName = "0SecondsCacheProfile")]
    [Produces("application/json", "application/xml", "application/vnd.restwall.hateoas+json")]
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
        //[Produces("application/json", "application/xml", "application/vnd.restwall.hateoas+json")]
        public async Task<ActionResult<IEnumerable<ResponseTopicDto>>> GetTopicsAsync(Guid boardId, [FromQuery] TopicsParams topicsParams, [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
            {
                return BadRequest();
            }

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

            var includeLinks = parsedMediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);

            if (includeLinks)
            {
                var topicsWithLinks = new List<ResponseTopicLinksDto>();

                foreach (var item in topics)
                {
                    IEnumerable<LinkDto> links = new List<LinkDto>();
                    links = CreateTopicLinks(item.BoardId, item.Id);
                    var topicWithLinks = _mapper.Map<ResponseTopicLinksDto>(item);
                    topicWithLinks.Links = links;

                    topicsWithLinks.Add(topicWithLinks);
                }

                var envelopeWithLinks = new EnvelopeResponseTopicLinksDto();
                envelopeWithLinks.Value = topicsWithLinks;
                envelopeWithLinks.Links = CreateTopicsLinks(topicsParams, topics.HasPrevious, topics.HasNext);

                return Ok(envelopeWithLinks);
            }

            var responseDtos = _mapper.Map<IEnumerable<ResponseTopicDto>>(topics);

            return Ok(responseDtos);
        }

        [HttpGet("{topicId}", Name = "GetTopic")]
        [HttpHead]
        //[Produces("application/json", "application/xml", "application/vnd.restwall.hateoas+json")]
        public async Task<ActionResult<ResponseTopicDto>> GetTopicAsync(Guid boardId, Guid topicId, [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
            {
                return BadRequest();
            }

            var topicDto = await _topicService.GetTopicAsync(boardId, topicId);

            if (topicDto == null)
            {
                return NotFound();
            }

            var includeLinks = parsedMediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);

            if (includeLinks)
            {
                IEnumerable<LinkDto> links = new List<LinkDto>();
                links = CreateTopicLinks(topicDto.BoardId, topicDto.Id);
                var topicWithLinks = _mapper.Map<ResponseTopicLinksDto>(topicDto);
                topicWithLinks.Links = links;

                return Ok(topicWithLinks);
            }

            return Ok(topicDto);
        }

        [HttpPost(Name = "CreateTopic")]

        public async Task<ActionResult<ResponseTopicDto>> CreateTopicAsync(Guid boardId, [FromBody] RequestTopicDto topic, [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
            {
                return BadRequest();
            }

            var topicDto = await _topicService.CreateTopicAsync(boardId, topic);

            if (topicDto == null)
            {
                return NotFound();
            }

            var includeLinks = parsedMediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);

            if (includeLinks)
            {
                IEnumerable<LinkDto> links = new List<LinkDto>();
                links = CreateTopicLinks(topicDto.BoardId, topicDto.Id);
                var topicWithLinks = _mapper.Map<ResponseTopicLinksDto>(topicDto);
                topicWithLinks.Links = links;

                return Ok(topicWithLinks);
            }

            return CreatedAtRoute("GetTopic", new { boardId = topicDto.BoardId, topicId = topicDto.Id }, topicDto);
        }

        [HttpPut("{topicId}", Name = "UpdateTopic")]
        //[Produces("application/json", "application/xml", "application/vnd.restwall.hateoas+json")]
        public async Task<ActionResult<ResponseTopicDto>> UpdateTopicAsync(Guid boardId, Guid topicId, [FromBody] UpdateTopicDto topic, [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
            {
                return BadRequest();
            }

            if (!await _topicService.TopicExistsAsync(topicId))
            {
                return NotFound();
            }

            var topicDto = await _topicService.UpdateTopicAsync(boardId, topicId, topic);

            if (topicDto == null)
            {
                return NotFound();
            }

            var includeLinks = parsedMediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);

            if (includeLinks)
            {
                IEnumerable<LinkDto> links = new List<LinkDto>();
                links = CreateTopicLinks(topicDto.BoardId, topicDto.Id);
                var topicWithLinks = _mapper.Map<ResponseTopicLinksDto>(topicDto);
                topicWithLinks.Links = links;

                return Ok(topicWithLinks);
            }

            return Ok(topicDto);
        }

        [HttpDelete("{topicId}", Name = "DeleteTopic")]
        public async Task<ActionResult<ResponseTopicDto>> DeleteTopicAsync(Guid boardId, Guid topicId, [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
            {
                return BadRequest();
            }

            if (!await _topicService.TopicExistsAsync(topicId))
            {
                return NotFound();
            }

            var topicDto = await _topicService.GetTopicAsync(boardId, topicId);

            if (topicDto == null)
            {
                return NoContent();
            }

            var deletedTopicDto = await _topicService.DeleteTopicAsync(boardId, topicDto);

            var includeLinks = parsedMediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);

            if (includeLinks)
            {
                IEnumerable<LinkDto> links = new List<LinkDto>();
                links = CreateTopicLinks(deletedTopicDto.BoardId, deletedTopicDto.Id);
                var topicWithLinks = _mapper.Map<ResponseTopicLinksDto>(deletedTopicDto);
                topicWithLinks.Links = links;

                return Ok(topicWithLinks);
            }

            return Ok(deletedTopicDto);
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