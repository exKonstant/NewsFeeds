using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NewsFeeds.BLL.Services.FeedNews
{
    public interface IFeedNewsService
    {
        XElement GetXmlFeed(string feedUrl);

        Task<ICollection<Common.FeedNewsBusinessModel.FeedNews>> GetFeedNewsByFeed(int feedId, int feedCollectionId,
            int userId);

        Task<ICollection<Common.FeedNewsBusinessModel.FeedNews>> GetFeedNewsByFeedCollection(int feedCollectionId,
            int userId);
        ICollection<Common.FeedNewsBusinessModel.FeedNews> GetFeedNews(XElement document);
    }
}