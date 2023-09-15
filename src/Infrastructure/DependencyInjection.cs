using Domain.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Db;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPlateRepository, PlateRepository>();
            services.AddScoped<IMealRepository, MealRepository>();
            services.AddScoped<ITemplateMealRepository, TemplateMealRepository>();

            return services;
        }
    }
}
