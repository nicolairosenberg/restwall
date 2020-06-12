using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Helpers;
using RestLib.Infrastructure.Models.V1.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        Task<ICollection<ResponseUserDto>> GetUsersAsync();
        Task<ResponseUserDto> GetUserAsync(Guid userId);
        Task<ResponseUserDto> CreateUserAsync(RequestUserDto user);
        Task<ResponseUserDto> UpdateUserAsync(Guid userId, UpdateUserDto user);
        Task<ResponseUserDto> DeleteUserAsync(ResponseUserDto user);
        Task<bool> UserExistsAsync(Guid userId);
    }
}