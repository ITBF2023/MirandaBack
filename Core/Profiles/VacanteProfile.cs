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

            CreateMap<SkillVacante, SkillVacanteResponse>()
                .ForMember(destino => destino.Id, actual => actual.MapFrom(src => src.IdSkillVacante))
                .ForMember(destino => destino.DescripcionCategoria, actual => actual.MapFrom(src => src.Categoria.Description))
                ;

            CreateMap<Vacante, VacanteResponse>()
                .ForMember(destino => destino.TiempoContrato, actual => actual.MapFrom(src => src.TiempoContrato.Descripcion))
                .ForMember(destino => destino.DescripcionContrato, actual => actual.MapFrom(src => src.Contrato.Description))
                .ForMember(destino => destino.DescripcionModalidadTrabajo, actual => actual.MapFrom(src => src.ModalidadTrabajo.Description))
                .ForMember(destino => destino.DescripcionRangoEdad, actual => actual.MapFrom(src => src.RangoEdad.Descripcion))
                .ForMember(destino => destino.LogoCliente, actual => actual.MapFrom(src => src.Cliente.PathLogo))
                .ForMember(destino => destino.NombreCliente, actual => actual.MapFrom(src => src.Cliente.Name))
                .ForMember(destino => destino.IdComercial, actual => actual.MapFrom(src => src.IdUserCreated))
                .ForMember(destino => destino.NombreComercial, actual => actual.MapFrom(src => string.Format("{0} {1}", src.UserCreated.Nombres, src.UserCreated.Apellidos)))
                .ForMember(destino => destino.IdEstado, actual => actual.MapFrom(src => src.IdEstadoVacante))
                .ForMember(destino => destino.DescripcionEstado, actual => actual.MapFrom(src => src.EstadoVacante.Description));

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