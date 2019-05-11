using System;
using System.Collections.Generic;
using System.Text;

namespace NewsFeeds.BLL.DTOs.FeedDTOs
{
    public class FeedDtoForUpdate
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
