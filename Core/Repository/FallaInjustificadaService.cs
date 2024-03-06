using AutoMapper;
using Core.Interfaces;
using DataAccess;
using DataAccess.Interface;
using Domain.Common;
using Domain.Dto;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Core.Repository
{
    public class FallaInjustificadaService : IFallaInjustificadaService
    {
        private readonly IRepository<FallaInjustificada> fallaInjustificadaRepository;

        private readonly IMapper mapper;
        private readonly ManejoRHContext manejoRHContext;

        public FallaInjustificadaService(IMapper mapper, ManejoRHContext manejoRHContext,
            IRepository<FallaInjustificada> fallaInjustificadaRepository
            )
        {
            this.mapper = mapper;
            this.manejoRHContext = manejoRHContext;
            this.fallaInjustificadaRepository = fallaInjustificadaRepository;
        }

        public async Task<BaseResponse> Create(FallaInjustificadaRequest request)
        {
            var outPut = new BaseResponse();
            var strategy = manejoRHContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                try
                {
                    var entity = mapper.Map<FallaInjustificada>(request);
                    entity.FechaCreacion = DateTime.Now;

                    await fallaInjustificadaRepository.Insert(entity);

                    outPut = new BaseResponse()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Falla injustificada creada con exito"
                    };
                }
                catch (Exception ex)
                {
                    outPut.StatusCode = HttpStatusCode.InternalServerError;
                    outPut.Message = ex.Message;
                }
            });

            return outPut;
        }

        public async Task<List<FallaInjustificadaResponse>> GetAll()
        {
            List<FallaInjustificadaResponse> listResponse;
            try
            {
                var list = await fallaInjustificadaRepository.GetAllByParamIncluding(null, (i => i.Empleado), (i => i.UsuarioCreacion));

                listResponse = mapper.Map<List<FallaInjustificadaResponse>>(list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listResponse;
        }

        /// <summary>
        /// Obtener candidatos por identificacion
        /// </summary>
        /// <param name="document">Documento de identificacion</param>
        /// <returns></returns>
        public async Task<List<FallaInjustificadaResponse>> GetByEmpleado(int id)
        {
            var listResponse = new List<FallaInjustificadaResponse>();

            try
            {
                var list = await fallaInjustificadaRepository.GetAllByParamIncluding(x => x.IdEmpleado == id,
                    (i => i.Empleado),
                    (i => i.UsuarioCreacion)
                    );

                listResponse = mapper.Map<List<FallaInjustificadaResponse>>(list);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listResponse;
        }
    }
}