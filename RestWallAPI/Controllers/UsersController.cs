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
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;

        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            var responseDtos = await _userService.GetUsersAsync();

            if (responseDtos == null)
            {
                return NotFound();
            }

            return Ok(responseDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] RequestUserDto user)
        {
            var userDto = await _userService.CreateUserAsync(user);

            if (userDto == null)
            {
                return NotFound();
            }

            return CreatedAtRoute("GetUserAsync", new { userId = userDto.Id }, userDto);
        }

        [HttpGet("{userId}", Name = "GetUserAsync")]
        public async Task<IActionResult> GetUserAsync(Guid userId)
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

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserAsync(Guid userId, [FromBody] UpdateUserDto user)
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

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserAsync(Guid userId)
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

        [HttpGet("{userId}/topics")]
        public async Task<IActionResult> GetTopicsAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{userId}/topics/{topicId}")]
        public async Task<IActionResult> GetTopicAsync(Guid userId, Guid topicId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{userId}/topics/{topicId}/messages")]
        public async Task<IActionResult> GetMessagesAsync(Guid userId, Guid topicId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{userId}/topics/{topicId}/messages/{messageId}")]
        public async Task<IActionResult> GetMessagesAsync(Guid userId, Guid topicId, Guid messageId)
        {
            throw new NotImplementedException();
        }
    }
}