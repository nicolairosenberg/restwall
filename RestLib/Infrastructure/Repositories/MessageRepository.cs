using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository, IDisposable
    {
        private readonly DataContext _dataContext;

        public MessageRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Task CreateMessageAsync()
        {
            throw new NotImplementedException();
        }

        public Task GetMessageAsync()
        {
            throw new NotImplementedException();
        }

        public Task GetMessagesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateMessageAsync()
        {
            throw new NotImplementedException();
        }
        public Task DeleteMessageAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
