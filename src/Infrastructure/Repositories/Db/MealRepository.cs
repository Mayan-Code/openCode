using Application.Dtos;
using Application.Interfaces;
using Domain;
using Domain.Entities;
using Domain.Filters;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Db
{
    public class MealRepository : IMealRepository
    {
        private static NLog.ILogger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly WeightContext _weightContext;

        public MealRepository(WeightContext weightContext)
        {
            _weightContext = weightContext;
        }

        public async Task<List<Meal>> GetAllAsync(ActionContext actionContext, MealFilters filters)
        {
            var query = _weightContext.Meals.AsNoTracking()
                .Include(q => q.Products)
                    .ThenInclude(q => q.Product)
             .Where(q => q.IsActive && !q.IsDeleted)
             .Where(q => EF.Functions.Like(q.Creator.Username, actionContext.Username));

            if (filters.Day.HasValue)
                query = query.Where(q => q.CreateDateTime.Date == filters.Day.Value.Date);

            if(filters.OnlyFavorite)
                query = query.Where(q => q.IsFavorite);

            return await query.ToListAsync();
        }

        public async Task<Meal> GetByIdAsync(ActionContext actionContext, long id)
        {
            return await _weightContext.Meals.AsNoTracking()
                .Include(q => q.Products)
                    .ThenInclude(q => q.Product)
                .Where(q => q.IsActive && !q.IsDeleted)
                .Where(q => EF.Functions.Like(q.Creator.Username, actionContext.Username))
                .Where(q => q.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Meal> CreateAsync(ActionContext actionContext, Meal entity)
        {
            _weightContext.Meals.Add(entity);

            await _weightContext.SaveChangesAsync(actionContext);

            return entity;
        }
       
        public async Task<bool> UpdateAsync(ActionContext actionContext, Meal entity)
        {
            var itemRepo = await _weightContext.Meals.AsNoTracking()
                    .Include(q => q.Products)
                .Where(q => q.IsActive && !q.IsDeleted)
                .Where(q => EF.Functions.Like(q.Creator.Username, actionContext.Username))
                .Where(q => q.Id == entity.Id).FirstOrDefaultAsync();

            if (itemRepo != null)
            {
                _weightContext.Meals.Update(entity);

                await _weightContext.SaveChangesAsync(actionContext);
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(ActionContext actionContext, Meal entity)
        {
            var itemRepo = await _weightContext.Meals
               .Where(q => q.IsActive && !q.IsDeleted)
               .Where(q => EF.Functions.Like(q.Creator.Username, actionContext.Username))
               .Where(q => q.Id == entity.Id).FirstOrDefaultAsync();

            if (itemRepo != null)
            {
                itemRepo.IsDeleted = true;

                _weightContext.Meals.Update(itemRepo);

                await _weightContext.SaveChangesAsync(actionContext);

                return true;
            }

            return false;
        }

        public async Task<ProductMeal> AddProductAsync(ActionContext actionContext, ProductMeal productMeal)
        {
            _weightContext.ProductMeals.Add(productMeal);

            await _weightContext.SaveChangesAsync(actionContext);

            return productMeal;
        }
    }
}
