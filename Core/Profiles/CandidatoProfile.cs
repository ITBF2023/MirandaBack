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

            CreateMap<ReferenciasLaboralesCandidatoRequest, ReferenciasLaboralesCandidato>()
            .ReverseMap();

            CreateMap<ReferenciasPersonalesCandidatoRequest, ReferenciasPersonalesCandidato>()
            .ReverseMap();

            CreateMap<Candidato, CandidatoResponse>()
                .ForMember(dest => dest.Nombre, act => act.MapFrom(src => string.Format("{0} {1}", src.UserCreated.Nombres, src.UserCreated.Apellidos)))
                .ForMember(dest => dest.Cliente, act => act.MapFrom(src => src.Vacante.Cliente.Name));

            CreateMap<EstudioCandidatoResponse, EstudioCandidato>()
              .ReverseMap();

            CreateMap<ReferenciasPersonalesResponse, ReferenciasPersonalesCandidato>()
             .ReverseMap();

            CreateMap<ReferenciasLaboralesResponse, ReferenciasLaboralesCandidato>()
            .ReverseMap();
        }
    }
}