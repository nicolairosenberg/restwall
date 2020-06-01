using Microsoft.EntityFrameworkCore;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Services
{
    public class MessageService : IMessageService
    {
        private readonly DataContext _dataContext;

        public MessageService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Message> CreateMessageAsync(Guid userGuid, Message message)
        {
            // Business validation

            await _dataContext.Messages.AddAsync(message);
            await _dataContext.SaveChangesAsync();
            return message;
        }

        public async Task<Message> GetMessageAsync(Guid messageGuid)
        {
            return await _dataContext.Messages.SingleAsync(x => x.Guid == messageGuid);
        }

        public async Task<List<Message>> GetMessagesAsync(Guid userGuid)
        {
            // test
            return await _dataContext.Messages.ToListAsync();
        }
    }
}
