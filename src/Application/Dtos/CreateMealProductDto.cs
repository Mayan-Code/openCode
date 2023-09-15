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
    public class CreateMealProductDto : IMap, IEntity
    {
        [Required]
        public long ProductId { get; set; }
        public long MealId { get; set; }
        public decimal Quantity { get; set; }
        public string Description { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateMealProductDto, ProductMeal>();
        }
    }
}
