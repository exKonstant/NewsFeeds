﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsFeeds.API.Models.Feeds
{
    public class FeedAddModel
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public int FeedCollectionId { get; set; }
    }
}
