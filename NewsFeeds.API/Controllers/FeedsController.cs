using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewsFeeds.API.Models.FeedCollections;
using NewsFeeds.API.Models.Feeds;
using NewsFeeds.API.Services.FeedCollections;
using NewsFeeds.API.Services.Feeds;
using NewsFeeds.BLL.DTOs.FeedCollectionDTOs;
using NewsFeeds.BLL.DTOs.FeedDTOs;
using NewsFeeds.BLL.Services.FeedCollections;
using NewsFeeds.BLL.Services.Feeds;

namespace NewsFeeds.API.Controllers
{
    public class FeedsController : Controller
    {
        private readonly IFeedService _feedService;
        private readonly IMapper _mapper;
        private readonly IFeedResponseCreator _feedResponseCreator;

        public FeedsController(IFeedService feedService, IMapper mapper, IFeedResponseCreator feedResponseCreator)
        {
            _feedService = feedService;
            _mapper = mapper;
            _feedResponseCreator = feedResponseCreator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var feedDtos = await _feedService.GetAllAsync();
            return _feedResponseCreator.ResponseForGetAll(feedDtos);
        }

        [HttpGet("{id}", Name = "GetCollection")]
        public async Task<IActionResult> Get(int id)
        {
            var feedDto = await _feedService.GetAsync(id);
            return _feedResponseCreator.ResponseForGet(feedDto);
        }        

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] FeedAddModel feedAddModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var feedDto = _mapper.Map<FeedDtoForCreate>(feedAddModel);
            var response = await _feedService.AddAsync(feedDto);
            return _feedResponseCreator.ResponseForCreate(response/*, feedCollectionDto*/);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] FeedUpdateModel feedUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var feedDto =
                _mapper.Map<FeedDtoForUpdate>(feedUpdateModel);
            feedDto.Id = id;
            var response = await _feedService.UpdateAsync(feedDto);
            return _feedResponseCreator.ResponseForUpdate(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _feedService.DeleteAsync(id);
            return _feedResponseCreator.ResponseForDelete(response);
        }
    }
}
