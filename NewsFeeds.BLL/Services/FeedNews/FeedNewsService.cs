using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NewsFeeds.BLL.Common;
using NewsFeeds.BLL.DTOs.FeedDTOs;
using NewsFeeds.BLL.Common.FeedNewsBusinessModel;
using NewsFeeds.DAL.Entities;
using NewsFeeds.DAL.UnitOfWork;

namespace NewsFeeds.BLL.Services.FeedNews
{
    public class FeedNewsService : IFeedNewsService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _memoryCache;

        public FeedNewsService(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache memoryCache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public async Task<ICollection<Common.FeedNewsBusinessModel.FeedNews>> GetFeedNewsByFeed(int feedId, int feedCollectionId, int userId)
        {
            Feed feed;
            if (!_memoryCache.TryGetValue(feedId, out feed))
            {
                feed = await _unitOfWork.Feeds.GetAsync(feedId, feedCollectionId, userId);
                if (feed != null)
                {
                    _memoryCache.Set(feed.Id, feed,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
                else
                {
                    return new List<Common.FeedNewsBusinessModel.FeedNews>();
                }
            }

            var feedDto = _mapper.Map<FeedDto>(feed);
            return GetFeedNews(GetXmlFeed(feedDto.Link));
        }

        public async Task<ICollection<Common.FeedNewsBusinessModel.FeedNews>> GetFeedNewsByFeedCollection(int feedCollectionId, int userId)
        {
            var feeds = await _unitOfWork.Feeds.GetFeedsByFeedCollection(feedCollectionId, userId).ToListAsync();
            if (feeds == null)
            {
                return new List<Common.FeedNewsBusinessModel.FeedNews>(); ;
            }
            var feedDtos = _mapper.Map<IEnumerable<FeedDto>>(feeds);

            var feedNews = new List<Common.FeedNewsBusinessModel.FeedNews>();
            foreach (var item in feedDtos)
            {
                feedNews.AddRange(GetFeedNews(GetXmlFeed(item.Link)));
            }
            return feedNews;
        }

        public XElement GetXmlFeed(string feedUrl)
        {
            if (feedUrl == null) throw new ArgumentNullException(nameof(feedUrl));
            var webClient = new WebClient();
            var data = webClient.DownloadString(feedUrl);
            return XDocument.Parse(data).Descendants("channel").First();
        }

        public ICollection<Common.FeedNewsBusinessModel.FeedNews> GetFeedNews(XElement document)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));
            return document.Descendants("item")
                .Select(
                    news => new Common.FeedNewsBusinessModel.FeedNews
                    {
                        Title = (string)news.Element("title"),
                        Link = (string)news.Element("link"),
                        Description = (string)news.Element("description"),                        
                    }).ToList();
        }
    }
}
