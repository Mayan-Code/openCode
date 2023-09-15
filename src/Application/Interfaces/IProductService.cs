using Application.Dtos;
using Domain;
using Domain.Entities;
using Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllAsync(ActionContext actionContext, ProductFilters filters);

        Task<ProductDto> GetDetailsAsync(ActionContext actionContext, long id);

        Task<ProductDto> CreateAsync(ActionContext actionContext, CreateProductDto newProduct);

        Task UpdateAsync(ActionContext actionContext, UpdateProductDto updateProductDto);

        Task DeleteAsync(ActionContext actionContext, long id);
    }
}
