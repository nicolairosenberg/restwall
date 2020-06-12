using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using RestLib.Infrastructure.Helpers;
using RestLib.Infrastructure.Models.V1;
using RestLib.Infrastructure.Models.V1.Boards;
using RestLib.Infrastructure.Services.Interfaces;

namespace RestWallAPI.Controllers
{
    [ApiController]
    [Route("api/boards")]
    public class BoardsControllers : ControllerBase
    {
        private readonly IBoardService _boardService;
        public BoardsControllers(IBoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpGet]
        public async Task<ActionResult> GetBoardsAsync()
        {
            var responseDtos = await _boardService.GetBoardsAsync();

            if (responseDtos == null)
            {
                return NotFound();
            }

            if (!responseDtos.Any())
            {
                return NotFound();
            }

            return Ok(responseDtos);
        }

        
        [HttpGet("{boardId}")]
        public async Task<ActionResult> GetBoardAsync(Guid boardId)
        {
            var board = await _boardService.GetBoardAsync(boardId);

            if (board == null)
            {
                return NotFound();
            }

            return Ok(board);
        }
    }
}
