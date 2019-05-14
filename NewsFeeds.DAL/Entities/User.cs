using System.Collections.Generic;

namespace NewsFeeds.DAL.Entities
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<FeedCollection> FeedCollections { get; set; }
    }
}
