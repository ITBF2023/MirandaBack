using AutoMapper;
using Domain.Dto;
using Domain.Entities;

namespace Core.Profiles
{
    public class NovedadProfile : Profile
    {
        public NovedadProfile()
        {
            CreateMap<NovedadRequest, Novedad>()
            .ReverseMap();
        }
    }
}