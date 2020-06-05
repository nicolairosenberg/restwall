using RestLib.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Repositories.Interfaces
{
    public interface IBoardRepository
    {
        Task<Board> GetBoardAsync(Guid boardId);
        Task<IEnumerable<Board>> GetBoardsAsync();
        Task<bool> ExistsAsync(Guid boardId);
    }
}
