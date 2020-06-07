using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace RestWallAPI.Controllers
{

    [ApiController]
    [Route("api/users")]
    [ResponseCache(CacheProfileName = "360SecondsCacheProfile")]
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
        public async Task<IActionResult> GetUsersAsync()
        {
            return null;
        }

        [HttpPost(Name = "CreateUser")]
        public async Task<IActionResult> CreateUserAsync([FromBody] User user)
        {
            return null;
        }

        [HttpGet("{userId}", Name = "GetUser")]
        public async Task<IActionResult> GetUserAsync(Guid userId)
        {
            return null;
        }

        [HttpPut("{userId}", Name = "UpdateUser")]
        public async Task<IActionResult> UpdateUserAsync(Guid userId)
        {
            return null;
        }

        [HttpDelete("{userId}", Name = "DeleteUser")]
        public async Task<IActionResult> DeleteUserAsync(Guid userId)
        {
            return null;
        }

        // topics
        [HttpGet("{userId}/topics", Name = "GetUserTopics")]
        public async Task<IActionResult> GetTopicsAsync(Guid userId)
        {
            return null;
        }

        [HttpGet("{userId}/topics/{topicId}", Name = "GetUserTopic")]
        public async Task<IActionResult> GetTopicAsync(Guid userId, Guid topicId)
        {
            return null;
        }

        [HttpGet("{userId}/topics/{topicId}/messages", Name = "GetUserMessages")]
        public async Task<IActionResult> GetMessagesAsync(Guid userId, Guid topicId)
        {
            return null;
        }

        [HttpGet("{userId}/topics/{topicId}/messages/{messageId}", Name = "GetUserMessage")]
        public async Task<IActionResult> GetMessageAsync(Guid userId, Guid topicId, Guid messageId)
        {
            return null;
        }

        [HttpOptions]
        public IActionResult GetUsersOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST, PUT, DELETE");
            return Ok();
        }
    }
}