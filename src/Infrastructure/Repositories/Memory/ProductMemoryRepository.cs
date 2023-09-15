using Domain;
using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Filters;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    class ProductMemoryRepository : IProductRepository
    {
        public static List<Product> _repo = new List<Product>
        {
            //new Product(name : "Banan", fat : 1.2M, protein : 1.1M, carbo : 23.8M, foodCategory: ProductCategoryType.VegetableAndFruit, foodUnit: ProductUnitType.Per100) {Id = 0 },
            //new Product(name : "Tuna", fat : 1.2M, protein : 21.5M, carbo : 2.8M, foodCategory: ProductCategoryType.MeatAndFish, foodUnit: ProductUnitType.Per100) {Id = 1 },
        };

        public async Task<List<Product>> GetAllAsync(ActionContext actionContext, ProductFilters filters)
        {
            return _repo.Where(q => q.IsActive && !q.IsDeleted).ToList();
        }
        public async Task<Product> GetByIdAsync(ActionContext actionContext, long id)
        {
            return _repo.Where(q => q.Id == id).FirstOrDefault();
        }
        public async Task<Product> CreateAsync(ActionContext actionContext, Product product)
        {
            product.Id = _repo.Count;

            _repo.Add(product);

            return product;
        }
        public async Task<bool> UpdateAsync(ActionContext actionContext, Product product)
        {
            var productRepo = _repo.Where(q => q.Id == product.Id).FirstOrDefault();

            if (productRepo != null)
            {
                productRepo = product;

                return true;
            }

            return false;
        }
        public async Task<bool> DeleteAsync(ActionContext actionContext, Product product)
        {
            var productRepo = _repo.Where(q => q.Id == product.Id).FirstOrDefault();

            if (productRepo != null)
            {
                productRepo.IsDeleted = true;

                return true;
            }

            return false;
        }
    }
}
