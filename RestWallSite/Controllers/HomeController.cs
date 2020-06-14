using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestLib.Infrastructure.Models.V1.Boards;
using RestLib.Infrastructure.Models.V1.Topics;
using RestWallSite.Models;
using ResWallSite.Models;

namespace ResWallSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            BoardViewModel model = new BoardViewModel();

            using(var client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:44366/api/boards/");
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync();

                    var responseDtos = JsonConvert.DeserializeObject<ICollection<ResponseBoardDto>>(result.Result);
                    model.Boards = responseDtos;

                    return View(model);
                }
            }

            return BadRequest();
        }

        [Route("board/{boardId}")]
        public async Task<IActionResult> Board(Guid boardId)
        {
            BoardViewModel model = new BoardViewModel();

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"https://localhost:44366/api/topics/{boardId}");
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync();

                    var responseDtos = JsonConvert.DeserializeObject<ICollection<ResponseTopicDto>>(result.Result);
                    model.Topics = responseDtos;

                    return View(model);
                }
            }

            return BadRequest();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
