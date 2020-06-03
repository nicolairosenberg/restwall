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
            return message;
        }

        public async Task<Message> GetMessageAsync(Guid id)
        {
            return await _dataContext.Messages.Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        public async Task<ICollection<Message>> GetMessagesAsync(Guid topicId)
        {
            return await _dataContext.Messages.ToListAsync();
        }

        public async Task<Message> UpdateMessageAsync(Message message)
        {
            var oldMessage = await _dataContext.Messages.Where(x => x.Id == message.Id).SingleOrDefaultAsync();
            oldMessage.Text = message.Text;
            oldMessage.Title = message.Title;
            oldMessage.UpdatedOn = DateTime.Now;

            await _dataContext.SaveChangesAsync();

            return oldMessage;
        }

        public async Task DeleteMessageAsync(Message message)
        {
            //var message = await _dataContext.Messages.Where(x => x.Id == id).SingleOrDefaultAsync();
            _dataContext.Messages.Remove(message);
            await _dataContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            
        }
    }
}
