using AutoMapper;
using Domain.Dto;
using Domain.Entities;

namespace Core.Profiles
{
    public class ProcesoDiciplinarioProfile : Profile
    {
        public ProcesoDiciplinarioProfile()
        {
            CreateMap<ProcesoDiciplinarioRequest, ProcesoDiciplinario>()
                .ForMember(dest => dest.IdUsuarioCreacion, act => act.MapFrom(src => src.IdUsuario));

            CreateMap<ProcesoDiciplinario, ProcesoDiciplinarioResponse>()
                .ForMember(dest => dest.IdUsuario, act => act.MapFrom(src => src.IdUsuarioCreacion))
                .ForMember(dest => dest.NombreUsuario, act => act.MapFrom(src => string.Format("{0} {1}", src.UsuarioCreacion.Nombres, src.UsuarioCreacion.Apellidos)));
        }
    }
}