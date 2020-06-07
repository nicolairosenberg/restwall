using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Helpers;
using RestLib.Infrastructure.Models.V1.Messages;
using System;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Services.Interfaces
{
    public interface IMessageService
    {
        Task<PagedList<Message>> GetMessagesAsync(Guid boardId, MessagesParams MessagesParams);
        Task<ResponseMessageDto> GetMessageAsync(Guid boardId, Guid MessageId);
        Task<ResponseMessageDto> CreateMessageAsync(Guid boardId, RequestMessageDto Message);
        Task<ResponseMessageDto> UpdateMessageAsync(Guid boardId, Guid MessageId, UpdateMessageDto Message);
        Task<ResponseMessageDto> DeleteMessageAsync(Guid boardId, ResponseMessageDto Message);
        Task<bool> MessageExistsAsync(Guid MessageId);
    }
}