using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NewsFeeds.BLL.DTOs.FeedCollectionDTOs;
using NewsFeeds.BLL.DTOs.FeedDTOs;
using NewsFeeds.BLL.Enums;
using NewsFeeds.DAL.Entities;
using NewsFeeds.DAL.UnitOfWork;

namespace NewsFeeds.BLL.Services.Feeds
{
    public class FeedService : IFeedService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public FeedService(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache memoryCache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<FeedDto>> GetFeedsByFeedCollectionAsync(int feedCollectionId, int userId)
        {            
            var feeds = await _unitOfWork.Feeds.GetFeedsByFeedCollection(feedCollectionId, userId).ToListAsync();
            return _mapper.Map<IEnumerable<FeedDto>>(feeds);
        }

        public async Task<FeedDto> GetAsync(int id, int feedCollectionId, int userId)
        {
            Feed feed;
            if (!_memoryCache.TryGetValue(id, out feed))
            {
                feed = await _unitOfWork.Feeds.GetAsync(id, feedCollectionId, userId);
                if (feed != null)
                {
                    _memoryCache.Set(feed.Id, feed,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }           
            return _mapper.Map<FeedDto>(feed);
        }

        public async Task<Result> AddAsync(int feedCollectionId, int userId, FeedDtoForCreate feedDtoForCreate)
        {
            if (string.IsNullOrEmpty(feedDtoForCreate.Title))
            {
                return Result.Fail("Invalid title");
            }
            if (await _unitOfWork.Feeds.ContainsEntity(feedDtoForCreate.Title, feedCollectionId))
            {
                return Result.Fail("Feed with title already exists");
            }
            if (!await _unitOfWork.FeedCollections.ContainsEntityWithIds(feedCollectionId, userId))
            {
                return Result.Fail("Feed collection doesn't exist");
            }
            var feed = _mapper.Map<Feed>(feedDtoForCreate);
            feed.FeedCollectionId = feedCollectionId;

            await _unitOfWork.Feeds.AddAsync(feed);
            await _unitOfWork.SaveChangesAsync();

            _memoryCache.Set(feed.Id, feed,
                new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });

            return Result.Ok(feed.Id);
        }

        public async Task<Result> UpdateAsync(int feedCollectionId, int userId, FeedDtoForUpdate feedDtoForUpdate)
        {
            if (!await _unitOfWork.Feeds.ContainsEntityWithId(feedDtoForUpdate.Id))
            {
                return Result.Fail("Feed doesn't exist");
            }
            if (string.IsNullOrEmpty(feedDtoForUpdate.Title))
            {
                return Result.Fail("Invalid title");
            }
            if (await _unitOfWork.Feeds.ContainsEntity(feedDtoForUpdate.Title, feedCollectionId))
            {
                return Result.Fail("Feed with title already exists");
            }

            var feed = await _unitOfWork.Feeds.GetAsync(feedDtoForUpdate.Id, feedCollectionId, userId);
            _mapper.Map(feedDtoForUpdate, feed);
            _unitOfWork.Feeds.Update(feed);
            await _unitOfWork.SaveChangesAsync();

            _memoryCache.Set(feed.Id, feed,
                new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });

            return Result.Ok("Ok");
        }

        public async Task<Result> DeleteAsync(int id, int feedCollectionId, int userId)
        {
            if (!await _unitOfWork.Feeds.ContainsEntityWithId(id))
            {
                return Result.Fail("Feed doesn't exist");
            }

            if (!await _unitOfWork.FeedCollections.ContainsEntityWithIds(feedCollectionId, userId))
            {
                return Result.Fail("Feed collection doesn't exist");
            }
            _unitOfWork.Feeds.Delete(id, feedCollectionId);
            await _unitOfWork.SaveChangesAsync();

            _memoryCache.Remove(id);

            return Result.Ok("Ok");
        }
    }
}
