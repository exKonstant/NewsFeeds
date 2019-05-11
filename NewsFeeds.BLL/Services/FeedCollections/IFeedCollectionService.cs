using System.Collections.Generic;
using System.Threading.Tasks;
using NewsFeeds.BLL.DTOs.FeedCollectionDTOs;
using NewsFeeds.BLL.DTOs.FeedDTOs;
using NewsFeeds.BLL.Enums;

namespace NewsFeeds.BLL.Services.FeedCollections
{
    public interface IFeedCollectionService
    {
        Task<IEnumerable<FeedCollectionDto>> GetFeedCollectionByUserAsync(int userId);
        Task<FeedCollectionDto> GetAsync(int id, int userId);
        Task<FeedCollectionResponse> AddAsync(int userId, FeedCollectionDtoForCreate feedCollectionDtoForCreate);
        Task<FeedCollectionResponse> UpdateAsync(int userId, FeedCollectionDtoForUpdate feedCollectionDtoForUpdate);
        Task<FeedCollectionResponse> DeleteAsync(int id, int userId);
    }
}