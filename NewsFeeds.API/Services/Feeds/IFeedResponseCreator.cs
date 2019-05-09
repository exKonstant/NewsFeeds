using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NewsFeeds.BLL.DTOs.FeedDTOs;
using NewsFeeds.BLL.Enums;

namespace NewsFeeds.API.Services.Feeds
{
    public interface IFeedResponseCreator
    {
        IActionResult ResponseForGetAll(IEnumerable<FeedDto> feedDtos);
        IActionResult ResponseForGet(FeedDto feedDto);
        IActionResult ResponseForCreate(FeedResponse response /*,FeedDtoForCreate feedDtoForCreate*/);
        IActionResult ResponseForUpdate(FeedResponse response);
        IActionResult ResponseForDelete(FeedResponse response);
    }
}