using Application.Interfaces;
using Application.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.User
{
    public class CreateUserDto : IMap, IEntity
    {
        [MaxLength(32)]
        public string FirstName { get; set; }
        [MaxLength(32)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(32)]
        public string Username { get; set; }
        [Required]
        [MaxLength(64)]
        public string Password { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUserDto, Domain.Entities.User>();
        }
    }
}
