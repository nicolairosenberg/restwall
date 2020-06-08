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
    [Route("api/boards/{boardId}/topics/{topicId}/messages")]
    [ResponseCache(CacheProfileName = "0SecondsCacheProfile")]
    [Produces("application/json", "application/xml", "application/vnd.restwall.hateoas+json")]
    public class MessagesController : ControllerBase
    {
        private readonly ILogger<MessagesController> _logger;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public MessagesController(ILogger<MessagesController> logger, IMessageService messageService, IMapper mapper)
        {
            _logger = logger;
            _messageService = messageService;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetMessages")]
        public async Task<IActionResult> GetMessagesAsync(Guid boardId, Guid topicId, [FromQuery] MessagesParams messagesParams, [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
            {
                return BadRequest();
            }

            PagedList<Message> messages = await _messageService.GetMessagesAsync(boardId, topicId, messagesParams);

            if (messages == null)
            {
                return NotFound();
            }

            var previousPageLink = messages.HasPrevious ? CreateMessageResourceUri(messagesParams, UriTypeEnum.PreviousPage) : null;
            var nextPageLink = messages.HasNext ? CreateMessageResourceUri(messagesParams, UriTypeEnum.NextPage) : null;

            var paginationMetaData = new
            {
                totalCount = messages.TotalCount,
                pageSize = messages.PageSize,
                currentPage = messages.CurrentPage,
                totalPages = messages.TotalPages,
                previousPageLink,
                nextPageLink
            };

            var paginationMetaDataJson = JsonSerializer.Serialize(paginationMetaData);

            Response.Headers.Add("X-Pagination", paginationMetaDataJson);

            var includeLinks = parsedMediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);

            if (includeLinks)
            {
                var messagesWithLinks = new List<ResponseMessageLinksDto>();

                foreach (var item in messages)
                {
                    IEnumerable<LinkDto> links = new List<LinkDto>();
                    links = CreateMessageLinks(boardId, item.TopicId, item.Id);
                    var messageWithLinks = _mapper.Map<ResponseMessageLinksDto>(item);
                    messageWithLinks.Links = links;

                    messagesWithLinks.Add(messageWithLinks);
                }

                var envelopeWithLinks = new EnvelopeResponseMessageLinksDto();
                envelopeWithLinks.Value = messagesWithLinks;
                envelopeWithLinks.Links = CreateMessagesLinks(messagesParams, messages.HasPrevious, messages.HasNext);

                return Ok(envelopeWithLinks);
            }

            var responseDtos = _mapper.Map<IEnumerable<ResponseMessageDto>>(messages);

            return Ok(responseDtos);
        }

        [HttpGet("{messageId}", Name = "GetMessage")]
        public async Task<IActionResult> GetMessageAsync(Guid boardId, Guid topicId, Guid messageId, [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
            {
                return BadRequest();
            }

            var messageDto = await _messageService.GetMessageAsync(boardId, topicId, messageId);

            if (messageDto == null)
            {
                return NotFound();
            }

            var includeLinks = parsedMediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);

            if (includeLinks)
            {
                IEnumerable<LinkDto> links = new List<LinkDto>();
                links = CreateMessageLinks(boardId, topicId, messageDto.Id);
                var messageWithLinks = _mapper.Map<ResponseMessageLinksDto>(messageDto);
                messageWithLinks.Links = links;

                return Ok(messageWithLinks);
            }

            return Ok(messageDto);
        }
        
        [HttpPost(Name = "CreateMessage")]
        public async Task<IActionResult> CreateMessageAsync(Guid boardId, Guid topicId, [FromBody] RequestMessageDto message, [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
            {
                return BadRequest();
            }

            var messageDto = await _messageService.CreateMessageAsync(boardId, topicId, message);

            if (messageDto == null)
            {
                return NotFound();
            }

            var includeLinks = parsedMediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);

            if (includeLinks)
            {
                IEnumerable<LinkDto> links = new List<LinkDto>();
                links = CreateMessageLinks(boardId, topicId, messageDto.Id);
                var messageWithLinks = _mapper.Map<ResponseMessageLinksDto>(messageDto);
                messageWithLinks.Links = links;

                return Ok(messageWithLinks);
            }

            return CreatedAtRoute("GetMessage", new { boardId, topicId, messageId = messageDto.Id }, messageDto);
        }

        [HttpPut("{messageId}", Name = "UpdateMessage")]
        public async Task<IActionResult> UpdateMessageAsync(Guid boardId, Guid topicId, Guid messageId, [FromBody] UpdateMessageDto message, [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
            {
                return BadRequest();
            }

            var messageDto = await _messageService.UpdateMessageAsync(boardId, topicId, messageId, message);

            if (messageDto == null)
            {
                return NotFound();
            }

            var includeLinks = parsedMediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);

            if (includeLinks)
            {
                IEnumerable<LinkDto> links = new List<LinkDto>();
                links = CreateMessageLinks(boardId, topicId, messageDto.Id);
                var messageWithLinks = _mapper.Map<ResponseMessageLinksDto>(messageDto);
                messageWithLinks.Links = links;

                return Ok(messageWithLinks);
            }

            return Ok(messageDto);
        }

        [HttpDelete("{messageId}", Name = "DeleteMessage")]
        public async Task<IActionResult> DeleteMessageAsync(Guid boardId, Guid topicId, Guid messageId, [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
            {
                return BadRequest();
            }

            if (!await _messageService.MessageExistsAsync(topicId))
            {
                return NotFound();
            }

            var messageDto = await _messageService.GetMessageAsync(boardId, topicId, messageId);

            if (messageDto == null)
            {
                return NoContent();
            }

            var deletedTopicDto = await _messageService.DeleteMessageAsync(boardId, topicId, messageDto);

            var includeLinks = parsedMediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);

            if (includeLinks)
            {
                IEnumerable<LinkDto> links = new List<LinkDto>();
                links = CreateMessageLinks(boardId, deletedTopicDto.TopicId, deletedTopicDto.Id);
                var messageWithLinks = _mapper.Map<ResponseMessageLinksDto>(deletedTopicDto);
                messageWithLinks.Links = links;

                return Ok(messageWithLinks);
            }

            return Ok(deletedTopicDto);
        }

        [HttpOptions]
        public IActionResult GetMessagesOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST, PUT, DELETE");
            return Ok();
        }

        private string CreateMessageResourceUri(MessagesParams messagesParams, UriTypeEnum uriType)
        {
            switch (uriType)
            {
                case UriTypeEnum.PreviousPage:
                    return Url.Link("GetMessages",
                        new
                        {
                            pageNumber = messagesParams.PageNumber - 1,
                            pageSize = messagesParams.PageSize

                        });
                case UriTypeEnum.NextPage:
                    return Url.Link("GetMessages",
                        new
                        {
                            pageNumber = messagesParams.PageNumber + 1,
                            pageSize = messagesParams.PageSize

                        });
                case UriTypeEnum.Current:
                default:
                    return Url.Link("GetMessages",
                        new
                        {
                            pageNumber = messagesParams.PageNumber,
                            pageSize = messagesParams.PageSize

                        });
            }
        }

        private IEnumerable<LinkDto> CreateMessageLinks(Guid boardId, Guid topicId, Guid messageId)
        {
            var links = new List<LinkDto>();

            links.Add(
                new LinkDto(
                    Url.Link("GetMessage", new { boardId, topicId, messageId }),
                    "self",
                    "GET"));

            links.Add(
                new LinkDto(
                    Url.Link("GetMessages", new { boardId, topicId, messageId }),
                    "messages",
                    "GET"));

            links.Add(
                new LinkDto(
                    Url.Link("DeleteMessage", new { boardId, topicId, messageId }),
                    "delete_topic",
                    "DELETE"));

            links.Add(
                new LinkDto(
                    Url.Link("CreateMessage", new { boardId, topicId, messageId }),
                    "create_message",
                    "POST"));

            links.Add(
                new LinkDto(
                    Url.Link("GetMessages", new { boardId, topicId, messageId }),
                    "messages",
                    "GET"));

            return links;
        }

        private IEnumerable<LinkDto> CreateMessagesLinks(MessagesParams messagesParams, bool hasPrevious, bool hasNext)
        {
            var links = new List<LinkDto>();

            links.Add(
                new LinkDto(CreateMessageResourceUri(messagesParams, UriTypeEnum.Current),
                    "self",
                    "GET"));

            if (hasNext)
            {
                links.Add(
                new LinkDto(CreateMessageResourceUri(messagesParams, UriTypeEnum.NextPage),
                "nextPage",
                "GET")
                );
            }

            if (hasPrevious)
            {
                links.Add(
                new LinkDto(CreateMessageResourceUri(messagesParams, UriTypeEnum.PreviousPage),
                "previousPage",
                "GET")
                );
            }


            return links;
        }
    }
}
