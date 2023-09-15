using AutoMapper;
using Domain.Entities.Enums;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Mappings;

namespace Application.Dtos
{
    public class CreatePlateDto : IMap, IEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Weight { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePlateDto, Plate>();
        }
    }
}
