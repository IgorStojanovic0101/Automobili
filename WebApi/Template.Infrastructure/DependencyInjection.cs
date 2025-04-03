using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Services;
using Template.Application.Utilities;
using Template.Domain.Repositories;
using Template.Domain.UnitOfWork;
using Template.Infrastructure.EF;
using Template.Infrastructure.Repositories;
using Template.Infrastructure.UoW;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace Template.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext1>();           
           


            services.AddScoped<IAutoRepository, AutoRepository>();

            services.AddMemoryCache();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
       
    }
}
