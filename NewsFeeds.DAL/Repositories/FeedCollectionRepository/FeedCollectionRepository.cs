using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewsFeeds.DAL.EF;
using NewsFeeds.DAL.Entities;

namespace NewsFeeds.DAL.Repositories.FeedCollectionRepository
{
    public class FeedCollectionRepository : Repository<FeedCollection>, IFeedCollectionRepository
    {
        private readonly DbSet<FeedCollection> _feedCollections;

        public FeedCollectionRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _feedCollections = DbContext.FeedCollections;
        }
        public override async Task AddAsync(FeedCollection entity)
        {
            await _feedCollections.AddAsync(entity);
        }

        public override async Task<bool> ContainsEntityWithId(int id)
        {
            return await _feedCollections.AnyAsync(fc => fc.Id == id);
        }

        public async Task<bool> ContainsEntityWithIds(int id, int userId)
        {
            return await _feedCollections.AnyAsync(u => u.Id == id && u.UserId == userId);
        }

        public async Task<bool> ContainsFeedCollectionWithName(string name, int userId)
        {
            return await _feedCollections.AnyAsync(fc => fc.Name == name && fc.UserId == userId);
        }

        public override void Delete(int id)
        {
            var feedCollection = new FeedCollection { Id = id };
            _feedCollections.Remove(feedCollection);
        }

        public override void Delete(int id, int userId)
        {
            var feedCollection = new FeedCollection { Id = id, UserId = userId };
            _feedCollections.Remove(feedCollection);
        }

        public override IQueryable<FeedCollection> GetAll()
        {
            return _feedCollections.Include(fc => fc.Feeds);
        }

        public IQueryable<FeedCollection> GetFeedCollectionsByUser(int userId)
        {
            return _feedCollections.Where(f => f.UserId == userId).Include(fc => fc.Feeds);
        }

        public override async Task<FeedCollection> GetAsync(int id)
        {
            return await _feedCollections.Include(fc => fc.Feeds).FirstOrDefaultAsync(fc => fc.Id == id);
        }

        public async Task<FeedCollection> GetAsync(int id, int userId)
        {
            return await _feedCollections.FirstOrDefaultAsync(u => u.Id == id && u.UserId == userId);
        }

        public override void Update(FeedCollection entity)
        {
            _feedCollections.Update(entity);
        }
    }
}
