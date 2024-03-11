using AutoMapper;
using Domain.Dto;
using Domain.Entities;

namespace Core.Profiles
{
    public class TipoTablaProfile : Profile
    {
        public TipoTablaProfile()
        {
            CreateMap<CategoriaRequest, Categoria>();

            CreateMap<TipoContratoRequest, TipoContrato>();

            CreateMap<TipoSalarioRequest, TipoSalario>();

            CreateMap<TipoDocumentoContrato, TipoTableResponse>()
                .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Descripcion));
        }
    }
}