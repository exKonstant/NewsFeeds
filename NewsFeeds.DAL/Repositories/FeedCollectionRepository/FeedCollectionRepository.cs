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
            return await _feedCollections.AnyAsync(u => u.Id == id);
        }

        public override void Delete(int id)
        {
            var feedCollection = new FeedCollection { Id = id };
            _feedCollections.Remove(feedCollection);
        }

        public override IQueryable<FeedCollection> GetAll()
        {
            return _feedCollections.Include(u => u.Feeds);
        }

        public override async Task<FeedCollection> GetAsync(int id)
        {
            return await _feedCollections.Include(u => u.Feeds).FirstOrDefaultAsync(u => u.Id == id);
        }

        public IQueryable<Feed> GetFeedByFeedCollection(int feedCollectionId)
        {
            return _feedCollections.SelectMany(u => u.Feeds.Where(f => f.FeedCollectionId == feedCollectionId).Select(f => f));
        }

        public override void Update(FeedCollection entity)
        {
            _feedCollections.Update(entity);
        }
    }
}
