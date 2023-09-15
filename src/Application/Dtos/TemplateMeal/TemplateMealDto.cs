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
    public class TemplateMealDto : IMap, IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<TemplateMealProductDto> Products { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.TemplateMeal, TemplateMealDto>();
        }
    }
}
