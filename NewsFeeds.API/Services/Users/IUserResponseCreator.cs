using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NewsFeeds.BLL.DTOs.FeedCollectionDTOs;
using NewsFeeds.BLL.DTOs.UserDTOs;
using NewsFeeds.BLL.Enums;

namespace NewsFeeds.API.Services.Users
{
    public interface IUserResponseCreator
    {
        IActionResult ResponseForGetFeedCollections(IEnumerable<FeedCollectionDto> feedCollectionDtos);
        IActionResult ResponseForGetAll(IEnumerable<UserDto> userDtos);
        IActionResult ResponseForGet(UserDto userDto);
        IActionResult ResponseForCreate(UserResponse response/*, UserDtoForCreate userDtoForCreate*/);
        IActionResult ResponseForUpdate(UserResponse response);
        IActionResult ResponseForDelete(UserResponse response);
    }
}