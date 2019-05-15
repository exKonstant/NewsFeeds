using NewsFeeds.BLL.Services.Users;
using System;
using NewsFeeds.BLL.Services.FeedCollections;
using NewsFeeds.BLL.Services.FeedNews;
using NewsFeeds.BLL.Services.Feeds;
using NewsFeeds.DAL.UnitOfWork;

namespace TestConsoleApp
{
    class Program
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IFeedCollectionService _feedCollectionService;
        private readonly IFeedService _feedService;
        private readonly IFeedNewsService _feedNewsService;

        static void Main(string[] args)
        {
            _unitOfWork = new UnitOfWork();
            _userService = new UserService();
        }
    }
}
