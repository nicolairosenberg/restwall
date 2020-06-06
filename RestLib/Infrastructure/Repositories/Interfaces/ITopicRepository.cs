using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Helpers;
using RestLib.Infrastructure.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Repositories.Interfaces
{
    public interface ITopicRepository
    {
        Task<Topic> CreateTopicAsync(Topic topic);
        Task<Topic> GetTopicAsync(Guid topicId);
        //Task<IEnumerable<Topic>> GetTopicsAsync(Guid boardId);
        ////Task<PagedList<Topic>> GetTopicsAsync(Guid boardId, TopicsParams topicsParams);
        Task<IQueryable<Topic>> GetTopicsAsync(Guid boardId);
        Task<Topic> UpdateTopicAsync(Topic topic);
        Task<Topic> DeleteTopicAsync(Topic topic);
        Task<bool> ExistsAsync(Guid topicId);
    }
}
