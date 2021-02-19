﻿using Grouper.Api.Data.Context;
using Grouper.Api.Infrastructure.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Grouper.Api.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Grouper.Api.Data.Interfaces;
using Grouper.Api.Data.Repositories;

namespace Grouper.Api.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddData(this IServiceCollection services, 
            IConfiguration configuration,
            ILoggerFactory loggerFactory)
        {
            services.AddDbContext<GrouperDbContext>(options =>
            {
                options.UseLoggerFactory(loggerFactory)
                .EnableSensitiveDataLogging()
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<GrouperDbContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IFormRepository, FormRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IPostRepository, PostRepository>();

            return services;
        }
    }
}
