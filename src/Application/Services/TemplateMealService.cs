using Application.Dtos;
using Application.GlobalErrorHandlong;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Filters;
using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.TemplateMeal;

namespace Application.Services
{
    public class TemplateMealService : ITemplateMealService
    {
        private readonly ITemplateMealRepository _templateMealRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public TemplateMealService(ITemplateMealRepository templateMealRepository, IUserRepository userRepository, IMapper mapper)
        {
            _templateMealRepository = templateMealRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<TemplateMealDto>> GetAllAsync(ActionContext actionContext, TemplateMealFilters filters)
        {
            var domainModel = await _templateMealRepository.GetAllAsync(actionContext, filters);

            return _mapper.Map<List<TemplateMealDto>>(domainModel);
        }

        public async Task<TemplateMealDto> GetDetailsAsync(ActionContext actionContext, long id)
        {
            var item = await _templateMealRepository.GetByIdAsync(actionContext, id);

            return _mapper.Map<TemplateMealDto>(item);
        }
        public async Task<TemplateMealDto> CreateAsync(ActionContext actionContext, CreateTemplateMealDto newItem)
        {
            if (string.IsNullOrEmpty(newItem.Name))
            {
                throw new Exception("Template Meal can not have an empty name.");
            }

            if (newItem.Products == null || newItem.Products.Any() == false)
            {
                throw new Exception("Template Meal can not have an empty product list.");
            }

            var item = _mapper.Map<TemplateMeal>(newItem);

            var user = await _userRepository.GetByContextAsync(actionContext);
            item.Creator = user;

            await _templateMealRepository.CreateAsync(actionContext, item);

            return _mapper.Map<TemplateMealDto>(item);
        }

        public async Task DeleteAsync(ActionContext actionContext, long id)
        {
            if (id < 0)
            {
                throw new Exception("Template meal can not have an empty id.");
            }

            var existingItem = await _templateMealRepository.GetByIdAsync(actionContext, id);

            var result = await _templateMealRepository.DeleteAsync(actionContext, existingItem);

            if (result == false)
                throw new NotYourObject($"Template Meal id: {id} ");
        }

        public async Task UpdateAsync(ActionContext actionContext, UpdateTemplateMealDto updatedMealDto)
        {
            if (updatedMealDto.Id < 0)
            {
                throw new Exception("Template Meal can not have an empty id.");
            }

            var existingMeal = await _templateMealRepository.GetByIdAsync(actionContext, updatedMealDto.Id);

            var meal = _mapper.Map(updatedMealDto, existingMeal);

            var result = await _templateMealRepository.UpdateAsync(actionContext, meal);

            if (result == false)
                throw new NotYourObject($"Template meal id: {updatedMealDto.Id} ");
        }
    }
}
