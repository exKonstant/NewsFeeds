using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewsFeeds.API.Models.Users;
using NewsFeeds.API.Services.Users;
using NewsFeeds.BLL.DTOs.UserDTOs;
using NewsFeeds.BLL.Services.Users;

namespace NewsFeeds.API.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUserResponseCreator _userResponseCreator;

        public UsersController(IUserService userService, IMapper mapper, IUserResponseCreator userResponseCreator)
        {
            _userService = userService;
            _mapper = mapper;
            _userResponseCreator = userResponseCreator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userDtos = await _userService.GetAllAsync();
            return _userResponseCreator.ResponseForGetAll(userDtos);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> Get(int id)
        {
            var userDto = await _userService.GetAsync(id);
            return _userResponseCreator.ResponseForGet(userDto);
        }

        [HttpGet("{userId}/feedCollections")]
        public async Task<IActionResult> GetFeedCollectionsByUser(int userId)
        {
            var feedCollectionDtos = await _userService.GetFeedCollectionsByUserAsync(userId);
            return _userResponseCreator.ResponseForGetFeedCollections(feedCollectionDtos);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UserAddOrUpdateModel userAddOrUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userDto = _mapper.Map<UserDtoForCreate>(userAddOrUpdateModel);
            var response = await _userService.AddAsync(userDto);
            return _userResponseCreator.ResponseForCreate(response/*, userDto*/);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] UserAddOrUpdateModel userAddOrUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userDto =
                _mapper.Map<UserDto>(userAddOrUpdateModel);
            userDto.Id = id;
            var response = await _userService.UpdateAsync(userDto);
            return _userResponseCreator.ResponseForUpdate(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _userService.DeleteAsync(id);
            return _userResponseCreator.ResponseForDelete(response);
        }
    }
}
