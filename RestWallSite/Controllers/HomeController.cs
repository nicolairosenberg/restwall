using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
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
            Client.BaseAddress = new Uri("https://api.restwall.dk/v1/");
            //Client.BaseAddress = new Uri("https://localhost:44366/v1/");
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

                    var response = await client.GetAsync($"boards/{boardId}/topics/");
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
                var boardResponse = await client.GetAsync($"boards/{boardId}");
                if (boardResponse.IsSuccessStatusCode)
                {
                    var boardResult = boardResponse.Content.ReadAsStringAsync();

                    var board = JsonConvert.DeserializeObject<ResponseBoardDto>(boardResult.Result);

                    model.CurrentBoard = board;

                    var topicResponse = await client.GetAsync($"boards/{boardId}/topics/{topicId}");

                    if (topicResponse.IsSuccessStatusCode)
                    {
                        var topicResult = topicResponse.Content.ReadAsStringAsync();
                        var currentTopic = JsonConvert.DeserializeObject<ResponseTopicDto>(topicResult.Result);

                        model.CurrentTopic = currentTopic;

                        var response = await client.GetAsync($"boards/{boardId}/topics/{topicId}/messages");
                        if (response.IsSuccessStatusCode)
                        {
                            var result = response.Content.ReadAsStringAsync();

                            var responseDtos = JsonConvert.DeserializeObject<ICollection<ResponseMessageDto>>(result.Result);
                            model.Messages = responseDtos;

                            return View(model);
                        }
                    }
                }
            }

            return BadRequest();
        }

        [HttpGet("boards/{boardId}/createtopic")]
        public async Task<IActionResult> CreateTopic(Guid boardId)
        {
            CreateTopicModel model = new CreateTopicModel();

            model.BoardId = boardId;

            return View(model);
        }

        [HttpPost("boards/{boardId}/createtopic")]
        public async Task<IActionResult> CreateTopic(Guid boardId, CreateTopicModel model)
        {
            model.BoardId = boardId;
            var jsonString = JsonConvert.SerializeObject(new { title = model.Title, text = model.Text, userId = Guid.NewGuid() });
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            using (var client = Client)
            {
                var httpResponse = await client.PostAsync($"boards/{boardId}/topics/", httpContent);
                if(httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();

                    ResponseTopicDto responseTopicDto = JsonConvert.DeserializeObject<ResponseTopicDto>(responseContent);

                    model.TopicId = responseTopicDto.Id;

                    return RedirectToAction("Topic", new { boardId, topicId = model.TopicId });
                }
            }

            return View(model);
        }

        [HttpGet("boards/{boardId}/topics/{topicId}/createreply")]
        public async Task<IActionResult> CreateReply(Guid boardId, Guid topicId)
        {
            CreateTopicModel model = new CreateTopicModel();

            model.BoardId = boardId;
            model.TopicId = topicId;

            return View(model);
        }

        [HttpPost("boards/{boardId}/topics/{topicId}/createreply")]
        public async Task<IActionResult> CreateReply(Guid boardId, Guid topicId, CreateTopicModel model)
        {
            model.BoardId = boardId;
            var jsonString = JsonConvert.SerializeObject(new { title = model.Title, text = model.Text, userId = Guid.NewGuid() });
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            using (var client = Client)
            {
                var httpResponse = await client.PostAsync($"boards/{boardId}/topics/{topicId}/messages", httpContent);
                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();

                    ResponseTopicDto responseTopicDto = JsonConvert.DeserializeObject<ResponseTopicDto>(responseContent);
                    
                    return RedirectToAction("Topic", new { boardId, topicId });
                }
            }

            return View(model);
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
