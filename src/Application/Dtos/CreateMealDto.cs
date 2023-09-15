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

namespace Application.Dtos
{
    public class CreateMealDto : IMap, IEntity
    {
        [Required]
        public string Name { get; set; }
        public bool IsFavorite { get; set; }
        public List<CreateMealProductDto> Products { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateMealDto, Meal>();
        }
    }
}
