using Microsoft.AspNetCore.Mvc;
using NewsFeeds.BLL.Common;
using NewsFeeds.BLL.DTOs.FeedCollectionDTOs;
using System.Collections.Generic;

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