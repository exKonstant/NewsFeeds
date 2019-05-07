using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NewsFeeds.DAL.EF;
using NewsFeeds.DAL.Repositories.FeedCollectionRepository;
using NewsFeeds.DAL.Repositories.FeedRepository;
using NewsFeeds.DAL.Repositories.UserRepository;

namespace NewsFeeds.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private IUserRepository _userRepository;
        public IUserRepository Users
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_dbContext);
                }
                return _userRepository;
            }

        }

        private IFeedCollectionRepository _feedCollectionRepository;
        public IFeedCollectionRepository FeedCollections
        {
            get
            {
                if (_feedCollectionRepository == null)
                {
                    _feedCollectionRepository = new FeedCollectionRepository(_dbContext);
                }
                return _feedCollectionRepository;
            }

        }

        private IFeedRepository _feedRepository;
        public IFeedRepository Feeds
        {
            get
            {
                if (_feedRepository == null)
                {
                    _feedRepository = new FeedRepository(_dbContext);
                }
                return _feedRepository;
            }

        }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
