using Microsoft.EntityFrameworkCore;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Repositories.Interfaces;
using RestLib.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Services
{
    public class BoardService : IBoardService
    {
        private readonly DataContext _dataContext;
        private readonly IBoardRepository _boardRepository;

        public BoardService(DataContext dataContext, IBoardRepository boardRepository)
        {
            _dataContext = dataContext;
            _boardRepository = boardRepository;
        }

        public async Task<Board> GetBoardAsync(Guid id)
        {
            return await _boardRepository.GetBoardAsync(id);
            //return await _dataContext.Boards.Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        public async Task<ICollection<Board>> GetBoardsAsync()
        {
            return await _boardRepository.GetBoardsAsync();
        }
    }
}
