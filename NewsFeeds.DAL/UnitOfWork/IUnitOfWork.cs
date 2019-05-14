using NewsFeeds.DAL.Repositories.FeedCollectionRepository;
using NewsFeeds.DAL.Repositories.FeedRepository;
using NewsFeeds.DAL.Repositories.UserRepository;
using System.Threading.Tasks;

namespace NewsFeeds.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IFeedCollectionRepository FeedCollections { get; }
        IFeedRepository Feeds { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}