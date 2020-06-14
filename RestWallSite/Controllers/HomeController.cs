using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestLib.Infrastructure.Models.V1.Boards;
using RestLib.Infrastructure.Models.V1.Messages;
using RestLib.Infrastructure.Models.V1.Topics;
using RestWallSite.Models;
using ResWallSite.Models;

namespace ResWallSite.Controllers
{
    public class HomeController : Controller
    {
        public HttpClient Client { get; set; } = new HttpClient();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            Client.BaseAddress = new Uri("http://api.restwall.dk/v1/");
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            BoardViewModel model = new BoardViewModel();

            using (var client = Client)
            {
                var response = await client.GetAsync("boards");
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

        [Route("boards/{boardId}")]
        public async Task<IActionResult> Board(Guid boardId)
        {
            BoardViewModel model = new BoardViewModel();

            using (var client = Client)
            {
                var boardResponse = await client.GetAsync($"boards/{boardId}");
                if (boardResponse.IsSuccessStatusCode)
                {
                    var boardResult = boardResponse.Content.ReadAsStringAsync();

                    var board = JsonConvert.DeserializeObject<ResponseBoardDto>(boardResult.Result);

                    model.CurrentBoard = board;

                    var response = await client.GetAsync($"topics/{boardId}");
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync();

                        var responseDtos = JsonConvert.DeserializeObject<ICollection<ResponseTopicDto>>(result.Result);
                        model.Topics = responseDtos;

                        return View(model);
                    }
                }
            }

            return BadRequest();
        }

        [Route("boards/{boardId}/topics/{topicId}")]
        public async Task<IActionResult> Topic(Guid boardId, Guid topicId)
        {
            BoardViewModel model = new BoardViewModel();

            using (var client = Client)
            {
                var response = await client.GetAsync($"messages/{topicId}");
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync();

                    var responseDtos = JsonConvert.DeserializeObject<ICollection<ResponseMessageDto>>(result.Result);
                    model.Messages = responseDtos;

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
