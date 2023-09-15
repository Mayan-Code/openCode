using Application.Dtos;
using Domain.Filters;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.TemplateMeal;

namespace Application.Interfaces
{
    public interface ITemplateMealService
    {
        Task<List<TemplateMealDto>> GetAllAsync(ActionContext actionContext, TemplateMealFilters filters);

        Task<TemplateMealDto> GetDetailsAsync(ActionContext actionContext, long id);

        Task<TemplateMealDto> CreateAsync(ActionContext actionContext, CreateTemplateMealDto newPlate);

        Task UpdateAsync(ActionContext actionContext, UpdateTemplateMealDto updatePlate);

        Task DeleteAsync(ActionContext actionContext, long id);
    }
}
