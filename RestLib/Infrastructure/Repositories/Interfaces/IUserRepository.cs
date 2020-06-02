using System.Threading.Tasks;

namespace RestLib.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUserAsync();
        Task GetUserAsync();
        Task GetUsersAsync();
        Task UpdateUserAsync();
        Task DeleteUserAsync();
    }
}
