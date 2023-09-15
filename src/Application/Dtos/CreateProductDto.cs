using Application.Interfaces;
using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CreateProductDto : IMap, IEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Protein { get; set; }
        [Required]
        public decimal Carbo { get; set; }
        [Required]
        public decimal Fat { get; set; }
        public string Manufacturer { get; set; }
        public string Barcode { get; set; }
        public ProductCategoryType FoodCategory { get; set; }
        public ProductUnitType FoodUnit { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateProductDto, Product>();
        }
    }
}
