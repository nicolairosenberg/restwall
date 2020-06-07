using System;
using System.Threading.Tasks;
using AutoMapper;
using RestLib.Infrastructure.Entities;
using RestLib.Infrastructure.Helpers;
using RestLib.Infrastructure.Models.V1.Users;
using RestLib.Infrastructure.Repositories.Interfaces;
using RestLib.Infrastructure.Services.Interfaces;

namespace RestLib.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ResponseUserDto> CreateUserAsync(Guid boardId, Guid topicId, RequestUserDto user)
        {
            var userEntity = _mapper.Map<User>(user);
            userEntity.Id = Guid.NewGuid();

            var createdEntity = await _userRepository.CreateUserAsync(userEntity);
            var responseDto = _mapper.Map<ResponseUserDto>(createdEntity);

            return responseDto;
        }

        public async Task<ResponseUserDto> DeleteUserAsync(Guid boardId, Guid topicId, ResponseUserDto user)
        {
            var existingTopic = await _userRepository.GetUserAsync(user.Id);

            var returnedEntity = await _userRepository.DeleteUserAsync(existingTopic);

            var responseDto = _mapper.Map<User, ResponseUserDto>(returnedEntity);

            return responseDto;
        }

        public async Task<ResponseUserDto> GetUserAsync(Guid boardId, Guid topicId, Guid userId)
        {
            var user = await _userRepository.GetUserAsync(userId);

            if (user == null)
            {
                return null;
            }

            var responseDto = _mapper.Map<User, ResponseUserDto>(user);
            return responseDto;
        }

        public async Task<PagedList<User>> GetUsersAsync(Guid boardId, Guid topicId, UsersParams usersParams)
        {
            var collection = await _userRepository.GetUsersAsync();

            var pagedList = PagedList<User>.Create(collection, usersParams.PageNumber, usersParams.PageSize);

            return pagedList;
        }

        public async Task<ResponseUserDto> UpdateUserAsync(Guid boardId, Guid topicId, Guid userId, UpdateUserDto user)
        {
            var existingTopic = await _userRepository.GetUserAsync(userId);

            var updatedEntity = _mapper.Map(user, existingTopic);

            var returnedEntity = await _userRepository.UpdateUserAsync(updatedEntity);

            var responseDto = _mapper.Map<User, ResponseUserDto>(returnedEntity);

            return responseDto;
        }

        public async Task<bool> UserExistsAsync(Guid userId)
        {
            return await _userRepository.ExistsAsync(userId);
        }
    }
}
