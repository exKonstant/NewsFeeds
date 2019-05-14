using NewsFeeds.BLL.Common.FeedNewsBusinessModel;
using System.Collections.Generic;

namespace NewsFeeds.BLL.DTOs.FeedDTOs
{
    public class FeedDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public int FeedCollectionId { get; set; }
        public ICollection<FeedNews> FeedNews { get; set; }
    }
}
