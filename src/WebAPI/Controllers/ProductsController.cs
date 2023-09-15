using Application.Dtos;
using Application.Interfaces;
using Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.ActionFilters;
using WebAPI.Infrastructure.Extensions;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            (_productService, _logger) = (productService, logger);
        }

        [SwaggerOperation(Summary = "Retrieves all products")]
        [HttpGet("")]
        public async Task<IActionResult> GetAll([FromQuery] ProductFilters filters)
        {
            var products = await _productService.GetAllAsync(this.GetActionContext(), filters);


            return Ok(products);
        }

        [SwaggerOperation(Summary = "Retrieves a specific product by unique id")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var product = await _productService.GetDetailsAsync(this.GetActionContext(), id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [SwaggerOperation(Summary = "Create new product")]
        [ServiceFilter(typeof(ValidationModelFilterAttribute))]
        [HttpPost()]
        public async Task<IActionResult> Create(CreateProductDto createProductDto)
        {
            var product = await _productService.CreateAsync(this.GetActionContext(), createProductDto);

            return Created($"api/products/{product.Id}", product);
        }

        [SwaggerOperation(Summary = "Update an existing product")]
        [ServiceFilter(typeof(ValidationModelFilterAttribute))]
        [HttpPut()]
        public async Task<IActionResult> Update(UpdateProductDto updateProductDto)
        {
            await _productService.UpdateAsync(this.GetActionContext(), updateProductDto);

            return NoContent();
        }

        [SwaggerOperation(Summary = "Delete an existing product")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _productService.DeleteAsync(this.GetActionContext(), id);

            return NoContent();
        }
    }
}
