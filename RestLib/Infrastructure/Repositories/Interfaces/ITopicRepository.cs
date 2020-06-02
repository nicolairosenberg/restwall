using RestLib.Infrastructure.Entities;
using System;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Repositories.Interfaces
{
    public interface ITopicRepository
    {
        Task<Topic> CreateTopicAsync(Guid userId, Topic topic);
        Task GetTopicAsync();
        Task GetTopicsAsync();
        Task UpdateTopicAsync();
        Task DeleteTopicAsync();
    }
}
