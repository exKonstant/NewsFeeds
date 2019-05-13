using System.Collections.Generic;
using System.Threading.Tasks;
using NewsFeeds.BLL.DTOs.FeedDTOs;
using NewsFeeds.BLL.Enums;

namespace NewsFeeds.BLL.Services.Feeds
{
    public interface IFeedService
    {
        Task<IEnumerable<FeedDto>> GetFeedsByFeedCollectionAsync(int feedCollectionId, int userId);
        Task<FeedDto> GetAsync(int id, int feedCollectionId, int userId);
        Task<Result> AddAsync(int feedCollectionId, int userId, FeedDtoForCreate feedDtoForCreate);
        Task<Result> UpdateAsync(int feedCollectionId, int userId, FeedDtoForUpdate feedDtoForUpdate);
        Task<Result> DeleteAsync(int id, int feedCollectionId, int userId);
    }
}