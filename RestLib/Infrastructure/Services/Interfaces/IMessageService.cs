using RestLib.Infrastructure.Helpers;
using RestLib.Infrastructure.Models.V1.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Services.Interfaces
{
    public interface IMessageService
    {
        Task<ICollection<ResponseMessageDto>> GetMessagesAsync(Guid topicId);
        Task<ICollection<ResponseMessageDto>> GetUserMessagesAsync(Guid userId, Guid topicId);
        Task<ResponseMessageDto> GetMessageAsync(Guid topicId, Guid messageId);
        Task<ResponseMessageDto> CreateMessageAsync(Guid topicId, RequestMessageDto message);
        Task<ResponseMessageDto> UpdateMessageAsync(Guid topicId, Guid mssageId, UpdateMessageDto message);
        Task<ResponseMessageDto> DeleteMessageAsync(Guid topicId, ResponseMessageDto message);
        Task<bool> MessageExistsAsync(Guid messageId);
    }
}