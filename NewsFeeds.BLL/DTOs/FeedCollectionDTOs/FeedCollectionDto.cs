using System;
using System.Collections.Generic;
using System.Text;
using NewsFeeds.BLL.DTOs.FeedDTOs;
using NewsFeeds.DAL.Entities;

namespace NewsFeeds.BLL.DTOs.FeedCollectionDTOs
{
    public class FeedCollectionDto
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public int UserId { get; set; }
        public ICollection<FeedDto> Feeds { get; set; }
    }
}
