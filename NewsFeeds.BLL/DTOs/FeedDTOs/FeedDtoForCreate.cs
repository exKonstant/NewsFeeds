using System;
using System.Collections.Generic;
using System.Text;

namespace NewsFeeds.BLL.DTOs.FeedDTOs
{
    public class FeedDtoForCreate
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public int FeedCollectionId { get; set; }
    }
}
