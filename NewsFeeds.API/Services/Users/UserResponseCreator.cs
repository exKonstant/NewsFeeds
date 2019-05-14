using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewsFeeds.API.Models.Users;
using NewsFeeds.BLL.Common;
using NewsFeeds.BLL.DTOs.UserDTOs;
using System.Collections.Generic;

namespace NewsFeeds.API.Services.Users
{
    public class UserResponseCreator : IUserResponseCreator
    {
        private readonly IMapper _mapper;

        public UserResponseCreator(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult ResponseForGetAll(IEnumerable<UserDto> userDtos)
        {
            var userModels = _mapper.Map<IEnumerable<UserModel>>(userDtos);
            return new OkObjectResult(userModels);
        }

        public IActionResult ResponseForGet(UserDto userDto)
        {
            if (userDto == null)
            {
                return new NotFoundResult();
            }
            var userModel = _mapper.Map<UserModel>(userDto);
            return new OkObjectResult(userModel);
        }

        public IActionResult ResponseForCreate(Result result, UserDtoForCreate userDtoForCreate)
        {
            switch (result.IsFailure)
            {
                case true:
                    return new BadRequestObjectResult(result.Message);
                default:
                    return new CreatedAtRouteResult("GetUser", new { Id = ((Result<int>)result).Value }, userDtoForCreate);
            }
        }        
    }
}

