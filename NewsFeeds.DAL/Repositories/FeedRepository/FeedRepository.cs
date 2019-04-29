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

        public override async Task<bool> ContainsEntityWithId(int id)
        {
            return await _feeds.AnyAsync(u => u.Id == id);
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

        public override async Task<Feed> GetAsync(int id)
        {
            return await _feeds.FirstOrDefaultAsync(u => u.Id == id);
        }        

        public override void Update(Feed entity)
        {
            _feeds.Update(entity);
        }
    }
}
