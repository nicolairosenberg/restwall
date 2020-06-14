using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Models.V1.Topics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Services.Interfaces
{
    public interface ITopicService
    {
        Task<ICollection<ResponseTopicDto>> GetTopicsAsync(Guid boardId);
        Task<ICollection<ResponseTopicDto>> GetUserTopicsAsync(Guid userId);
        Task<ResponseTopicDto> GetTopicAsync(Guid boardId, Guid topicId);
        Task<ResponseTopicDto> CreateTopicAsync(Guid boardId, RequestTopicDto topic);
        Task<ResponseTopicDto> UpdateTopicAsync(Guid boardId, Guid topicId, UpdateTopicDto topic);
        Task<ResponseTopicDto> DeleteTopicAsync(Guid boardId, ResponseTopicDto topic);
        Task<bool> TopicExistsAsync(Guid topicId);
    }
}