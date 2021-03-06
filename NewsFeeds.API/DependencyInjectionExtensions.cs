﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewsFeeds.API.Services.FeedCollections;
using NewsFeeds.API.Services.FeedNews;
using NewsFeeds.API.Services.Feeds;
using NewsFeeds.API.Services.Users;
using NewsFeeds.BLL.Services.FeedCollections;
using NewsFeeds.BLL.Services.FeedNews;
using NewsFeeds.BLL.Services.Feeds;
using NewsFeeds.BLL.Services.Users;
using NewsFeeds.DAL.EF;
using NewsFeeds.DAL.UnitOfWork;
using Swashbuckle.AspNetCore.Swagger;

namespace NewsFeeds.API
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection ResolveDalDependencies(this IServiceCollection services, string conString)
        {
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(conString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection ResolveServicesDependencies(this IServiceCollection services)
        {
            services.AddScoped<IFeedService, FeedService>();
            services.AddScoped<IFeedCollectionService, FeedCollectionService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFeedNewsService, FeedNewsService>();

            services.AddScoped<IFeedResponseCreator, FeedResponseCreator>();
            services.AddScoped<IFeedCollectionResponseCreator, FeedCollectionResponseCreator>();
            services.AddScoped<IUserResponseCreator, UserResponseCreator>();
            services.AddScoped<IFeedNewsResponseCreator, FeedNewsResponseCreator>();

            return services;
        }

        public static IServiceCollection RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "NewsFeeds",
                    Version = "v1"
                });
            });

            return services;
        }       
    }
}
