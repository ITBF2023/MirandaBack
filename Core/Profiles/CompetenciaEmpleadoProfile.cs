using AutoMapper;
using Domain.Dto;
using Domain.Entities;

namespace Core.Profiles
{
    public class CompetenciaEmpleadoProfile : Profile
    {
        public CompetenciaEmpleadoProfile()
        {
            CreateMap<CompetenciaLaboralEmpleado, CompetenciaLaboralResponse>()
                .ForMember(dest => dest.IdCompetenciaLaboral, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.UsuarioCreacion, act => act.MapFrom(src => string.Format("{0} {1}", src.UsuarioCreacion.Nombres, src.UsuarioCreacion.Apellidos)))
                .ForMember(dest => dest.UsuarioModificacion, act => act.MapFrom(src => string.Format("{0} {1}", src.UsuarioModificacion.Nombres, src.UsuarioModificacion.Apellidos)))
                ;

            CreateMap<CompetenciaPersonalEmpleado, CompetenciaPersonalResponse>()
                .ForMember(dest => dest.IdCompetenciaPersonal, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.UsuarioCreacion, act => act.MapFrom(src => string.Format("{0} {1}", src.UsuarioCreacion.Nombres, src.UsuarioCreacion.Apellidos)))
                .ForMember(dest => dest.UsuarioModificacion, act => act.MapFrom(src => string.Format("{0} {1}", src.UsuarioModificacion.Nombres, src.UsuarioModificacion.Apellidos)))
                ;
        }
    }
}