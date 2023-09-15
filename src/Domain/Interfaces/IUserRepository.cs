using Domain.Entities;
using Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository : IAsyncRepository<User, BaseEntityFilter>
    {
        Task<User> AuthenticateAsync(string username, string password);

        Task<User> GetByContextAsync(ActionContext actionContext);
    }
}
