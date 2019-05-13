using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NewsFeeds.BLL;
using NewsFeeds.BLL.DTOs.FeedCollectionDTOs;
using NewsFeeds.BLL.DTOs.FeedDTOs;
using NewsFeeds.BLL.Enums;

namespace NewsFeeds.API.Services.FeedCollections
{
    public interface IFeedCollectionResponseCreator
    {
        IActionResult ResponseForGetAll(IEnumerable<FeedCollectionDto> feedCollectionDtos);
        IActionResult ResponseForGet(FeedCollectionDto feedCollectionDto);
        IActionResult ResponseForCreate(Result result, int userId, FeedCollectionDtoForCreate feedCollectionDtoForCreate);
        IActionResult ResponseForUpdate(Result result);
        IActionResult ResponseForDelete(Result result);
    }
}