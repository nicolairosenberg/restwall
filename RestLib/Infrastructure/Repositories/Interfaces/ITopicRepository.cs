using RestLib.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Repositories.Interfaces
{
    public interface ITopicRepository
    {
        Task<Topic> CreateTopicAsync(Topic topic);
        Task<Topic> GetTopicAsync(Guid topicId);
        Task<ICollection<Topic>> GetTopicsAsync(Guid boardId);
        Task<Topic> UpdateTopicAsync(Topic topic);
        Task DeleteTopicAsync(Topic topic);
    }
}
