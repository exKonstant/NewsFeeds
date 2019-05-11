using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewsFeeds.DAL.EF;
using NewsFeeds.DAL.Entities;

namespace NewsFeeds.DAL.Repositories.FeedRepository
{
    public class FeedRepository : Repository<Feed>, IFeedRepository
    {
        private readonly DbSet<Feed> _feeds;

        public FeedRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _feeds = DbContext.Feeds;
        }
        public override async Task AddAsync(Feed entity)
        {
            await _feeds.AddAsync(entity);
        }

        public async Task<bool> ContainsEntity(string title, int feedCollectionId)
        {
            return await _feeds.AnyAsync(f => f.Title == title && f.FeedCollectionId == feedCollectionId);
        }

        public override async Task<bool> ContainsEntityWithId(int id)
        {
            return await _feeds.AnyAsync(u => u.Id == id);
        }

        public async Task<bool> ContainsEntityWithIds(int id, int feedCollectionId)
        {
            return await _feeds.AnyAsync(u => u.Id == id);
        }

        public void Delete(int id, int feedCollectionId)
        {
            var feed = new Feed { Id = id, FeedCollectionId = feedCollectionId};
            _feeds.Remove(feed);
        }

        public override void Delete(int id)
        {
            var feed = new Feed { Id = id };
            _feeds.Remove(feed);
        }

        public override IQueryable<Feed> GetAll()
        {
            return _feeds;
        }

        public IQueryable<Feed> GetFeedsByFeedCollection(int feedCollectionId, int userId)
        {
            return _feeds.Where(f => f.FeedCollectionId == feedCollectionId && f.FeedCollection.UserId == userId);
        }

        public override async Task<Feed> GetAsync(int id)
        {
            return await _feeds.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Feed> GetAsync(int id, int feedCollectionId, int userId)
        {
            return await _feeds.FirstOrDefaultAsync(u => u.Id == id && u.FeedCollectionId == feedCollectionId && u.FeedCollection.UserId == userId);
        }

        public override void Update(Feed entity)
        {
            _feeds.Update(entity);
        }
    }
}
