using Application.Interfaces;
using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class MealDto : IMap, IEntity
    {
        public long Id { get; set; }    
        public string Name { get; set; }
        public bool IsFavorite { get; set; }
        public List<MealProductDto> Products { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Meal, MealDto>();
        }
    }
}
