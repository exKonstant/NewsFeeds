using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsFeeds.BLL.DTOs.FeedCollectionDTOs;
using NewsFeeds.BLL.DTOs.UserDTOs;
using NewsFeeds.BLL.Enums;
using NewsFeeds.DAL.Entities;
using NewsFeeds.DAL.UnitOfWork;

namespace NewsFeeds.BLL.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _unitOfWork.Users.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetAsync(int id)
        {
            var user = await _unitOfWork.Users.GetAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<FeedCollectionDto>> GetFeedCollectionsByUserAsync(int userId)
        {
            var feedCollections = await _unitOfWork.Users.GetFeedCollectionsByUser(userId).ToListAsync();
            return _mapper.Map<IEnumerable<FeedCollectionDto>>(feedCollections);
        }

        public async Task<UserResponse> AddAsync(UserDtoForCreate userDtoForCreate)
        {
            if (string.IsNullOrEmpty(userDtoForCreate.FirstName))
            {
                return UserResponse.InvalidFirstName;
            }
            if (string.IsNullOrEmpty(userDtoForCreate.LastName))
            {
                return UserResponse.InvalidLastName;
            }
            var user = _mapper.Map<User>(userDtoForCreate);
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return UserResponse.Ok;
        }

        public async Task<UserResponse> UpdateAsync(UserDto userDto)
        {
            if (string.IsNullOrEmpty(userDto.FirstName))
            {
                return UserResponse.InvalidFirstName;
            }
            if (string.IsNullOrEmpty(userDto.LastName))
            {
                return UserResponse.InvalidLastName;
            }
            if (!await _unitOfWork.Users.ContainsEntityWithId(userDto.Id))
            {
                return UserResponse.NotExist;
            }
            var user = await _unitOfWork.Users.GetAsync(userDto.Id);
            _mapper.Map(userDto, user);
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();
            return UserResponse.Ok;
        }

        public async Task<UserResponse> DeleteAsync(int id)
        {
            if (!await _unitOfWork.Users.ContainsEntityWithId(id))
            {
                return UserResponse.NotExist;
            }
            _unitOfWork.Users.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return UserResponse.Ok;
        }
    }
}
