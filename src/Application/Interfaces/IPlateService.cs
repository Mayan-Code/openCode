using Application.Dtos;
using Domain;
using Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPlateService
    {
        Task<List<PlateDto>> GetAllAsync(ActionContext actionContext, BaseEntityFilter filters);

        Task<PlateDto> GetDetailsAsync(ActionContext actionContext, long id);

        Task<PlateDto> CreateAsync(ActionContext actionContext, CreatePlateDto newPlate);

        Task UpdateAsync(ActionContext actionContext, UpdatePlateDto updatePlate);

        Task DeleteAsync(ActionContext actionContext, long id);
    }
}
