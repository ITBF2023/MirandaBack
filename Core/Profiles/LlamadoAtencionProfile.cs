using AutoMapper;
using Domain.Dto;
using Domain.Entities;

namespace Core.Profiles
{
    public class LlamadoAtencionProfile : Profile
    {
        public LlamadoAtencionProfile()
        {
            CreateMap<LlamadoAtencionRequest, LlamadoAtencion>()
                .ForMember(dest => dest.IdUsuarioCreacion, act => act.MapFrom(src => src.IdUsuario));

            CreateMap<LlamadoAtencion, LlamadoAtencionResponse>()
                .ForMember(dest => dest.IdUsuario, act => act.MapFrom(src => src.IdUsuarioCreacion))
                .ForMember(dest => dest.NombreUsuario, act => act.MapFrom(src => string.Format("{0} {1}", src.UsuarioCreacion.Nombres, src.UsuarioCreacion.Apellidos)));
        }
    }
}