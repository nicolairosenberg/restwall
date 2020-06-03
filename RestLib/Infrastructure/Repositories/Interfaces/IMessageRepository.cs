using RestLib.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        Task<Message> CreateMessageAsync(Message message);
        Task<Message> GetMessageAsync(Guid id);
        Task<ICollection<Message>> GetMessagesAsync(Guid topicId);
        Task<Message> UpdateMessageAsync(Message message);
        Task DeleteMessageAsync(Message message);
    }
}
