using Application.Dtos;
using Application.Dtos.TemplateMeal;
using Application.Interfaces;
using Application.Services;
using Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using WebAPI.ActionFilters;
using WebAPI.Infrastructure.Extensions;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TemplateMealsController : Controller
    {
        private readonly ITemplateMealService _templateMealService;
        private readonly ILogger _logger;

        public TemplateMealsController(ITemplateMealService templateMealService, ILogger<TemplateMealsController> logger)
        {
            (_templateMealService, _logger) = (templateMealService, logger);
        }

        [SwaggerOperation(Summary = "Retrieves all meal's template")]
        [HttpGet("")]
        public async Task<IActionResult> GetAllByDay([FromQuery] TemplateMealFilters filters)
        {
            var items = await _templateMealService.GetAllAsync(this.GetActionContext(), filters);

            return Ok(items);
        }

        [SwaggerOperation(Summary = "Retrieves a specific meal's template by unique id")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var item = await _templateMealService.GetDetailsAsync(this.GetActionContext(), id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [SwaggerOperation(Summary = "Create new meal's template")]
        [ServiceFilter(typeof(ValidationModelFilterAttribute))]
        [HttpPost()]
        public async Task<IActionResult> Create(CreateTemplateMealDto createItem)
        {
            var item = await _templateMealService.CreateAsync(this.GetActionContext(), createItem);

            return Created($"api/meals/{item.Id}", item);
        }

        [SwaggerOperation(Summary = "Update an existing meal's template")]
        [ServiceFilter(typeof(ValidationModelFilterAttribute))]
        [HttpPut()]
        public async Task<IActionResult> Update(UpdateTemplateMealDto updateItem)
        {
            await _templateMealService.UpdateAsync(this.GetActionContext(), updateItem);
            return NoContent();
        }

        [SwaggerOperation(Summary = "Delete an existing meal's template")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _templateMealService.DeleteAsync(this.GetActionContext(), id);

            return NoContent();
        }
    }
}
