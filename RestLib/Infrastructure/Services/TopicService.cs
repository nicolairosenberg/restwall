using AutoMapper;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Models.V1;
using RestLib.Infrastructure.Repositories.Interfaces;
using RestLib.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
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
            var createdEntity = await _topicRepository.CreateTopicAsync(topicEntity);
            var responseDto = _mapper.Map<ResponseTopicDto>(createdEntity);

            return responseDto;
        }

        public async Task<ResponseTopicDto> GetTopicAsync(Guid boardId, Guid topicId)
        {
            var topic = await _topicRepository.GetTopicAsync(topicId);

            if(topic == null)
            {
                return null;
            }

            var responseDto = _mapper.Map<Topic, ResponseTopicDto>(topic);
            return responseDto;
        }

        public async Task<IEnumerable<ResponseTopicDto>> GetTopicsAsync(Guid boardId)
        {
            var topics = await _topicRepository.GetTopicsAsync(boardId);
            var topicsDto = _mapper.Map<IEnumerable<ResponseTopicDto>>(topics);
            return topicsDto;
        }

        public Task<ResponseTopicDto> UpdateTopicAsync(Guid boardId, ResponseTopicDto topic)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseTopicDto> DeleteTopicAsync(Guid boardId, ResponseTopicDto topic)
        {
            throw new NotImplementedException();
        }
    }
}
