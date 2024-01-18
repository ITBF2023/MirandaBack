using AutoMapper;
using Domain.Dto;
using Domain.Entities;

namespace Core.Profiles
{
    public class ComisionProfile : Profile
    {
        public ComisionProfile()
        {
            CreateMap<ComisionRequest, Comision>()
              .ReverseMap();

            CreateMap<ComisionResponse, Comision>()
             .ReverseMap();
        }
    }
}