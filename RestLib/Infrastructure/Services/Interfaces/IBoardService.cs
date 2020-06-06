using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Helpers;
using RestLib.Infrastructure.Models.V1;
using System;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Services.Interfaces
{
    public interface IBoardService
    {
        Task<ResponseBoardDto> GetBoardAsync(Guid boardId);
        Task<PagedList<Board>> GetBoardsAsync(BoardsParams boardParams);
    }
}