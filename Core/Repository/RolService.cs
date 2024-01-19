using AutoMapper;
using Core.Common;
using Core.Interfaces;
using DataAccess.Interface;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Common;
using Domain.Common.Enum;
using Domain.Dto;
using Domain.Entities;
using Domain.Entities.StoreProcedure;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Core.Repository
{
    public class RolService : IRolService
    {
        private readonly IRepository<Rol> RolRepository;
        private readonly IMapper mapper;

        public RolService(IRepository<Rol> rolRepository, IMapper mapper)
        {
            this.RolRepository = rolRepository;
            this.mapper = mapper;
        }

        public async Task<List<RolResponse>> GetAll()
        {
            List<RolResponse> list;
            try
            {
                var roles = await RolRepository.GetAll();
                list = mapper.Map<List<RolResponse>>(roles);
                    
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}