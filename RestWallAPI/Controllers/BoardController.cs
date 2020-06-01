using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestLib.Infrastructure.Services.Interfaces;

namespace RestWallAPI.Controllers
{
    [ApiController]
    public class BoardController : ControllerBase
    {
        private readonly IBoardService _boardService;
        public BoardController(IBoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpGet("{controller}")]
        public async Task<IActionResult> GetBoardsAsync()
        {
            var boards = await _boardService.GetBoardsAsync();
            return Ok(boards);
        }

        [HttpGet("{controller}/{boardGuid}")]
        public async Task<IActionResult> GetBoardAsync(Guid boardGuid)
        {
            var board = await _boardService.GetBoardAsync(boardGuid);
            return Ok(board);
        }
    }
}
