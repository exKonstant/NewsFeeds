using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsFeeds.BLL.DTOs.FeedCollectionDTOs;
using NewsFeeds.BLL.DTOs.FeedDTOs;
using NewsFeeds.BLL.DTOs.UserDTOs;
using NewsFeeds.BLL.Enums;
using NewsFeeds.BLL.Services.FeedCollections;
using NewsFeeds.DAL.Entities;
using NewsFeeds.DAL.UnitOfWork;

namespace NewsFeeds.BLL.Services.FeedCollections
{
    public class FeedCollectionService : IFeedCollectionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FeedCollectionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FeedCollectionDto>> GetAllAsync()
        {
            var feedCollections = await _unitOfWork.FeedCollections.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<FeedCollectionDto>>(feedCollections);
        }

        public async Task<FeedCollectionDto> GetAsync(int id)
        {
            var feedCollections = await _unitOfWork.FeedCollections.GetAsync(id);
            return _mapper.Map<FeedCollectionDto>(feedCollections);
        }

        public async Task<IEnumerable<FeedDto>> GetFeedsByFeedCollectionAsync(int feedCollectionId)
        {
            var feeds = await _unitOfWork.FeedCollections.GetFeedsByFeedCollection(feedCollectionId).ToListAsync();
            return _mapper.Map<IEnumerable<FeedDto>>(feeds);
        }

        public async Task<FeedCollectionResponse> AddAsync(FeedCollectionDtoForCreate feedCollectionDtoForCreate)
        {
            if (!await _unitOfWork.Users.ContainsEntityWithId(feedCollectionDtoForCreate.UserId))
            {
                return FeedCollectionResponse.UserNotExist;
            }
            if (string.IsNullOrEmpty(feedCollectionDtoForCreate.Name))
            {
                return FeedCollectionResponse.InvalidName;
            }
            if (await _unitOfWork.FeedCollections.ContainsFeedCollectionWithName(feedCollectionDtoForCreate.Name, feedCollectionDtoForCreate.UserId))
            {
                return FeedCollectionResponse.FeedCollectionWithThisNameAlreadyExists;
            }
            var feedCollection = _mapper.Map<FeedCollection>(feedCollectionDtoForCreate);
            await _unitOfWork.FeedCollections.AddAsync(feedCollection);
            await _unitOfWork.SaveChangesAsync();
            return FeedCollectionResponse.Ok;
        }

        public async Task<FeedCollectionResponse> UpdateAsync(FeedCollectionDtoForUpdate feedCollectionDtoForUpdate)
        {
            if (string.IsNullOrEmpty(feedCollectionDtoForUpdate.Name))
            {
                return FeedCollectionResponse.InvalidName;
            }
            if (!await _unitOfWork.FeedCollections.ContainsEntityWithId(feedCollectionDtoForUpdate.Id))
            {
                return FeedCollectionResponse.FeedCollectionNotExist;
            }
            var feedCollection = await _unitOfWork.FeedCollections.GetAsync(feedCollectionDtoForUpdate.Id);
            _mapper.Map(feedCollectionDtoForUpdate, feedCollection);
            _unitOfWork.FeedCollections.Update(feedCollection);
            await _unitOfWork.SaveChangesAsync();
            return FeedCollectionResponse.Ok;
        }

        public async Task<FeedCollectionResponse> DeleteAsync(int id)
        {
            if (!await _unitOfWork.FeedCollections.ContainsEntityWithId(id))
            {
                return FeedCollectionResponse.FeedCollectionNotExist;
            }
            _unitOfWork.FeedCollections.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return FeedCollectionResponse.Ok;
        }
    }
}
