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
        Task<Result> AddAsync(UserDtoForCreate userDtoForCreate);
        Task<Result> UpdateAsync(UserDto userDto);
        Task<Result> DeleteAsync(int id);
    }
}