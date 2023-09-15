using Application.Interfaces;
using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.TemplateMeal
{
    public class TemplateMealProductDto : IMap, IEntity
    {
        public long ProductId { get; set; }
        public ProductDto Product { get; set; }
        public decimal Quantity { get; set; }
        public string Description { get; set; }
        public long TemplateId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TemplateMealProduct, TemplateMealProductDto>();
            profile.CreateMap<TemplateMealProductDto, TemplateMealProduct>().ForMember(q => q.Product, opt => opt.Ignore());
        }
    }
}
