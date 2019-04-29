using System.Linq;
using NewsFeeds.DAL.Entities;

namespace NewsFeeds.DAL.Repositories.FeedCollectionRepository
{
    public interface IFeedCollectionRepository : IRepository<FeedCollection>
    {
        IQueryable<Feed> GetFeedByFeedCollection(int feedCollectionId);
    }
}