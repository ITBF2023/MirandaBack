using AutoMapper;
using Domain.Dto;
using Domain.Entities;

namespace Core.Profiles
{
    public class RolUsuarioProfile : Profile
    {
        public RolUsuarioProfile()
        {
            CreateMap<RolUsuario, RolResponse>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.IdRol))
                .ForMember(dest => dest.Descripcion, act => act.MapFrom(src => src.Rol.Description));
            //.ReverseMap();

            CreateMap<RolRequest, RolUsuario>()
                .ForMember(dest => dest.IdRol, act => act.MapFrom(src => src.Id));
            //.ReverseMap();
        }
    }
}