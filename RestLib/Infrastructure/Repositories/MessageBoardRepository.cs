using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Repositories.Interfaces;
using System;

namespace RestLib.Infrastructure.Repositories
{
    public class MessageBoardRepository : IMessageBoardRepository, IDisposable
    {
        private readonly DataContext _dataContext;

        public MessageBoardRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
