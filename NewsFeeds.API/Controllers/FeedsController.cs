﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsFeeds.API.Models.FeedCollections;
using NewsFeeds.API.Models.Feeds;
using NewsFeeds.API.Services.FeedCollections;
using NewsFeeds.API.Services.Feeds;
using NewsFeeds.BLL.DTOs.FeedCollectionDTOs;
using NewsFeeds.BLL.DTOs.FeedDTOs;
using NewsFeeds.BLL.MappingProfiles;
using NewsFeeds.BLL.Services.FeedCollections;
using NewsFeeds.BLL.Services.Feeds;

namespace NewsFeeds.API.Controllers
{
    [Authorize(Roles = "user")]
    [Route("api/{userId}/feedCollections/{feedCollectionId}/feeds")]
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
        public async Task<IActionResult> GetFeedsByFeedCollection(int feedCollectionId, int userId)
        {
            var feedDtos = await _feedService.GetFeedsByFeedCollectionAsync(feedCollectionId, userId);
            return _feedResponseCreator.ResponseForGetFeedsByFeedCollection(feedDtos);
        }

        [HttpGet("{id}", Name = "GetFeed")]
        public async Task<IActionResult> Get(int id, int feedCollectionId, int userId)
        {
            var feedDto = await _feedService.GetAsync(id, feedCollectionId, userId);
            return _feedResponseCreator.ResponseForGet(feedDto);
        }        

        [HttpPost]
        public async Task<IActionResult> AddToCollection(int feedCollectionId, int userId, string feedUrl)
        {
            var response = await _feedService.AddAsync(feedCollectionId, userId, feedUrl);
            return _feedResponseCreator.ResponseForCreate(response, feedCollectionId, userId, feedUrl);
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id, int feedCollectionId, int userId)
        {
            var response = await _feedService.DeleteAsync(id, feedCollectionId, userId);
            return _feedResponseCreator.ResponseForDelete(response);
        }
    }
}
