using System.Linq;
using System.Threading.Tasks;
using NewsFeeds.DAL.Entities;

namespace NewsFeeds.DAL.Repositories.FeedCollectionRepository
{
    public interface IFeedCollectionRepository : IRepository<FeedCollection>
    {
        Task<FeedCollection> GetAsync(int id, int userId);
        IQueryable<FeedCollection> GetFeedCollectionsByUser(int userId);
        Task<bool> ContainsEntityWithIds(int id, int userId);
        Task<bool> ContainsFeedCollectionWithName(string name, int userId);
        void Update(FeedCollection entity);
    }
}