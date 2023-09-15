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
    public class UpdateMealDto : IMap, IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<MealProductDto> Products { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateMealDto, Meal>();
        }
    }
}
