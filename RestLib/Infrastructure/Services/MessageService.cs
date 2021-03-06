﻿using AutoMapper;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Helpers;
using RestLib.Infrastructure.Models.V1.Messages;
using RestLib.Infrastructure.Repositories.Interfaces;
using RestLib.Infrastructure.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace RestLib.Infrastructure.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMapper _mapper;
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMapper mapper, IMessageRepository messageRepository)
        {
            _mapper = mapper;
            _messageRepository = messageRepository;
        }

        public async Task<ResponseMessageDto> CreateMessageAsync(Guid boardId, Guid topicId, RequestMessageDto message)
        {
            var messageEntity = _mapper.Map<Message>(message);
            messageEntity.Id = Guid.NewGuid();
            messageEntity.TopicId = topicId;

            var createdEntity = await _messageRepository.CreateMessageAsync(messageEntity);
            var responseDto = _mapper.Map<ResponseMessageDto>(createdEntity);

            return responseDto;
        }

        public async Task<ResponseMessageDto> GetMessageAsync(Guid boardId, Guid topicId, Guid messageId)
        {
            var message = await _messageRepository.GetMessageAsync(messageId);

            if (message == null)
            {
                return null;
            }

            var responseDto = _mapper.Map<Message, ResponseMessageDto>(message);
            return responseDto;
        }

        public async Task<PagedList<Message>> GetMessagesAsync(Guid boardId, Guid topicId, MessagesParams messagesParams)
        {
            var collection = await _messageRepository.GetMessagesAsync();

            var pagedList = PagedList<Message>.Create(collection, messagesParams.PageNumber, messagesParams.PageSize);

            return pagedList;
        }

        public async Task<ResponseMessageDto> UpdateMessageAsync(Guid boardId, Guid topicId, Guid messageId, UpdateMessageDto message)
        {
            var existingTopic = await _messageRepository.GetMessageAsync(messageId);

            var updatedEntity = _mapper.Map(message, existingTopic);

            var returnedEntity = await _messageRepository.UpdateMessageAsync(updatedEntity);

            var responseDto = _mapper.Map<Message, ResponseMessageDto>(returnedEntity);

            return responseDto;
        }

        public async Task<ResponseMessageDto> DeleteMessageAsync(Guid boardId, Guid topicId, ResponseMessageDto message)
        {
            var existingTopic = await _messageRepository.GetMessageAsync(message.Id);

            var returnedEntity = await _messageRepository.DeleteMessageAsync(existingTopic);

            var responseDto = _mapper.Map<Message, ResponseMessageDto>(returnedEntity);

            return responseDto;
        }

        public async Task<bool> MessageExistsAsync(Guid messageId)
        {
            return await _messageRepository.ExistsAsync(messageId);
        }
    }
}
