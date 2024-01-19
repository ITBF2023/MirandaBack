using AutoMapper;
using Domain.Dto;
using Domain.Entities;

namespace Core.Profiles
{
    public class RolProfile : Profile
    {
        public RolProfile()
        {
            CreateMap<RolResponse, Rol>()
                .ForMember(dest => dest.IdRol, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Descripcion))
                .ReverseMap();
        }
    }
}