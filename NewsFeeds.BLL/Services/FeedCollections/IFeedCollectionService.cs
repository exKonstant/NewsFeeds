using System.Collections.Generic;
using System.Threading.Tasks;
using NewsFeeds.BLL.DTOs.FeedCollectionDTOs;
using NewsFeeds.BLL.DTOs.FeedDTOs;
using NewsFeeds.BLL.Enums;

namespace NewsFeeds.BLL.Services.FeedCollections
{
    public interface IFeedCollectionService
    {
        Task<IEnumerable<FeedCollectionDto>> GetAllAsync();
        Task<FeedCollectionDto> GetAsync(int id);
        Task<IEnumerable<FeedDto>> GetFeedsByFeedCollectionAsync(int feedCollectionId);
        Task<FeedCollectionResponse> AddAsync(FeedCollectionDtoForCreate feedCollectionDtoForCreate);
        Task<FeedCollectionResponse> UpdateAsync(FeedCollectionDtoForUpdate feedCollectionDtoForUpdate);
        Task<FeedCollectionResponse> DeleteAsync(int id);
    }
}