using Application.Dtos;
using Application.Interfaces;
using Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;
using WebAPI.ActionFilters;
using WebAPI.Infrastructure.Extensions;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MealsController : Controller
    {
        private readonly IMealService _mealService;
        private readonly ILogger _logger;

        public MealsController(IMealService mealService, ILogger<MealsController> logger)
        {
            (_mealService, _logger) = (mealService, logger);
        }

        [SwaggerOperation(Summary = "Retrieves all meals by day")]
        [HttpGet("")]
        public async Task<IActionResult> GetAllByDay([FromQuery]MealFilters filters)
        {
            var items = await _mealService.GetAllByDayAsync(this.GetActionContext(), filters);

            return Ok(items);
        }

        [SwaggerOperation(Summary = "Retrieves a specific meal by unique id")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var item = await _mealService.GetDetailsAsync(this.GetActionContext(), id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [SwaggerOperation(Summary = "Create new meal")]
        [ServiceFilter(typeof(ValidationModelFilterAttribute))]
        [HttpPost()]
        public async Task<IActionResult> Create(CreateMealDto createItem)
        {
            var item = await _mealService.CreateAsync(this.GetActionContext(), createItem);

            return Created($"api/meals/{item.Id}", item);
        }

        [SwaggerOperation(Summary = "Add new product to meal")]
        [ServiceFilter(typeof(ValidationModelFilterAttribute))]
        [HttpPost("products")]
        public async Task<IActionResult> AddProduct(CreateMealProductDto createItem)
        {
            var item = await _mealService.AddProductAsync(this.GetActionContext(), createItem);

            return Created($"api/meals/products", item);
        }

        [SwaggerOperation(Summary = "Update an existing meal")]
        [ServiceFilter(typeof(ValidationModelFilterAttribute))]
        [HttpPut()]
        public async Task<IActionResult> Update(UpdateMealDto updateItem)
        {
            await _mealService.UpdateAsync(this.GetActionContext(), updateItem);
            return NoContent();
        }

        [SwaggerOperation(Summary = "Delete an existing meal")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _mealService.DeleteAsync(this.GetActionContext(), id);

            return NoContent();
        }

    }
}
