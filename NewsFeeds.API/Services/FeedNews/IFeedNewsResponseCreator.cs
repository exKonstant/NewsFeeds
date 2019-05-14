using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace NewsFeeds.API.Services.FeedNews
{
    public interface IFeedNewsResponseCreator
    {
        IActionResult ResponseForGetFeedNews(ICollection<BLL.Common.FeedNewsBusinessModel.FeedNews> feedNews);
    }
}