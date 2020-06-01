using RestLib.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Services.Interfaces
{
    public interface IBoardService
    {
        Task<Board> GetBoardAsync(Guid boardGuid);
        Task<List<Board>> GetBoardsAsync();
    }
}