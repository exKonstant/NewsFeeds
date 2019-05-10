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

        public async Task<IEnumerable<FeedDto>> GetAllAsync()
        {            
            var feeds = await _unitOfWork.Feeds.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<FeedDto>>(feeds);
        }

        public async Task<FeedDto> GetAsync(int id)
        {
            Feed feed;
            if (_memoryCache.TryGetValue(id, out feed))
            {
                feed = await _unitOfWork.Feeds.GetAsync(id);
                if (feed != null)
                {
                    _memoryCache.Set(feed.Id, feed,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }           
            return _mapper.Map<FeedDto>(feed);
        }

        public async Task<FeedResponse> AddAsync(FeedDtoForCreate feedDtoForCreate)
        {
            if (string.IsNullOrEmpty(feedDtoForCreate.Title))
            {
                return FeedResponse.InvalidTitle;
            }
            if (string.IsNullOrEmpty(feedDtoForCreate.Link))
            {
                return FeedResponse.InvalidLink;
            }
            if (!await _unitOfWork.FeedCollections.ContainsEntityWithId(feedDtoForCreate.FeedCollectionId))
            {
                return FeedResponse.FeedCollectionNotExist;
            }
            if (await _unitOfWork.Feeds.ContainsEntity(feedDtoForCreate.Title, feedDtoForCreate.FeedCollectionId))
            {
                return FeedResponse.FeedWithTitleAlreadyExists;
            }
            var feed = _mapper.Map<Feed>(feedDtoForCreate);
            await _unitOfWork.Feeds.AddAsync(feed);
            await _unitOfWork.SaveChangesAsync();

            _memoryCache.Set(feed.Id, feed,
                new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });

            return FeedResponse.Ok;
        }

        public async Task<FeedResponse> UpdateAsync(FeedDtoForUpdate feedDtoForUpdate)
        {
            if (!await _unitOfWork.Feeds.ContainsEntityWithId(feedDtoForUpdate.Id))
            {
                return FeedResponse.FeedNotExist;
            }
            if (string.IsNullOrEmpty(feedDtoForUpdate.Title))
            {
                return FeedResponse.InvalidTitle;
            }
            if (string.IsNullOrEmpty(feedDtoForUpdate.Link))
            {
                return FeedResponse.InvalidLink;
            }

            var feed = await _unitOfWork.Feeds.GetAsync(feedDtoForUpdate.Id);
            _mapper.Map(feedDtoForUpdate, feed);
            _unitOfWork.Feeds.Update(feed);
            await _unitOfWork.SaveChangesAsync();

            _memoryCache.Set(feed.Id, feed,
                new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });

            return FeedResponse.Ok;
        }

        public async Task<FeedResponse> DeleteAsync(int id)
        {
            if (!await _unitOfWork.Feeds.ContainsEntityWithId(id))
            {
                return FeedResponse.FeedNotExist;
            }
            _unitOfWork.Feeds.Delete(id);
            await _unitOfWork.SaveChangesAsync();

            _memoryCache.Remove(id);

            return FeedResponse.Ok;
        }
    }
}
