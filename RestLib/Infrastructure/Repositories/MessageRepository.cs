using Microsoft.EntityFrameworkCore;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Message> CreateMessageAsync(Message message)
        {
            await _dataContext.Messages.AddAsync(message);
            await _dataContext.SaveChangesAsync();
            return message;
        }

        public async Task<Message> GetMessageAsync(Guid messageId)
        {
            return await _dataContext.Messages.Where(x => x.Id == messageId).SingleOrDefaultAsync();
        }

        public async Task<ICollection<Message>> GetMessagesAsync(Guid topicId)
        {
            return await _dataContext.Messages.Where(x => x.TopicId == topicId).ToListAsync();
        }

        public async Task<Message> UpdateMessageAsync(Message message)
        {
            message.UpdatedOn = DateTime.Now;
            _dataContext.Update(message);
            await _dataContext.SaveChangesAsync();

            return message;
        }

        public async Task<Message> DeleteMessageAsync(Message message)
        {
            _dataContext.Messages.Remove(message);
            await _dataContext.SaveChangesAsync();

            return message;
        }

        public async Task<bool> ExistsAsync(Guid messageId)
        {
            return await _dataContext.Messages.Where(x => x.Id == messageId).AnyAsync();
        }

        public void Dispose()
        {
            
        }
    }
}
