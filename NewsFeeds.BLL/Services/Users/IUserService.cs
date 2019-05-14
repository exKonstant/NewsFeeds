using NewsFeeds.BLL.Common;
using NewsFeeds.BLL.DTOs.UserDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsFeeds.BLL.Services.Users
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetAsync(int id);
        Task<Result> AddAsync(UserDtoForCreate userDtoForCreate);
        Task<Result> DeleteAsync(int id);
    }
}