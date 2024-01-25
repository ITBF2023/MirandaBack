using AutoMapper;
using Domain.Dto;
using Domain.Entities;

namespace Core.Profiles
{
    public class TiempoContratoProfile : Profile
    {
        public TiempoContratoProfile()
        {
            CreateMap<TiempoContratoResponse, TiempoContrato>()
                .ForMember(dest => dest.IdTiempoContrato, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.Descripcion, act => act.MapFrom(src => src.Descripcion))
                .ReverseMap();
        }
    }
}