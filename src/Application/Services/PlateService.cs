using Application.Dtos;
using Application.GlobalErrorHandlong;
using Application.Interfaces;
using AutoMapper;
using Domain;
using Domain.Entities;
using Domain.Filters;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    internal class PlateService : IPlateService
    {
        private readonly IPlateRepository _plateRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public PlateService(IPlateRepository plateRepository, IUserRepository userRepository, IMapper mapper)
        {
            _plateRepository = plateRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PlateDto> CreateAsync(ActionContext actionContext, CreatePlateDto newItem)
        {
            if (string.IsNullOrEmpty(newItem.Name))
            {
                throw new Exception("Plate can not have an empty name.");
            }

            var item = _mapper.Map<Plate>(newItem);

            var user = await _userRepository.GetByContextAsync(actionContext);
            item.Creator = user;

            await _plateRepository.CreateAsync(actionContext, item);

            return _mapper.Map<PlateDto>(item);
        }

        public async Task DeleteAsync(ActionContext actionContext, long id)
        {
            if (id < 0)
            {
                throw new Exception("Plate can not have an empty id.");
            }

            var existingPlate = await _plateRepository.GetByIdAsync(actionContext, id);

            var result = await _plateRepository.DeleteAsync(actionContext, existingPlate);

            if (result == false)
                throw new NotYourObject($"Plate id: {id} ");
        }

        public async Task<List<PlateDto>> GetAllAsync(ActionContext actionContext, BaseEntityFilter filters)
        {
            var domainModel = await _plateRepository.GetAllAsync(actionContext, filters);

            return _mapper.Map<List<PlateDto>>(domainModel);
        }

        public async Task<PlateDto> GetDetailsAsync(ActionContext actionContext, long id)
        {
            var item = await _plateRepository.GetByIdAsync(actionContext, id);

            return _mapper.Map<PlateDto>(item);
        }

        public async Task UpdateAsync(ActionContext actionContext, UpdatePlateDto updatedPlateDto)
        {
            if (updatedPlateDto.Id < 0)
            {
                throw new Exception("Plate can not have an empty id.");
            }

            var existingPlate = await _plateRepository.GetByIdAsync(actionContext, updatedPlateDto.Id);

            var product = _mapper.Map(updatedPlateDto, existingPlate);

            var result = await _plateRepository.UpdateAsync(actionContext, product);

            if (result == false)
                throw new NotYourObject($"Plate id: {updatedPlateDto.Id} ");
        }
    }
}
