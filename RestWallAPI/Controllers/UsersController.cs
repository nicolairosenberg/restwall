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
        private readonly ITopicService _topicService;
        private readonly IMessageService _messageService;

        public UsersController(ILogger<UsersController> logger, IUserService userService, ITopicService topicService, IMessageService messageService)
        {
            _logger = logger;
            _userService = userService;
            _topicService = topicService;
            _messageService = messageService;
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
            var userDto = await _userService.GetUserAsync(userId);

            if (userDto == null)
            {
                return NotFound();
            }

            return Ok(userDto);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserAsync(Guid userId, [FromBody] UpdateUserDto user)
        {
            if(userId == null)
            {
                return BadRequest();
            }

            if(user == null)
            {
                return BadRequest();
            }

            var userDto = await _userService.UpdateUserAsync(userId, user);

            if (userDto == null)
            {
                return NotFound();
            }

            return Ok(userDto);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserAsync(Guid userId)
        {
           
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

            return Ok(deletedUserDto);
        }

        [HttpGet("{userId}/topics")]
        public async Task<IActionResult> GetTopicsAsync(Guid userId)
        {
            var topicDtos = await _topicService.GetUserTopicsAsync(userId);

            return Ok(topicDtos);
        }

        [HttpGet("{userId}/topics/{topicId}/messages")]
        public async Task<IActionResult> GetMessagesAsync(Guid userId, Guid topicId)
        {
            var messageDtos = await _messageService.GetUserMessagesAsync(userId, topicId);

            return Ok(messageDtos);
        }
    }
}