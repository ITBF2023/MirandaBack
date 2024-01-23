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
    public class RolUsuarioService : IRolUsuarioService
    {
        private readonly IRepository<RolUsuario> rolUsuarioRepository;
        private readonly IMapper mapper;

        public RolUsuarioService(IRepository<RolUsuario> rolUsuarioRepository, IMapper mapper)
        {
            this.rolUsuarioRepository = rolUsuarioRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rolUsuario"></param>
        /// <returns></returns>
        public bool Insert(RolUsuario rolUsuario)
        {
            try
            {
                var usuarioRol = rolUsuarioRepository.Insert(rolUsuario);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}