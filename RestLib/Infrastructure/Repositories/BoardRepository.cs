using Microsoft.EntityFrameworkCore;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Repositories
{
    public class BoardRepository : IBoardRepository, IDisposable
    {
        private readonly DataContext _dataContext;

        public BoardRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Task<Board> GetBoardAsync(Guid id)
        {
            var board = _dataContext.Boards.Where(x => x.Id == id).SingleOrDefaultAsync();
            return board;
        }

        public async Task<ICollection<Board>> GetBoardsAsync()
        {
            var boards = await _dataContext.Boards.ToListAsync();
            return boards;
        }

        public void Dispose()
        {

        }
    }
}
