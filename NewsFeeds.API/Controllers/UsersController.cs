using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsFeeds.API.Models.Users;
using NewsFeeds.API.Services.Users;
using NewsFeeds.BLL.DTOs.UserDTOs;
using NewsFeeds.BLL.Services.Users;

namespace NewsFeeds.API.Controllers
{
    [Route("api/users")]
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

        [Authorize(Roles = "admin")]
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

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UserAddModel userAddModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userDto = _mapper.Map<UserDtoForCreate>(userAddModel);
            var response = await _userService.AddAsync(userDto);
            return _userResponseCreator.ResponseForCreate(response, userDto);
        }
    }
}
