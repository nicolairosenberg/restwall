using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestLib.Infrastructure.Models.V1;
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
        [HttpHead]
        public async Task<ActionResult<IEnumerable<ResponseBoardDto>>> GetBoardsAsync()
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
        [HttpHead]
        public async Task<ActionResult<ResponseBoardDto>> GetBoardAsync(Guid boardId)
        {
            var board = await _boardService.GetBoardAsync(boardId);

            if (board == null)
            {
                return NotFound();
            }

            return Ok(board);
        }

        [HttpOptions]
        public IActionResult GetBoardOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS");
            return Ok();
        }
    }
}
