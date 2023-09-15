using Application.Dtos.User;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
    //[ServiceFilter(typeof(AsyncGlobalFilter))]
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            (_userService, _logger) = (userService, logger);
        }

        [SwaggerOperation(Summary = "Retrieves all users")]
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync(this.GetActionContext(), null);

            return Ok(users);
        }

        [SwaggerOperation(Summary = "Retrieves a specific user by unique id")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var user = await _userService.GetDetailsAsync(this.GetActionContext(), id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [SwaggerOperation(Summary = "Create new user")]
        [ServiceFilter(typeof(ValidationModelFilterAttribute))]
        [HttpPost()]
        public async Task<IActionResult> Create(CreateUserDto newUser)
        {
            var user = await _userService.CreateAsync(this.GetActionContext(), newUser);

            return Created($"api/users/{user.Id}", user);
        }

        [SwaggerOperation(Summary = "Update an existing user")]
        [ServiceFilter(typeof(ValidationModelFilterAttribute))]
        [HttpPut()]
        public async Task<IActionResult> Update(UpdateUserDto updateUser)
        {
            await _userService.UpdateAsync(this.GetActionContext(), updateUser);

            return NoContent();
        }

        [SwaggerOperation(Summary = "Delete an existing user")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _userService.DeleteAsync(this.GetActionContext(), id);

            return NoContent();
        }
    }
}
