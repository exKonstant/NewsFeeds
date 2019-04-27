using System.Linq;
using NewsFeeds.DAL.Entities;

namespace NewsFeeds.DAL.Repositories.UserRepository
{
    public interface IUserRepository : IRepository<User>
    {
        IQueryable<FeedCollection> GetFeedCollectionsByUser(int userId);
    }
}