using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Helpers;
using RestLib.Infrastructure.Models.V1.Messages;
using System;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Services.Interfaces
{
    public interface IMessageService
    {
        Task<PagedList<Message>> GetMessagesAsync(Guid boardId, Guid topicId, MessagesParams messagesParams);
        Task<ResponseMessageDto> GetMessageAsync(Guid boardId, Guid topicId, Guid messageId);
        Task<ResponseMessageDto> CreateMessageAsync(Guid boardId, Guid topicId, RequestMessageDto message);
        Task<ResponseMessageDto> UpdateMessageAsync(Guid boardId, Guid topicId, Guid mssageId, UpdateMessageDto message);
        Task<ResponseMessageDto> DeleteMessageAsync(Guid boardId, Guid topicId, ResponseMessageDto message);
        Task<bool> MessageExistsAsync(Guid messageId);
    }
}