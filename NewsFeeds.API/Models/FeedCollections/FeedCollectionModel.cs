using NewsFeeds.API.Models.Feeds;
using System.Collections.Generic;

namespace NewsFeeds.API.Models.FeedCollections
{
    public class FeedCollectionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
    }
}
