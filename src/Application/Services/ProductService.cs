using Application.Dtos;
using Application.GlobalErrorHandlong;
using Application.Interfaces;
using AutoMapper;
using Domain;
using Domain.Entities;
using Domain.Filters;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository  productRepository, IUserRepository userRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> GetAllAsync(ActionContext actionContext, ProductFilters filters)
        {
            var domainModel = await _productRepository.GetAllAsync(actionContext, filters);

            return _mapper.Map<List<ProductDto>>(domainModel);
        }

        public async Task<ProductDto> GetDetailsAsync(ActionContext actionContext, long id)
        {
            var product = await _productRepository.GetByIdAsync(actionContext, id);

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateAsync(ActionContext actionContext, CreateProductDto newProduct)
        {
            if (string.IsNullOrEmpty(newProduct.Name))
            {
                throw new Exception("Product can not have an empty name.");
            }

            var product = _mapper.Map<Product>(newProduct);

            var user = await _userRepository.GetByContextAsync(actionContext);
            product.Creator = user;

            await _productRepository.CreateAsync(actionContext, product);

            return _mapper.Map<ProductDto>(product);
        }

        public async Task UpdateAsync(ActionContext actionContext, UpdateProductDto updateProductDto)
        {
            if (updateProductDto.Id < 0)
            {
                throw new Exception("Product can not have an empty id.");
            }

            var existingProduct = await _productRepository.GetByIdAsync(actionContext, updateProductDto.Id);

            var product = _mapper.Map(updateProductDto, existingProduct);
            
            var result = await _productRepository.UpdateAsync(actionContext, product);

            if(result == false)
                throw new NotYourObject($"Product id: {updateProductDto.Id} ");
        }

        public async Task DeleteAsync(ActionContext actionContext, long id)
        {
            if (id < 0)
            {
                throw new Exception("Product can not have an empty id.");
            }

            var existingProduct = await _productRepository.GetByIdAsync(actionContext, id);

            var result = await _productRepository.DeleteAsync(actionContext, existingProduct);

            if (result == false)
                throw new NotYourObject($"Product id: {id} ");
        }
    }
}
