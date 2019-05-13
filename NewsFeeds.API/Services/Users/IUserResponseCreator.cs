using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NewsFeeds.BLL;
using NewsFeeds.BLL.DTOs.FeedCollectionDTOs;
using NewsFeeds.BLL.DTOs.UserDTOs;
using NewsFeeds.BLL.Enums;

namespace NewsFeeds.API.Services.Users
{
    public interface IUserResponseCreator
    {
        IActionResult ResponseForGetAll(IEnumerable<UserDto> userDtos);
        IActionResult ResponseForGet(UserDto userDto);
        IActionResult ResponseForCreate(Result result, UserDtoForCreate userDtoForCreate);
    }
}