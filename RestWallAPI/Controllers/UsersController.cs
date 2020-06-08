using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Helpers;
using RestLib.Infrastructure.Models.V1;
using RestLib.Infrastructure.Models.V1.Users;
using RestLib.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestWallAPI.Controllers
{

    [ApiController]
    [Route("api/users")]
    [ResponseCache(CacheProfileName = "0SecondsCacheProfile")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(ILogger<UsersController> logger, IUserService userService, IMapper mapper)
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetUsers")]
        public async Task<IActionResult> GetUsersAsync([FromQuery] UsersParams usersParams, [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
            {
                return BadRequest();
            }

            PagedList<User> users = await _userService.GetUsersAsync(usersParams);

            if (users == null)
            {
                return NotFound();
            }

            var previousPageLink = users.HasPrevious ? CreateUserResourceUri(usersParams, UriTypeEnum.PreviousPage) : null;
            var nextPageLink = users.HasNext ? CreateUserResourceUri(usersParams, UriTypeEnum.NextPage) : null;

            var paginationMetaData = new
            {
                totalCount = users.TotalCount,
                pageSize = users.PageSize,
                currentPage = users.CurrentPage,
                totalPages = users.TotalPages,
                previousPageLink,
                nextPageLink
            };

            var paginationMetaDataJson = JsonSerializer.Serialize(paginationMetaData);

            Response.Headers.Add("X-Pagination", paginationMetaDataJson);

            var includeLinks = parsedMediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);

            if (includeLinks)
            {
                var usersWithLinks = new List<ResponseUserLinksDto>();

                foreach (var item in users)
                {
                    IEnumerable<LinkDto> links = new List<LinkDto>();
                    links = CreateUserLinks(item.Id);
                    var userWithLinks = _mapper.Map<ResponseUserLinksDto>(item);
                    userWithLinks.Links = links;

                    usersWithLinks.Add(userWithLinks);
                }

                var envelopeWithLinks = new EnvelopeResponseUserLinksDto();
                envelopeWithLinks.Value = usersWithLinks;
                envelopeWithLinks.Links = CreateUsersLinks(usersParams, users.HasPrevious, users.HasNext);

                return Ok(envelopeWithLinks);
            }

            var responseDtos = _mapper.Map<IEnumerable<ResponseUserDto>>(users);

            return Ok(responseDtos);
        }

        [HttpPost(Name = "CreateUser")]
        public async Task<IActionResult> CreateUserAsync([FromBody] RequestUserDto user, [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
            {
                return BadRequest();
            }

            var userDto = await _userService.CreateUserAsync(user);

            if (userDto == null)
            {
                return NotFound();
            }

            var includeLinks = parsedMediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);

            if (includeLinks)
            {
                IEnumerable<LinkDto> links = new List<LinkDto>();
                links = CreateUserLinks(userDto.Id);
                var userWithLinks = _mapper.Map<ResponseUserLinksDto>(userDto);
                userWithLinks.Links = links;

                return Ok(userWithLinks);
            }

            return CreatedAtRoute("GetUser", new { userId = userDto.Id }, userDto);
        }

        [HttpGet("{userId}", Name = "GetUser")]
        public async Task<IActionResult> GetUserAsync(Guid userId, [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
            {
                return BadRequest();
            }

            var userDto = await _userService.GetUserAsync(userId);

            if (userDto == null)
            {
                return NotFound();
            }

            var includeLinks = parsedMediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);

            if (includeLinks)
            {
                IEnumerable<LinkDto> links = new List<LinkDto>();
                links = CreateUserLinks(userDto.Id);
                var userWithLinks = _mapper.Map<ResponseUserLinksDto>(userDto);
                userWithLinks.Links = links;

                return Ok(userWithLinks);
            }

            return Ok(userDto);
        }

        [HttpPut("{userId}", Name = "UpdateUser")]
        public async Task<IActionResult> UpdateUserAsync(Guid userId, [FromBody] UpdateUserDto user, [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
            {
                return BadRequest();
            }

            var userDto = await _userService.UpdateUserAsync(userId, user);

            if (userDto == null)
            {
                return NotFound();
            }

            var includeLinks = parsedMediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);

            if (includeLinks)
            {
                IEnumerable<LinkDto> links = new List<LinkDto>();
                links = CreateUserLinks(userId);
                var userWithLinks = _mapper.Map<ResponseUserLinksDto>(userDto);
                userWithLinks.Links = links;

                return Ok(userWithLinks);
            }

            return Ok(userDto);
        }

        [HttpDelete("{userId}", Name = "DeleteUser")]
        public async Task<IActionResult> DeleteUserAsync(Guid userId, [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
            {
                return BadRequest();
            }

            if (!await _userService.UserExistsAsync(userId))
            {
                return NotFound();
            }

            var userDto = await _userService.GetUserAsync(userId);

            if (userDto == null)
            {
                return NoContent();
            }

            var deletedUserDto = await _userService.DeleteUserAsync(userDto);

            var includeLinks = parsedMediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);

            if (includeLinks)
            {
                IEnumerable<LinkDto> links = new List<LinkDto>();
                links = CreateUserLinks(deletedUserDto.Id);
                var userWithLinks = _mapper.Map<ResponseUserLinksDto>(deletedUserDto);
                userWithLinks.Links = links;

                return Ok(userWithLinks);
            }

            return Ok(deletedUserDto);
        }

        // NR: Not implemented due to issues and time constraint.. ;/ (will explain)
        [HttpGet("{userId}/topics", Name = "GetUserTopics")]
        public async Task<IActionResult> GetTopicsAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{userId}/topics/{topicId}", Name = "GetUserTopic")]
        public async Task<IActionResult> GetTopicAsync(Guid userId, Guid topicId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{userId}/topics/{topicId}/messages", Name = "GetUserMessages")]
        public async Task<IActionResult> GetMessagesAsync(Guid userId, Guid topicId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{userId}/topics/{topicId}/messages/{messageId}", Name = "GetUserMessage")]
        public async Task<IActionResult> GetMessageAsync(Guid userId, Guid topicId, Guid messageId)
        {
            throw new NotImplementedException();
        }

        [HttpOptions]
        public IActionResult GetUsersOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST, PUT, DELETE");
            return Ok();
        }

        private string CreateUserResourceUri(UsersParams usersParam, UriTypeEnum uriType)
        {
            switch (uriType)
            {
                case UriTypeEnum.PreviousPage:
                    return Url.Link("GetUsers",
                        new
                        {
                            pageNumber = usersParam.PageNumber - 1,
                            pageSize = usersParam.PageSize

                        });
                case UriTypeEnum.NextPage:
                    return Url.Link("GetUsers",
                        new
                        {
                            pageNumber = usersParam.PageNumber + 1,
                            pageSize = usersParam.PageSize

                        });
                case UriTypeEnum.Current:
                default:
                    return Url.Link("GetUsers",
                        new
                        {
                            pageNumber = usersParam.PageNumber,
                            pageSize = usersParam.PageSize

                        });
            }
        }

        private IEnumerable<LinkDto> CreateUserLinks(Guid userId)
        {
            var links = new List<LinkDto>();

            links.Add(
                new LinkDto(
                    Url.Link("GetUser", new { userId }),
                    "self",
                    "GET"));

            links.Add(
                new LinkDto(
                    Url.Link("GetUsers", new { }),
                    "users",
                    "GET"));

            links.Add(
                new LinkDto(
                    Url.Link("DeleteUser", new { userId }),
                    "delete_topic",
                    "DELETE"));

            links.Add(
                new LinkDto(
                    Url.Link("CreateUser", new { }),
                    "create_user",
                    "POST"));

            return links;
        }

        private IEnumerable<LinkDto> CreateUsersLinks(UsersParams usersParams, bool hasPrevious, bool hasNext)
        {
            var links = new List<LinkDto>();

            links.Add(
                new LinkDto(CreateUserResourceUri(usersParams, UriTypeEnum.Current),
                    "self",
                    "GET"));

            if (hasNext)
            {
                links.Add(
                new LinkDto(CreateUserResourceUri(usersParams, UriTypeEnum.NextPage),
                "nextPage",
                "GET")
                );
            }

            if (hasPrevious)
            {
                links.Add(
                new LinkDto(CreateUserResourceUri(usersParams, UriTypeEnum.PreviousPage),
                "previousPage",
                "GET")
                );
            }


            return links;
        }
    }
}