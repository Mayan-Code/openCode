using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAsyncRepository<T, F> where T : class where F : class
    {
        Task<List<T>> GetAllAsync(ActionContext actionContext, F filters);
        Task<T> GetByIdAsync(ActionContext actionContext, long id);
        Task<T> CreateAsync(ActionContext actionContext, T entity);
        Task<bool> UpdateAsync(ActionContext actionContext, T entity);
        Task<bool> DeleteAsync(ActionContext actionContext, T entity);
    }
}
