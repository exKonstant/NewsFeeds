using System;
using System.Collections.Generic;
using System.Text;

namespace NewsFeeds.DAL.Entities
{
    public class Feed : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public FeedCollection FeedCollection { get; set; }
        public int FeedCollectionId { get; set; }
    }
}
