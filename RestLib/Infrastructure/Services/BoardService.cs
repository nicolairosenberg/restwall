using Microsoft.EntityFrameworkCore;
using RestLib.Infrastructure.Entities;
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

        public BoardService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Board> GetBoardAsync(Guid boardGuid)
        {
            return await _dataContext.Boards.Where(x => x.Guid == boardGuid).SingleOrDefaultAsync();
        }

        public async Task<List<Board>> GetBoardsAsync()
        {
            return await _dataContext.Boards.ToListAsync();
        }
    }
}
