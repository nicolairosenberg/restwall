using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestLib.Infrastructure.Models.V1;

namespace RestWallAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class RootController : ControllerBase
    {
        [HttpGet(Name = "GetRoot")]
        public IActionResult GetRoot()
        {
            var links = new List<LinkDto>();

            links.Add(
                new LinkDto(Url.Link("GetRoot", new { }),
                "self",
                "GET"));

            links.Add(
                new LinkDto(Url.Link("GetBoardsAsync", new { }),
                "boards",
                "GET"));

            return Ok(links);
        }
    }
}