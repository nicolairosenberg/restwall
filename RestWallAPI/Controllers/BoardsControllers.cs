using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestLib.Infrastructure.Services.Interfaces;

namespace RestWallAPI.Controllers
{
    [ApiController]
    public class BoardsControllers : ControllerBase
    {
        private readonly IBoardService _boardService;
        public BoardsControllers(IBoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpGet("{controller}s")]
        public async Task<IActionResult> GetBoardsAsync()
        {
            var boards = await _boardService.GetBoardsAsync();
            return new JsonResult(boards);
        }

        [HttpGet("{controller}/{boardGuid}")]
        public async Task<IActionResult> GetBoardAsync(Guid boardGuid)
        {
            var board = await _boardService.GetBoardAsync(boardGuid);
            return Ok(board);
        }
    }
}
