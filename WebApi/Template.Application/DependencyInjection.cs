using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Abstraction;
using Template.Application.Commands;
using Template.Application.DTOs.User;
using Template.Application.Queries;
using Template.Application.Services;
using Template.Application.Utilities;
using Template.Application.Validators;
using Test.Application.Validators;

namespace Template.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
    
            services.AddTransient<Command>();
            services.AddTransient<Query>();
            services.AddTransient<Service>();
            services.AddScoped<ITemplateClient, TemplateClient>();


            return services;
        }
    }
}
