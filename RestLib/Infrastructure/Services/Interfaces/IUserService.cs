using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Helpers;
using RestLib.Infrastructure.Models.V1.Users;
using System;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        Task<PagedList<User>> GetUsersAsync(Guid boardId, Guid topicId, UsersParams usersParams);
        Task<ResponseUserDto> GetUserAsync(Guid boardId, Guid topicId, Guid userId);
        Task<ResponseUserDto> CreateUserAsync(Guid boardId, Guid topicId, RequestUserDto user);
        Task<ResponseUserDto> UpdateUserAsync(Guid boardId, Guid topicId, Guid userId, UpdateUserDto user);
        Task<ResponseUserDto> DeleteUserAsync(Guid boardId, Guid topicId, ResponseUserDto user);
        Task<bool> UserExistsAsync(Guid userId);
    }
}