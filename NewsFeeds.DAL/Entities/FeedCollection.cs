using System.Collections.Generic;

namespace NewsFeeds.DAL.Entities
{
    public class FeedCollection : Entity
    {
        public string Name { get; set; }
        public ICollection<Feed> Feeds { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
