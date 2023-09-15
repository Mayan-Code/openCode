using Application.Interfaces;
using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.User
{
    public class UpdateUserDto : IMap, IEntity
    {
        public int Id { get; set; }
        [MaxLength(32)]
        public string FirstName { get; set; }
        [MaxLength(32)]
        public string LastName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserDto, Domain.Entities.User>();
        }
    }
}
