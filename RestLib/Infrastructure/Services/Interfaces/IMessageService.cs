using RestLib.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Services.Interfaces
{
    public interface IMessageService
    {
        Task<Message> CreateMessageAsync(Guid userGuid, Message message);
        Task<Message> GetMessageAsync(Guid messageGuid);
        Task<List<Message>> GetMessagesAsync(Guid userGuid);
    }
}