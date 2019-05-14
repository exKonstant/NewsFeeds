using Microsoft.AspNetCore.Mvc;
using NewsFeeds.BLL.Common;
using NewsFeeds.BLL.DTOs.FeedDTOs;
using System.Collections.Generic;

namespace NewsFeeds.API.Services.Feeds
{
    public interface IFeedResponseCreator
    {
        IActionResult ResponseForGetFeedsByFeedCollection(IEnumerable<FeedDto> feedDtos);
        IActionResult ResponseForGet(FeedDto feedDto);
        IActionResult ResponseForCreate(Result result, int feedCollectionId, int userId, string feedUrl);
        IActionResult ResponseForDelete(Result result);
    }
}