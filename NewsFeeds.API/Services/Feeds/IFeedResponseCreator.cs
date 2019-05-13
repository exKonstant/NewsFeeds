using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NewsFeeds.BLL;
using NewsFeeds.BLL.DTOs.FeedDTOs;
using NewsFeeds.BLL.Enums;

namespace NewsFeeds.API.Services.Feeds
{
    public interface IFeedResponseCreator
    {
        IActionResult ResponseForGetFeedsByFeedCollection(IEnumerable<FeedDto> feedDtos);
        IActionResult ResponseForGet(FeedDto feedDto);
        IActionResult ResponseForCreate(Result result, int feedCollectionId, int userId,
            FeedDtoForCreate feedDtoForCreate);
        IActionResult ResponseForUpdate(Result result);
        IActionResult ResponseForDelete(Result result);
    }
}