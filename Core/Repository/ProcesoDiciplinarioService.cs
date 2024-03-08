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
    public class ProcesoDiciplinarioService : IProcesoDiciplinarioService
    {
        private readonly IRepository<ProcesoDiciplinario> fallaInjustificadaRepository;

        private readonly IMapper mapper;
        private readonly ManejoRHContext manejoRHContext;

        public ProcesoDiciplinarioService(IMapper mapper, ManejoRHContext manejoRHContext,
            IRepository<ProcesoDiciplinario> fallaInjustificadaRepository
            )
        {
            this.mapper = mapper;
            this.manejoRHContext = manejoRHContext;
            this.fallaInjustificadaRepository = fallaInjustificadaRepository;
        }

        public async Task<BaseResponse> Create(ProcesoDiciplinarioRequest request)
        {
            var outPut = new BaseResponse();
            var strategy = manejoRHContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                try
                {
                    var entity = mapper.Map<ProcesoDiciplinario>(request);
                    entity.FechaCreacion = DateTime.Now;

                    await fallaInjustificadaRepository.Insert(entity);

                    outPut = new BaseResponse()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Proceso diciplinario creado con exito"
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

        public async Task<List<ProcesoDiciplinarioResponse>> GetAll()
        {
            List<ProcesoDiciplinarioResponse> listResponse;
            try
            {
                var list = await fallaInjustificadaRepository.GetAllByParamIncluding(null, (i => i.Empleado), (i => i.UsuarioCreacion));

                listResponse = mapper.Map<List<ProcesoDiciplinarioResponse>>(list);
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
        public async Task<List<ProcesoDiciplinarioResponse>> GetByEmpleado(int id)
        {
            var listResponse = new List<ProcesoDiciplinarioResponse>();

            try
            {
                var list = await fallaInjustificadaRepository.GetAllByParamIncluding(x => x.IdEmpleado == id,
                    (i => i.Empleado),
                    (i => i.UsuarioCreacion)
                    );

                listResponse = mapper.Map<List<ProcesoDiciplinarioResponse>>(list);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listResponse;
        }
    }
}