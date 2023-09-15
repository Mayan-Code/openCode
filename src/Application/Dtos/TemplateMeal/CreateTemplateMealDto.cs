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

namespace Application.Dtos.TemplateMeal
{
    public class CreateTemplateMealDto : IMap, IEntity
    {
        [Required]
        public string Name { get; set; }
        public List<CreateTemplateMealProductDto> Products { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateTemplateMealDto, Domain.Entities.TemplateMeal>();
        }
    }
}
