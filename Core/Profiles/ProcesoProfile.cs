using AutoMapper;
using Domain.Dto;
using Domain.Entities;

namespace Core.Profiles
{
    public class ProcesoProfile : Profile
    {
        public ProcesoProfile()
        {
            CreateMap<ProcesoRequest, Proceso>()
             .ReverseMap();
        }
    }
}