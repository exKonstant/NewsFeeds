using System.Collections.Generic;
using System.Threading.Tasks;
using NewsFeeds.BLL.Common;
using NewsFeeds.BLL.DTOs.FeedDTOs;

namespace NewsFeeds.BLL.Services.Feeds
{
    public interface IFeedService
    {
        Task<IEnumerable<FeedDto>> GetFeedsByFeedCollectionAsync(int feedCollectionId, int userId);
        Task<Result> AddAsync(int feedCollectionId, int userId, string feedUrl);
        Task<FeedDto> GetAsync(int id, int feedCollectionId, int userId);
        Task<Result> DeleteAsync(int id, int feedCollectionId, int userId);        
    }
}