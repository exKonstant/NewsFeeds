using System.Collections.Generic;
using System.Threading.Tasks;
using NewsFeeds.BLL.DTOs.FeedCollectionDTOs;
using NewsFeeds.BLL.DTOs.UserDTOs;
using NewsFeeds.BLL.Enums;

namespace NewsFeeds.BLL.Services.Users
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetAsync(int id);
        Task<IEnumerable<FeedCollectionDto>> GetFeedCollectionsByUserAsync(int userId);
        Task<UserResponse> AddAsync(UserDtoForCreate userDtoForCreate);
        Task<UserResponse> UpdateAsync(UserDto userDto);
        Task<UserResponse> DeleteAsync(int id);
    }
}