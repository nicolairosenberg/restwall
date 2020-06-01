using RestLib.Infrastructure.Entities;
using System;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Services.Interfaces
{
    public interface IMessageService
    {
        Task<Message> CreateMessageAsync(Guid userGuid, string title, string text);
    }
}