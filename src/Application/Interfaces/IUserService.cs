using Application.Dtos.User;
using Domain;
using Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {  
        Task<List<UserDto>> GetAllAsync(ActionContext actionContext, BaseEntityFilter filters);

        Task<UserDto> GetDetailsAsync(ActionContext actionContext, long id);

        Task<UserDto> CreateAsync(ActionContext actionContext, CreateUserDto newDto);

        Task UpdateAsync(ActionContext actionContext, UpdateUserDto updateDto);

        Task DeleteAsync(ActionContext actionContext, long id);

        Task<UserDto> AuthenticateAsync(string username, string password);
    }
}
