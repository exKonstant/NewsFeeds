using Microsoft.AspNetCore.Mvc;
using NewsFeeds.BLL.Common;
using NewsFeeds.BLL.DTOs.UserDTOs;
using System.Collections.Generic;

namespace NewsFeeds.API.Services.Users
{
    public interface IUserResponseCreator
    {
        IActionResult ResponseForGetAll(IEnumerable<UserDto> userDtos);
        IActionResult ResponseForGet(UserDto userDto);
        IActionResult ResponseForCreate(Result result, UserDtoForCreate userDtoForCreate);
    }
}