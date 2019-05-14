using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace NewsFeeds.API.Services.FeedNews
{
    public interface IFeedNewsResponseCreator
    {
        IActionResult ResponseForGetFeedNews(ICollection<BLL.Common.FeedNewsBusinessModel.FeedNews> feedNews);
    }
}