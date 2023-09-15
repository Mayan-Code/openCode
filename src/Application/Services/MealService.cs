using Application.Dtos;
using Application.GlobalErrorHandlong;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Filters;

namespace Application.Services
{
    public class MealService : IMealService
    {
        private readonly IMealRepository _mealRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public MealService(IMealRepository mealRepository, IUserRepository userRepository, IMapper mapper)
        {
            _mealRepository = mealRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<List<MealDto>> GetAllByDayAsync(ActionContext actionContext, MealFilters filters)
        {
            var domainModel = await _mealRepository.GetAllAsync(actionContext, filters);

            return _mapper.Map<List<MealDto>>(domainModel);
        }

        public async Task<MealDto> GetDetailsAsync(ActionContext actionContext, long id)
        {
            var item = await _mealRepository.GetByIdAsync(actionContext, id);

            return _mapper.Map<MealDto>(item);
        }
        public async Task<MealDto> CreateAsync(ActionContext actionContext, CreateMealDto newItem)
        {
            if (string.IsNullOrEmpty(newItem.Name))
            {
                throw new Exception("Meal can not have an empty name.");
            }

            if (newItem.Products == null || newItem.Products.Any() == false)
            {
                throw new Exception("Meal can not have an empty product list.");
            }

            var item = _mapper.Map<Meal>(newItem);

            var user = await _userRepository.GetByContextAsync(actionContext);
            item.Creator = user;

            await _mealRepository.CreateAsync(actionContext, item);

            return _mapper.Map<MealDto>(item);
        }

        public async Task DeleteAsync(ActionContext actionContext, long id)
        {
            if (id < 0)
            {
                throw new Exception("Meal can not have an empty id.");
            }

            var existingItem = await _mealRepository.GetByIdAsync(actionContext, id);

            var result = await _mealRepository.DeleteAsync(actionContext, existingItem);

            if (result == false)
                throw new NotYourObject($"Meal id: {id} ");
        }

        public async Task UpdateAsync(ActionContext actionContext, UpdateMealDto updatedMealDto)
        {
            if (updatedMealDto.Id < 0)
            {
                throw new Exception("Meal can not have an empty id.");
            }

            var existingMeal = await _mealRepository.GetByIdAsync(actionContext, updatedMealDto.Id);

            var meal = _mapper.Map(updatedMealDto, existingMeal);

            var result = await _mealRepository.UpdateAsync(actionContext, meal);

            if (result == false)
                throw new NotYourObject($"Meal id: {updatedMealDto.Id} ");
        }

        public async Task<MealProductDto> AddProductAsync(ActionContext actionContext, CreateMealProductDto newItem)
        {
            var item = _mapper.Map<ProductMeal>(newItem);

            var result = await _mealRepository.AddProductAsync(actionContext, item);

            return _mapper.Map<MealProductDto>(result);
        }
    }
}