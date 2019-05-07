using System.Collections.Generic;
using System.Threading.Tasks;
using NewsFeeds.BLL.DTOs.FeedDTOs;
using NewsFeeds.BLL.Enums;

namespace NewsFeeds.BLL.Services.Feeds
{
    public interface IFeedService
    {
        Task<IEnumerable<FeedDto>> GetAllAsync();
        Task<FeedDto> GetAsync(int id);
        Task<FeedResponse> AddAsync(FeedDtoForCreate feedDtoForCreate);
        Task<FeedResponse> UpdateAsync(FeedDtoForUpdate feedDtoForUpdate);
        Task<FeedResponse> DeleteAsync(int id);
    }
}