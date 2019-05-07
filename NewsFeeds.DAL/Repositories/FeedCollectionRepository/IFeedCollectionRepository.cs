using System.Linq;
using System.Threading.Tasks;
using NewsFeeds.DAL.Entities;

namespace NewsFeeds.DAL.Repositories.FeedCollectionRepository
{
    public interface IFeedCollectionRepository : IRepository<FeedCollection>
    {
        IQueryable<Feed> GetFeedsByFeedCollection(int feedCollectionId);
        Task<bool> ContainsFeedCollectionWithName(string name, int userId);
    }
}