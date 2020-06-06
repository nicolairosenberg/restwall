using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Helpers;
using RestLib.Infrastructure.Models.V1;
using System;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Services.Interfaces
{
    public interface ITopicService
    {
        //Task<IEnumerable<ResponseTopicDto>> GetTopicsAsync(Guid boardId);
        Task<PagedList<Topic>> GetTopicsAsync(Guid boardId, TopicsParams topicsParams);
        Task<ResponseTopicDto> GetTopicAsync(Guid boardId, Guid topicId);
        Task<ResponseTopicDto> CreateTopicAsync(Guid boardId, RequestTopicDto topic);
        Task<ResponseTopicDto> UpdateTopicAsync(Guid boardId, Guid topicId, UpdateTopicDto topic);
        Task<ResponseTopicDto> DeleteTopicAsync(Guid boardId, ResponseTopicDto topic);
        Task<bool> TopicExistsAsync(Guid topicId);
    }
}