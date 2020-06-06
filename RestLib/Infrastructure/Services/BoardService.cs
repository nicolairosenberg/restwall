using AutoMapper;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Helpers;
using RestLib.Infrastructure.Models.V1;
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

            if (board == null)
            {
                return null;
            }

            var responseBoard = _mapper.Map<ResponseBoardDto>(board);

            return responseBoard;
        }

        public async Task<PagedList<Board>> GetBoardsAsync(BoardsParams boardsParams)
        {

            var collection = await _boardRepository.GetBoardsAsync();

            var pagedList = PagedList<Board>.Create(collection, boardsParams.PageNumber, boardsParams.PageSize);

            return pagedList;
            //var boards = await _boardRepository.GetBoardsAsync();

            //if(boards == null)
            //{
            //    return null;
            //}

            //var responseBoards = _mapper.Map<IEnumerable<ResponseBoardDto>>(boards);

            //return responseBoards;
        }
    }
}
