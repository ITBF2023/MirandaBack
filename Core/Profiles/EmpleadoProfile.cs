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