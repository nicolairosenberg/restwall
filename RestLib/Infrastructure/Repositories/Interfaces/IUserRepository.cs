using RestLib.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<User> GetUserAsync(Guid userId);
        Task<ICollection<User>> GetUsersAsync();
        Task<User> UpdateUserAsync(User user);
        Task<User> DeleteUserAsync(User user);
        Task<bool> ExistsAsync(Guid userId);
    }
}
