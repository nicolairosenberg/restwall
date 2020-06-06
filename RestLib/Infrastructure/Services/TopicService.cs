using AutoMapper;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Helpers;
using RestLib.Infrastructure.Models.V1;
using RestLib.Infrastructure.Parameters;
using RestLib.Infrastructure.Repositories.Interfaces;
using RestLib.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Services
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IMapper _mapper;

        public TopicService(ITopicRepository topicRepository, IMapper mapper)
        {
            _topicRepository = topicRepository;
            _mapper = mapper;
        }

        public async Task<ResponseTopicDto> CreateTopicAsync(Guid boardId, RequestTopicDto topic)
        {
            var topicEntity = _mapper.Map<Topic>(topic);
            topicEntity.Id = Guid.NewGuid();
            topicEntity.BoardId = boardId;

            foreach (var item in topicEntity.Messages)
            {
                item.Id = Guid.NewGuid();
                item.TopicId = topicEntity.Id;
                item.UserId = topicEntity.UserId;
            }

            var createdEntity = await _topicRepository.CreateTopicAsync(topicEntity);
            var responseDto = _mapper.Map<ResponseTopicDto>(createdEntity);

            return responseDto;
        }

        public async Task<ResponseTopicDto> GetTopicAsync(Guid boardId, Guid topicId)
        {
            var topic = await _topicRepository.GetTopicAsync(topicId);

            if (topic == null)
            {
                return null;
            }

            var responseDto = _mapper.Map<Topic, ResponseTopicDto>(topic);
            return responseDto;
        }

        //public async Task<IEnumerable<ResponseTopicDto>> GetTopicsAsync(Guid boardId)
        //{
        //    var topics = await _topicRepository.GetTopicsAsync(boardId);
        //    var topicsDto = _mapper.Map<IEnumerable<ResponseTopicDto>>(topics);
        //    return topicsDto;
        //}

        public async Task<PagedList<Topic>> GetTopicsAsync(Guid boardId, TopicsParams topicsParams)
        {
            var collection = await _topicRepository.GetTopicsAsync(boardId);

            //var dtoCollection = _mapper.Map<IQueryable<ResponseTopicDto>>(collection);

            var pagedList = PagedList<Topic>.Create(collection, topicsParams.PageNumber, topicsParams.PageSize);

            return pagedList;
            //var topicsDto = _mapper.Map<PagedList<ResponseTopicDto>>(topics);
            //return topicsDto;
        }

        public async Task<ResponseTopicDto> UpdateTopicAsync(Guid boardId, Guid topicId, UpdateTopicDto topic)
        {
            var existingTopic = await _topicRepository.GetTopicAsync(topicId);

            var updatedEntity = _mapper.Map(topic, existingTopic);

            var returnedEntity = await _topicRepository.UpdateTopicAsync(updatedEntity);

            var responseDto = _mapper.Map<Topic, ResponseTopicDto>(returnedEntity);

            return responseDto;
        }

        public async Task<ResponseTopicDto> DeleteTopicAsync(Guid boardId, ResponseTopicDto topic)
        {
            var existingTopic = await _topicRepository.GetTopicAsync(topic.Id);

            var returnedEntity = await _topicRepository.DeleteTopicAsync(existingTopic);

            var responseDto = _mapper.Map<Topic, ResponseTopicDto>(returnedEntity);

            return responseDto;
        }

        public async Task<bool> TopicExistsAsync(Guid topicId)
        {
            return await _topicRepository.ExistsAsync(topicId);
        }
    }
}
