using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewsFeeds.API.Models.FeedCollections;
using NewsFeeds.API.Models.Users;
using NewsFeeds.API.Services.FeedCollections;
using NewsFeeds.API.Services.Users;
using NewsFeeds.BLL.DTOs.FeedCollectionDTOs;
using NewsFeeds.BLL.DTOs.UserDTOs;
using NewsFeeds.BLL.Services.FeedCollections;
using NewsFeeds.BLL.Services.Users;

namespace NewsFeeds.API.Controllers
{
    [Route("api/feedCollections")]
    public class FeedCollectionsController : Controller
    {
        private readonly IFeedCollectionService _feedCollectionService;
        private readonly IMapper _mapper;
        private readonly IFeedCollectionResponseCreator _feedCollectionResponseCreator;

        public FeedCollectionsController(IFeedCollectionService feedCollectionService, IMapper mapper, IFeedCollectionResponseCreator feedCollectionResponseCreator)
        {
            _feedCollectionService = feedCollectionService;
            _mapper = mapper;
            _feedCollectionResponseCreator = feedCollectionResponseCreator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var feedCollectionDtos = await _feedCollectionService.GetAllAsync();
            return _feedCollectionResponseCreator.ResponseForGetAll(feedCollectionDtos);
        }

        [HttpGet("{id}", Name = "GetFeedCollection")]
        public async Task<IActionResult> Get(int id)
        {
            var feedCollectionDto = await _feedCollectionService.GetAsync(id);
            return _feedCollectionResponseCreator.ResponseForGet(feedCollectionDto);
        }

        [HttpGet("{feedCollectionId}/feeds")] //feedCollections/{feedCollectionId}/feeds
        public async Task<IActionResult> GetFeedsByFeedCollection(int feedCollectionId)
        {
            var feedDtos = await _feedCollectionService.GetFeedsByFeedCollectionAsync(feedCollectionId);
            return _feedCollectionResponseCreator.ResponseForGetFeeds(feedDtos);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] FeedCollectionAddModel feedCollectionAddModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var feedCollectionDto = _mapper.Map<FeedCollectionDtoForCreate>(feedCollectionAddModel);
            var response = await _feedCollectionService.AddAsync(feedCollectionDto);
            return _feedCollectionResponseCreator.ResponseForCreate(response/*, feedCollectionDto*/);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] FeedCollectionUpdateModel feedCollectionUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var feedCollectionDto =
                _mapper.Map<FeedCollectionDtoForUpdate>(feedCollectionUpdateModel);
            feedCollectionDto.Id = id;
            var response = await _feedCollectionService.UpdateAsync(feedCollectionDto);
            return _feedCollectionResponseCreator.ResponseForUpdate(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _feedCollectionService.DeleteAsync(id);
            return _feedCollectionResponseCreator.ResponseForDelete(response);
        }
    }
}
