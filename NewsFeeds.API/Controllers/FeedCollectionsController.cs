﻿using System;
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
    [Route("api/userId/feedCollections")]
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
        public async Task<IActionResult> GetFeedCollectionsByUser(int userId)
        {
            var feedCollectionDtos = await _feedCollectionService.GetFeedCollectionByUserAsync(userId);
            return _feedCollectionResponseCreator.ResponseForGetAll(feedCollectionDtos);
        }

        [HttpGet("{feedCollectionId}", Name = "GetFeedCollection")]
        public async Task<IActionResult> Get(int feedCollectionId, int userId)
        {
            var feedCollectionDto = await _feedCollectionService.GetAsync(feedCollectionId, userId);
            return _feedCollectionResponseCreator.ResponseForGet(feedCollectionDto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int userId, [FromBody] FeedCollectionAddModel feedCollectionAddModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var feedCollectionDto = _mapper.Map<FeedCollectionDtoForCreate>(feedCollectionAddModel);
            var response = await _feedCollectionService.AddAsync(userId, feedCollectionDto);
            return _feedCollectionResponseCreator.ResponseForCreate(response/*, feedCollectionDto*/);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, int userId, [FromBody] FeedCollectionUpdateModel feedCollectionUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var feedCollectionDto =
                _mapper.Map<FeedCollectionDtoForUpdate>(feedCollectionUpdateModel);
            feedCollectionDto.Id = id;
            var response = await _feedCollectionService.UpdateAsync(userId, feedCollectionDto);
            return _feedCollectionResponseCreator.ResponseForUpdate(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id, int userId)
        {
            var response = await _feedCollectionService.DeleteAsync(id, userId);
            return _feedCollectionResponseCreator.ResponseForDelete(response);
        }
    }
}
