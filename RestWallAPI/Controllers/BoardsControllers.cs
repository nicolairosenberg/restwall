using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using RestLib.Infrastructure.Helpers;
using RestLib.Infrastructure.Models.V1;
using RestLib.Infrastructure.Models.V1.Boards;
using RestLib.Infrastructure.Services.Interfaces;

namespace RestWallAPI.Controllers
{
    [ApiController]
    [Route("api/boards")]
    [ResponseCache(CacheProfileName = "0SecondsCacheProfile")]
    [Produces("application/json", "application/xml", "application/vnd.restwall.hateoas+json")]
    public class BoardsControllers : ControllerBase
    {
        private readonly IBoardService _boardService;
        private readonly IMapper _mapper;
        public BoardsControllers(IBoardService boardService, IMapper mapper)
        {
            _boardService = boardService;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetBoards")]
        [HttpHead]
        //[Produces("application/json", "application/xml", "application/vnd.restwall.hateoas+json")]
        public async Task<ActionResult> GetBoardsAsync([FromQuery] BoardsParams boardsParams, [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
            {
                return BadRequest();
            }

            var boards = await _boardService.GetBoardsAsync(boardsParams);

            if (boards == null)
            {
                return NotFound();
            }

            if (!boards.Any())
            {
                return NotFound();
            }

            var previousPageLink = boards.HasPrevious ? CreateBoardResourceUri(boardsParams, UriTypeEnum.PreviousPage) : null;
            var nextPageLink = boards.HasNext ? CreateBoardResourceUri(boardsParams, UriTypeEnum.NextPage) : null;

            var paginationMetaData = new
            {
                totalCount = boards.TotalCount,
                pageSize = boards.PageSize,
                currentPage = boards.CurrentPage,
                totalPages = boards.TotalPages,
                previousPageLink,
                nextPageLink
            };

            var paginationMetaDataJson = System.Text.Json.JsonSerializer.Serialize(paginationMetaData);

            Response.Headers.Add("X-Pagination", paginationMetaDataJson);

            var includeLinks = parsedMediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);

            if (includeLinks)
            {
                var boardsWithLinks = new List<ResponseBoardLinksDto>();

                foreach (var item in boards)
                {
                    IEnumerable<LinkDto> links = new List<LinkDto>();
                    links = CreateBoardLinks(item.Id);
                    var boardWithLinks = _mapper.Map<ResponseBoardLinksDto>(item);
                    boardWithLinks.Links = links;

                    boardsWithLinks.Add(boardWithLinks);
                }

                var envelopeWithLinks = new EnvelopeResponseBoardLinksDto();
                envelopeWithLinks.Value = boardsWithLinks;
                envelopeWithLinks.Links = CreateBoardsLinks(boardsParams, boards.HasPrevious, boards.HasNext);

                return Ok(envelopeWithLinks);
            }

            var responseDtos = _mapper.Map<IEnumerable<ResponseBoardDto>>(boards);

            return Ok(responseDtos);
        }

        
        [HttpGet("{boardId}", Name = "GetBoard")]
        [HttpHead]
        //[Produces("application/json", "application/xml", "application/vnd.restwall.hateoas+json")]
        public async Task<ActionResult> GetBoardAsync(Guid boardId, [FromHeader(Name = "Accept")] string mediaType)
        {
            // NR: could use tryparselist if I wanted to support multiple media types.
            if(!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
            {
                return BadRequest();
            }

            var board = await _boardService.GetBoardAsync(boardId);

            if (board == null)
            {
                return NotFound();
            }

            var includeLinks = parsedMediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);

            if (includeLinks)
            {
                IEnumerable<LinkDto> links = new List<LinkDto>();
                links = CreateBoardLinks(boardId);
                var boardWithLinks = _mapper.Map<ResponseBoardLinksDto>(board);
                boardWithLinks.Links = links;

                return Ok(boardWithLinks);
            }

            // NR: Letting this stay for explaination:

            //var primaryMediaType = includeLinks ?
            //    parsedMediaType.SubTypeWithoutSuffix.Substring(0, parsedMediaType.SubTypeWithoutSuffix.Length - 8)
            //    : parsedMediaType.SubTypeWithoutSuffix;

            //if(primaryMediaType == "vnd.restwall.board.full")
            //{
            //    if (includeLinks)
            //    {
            //        var boardWithLinks = _mapper.Map<ResponseBoardLinksDto>(board);
            //        boardWithLinks.Links = links;

            //        return Ok(boardWithLinks);
            //    }
            //}

            return Ok(board);
        }

        [HttpOptions]
        public IActionResult GetBoardsOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS");
            return Ok();
        }

        private string CreateBoardResourceUri(BoardsParams uriParams, UriTypeEnum uriType)
        {
            switch (uriType)
            {
                case UriTypeEnum.PreviousPage:
                    return Url.Link("GetBoards",
                        new
                        {
                            pageNumber = uriParams.PageNumber - 1,
                            pageSize = uriParams.PageSize

                        });
                case UriTypeEnum.NextPage:
                    return Url.Link("GetBoards",
                        new
                        {
                            pageNumber = uriParams.PageNumber + 1,
                            pageSize = uriParams.PageSize

                        });
                case UriTypeEnum.Current:
                default:
                    return Url.Link("GetBoards",
                        new
                        {
                            pageNumber = uriParams.PageNumber,
                            pageSize = uriParams.PageSize

                        });
            }
        }

        private IEnumerable<LinkDto> CreateBoardLinks(Guid boardId)
        {
            var links = new List<LinkDto>();

            links.Add(
                new LinkDto(
                    Url.Link("GetBoard", new { boardId }),
                    "self",
                    "GET"));

            links.Add(
                new LinkDto(
                    Url.Link("GetTopics", new { boardId }),
                    "topics",
                    "GET"));

            //links.Add(
            //    new LinkDto(
            //        Url.Link("CreateTopicAsync", new { boardId }),
            //        "topics",
            //        "GET"));

            return links;
        }

        private IEnumerable<LinkDto> CreateBoardsLinks(BoardsParams boardsParams, bool hasPrevious, bool hasNext)
        {
            var links = new List<LinkDto>();

            links.Add(
                new LinkDto(CreateBoardResourceUri(boardsParams, UriTypeEnum.Current),
                    "self",
                    "GET"));

            if (hasNext)
            {
                links.Add(
                new LinkDto(CreateBoardResourceUri(boardsParams, UriTypeEnum.NextPage),
                "nextPage",
                "GET")
                );
            }

            if (hasPrevious)
            {
                links.Add(
                new LinkDto(CreateBoardResourceUri(boardsParams, UriTypeEnum.PreviousPage),
                "previousPage",
                "GET")
                );
            }

            return links;
        }
    }
}
