using System.Threading.Tasks;
using NewsFeeds.DAL.Entities;

namespace NewsFeeds.DAL.Repositories.FeedRepository
{
    public interface IFeedRepository : IRepository<Feed>
    {
        Task<bool> ContainsEntity(string title, int feedCollectionId);
    }
}