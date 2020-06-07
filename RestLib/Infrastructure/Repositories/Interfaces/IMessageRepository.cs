using RestLib.Infrastructure.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        Task<Message> CreateMessageAsync(Message message);
        Task<Message> GetMessageAsync(Guid messageId);
        Task<IQueryable<Message>> GetMessagesAsync();
        Task<Message> UpdateMessageAsync(Message message);
        Task<Message> DeleteMessageAsync(Message message);
        Task<bool> ExistsAsync(Guid messageId);
    }
}
