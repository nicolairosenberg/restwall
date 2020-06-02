using Microsoft.EntityFrameworkCore;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Services
{
    public class MessageService : IMessageService
    {
        private readonly DataContext _dataContext;

        public MessageService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Message> CreateMessageAsync(Guid userGuid, Guid boardGuid, Message message)
        {
            //var board = await _dataContext.Boards.Where(x => x.Id == boardGuid).SingleOrDefaultAsync();
            //message.BoardId = board.Id;

            //await _dataContext.Messages.AddAsync(message);
            //await _dataContext.SaveChangesAsync();
            //return message;

            return null;
        }

        public async Task<Message> GetMessageAsync(Guid messageGuid)
        {
            return await _dataContext.Messages.SingleAsync(x => x.Id == messageGuid);
        }

        public async Task<List<Message>> GetMessagesAsync(Guid userGuid, Guid? boardGuid = null)
        {
            //if (boardGuid == null)
            //{
            //    var user = await _dataContext.Users.Where(x => x.Id == userGuid).SingleOrDefaultAsync();

            //    return await _dataContext.Messages.Where(x => x.UserId == user.Id).ToListAsync();

            //}
            //else
            //{
            //    if (boardGuid != Guid.Empty)
            //    {
            //        var board = await _dataContext.Boards.Where(x => x.Id == boardGuid.Value).SingleOrDefaultAsync();
            //        var userId = 1;
            //        return await _dataContext.Messages.Where(x => x.UserId == userId && x.BoardId == board.Id).ToListAsync();
            //    }

            //    throw new Exception();
            //}

            return null;

        }
    }
}
