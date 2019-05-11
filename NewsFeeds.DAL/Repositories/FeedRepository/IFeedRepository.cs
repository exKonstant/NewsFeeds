using System.Linq;
using System.Threading.Tasks;
using NewsFeeds.DAL.Entities;

namespace NewsFeeds.DAL.Repositories.FeedRepository
{
    public interface IFeedRepository : IRepository<Feed>
    {
        Task<Feed> GetAsync(int id, int feedCollectionId, int userId);
        Task<bool> ContainsEntity(string title, int feedCollectionId);
        Task<bool> ContainsEntityWithIds(int id, int feedCollectionId);
        IQueryable<Feed> GetFeedsByFeedCollection(int feedCollectionId, int userId);        
    }
}