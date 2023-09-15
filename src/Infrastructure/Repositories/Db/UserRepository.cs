using Domain;
using Domain.Entities;
using Domain.Filters;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Db
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger _logger;

        private readonly WeightContext _weightContext;
        public UserRepository(WeightContext weightContext, ILogger<UserRepository> logger)
        {
            (_weightContext, _logger) = (weightContext, logger);
        }
        public async Task<List<User>> GetAllAsync(ActionContext actionContext, BaseEntityFilter filters)
        {
            return await _weightContext.Users
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<User> GetByIdAsync(ActionContext actionContext, long id)
        {
            return await _weightContext.Users.AsNoTracking().Where(q => q.Id == id).FirstOrDefaultAsync();
        }
        public async Task<User> CreateAsync(ActionContext actionContext, User entity)
        {
            await _weightContext.Users.AddAsync(entity);
            await _weightContext.SaveChangesAsync(actionContext);

            return entity;
        }
        public async Task<bool> UpdateAsync(ActionContext actionContext, User entity)
        {
            var userRepo = await _weightContext.Users.Where(q => q.Id == entity.Id).FirstOrDefaultAsync();

            if (userRepo != null)
            {
                _weightContext.Users.Update(userRepo);

                await _weightContext.SaveChangesAsync(actionContext);
                return true;
            }

            return false;
        }
        public async Task<bool> DeleteAsync(ActionContext actionContext, User entity)
        {
            var userRepo = await _weightContext.Users.Where(q => q.Id == entity.Id).FirstOrDefaultAsync();

            if (userRepo != null)
            {
                userRepo.IsDeleted = true;

                _weightContext.Users.Update(entity);

                await _weightContext.SaveChangesAsync(actionContext);

                return true;
            }

            return false;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var userRepo = await _weightContext.Users
                .Where(q => EF.Functions.Like(q.Username, username) && EF.Functions.Like(q.Password, password))
                .FirstOrDefaultAsync();

            if (userRepo != null)
            {
                return userRepo;
            }

            return null;
        }

        public async Task<User> GetByContextAsync(ActionContext actionContext)
        {
            var userRepo = await _weightContext.Users
               .Where(q => EF.Functions.Like(q.Username, actionContext.Username))
               .FirstOrDefaultAsync();

            return userRepo;
        }
    }
}
