using Microsoft.EntityFrameworkCore;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
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

        public Task CreateBoardAsync()
        {
            throw new NotImplementedException();
        }

        public Task GetBoardAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Board>> GetBoardsAsync()
        {
            var boards = await _dataContext.Boards.ToListAsync();
            return boards;
        }

        public Task UpdateBoardAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteBoardAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
