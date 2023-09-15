using Application.Dtos.User;
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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository = null;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            (_userRepository, _mapper) = (userRepository, mapper);
        }
        public async Task<List<UserDto>> GetAllAsync(ActionContext actionContext, BaseEntityFilter filters)
        {
            var domainModel = await _userRepository.GetAllAsync(actionContext, filters);

            return _mapper.Map<List<UserDto>>(domainModel);
        }
        public async Task<UserDto> GetDetailsAsync(ActionContext actionContext, long id)
        {
            var product = await _userRepository.GetByIdAsync(actionContext, id);

            return _mapper.Map<UserDto>(product);
        }
        public async Task<UserDto> CreateAsync(ActionContext actionContext, CreateUserDto newDto)
        {
            //if (string.IsNullOrEmpty(newDto.Username))
            //{
            //    throw new Exception("User can not have an empty username.");
            //}

            //if (string.IsNullOrEmpty(newDto.Password))
            //{
            //    throw new Exception("User can not have an empty password.");
            //}

            var user = _mapper.Map<User>(newDto);

            await _userRepository.CreateAsync(actionContext, user);

            return _mapper.Map<UserDto>(user);
        }
        public async Task UpdateAsync(ActionContext actionContext, UpdateUserDto updateDto)
        {
            if (updateDto.Id < 0)
            {
                throw new Exception("User can not have an empty id.");
            }

            var existingUser = await _userRepository.GetByIdAsync(actionContext, updateDto.Id);

            var user = _mapper.Map(updateDto, existingUser);

            await _userRepository.UpdateAsync(actionContext, user);
        }
        public async Task DeleteAsync(ActionContext actionContext, long id)
        {
            if (id < 0)
            {
                throw new Exception("User can not have an empty id.");
            }

            var existingUser = await _userRepository.GetByIdAsync(actionContext, id);

            await _userRepository.DeleteAsync(actionContext, existingUser);
        }

        public async Task<UserDto> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.AuthenticateAsync(username, password);

            return _mapper.Map<UserDto>(user);
        }
    }
}
