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
                .ReverseMap();

            CreateMap<UserTokenResponse, Usuario>()
               .ReverseMap();

            CreateMap<UserResponse, Usuario>()
               .ReverseMap();
        }
    }
}