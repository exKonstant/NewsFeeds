using System.Threading.Tasks;
using NewsFeeds.DAL.Repositories.FeedCollectionRepository;
using NewsFeeds.DAL.Repositories.UserRepository;

namespace NewsFeeds.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IFeedCollectionRepository FeedCollections { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}