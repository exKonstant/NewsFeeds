using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NewsFeeds.DAL.Entities;

namespace NewsFeeds.DAL.EF
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Feed> Feeds { get; set; }
        public DbSet<FeedCollection> FeedCollections { get; set; } 

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {
            Database.EnsureCreated();
        }
    }
}
