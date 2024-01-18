using AutoMapper;
using Domain.Dto;
using Domain.Entities.StoreProcedure;

namespace Core.Profiles
{
    public class StoreProcedureProfile : Profile
    {
        public StoreProcedureProfile()
        {
            CreateMap<SPEmployeesByClientResponse, SPEmployeesByClient>()
              .ReverseMap();

            CreateMap<SPProcessByUserResponse, SPProcessByUser>()
             .ReverseMap();
        }
    }
}