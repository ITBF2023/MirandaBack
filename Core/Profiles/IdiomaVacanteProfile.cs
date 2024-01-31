﻿using AutoMapper;
using Domain.Dto;
using Domain.Entities;

namespace Core.Profiles
{
    public class IdiomaVacanteProfile : Profile
    {
        public IdiomaVacanteProfile()
        {
            CreateMap<IdiomaVacanteRequest, IdiomaVacante>()
                .ForMember(dest => dest.IdVacante, act => act.MapFrom(src => src.IdVacante))
                .ForMember(dest => dest.IdIdioma, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.Porcentaje, act => act.MapFrom(src => src.Porcentaje))
                .ReverseMap();
        }
    }
}