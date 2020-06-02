using RestLib.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Repositories.Interfaces
{
    public interface IBoardRepository
    {
        Task CreateBoardAsync();
        Task GetBoardAsync();
        Task<ICollection<Board>> GetBoardsAsync();
        Task UpdateBoardAsync();
        Task DeleteBoardAsync();
    }
}
