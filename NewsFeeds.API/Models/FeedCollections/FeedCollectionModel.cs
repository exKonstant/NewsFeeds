using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsFeeds.API.Models.Feeds;

namespace NewsFeeds.API.Models.FeedCollections
{
    public class FeedCollectionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public ICollection<FeedModel> Feeds { get; set; }
    }
}
