using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //Serwisy w warstwie aplikacji
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPlateService, PlateService>();
            services.AddScoped<IMealService, MealService>();
            services.AddScoped<ITemplateMealService, TemplateMealService>();

            return services;
        }
    }
}
