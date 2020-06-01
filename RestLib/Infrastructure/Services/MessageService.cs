using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Services
{
    public class MessageService : IMessageService
    {
        public Task<Message> CreateMessageAsync(Guid userGuid, string title, string text)
        {
            return null;
        }
    }
}
