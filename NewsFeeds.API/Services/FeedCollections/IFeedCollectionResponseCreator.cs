using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NewsFeeds.BLL.DTOs.FeedCollectionDTOs;
using NewsFeeds.BLL.DTOs.FeedDTOs;
using NewsFeeds.BLL.Enums;

namespace NewsFeeds.API.Services.FeedCollections
{
    public interface IFeedCollectionResponseCreator
    {
        IActionResult ResponseForGetAll(IEnumerable<FeedCollectionDto> feedCollectionDtos);
        IActionResult ResponseForGet(FeedCollectionDto feedCollectionDto);
        IActionResult ResponseForCreate(FeedCollectionResponse response);
        IActionResult ResponseForUpdate(FeedCollectionResponse response);
        IActionResult ResponseForDelete(FeedCollectionResponse response);
    }
}