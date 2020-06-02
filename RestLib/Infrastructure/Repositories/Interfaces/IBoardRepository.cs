using RestLib.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Repositories.Interfaces
{
    public interface IBoardRepository
    {
        Task<Board> GetBoardAsync(Guid id);
        Task<ICollection<Board>> GetBoardsAsync();
    }
}
