using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public FeedService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FeedDto>> GetAllAsync()
        {
            var feeds = await _unitOfWork.Feeds.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<FeedDto>>(feeds);
        }

        public async Task<FeedDto> GetAsync(int id)
        {
            var feed = await _unitOfWork.Feeds.GetAsync(id);
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
            return FeedResponse.Ok;
        }
    }
}
