using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsFeeds.API.Services.FeedNews;
using NewsFeeds.BLL.Services.FeedNews;

namespace NewsFeeds.API.Controllers
{
    [Authorize(Roles = "user")]    
    public class FeedNewsController : Controller
    {
        private readonly IFeedNewsService _feedNewsService;
        private readonly IFeedNewsResponseCreator _feedNewsResponseCreator;

        public FeedNewsController(IFeedNewsService feedNewsService, IFeedNewsResponseCreator feedNewsResponseCreator)
        {
            _feedNewsService = feedNewsService;
            _feedNewsResponseCreator = feedNewsResponseCreator;
        }

        [Route("api/{userId}/feedCollections/{feedCollectionId}/feeds/{feedId}/feedNews")]
        [HttpGet]
        public async Task<IActionResult> GetFeedNewsByFeed(int feedId, int feedCollectionId, int userId)
        {
            var feedNewsBusinessModels = await _feedNewsService.GetFeedNewsByFeed(feedId, feedCollectionId, userId);
            return _feedNewsResponseCreator.ResponseForGetFeedNews(feedNewsBusinessModels);
        }

        [Route("api/{userId}/feedCollections/{feedCollectionId}/feedNews")]
        [HttpGet]
        public async Task<IActionResult> GetFeedNewsByCollection(int feedCollectionId, int userId)
        {
            var feedNewsBusinessModels = await _feedNewsService.GetFeedNewsByFeedCollection(feedCollectionId, userId);
            return _feedNewsResponseCreator.ResponseForGetFeedNews(feedNewsBusinessModels);
        }
    }
}
