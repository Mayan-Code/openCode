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
    public class TemplateMealRepository : ITemplateMealRepository
    {
        private static NLog.ILogger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly WeightContext _weightContext;

        public TemplateMealRepository(WeightContext weightContext)
        {
            _weightContext = weightContext;
        }
        public async Task<List<TemplateMeal>> GetAllAsync(ActionContext actionContext, TemplateMealFilters filters)
        {
            var query = _weightContext.TemplateMeals.AsNoTracking()
               .Include(q => q.Products)
                   .ThenInclude(q => q.Product)
            .Where(q => q.IsActive && !q.IsDeleted)
            .Where(q => EF.Functions.Like(q.Creator.Username, actionContext.Username));

            return await query.ToListAsync();
        }
        public async Task<TemplateMeal> GetByIdAsync(ActionContext actionContext, long id)
        {
            return await _weightContext.TemplateMeals.AsNoTracking()
               .Include(q => q.Products)
                   .ThenInclude(q => q.Product)
               .Where(q => q.IsActive && !q.IsDeleted)
               .Where(q => EF.Functions.Like(q.Creator.Username, actionContext.Username))
               .Where(q => q.Id == id).FirstOrDefaultAsync();
        }

        public async Task<TemplateMeal> CreateAsync(ActionContext actionContext, TemplateMeal entity)
        {
            _weightContext.TemplateMeals.Add(entity);

            await _weightContext.SaveChangesAsync(actionContext);

            return entity;
        }
        public async Task<bool> UpdateAsync(ActionContext actionContext, TemplateMeal entity)
        {
            var itemRepo = await _weightContext.TemplateMeals.AsNoTracking()
                    .Include(q => q.Products)
                .Where(q => q.IsActive && !q.IsDeleted)
                .Where(q => EF.Functions.Like(q.Creator.Username, actionContext.Username))
                .Where(q => q.Id == entity.Id).FirstOrDefaultAsync();

            if (itemRepo != null)
            {
                _weightContext.TemplateMeals.Update(entity);

                await _weightContext.SaveChangesAsync(actionContext);

                return true;
            }

            return false;
        }
        public async Task<bool> DeleteAsync(ActionContext actionContext, TemplateMeal entity)
        {
            var itemRepo = await _weightContext.TemplateMeals
               .Where(q => q.IsActive && !q.IsDeleted)
               .Where(q => EF.Functions.Like(q.Creator.Username, actionContext.Username))
               .Where(q => q.Id == entity.Id).FirstOrDefaultAsync();

            if (itemRepo != null)
            {
                itemRepo.IsDeleted = true;

                _weightContext.TemplateMeals.Update(itemRepo);

                await _weightContext.SaveChangesAsync(actionContext);

                return true;
            }

            return false;
        }
    }
}
