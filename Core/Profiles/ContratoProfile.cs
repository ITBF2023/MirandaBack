using AutoMapper;
using Domain.Dto;
using Domain.Entities;

namespace Core.Profiles
{
    public class ContratoProfile : Profile
    {
        public ContratoProfile()
        {
            CreateMap<ContratoRequest, Contrato>()
             .ReverseMap();

            CreateMap<ContratoSingleResponse, Contrato>()
             .ReverseMap();

            CreateMap<ContratoResponse, Contrato>()
            .ReverseMap();
        }
    }
}