using AutoMapper;
using Domain.Dto;
using Domain.Entities;

namespace Core.Profiles
{
    public class VacanteProfile : Profile
    {
        public VacanteProfile()
        {
            CreateMap<VacanteRequest, Vacante>()
               .ReverseMap();

            CreateMap<SkillVacanteRequest, SkillVacante>()
              .ReverseMap();

            CreateMap<Vacante, VacanteResponse>()
                .ForMember(destino => destino.TiempoContrato, actual => actual.MapFrom(src => src.TiempoContrato.Descripcion));

            CreateMap<Vacante, VacanteDetailResponse>()
                .ForMember(destino => destino.DescripcionContrato, actual => actual.MapFrom(src => src.Contrato.Description))
                .ForMember(destino => destino.DescripcionModalidadTrabajo, actual => actual.MapFrom(src => src.ModalidadTrabajo.Description))
                .ForMember(destino => destino.DescripcionEstadoVacante, actual => actual.MapFrom(src => src.EstadoVacante.Description))
                .ForMember(destino => destino.TiempoContrato, actual => actual.MapFrom(src => src.TiempoContrato))
                .ForMember(destino => destino.NombreCliente, actual => actual.MapFrom(src => src.Cliente.Name))
                .ForMember(destino => destino.NombreComercial, actual => actual.MapFrom(src => string.Format("{0} {1}", src.UserCreated.Nombres, src.UserCreated.Apellidos)));
        }
    }
}