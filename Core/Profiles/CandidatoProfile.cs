using AutoMapper;
using Domain.Dto;
using Domain.Entities;

namespace Core.Profiles
{
    public class CandidatoProfile : Profile
    {
        public CandidatoProfile()
        {
            CreateMap<CandidatoRequest, Candidato>()
              .ReverseMap();

            CreateMap<EstudioCandidatoRequest, EstudioCandidato>()
             .ReverseMap();

            CreateMap<ReferenciaLaboralCandidatoRequest, ReferenciaLaboralCandidato>()
            .ReverseMap();

            CreateMap<ReferenciaPersonalCandidatoRequest, ReferenciaPersonalCandidato>()
            .ReverseMap();

            CreateMap<Candidato, CandidatoResponse>()
                .ForMember(dest => dest.Nombre, act => act.MapFrom(src => string.Format("{0} {1}", src.UserCreated.Nombres, src.UserCreated.Apellidos)))
                .ForMember(dest => dest.Cliente, act => act.MapFrom(src => src.Vacante.Cliente.Name))
                .ForMember(dest => dest.DescripcionTipoDocumento, act => act.MapFrom(src => src.TipoDocumento.Description));

            CreateMap<EstudioCandidatoResponse, EstudioCandidato>()
              .ReverseMap();

            CreateMap<ReferenciasPersonalesResponse, ReferenciaPersonalCandidato>()
             .ReverseMap();

            CreateMap<ReferenciasLaboralesResponse, ReferenciaLaboralCandidato>()
            .ReverseMap();
        }
    }
}