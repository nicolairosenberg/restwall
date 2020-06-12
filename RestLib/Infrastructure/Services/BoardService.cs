using AutoMapper;
using RestLib.Infrastructure.Models.V1.Boards;
using RestLib.Infrastructure.Repositories.Interfaces;
using RestLib.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Services
{
    public class BoardService : IBoardService
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IMapper _mapper;

        public BoardService(IBoardRepository boardRepository, IMapper mapper)
        {
            _boardRepository = boardRepository;
            _mapper = mapper;
        }

        public async Task<ResponseBoardDto> GetBoardAsync(Guid boardId)
        {
            var board = await _boardRepository.GetBoardAsync(boardId);

            var responseBoard = _mapper.Map<ResponseBoardDto>(board);

            return responseBoard;
        }

        public async Task<ICollection<ResponseBoardDto>> GetBoardsAsync()
        {
            var boards = await _boardRepository.GetBoardsAsync();

            var responseBoards = _mapper.Map<ICollection<ResponseBoardDto>>(boards);

            return responseBoards;
        }
    }
}
