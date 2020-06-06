using Microsoft.EntityFrameworkCore;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Repositories.Interfaces;
using System;
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

        public async Task<Board> GetBoardAsync(Guid boardId)
        {
            var board = await _dataContext.Boards.Where(x => x.Id == boardId).SingleOrDefaultAsync();
            return board;
        }

        public async Task<IQueryable<Board>> GetBoardsAsync()
        {
            // NR: async?...hmm I have to figure out a way to keep this async..
            return _dataContext.Boards as IQueryable<Board>;
        }

        public async Task<bool> ExistsAsync(Guid boardId)
        {
            return await _dataContext.Boards.Where(x => x.Id == boardId).AnyAsync();
        }

        public void Dispose()
        {

        }
    }
}
