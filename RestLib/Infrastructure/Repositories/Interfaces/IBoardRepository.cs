using RestLib.Infrastructure.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Repositories.Interfaces
{
    public interface IBoardRepository
    {
        Task<Board> GetBoardAsync(Guid boardId);
        Task<IQueryable<Board>> GetBoardsAsync();
        Task<bool> ExistsAsync(Guid boardId);
    }
}
