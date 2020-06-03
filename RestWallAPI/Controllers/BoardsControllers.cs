using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Services.Interfaces;

namespace RestWallAPI.Controllers
{
    [ApiController]
    [Route("boards")]
    public class BoardsControllers : ControllerBase
    {
        private readonly IBoardService _boardService;
        public BoardsControllers(IBoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBoardsAsync()
        {
            var boards = await _boardService.GetBoardsAsync();

            if(boards == null)
            {
                return NotFound();
            }

            if (!boards.Any())
            {
                return NotFound();
            }

            return Ok(boards);
        }

        [HttpGet("{boardId}")]
        public async Task<IActionResult> GetBoardAsync(Guid boardId)
        {
            var board = await _boardService.GetBoardAsync(boardId);

            if (board == null)
            {
                return NotFound();
            }

            return Ok(board);
        }

        [HttpGet("{controller}/{boardId}/topics")]
        public async Task<IActionResult> GetTopicsAsync()
        {
            return null;
        }

        [HttpPost("{controller}/{boardId}/topics")]
        public async Task<IActionResult> CreateTopicAsync(Guid boardId, [FromBody] Topic topic)
        {
            return null;
        }

        [HttpGet("{controller}/{boardId}/topics/{topicId}")]
        public async Task<IActionResult> GetTopicAsync(Guid boardId, Guid topicId)
        {
            return null;
        }

        [HttpPut("{controller}/{boardId}/topics/{topicId}")]
        public async Task<IActionResult> UpdateTopicAsync(Guid boardId, Guid topicId, [FromBody] Topic topic)
        {
            return null;
        }

        [HttpDelete("{controller}/{boardId}/topics/{topicId}")]
        public async Task<IActionResult> DeleteTopicAsync(Guid boardId, Guid topicId, [FromBody] Topic topic)
        {
            return null;
        }

        [HttpGet("{controller}/{boardId}/topics/{topicId}/messages")]
        public async Task<IActionResult> GetMessagesAsync(Guid boardId, Guid topicId)
        {
            return null;
        }

        [HttpPost("{controller}/{boardId}/topics/{topicId}/messages")]
        public async Task<IActionResult> CreateMessageAsync(Guid boardId, Guid topicId, [FromBody] Message message)
        {
            return null;
        }

        [HttpGet("{controller}/{boardId}/topics/{topicId}/messages/{messageId}")]
        public async Task<IActionResult> GetMessageAsync(Guid boardId, Guid topicId, Guid messageId)
        {
            return null;
        }

        [HttpPut("{controller}/{boardId}/topics/{topicId}/messages/{messageId}")]
        public async Task<IActionResult> UpdateMessageAsync(Guid boardId, Guid topicId, Guid messageId, [FromBody] Message message)
        {
            return null;
        }

        [HttpDelete("{controller}/{boardId}/topics/{topicId}/messages/{messageId}")]
        public async Task<IActionResult> DeleteMessageAsync(Guid boardId, Guid topicId, Guid messageId, [FromBody] Message message)
        {
            return null;
        }
    }
}
