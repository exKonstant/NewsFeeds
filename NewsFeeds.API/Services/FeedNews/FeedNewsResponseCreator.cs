using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewsFeeds.API.Models.FeedNews;
using System.Collections.Generic;

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
