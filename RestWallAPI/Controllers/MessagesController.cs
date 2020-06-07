using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        public async Task<IActionResult> GetMessagesAsync(Guid boardId, Guid topicId, [FromQuery] MessagesParams messagesParams)
        {
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

            var responseDtos = _mapper.Map<IEnumerable<ResponseMessageDto>>(messages);

            var links = CreatemessagesLinks(messagesParams, messages.HasPrevious, messages.HasNext);

            //foreach (var item in responseDtos)
            //{
            //    item.Links = links;
            //}

            return Ok(responseDtos);
        }

        [HttpPost(Name = "CreateMessage")]
        public async Task<IActionResult> CreateMessageAsync(Guid boardId, Guid topicId, [FromBody] Message message)
        {
            return null;

            // createdAtRoute GetTopicAsync
        }

        [HttpGet("{messageId}", Name = "GetMessage")]
        public async Task<IActionResult> GetMessageAsync(Guid boardId, Guid topicId, Guid messageId)
        {
            return null;
        }

        [HttpPut("{messageId}", Name = "UpdateMessage")]
        public async Task<IActionResult> UpdateMessageAsync(Guid boardId, Guid topicId, Guid messageId, [FromBody] Message message)
        {
            return null;
        }

        [HttpDelete("{messageId}", Name = "DeleteMessage")]
        public async Task<IActionResult> DeleteMessageAsync(Guid boardId, Guid topicId, Guid messageId, [FromBody] Message message)
        {
            return null;
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

        private IEnumerable<LinkDto> CreatemessagesLinks(MessagesParams messagesParams, bool hasPrevious, bool hasNext)
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
