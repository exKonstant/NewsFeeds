using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewsFeeds.API.Models.FeedNews;
using NewsFeeds.API.Models.Feeds;
using NewsFeeds.BLL.DTOs.FeedDTOs;

namespace NewsFeeds.API.Services.FeedNews
{
    public class FeedNewsResponseCreator : IFeedNewsResponseCreator
    {
        private readonly IMapper _mapper;

        public FeedNewsResponseCreator(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult ResponseForGetFeedNews(ICollection<BLL.Common.FeedNewsBusinessModel.FeedNews> feedNews)
        {
            var feedNewsModels = _mapper.Map<ICollection<FeedNewsModel>>(feedNews);
            return new OkObjectResult(feedNewsModels);
        }

    }
}
