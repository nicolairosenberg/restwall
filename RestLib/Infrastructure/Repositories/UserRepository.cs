using Microsoft.EntityFrameworkCore;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserAsync(Guid userId)
        {
            return await _dataContext.Users.Where(x => x.Id == userId).SingleOrDefaultAsync();
        }

        public async Task<IQueryable<User>> GetUsersAsync()
        {
            return _dataContext.Users as IQueryable<User>;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            user.UpdatedOn = DateTime.Now;
            _dataContext.Update(user);
            await _dataContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> DeleteUserAsync(User user)
        {
            _dataContext.Users.Remove(user);
            await _dataContext.SaveChangesAsync();

            return user;
        }

        public void Dispose()
        {

        }
    }
}
