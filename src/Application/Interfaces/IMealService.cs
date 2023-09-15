using Application.Dtos;
using Domain;
using Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMealService
    {
        Task<List<MealDto>> GetAllByDayAsync(ActionContext actionContext, MealFilters filters);

        Task<MealDto> GetDetailsAsync(ActionContext actionContext, long id);

        Task<MealDto> CreateAsync(ActionContext actionContext, CreateMealDto newMeal);
        Task<MealProductDto> AddProductAsync(ActionContext actionContext, CreateMealProductDto createItem);

        Task UpdateAsync(ActionContext actionContext, UpdateMealDto updateMeal);

        Task DeleteAsync(ActionContext actionContext, long id);
    }
}
