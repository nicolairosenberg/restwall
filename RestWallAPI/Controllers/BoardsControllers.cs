using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestLib.Infrastructure.Helpers;
using RestLib.Infrastructure.Models.V1;
using RestLib.Infrastructure.Services.Interfaces;

namespace RestWallAPI.Controllers
{
    [ApiController]
    [Route("boards")]
    public class BoardsControllers : ControllerBase
    {
        private readonly IBoardService _boardService;
        public BoardsControllers(IBoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpGet]
        [HttpHead]
        public async Task<ActionResult<IEnumerable<ResponseBoardDto>>> GetBoardsAsync([FromQuery] BoardsParams boardsParams)
        {
            var boards = await _boardService.GetBoardsAsync(boardsParams);
            //var previousPageLink = topics.HasPrevious ? CreateTopicResourceUri(topicsParams, UriTypeEnum.PreviousPage) : null;
            //var nextPageLink = topics.HasNext ? CreateTopicResourceUri(topicsParams, UriTypeEnum.NextPage) : null;

            //var paginationMetaData = new
            //{
            //    totalCount = topics.TotalCount,
            //    pageSize = topics.PageSize,
            //    currentPage = topics.CurrentPage,
            //    totalPages = topics.TotalPages,
            //    previousPageLink,
            //    nextPageLink
            //};

            //var paginationMetaDataJson = JsonSerializer.Serialize(paginationMetaData);

            //Response.Headers.Add("X-Pagination", paginationMetaDataJson);

            //var responseDtos = _mapper.Map<IEnumerable<ResponseTopicDto>>(topics);

            ////var boards = await _boardService.GetBoardsAsync();

            if(boards == null)
            {
                return NotFound();
            }

            if (!boards.Any())
            {
                return NotFound();
            }

            return Ok(boards);
        }

        [HttpGet("{boardId}")]
        [HttpHead]
        public async Task<ActionResult<ResponseBoardDto>> GetBoardAsync(Guid boardId)
        {
            var board = await _boardService.GetBoardAsync(boardId);

            if (board == null)
            {
                return NotFound();
            }

            return Ok(board);
        }

        [HttpOptions]
        public IActionResult GetBoardOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS");
            return Ok();
        }

        private string CreateTopicResourceUri(BoardsParams uriParams, UriTypeEnum uriType)
        {
            switch (uriType)
            {
                case UriTypeEnum.PreviousPage:
                    return Url.Link("GetTopicsAsync",
                        new
                        {
                            pageNumber = uriParams.PageNumber - 1,
                            pageSize = uriParams.PageSize

                        });
                case UriTypeEnum.NextPage:
                    return Url.Link("GetTopicsAsync",
                        new
                        {
                            pageNumber = uriParams.PageNumber + 1,
                            pageSize = uriParams.PageSize

                        });
                default:
                    return Url.Link("GetTopicsAsync",
                        new
                        {
                            pageNumber = uriParams.PageNumber,
                            pageSize = uriParams.PageSize

                        });
            }
        }

        private IEnumerable<LinkDto> CreateBoardLinks(Guid boardId, Guid topicId)
        {
            var links = new List<LinkDto>();

            links.Add(
                new LinkDto(
                    Url.Link("GetTopicAsync", new { topicId }),
                    "self",
                    "GET"));

            links.Add(
                new LinkDto(
                    Url.Link("GetTopicsAsync", new { boardId }),
                    "topics",
                    "GET"));

            links.Add(
                new LinkDto(
                    Url.Link("DeleteTopicAsync", new { boardId, topicId }),
                    "delete_topic",
                    "DELETE"));

            links.Add(
                new LinkDto(
                    Url.Link("CreateTopicsAsync", new { boardId, topicId }),
                    "create_message",
                    "POST"));

            links.Add(
                new LinkDto(
                    Url.Link("GetMessagesAsync", new { boardId, topicId }),
                    "messages",
                    "GET"));

            return links;
        }
    }
}
