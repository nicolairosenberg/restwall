using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Models.V1;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Services.Interfaces
{
    public interface IBoardService
    {
        Task<ResponseBoardDto> GetBoardAsync(Guid boardId);
        Task<IEnumerable<ResponseBoardDto>> GetBoardsAsync();
    }
}