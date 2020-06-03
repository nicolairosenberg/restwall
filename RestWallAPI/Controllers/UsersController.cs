using Microsoft.AspNetCore.Mvc;
using RestLib.Infrastructure.Entities;
using System;
using System.Threading.Tasks;

namespace RestWallAPI.Controllers
{

    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        public UsersController()
        {

        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] User user)
        {
            return null;
        }

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetUserAsync(Guid userId)
        {
            return null;
        }

        [HttpPut("{userId:guid}")]
        public async Task<IActionResult> UpdateUserAsync(Guid userId)
        {
            return null;
        }

        [HttpDelete("{userId:guid}")]
        public async Task<IActionResult> DeleteUserAsync(Guid userId)
        {
            return null;
        }

        // topics
        [HttpGet("{userId:guid}/topics")]
        public async Task<IActionResult> GetTopicsAsync(Guid userId)
        {
            return null;
        }

        [HttpPost("{controller}/{userId}/topics")]
        public async Task<IActionResult> CreateUserAsync(Guid userId, [FromBody] User user)
        {
            return null;
        }

        //[HttpGet("{controller}/{userId}")]
        //public async Task<IActionResult> GetUserAsync(Guid userId)
        //{
        //    return null;
        //}

        //[HttpPut("{controller}/{userId}")]
        //public async Task<IActionResult> UpdateUserAsync(Guid userId)
        //{
        //    return null;
        //}

        //[HttpDelete("{controller}/{userId}")]
        //public async Task<IActionResult> DeleteUserAsync(Guid userId)
        //{
        //    return null;
        //}
    }
}