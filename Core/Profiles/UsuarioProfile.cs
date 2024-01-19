using AutoMapper;
using Domain.Dto;
using Domain.Entities;

namespace Core.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UserRequest, Usuario>()
                .ForMember(dest => dest.Foto, act => act.MapFrom(src => src.FotoBase64))
                .ForMember(dest => dest.Password, act => act.MapFrom(src => src.Contraseña))
                .ReverseMap();

            CreateMap<UserTokenResponse, Usuario>()
               .ReverseMap();

            CreateMap<UserResponse, Usuario>()
               .ReverseMap();
        }
    }
}