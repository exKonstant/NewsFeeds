using NewsFeeds.BLL.Common;
using NewsFeeds.BLL.DTOs.FeedCollectionDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsFeeds.BLL.Services.FeedCollections
{
    public interface IFeedCollectionService
    {
        Task<IEnumerable<FeedCollectionDto>> GetFeedCollectionByUserAsync(int userId);
        Task<FeedCollectionDto> GetAsync(int id, int userId);
        Task<Result> AddAsync(int userId, FeedCollectionDtoForCreate feedCollectionDtoForCreate);
        Task<Result> UpdateAsync(int userId, FeedCollectionDtoForUpdate feedCollectionDtoForUpdate);
        Task<Result> DeleteAsync(int id, int userId);
    }
}