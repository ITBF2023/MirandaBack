using AutoMapper;
using Domain.Dto;
using Domain.Entities;
using Domain.Entities.StoreProcedure;

namespace Core.Profiles
{
    public class EmpleadoProfile : Profile
    {
        public EmpleadoProfile()
        {
            CreateMap<Empleado, EmpleadoResponse>()
                .ForMember(dest => dest.NombreEmpleado, act => act.MapFrom(src => string.Format("{0} {1} {2} {3}", src.Candidato.PrimerNombre, src.Candidato.SegundoNombre, src.Candidato.PrimerApellido, src.Candidato.SegundoApellido)))
                .ForMember(dest => dest.Empresa, act => act.MapFrom(src => src.Candidato.Vacante.Cliente.Name));

            CreateMap<SPInfoEmployeeResponse, SPInfoEmployee>()
             .ReverseMap();

            CreateMap<SPHistoricalNoverltyEmployee, SPHistoricalNoverltyEmployeeResponse>()
            .ReverseMap();

            CreateMap<CertificadoEstudiantilEmpleado, CertificadoEstudiantilResponse>()
            .ReverseMap();

            CreateMap<CertificadosEmpleado, CertificadoPersonalResponse>()
           .ReverseMap();

            CreateMap<CertificadosEmpleado, CertificadoLaboralResponse>()
           .ReverseMap();
        }
    }
}