using Domain.Entities;
using Domain.Interfaces;
using Domain;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Filters;

namespace Infrastructure.Repositories.Db
{
    public class PlateRepository : IPlateRepository
    {
        private static NLog.ILogger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly WeightContext _weightContext;

        public PlateRepository(WeightContext weightContext)
        {
            _weightContext = weightContext;
        }

        public async Task<List<Plate>> GetAllAsync(ActionContext actionContext, BaseEntityFilter filters)
        {
            return await _weightContext.Plates
                .Where(q => q.IsActive && !q.IsDeleted)
                .Where(q => EF.Functions.Like(q.Creator.Username, actionContext.Username))
                .ToListAsync();
        }
        public async Task<Plate> GetByIdAsync(ActionContext actionContext, long id)
        {
            return await _weightContext.Plates
                .Where(q => q.IsActive && !q.IsDeleted)
                .Where(q => EF.Functions.Like(q.Creator.Username, actionContext.Username))
                .Where(q => q.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Plate> CreateAsync(ActionContext actionContext, Plate plate)
        {
            _weightContext.Plates.Add(plate);

            await _weightContext.SaveChangesAsync(actionContext);

            return plate;
        }
        public async Task<bool> UpdateAsync(ActionContext actionContext, Plate plate)
        {
            var itemRepo = await _weightContext.Plates
                .Where(q => q.IsActive && !q.IsDeleted)
                .Where(q => EF.Functions.Like(q.Creator.Username, actionContext.Username))
                .Where(q => q.Id == plate.Id).FirstOrDefaultAsync();

            if (itemRepo != null)
            {
                _weightContext.Plates.Update(itemRepo);

                await _weightContext.SaveChangesAsync(actionContext);
                return true;
            }

            return false;
        }
        public async Task<bool> DeleteAsync(ActionContext actionContext, Plate plate)
        {
            var plateRepo = await _weightContext.Plates
                .Where(q => q.IsActive && !q.IsDeleted)
                .Where(q => EF.Functions.Like(q.Creator.Username, actionContext.Username))
                .Where(q => q.Id == plate.Id).FirstOrDefaultAsync();

            if (plateRepo != null)
            {
                plateRepo.IsDeleted = true;

                _weightContext.Plates.Update(plateRepo);

                await _weightContext.SaveChangesAsync(actionContext);

                return true;
            }

            return false;
        }
    }
}
