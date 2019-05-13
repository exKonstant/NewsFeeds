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

        public async Task<Result> AddAsync(UserDtoForCreate userDtoForCreate)
        {
            if (string.IsNullOrEmpty(userDtoForCreate.FirstName))
            {
                return Result.Fail("Invalid first name");
            }
            if (string.IsNullOrEmpty(userDtoForCreate.LastName))
            {
                return Result.Fail("Invalid last name");
            }
            var user = _mapper.Map<User>(userDtoForCreate);
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(user.Id);
        }

        public async Task<Result> UpdateAsync(UserDto userDto)
        {
            if (string.IsNullOrEmpty(userDto.FirstName))
            {
                return Result.Fail("Invalid first name");
            }
            if (string.IsNullOrEmpty(userDto.LastName))
            {
                return Result.Fail("Invalid last name");
            }
            if (!await _unitOfWork.Users.ContainsEntityWithId(userDto.Id))
            {
                return Result.Fail("User doesn't exist");
            }
            var user = await _unitOfWork.Users.GetAsync(userDto.Id);
            _mapper.Map(userDto, user);
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok("Ok");
        }

        public async Task<Result> DeleteAsync(int id)
        {
            if (!await _unitOfWork.Users.ContainsEntityWithId(id))
            {
                return Result.Fail("User doesn't exist");
            }
            _unitOfWork.Users.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok("Ok");
        }
    }
}
