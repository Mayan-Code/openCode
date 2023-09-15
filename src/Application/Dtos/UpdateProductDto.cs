using Application.Interfaces;
using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class UpdateProductDto : IMap, IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ProductCategoryType FoodCategory { get; set; }
        public ProductUnitType FoodUnit { get; set; }
        public decimal Protein { get; set; }
        public decimal Carbo { get; set; }
        public decimal Fat { get; set; }
        public string Manufacturer { get; set; }
        public string Barcode { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateProductDto, Product>();
        }
    }
}
