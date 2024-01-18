using AutoMapper;
using Domain.Dto;
using Domain.Entities;

namespace Core.Profiles
{
    public class TipoTablaProfile : Profile
    {
        public TipoTablaProfile()
        {
            CreateMap<CategoriaRequest, Categoria>()
                .ReverseMap();
            CreateMap<TipoContratoRequest, TipoContrato>()
               .ReverseMap();
            CreateMap<TipoSalarioRequest, TipoSalario>()
               .ReverseMap();
        }
    }
}