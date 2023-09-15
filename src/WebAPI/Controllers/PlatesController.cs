using Application.Dtos;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using System;
using WebAPI.ActionFilters;
using WebAPI.Infrastructure.Extensions;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PlatesController : Controller
    {
        private readonly IPlateService _plateService;
        private readonly ILogger _logger;
        public PlatesController(IPlateService plateService, ILogger<PlatesController> logger)
        {
            (_plateService, _logger) = (plateService, logger);
        }

        [SwaggerOperation(Summary = "Retrieves all plates")]
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var items = await _plateService.GetAllAsync(this.GetActionContext(), null);

            return Ok(items);
        }

        [SwaggerOperation(Summary = "Retrieves a specific plate by unique id")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var item = await _plateService.GetDetailsAsync(this.GetActionContext(), id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [SwaggerOperation(Summary = "Create new plate")]
        [ServiceFilter(typeof(ValidationModelFilterAttribute))]
        [HttpPost()]
        public async Task<IActionResult> Create(CreatePlateDto createItem)
        {
            var item = await _plateService.CreateAsync(this.GetActionContext(), createItem);

            return Created($"api/plates/{item.Id}", item);
        }

        [SwaggerOperation(Summary = "Update an existing plate")]
        [ServiceFilter(typeof(ValidationModelFilterAttribute))]
        [HttpPut()]
        public async Task<IActionResult> Update(UpdatePlateDto updateItem)
        {
            await _plateService.UpdateAsync(this.GetActionContext(), updateItem);
            return NoContent();
        }

        [SwaggerOperation(Summary = "Delete an existing plate")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _plateService.DeleteAsync(this.GetActionContext(), id);

            return NoContent();
        }
    }
}
