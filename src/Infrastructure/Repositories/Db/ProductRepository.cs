using Domain;
using Domain.Entities;
using Domain.Filters;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private static NLog.ILogger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly WeightContext _weightContext;

        public ProductRepository(WeightContext weightContext)
        {
            _weightContext = weightContext;
        }

        public async Task<List<Product>> GetAllAsync(ActionContext actionContext, ProductFilters filters)
        {
            var query = _weightContext.Products
                .Where(q => q.IsActive && !q.IsDeleted)
                .Where(q => q.Creator == null || EF.Functions.Like(q.Creator.Username, actionContext.Username));

            if (filters.ProductCategoryType.HasValue)
                query = query.Where(q => q.FoodCategory == filters.ProductCategoryType.Value);

            if(!string.IsNullOrEmpty(filters.Search))
            {
                query = query.Where(q => EF.Functions.Like(q.Name, filters.Search.ToLikeContains())
                );
            }

            if (!string.IsNullOrEmpty(filters.Barcode))
            {
                query = query.Where(q => EF.Functions.Like(q.Barcode, filters.Barcode));
            }

            if (filters.OnlyFavorite)
                query = query.Where(q => q.IsFavorite);

            return await query.ToListAsync();

        }
        public async Task<Product> GetByIdAsync(ActionContext actionContext, long id)
        {
            return await _weightContext.Products
                .Where(q => q.IsActive && !q.IsDeleted)
                .Where(q => q.Creator == null || EF.Functions.Like(q.Creator.Username, actionContext.Username))
                .Where(q => q.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Product> CreateAsync(ActionContext actionContext, Product product)
        {
            _weightContext.Products.Add(product);

            await _weightContext.SaveChangesAsync(actionContext);

            return product;
        }
        public async Task<bool> UpdateAsync(ActionContext actionContext, Product product)
        {
            var productRepo = await _weightContext.Products
                .Where(q => q.IsActive && !q.IsDeleted)
                .Where(q => EF.Functions.Like(q.Creator.Username, actionContext.Username))
                .Where(q => q.Id == product.Id).FirstOrDefaultAsync();

            if (productRepo != null)
            {
                _weightContext.Products.Update(productRepo);

                await _weightContext.SaveChangesAsync(actionContext);
                return true;
            }

            return false;
        }
        public async Task<bool> DeleteAsync(ActionContext actionContext, Product product)
        {
            var productRepo = await _weightContext.Products
                .Where(q => q.IsActive && !q.IsDeleted)
                .Where(q => EF.Functions.Like(q.Creator.Username, actionContext.Username))
                .Where(q => q.Id == product.Id).FirstOrDefaultAsync();

            if (productRepo != null)
            {
                
                productRepo.IsDeleted = true;

                _weightContext.Products.Update(productRepo);

                await _weightContext.SaveChangesAsync(actionContext);

                return true;
            }

            return false;
        }
    }
}
