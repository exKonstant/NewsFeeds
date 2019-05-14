using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsFeeds.BLL.Common;
using NewsFeeds.BLL.DTOs.FeedCollectionDTOs;
using NewsFeeds.BLL.DTOs.FeedDTOs;
using NewsFeeds.BLL.DTOs.UserDTOs;
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

        public async Task<IEnumerable<FeedCollectionDto>> GetFeedCollectionByUserAsync(int userId)
        {
            var feedCollections = await _unitOfWork.FeedCollections.GetFeedCollectionsByUser(userId).ToListAsync();
            return _mapper.Map<IEnumerable<FeedCollectionDto>>(feedCollections);
        }

        public async Task<FeedCollectionDto> GetAsync(int id, int userId)
        {
            var feedCollections = await _unitOfWork.FeedCollections.GetAsync(id, userId);
            return _mapper.Map<FeedCollectionDto>(feedCollections);
        }

        public async Task<Result> AddAsync(int userId, FeedCollectionDtoForCreate feedCollectionDtoForCreate)
        {
            if (!await _unitOfWork.Users.ContainsEntityWithId(userId))
            {
                return Result.Fail("User doesn't exist");
            }
            if (string.IsNullOrEmpty(feedCollectionDtoForCreate.Name))
            {
                return Result.Fail("Invalid name");
            }
            if (await _unitOfWork.FeedCollections.ContainsFeedCollectionWithName(feedCollectionDtoForCreate.Name, userId))
            {
                return Result.Fail("Feed collection with this name already exists");
            }
            var feedCollection = _mapper.Map<FeedCollection>(feedCollectionDtoForCreate);
            feedCollection.UserId = userId;

            await _unitOfWork.FeedCollections.AddAsync(feedCollection);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(feedCollection.Id);
        }

        public async Task<Result> UpdateAsync(int userId, FeedCollectionDtoForUpdate feedCollectionDtoForUpdate)
        {
            if (!await _unitOfWork.FeedCollections.ContainsEntityWithId(feedCollectionDtoForUpdate.Id))
            {
                return Result.Fail("Feed collection doesn't exist");
            }
            if (string.IsNullOrEmpty(feedCollectionDtoForUpdate.Name))
            {
                return Result.Fail("Invalid name");
            }
            if (await _unitOfWork.FeedCollections.ContainsFeedCollectionWithName(feedCollectionDtoForUpdate.Name, userId))
            {
                return Result.Fail("Feed collection with this name already exists");
            }

            var feedCollection = await _unitOfWork.FeedCollections.GetAsync(feedCollectionDtoForUpdate.Id, userId);
            _mapper.Map(feedCollectionDtoForUpdate, feedCollection);
            _unitOfWork.FeedCollections.Update(feedCollection);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok("Ok");
        }

        public async Task<Result> DeleteAsync(int id, int userId)
        {
            if (!await _unitOfWork.FeedCollections.ContainsEntityWithId(id))
            {
                return Result.Fail("Feed collection doesn't exist");
            }
            _unitOfWork.FeedCollections.Delete(id, userId);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok("Ok");
        }
    }
}
