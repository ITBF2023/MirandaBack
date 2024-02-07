﻿using AutoMapper;
using Core.Interfaces;
using DataAccess;
using DataAccess.Interface;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Common;
using Domain.Common.Enum;
using Domain.Dto;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Core.Repository
{
    public class VacantService : IVacantService
    {
        private readonly IRepository<Vacante> vacanteRepository;
        private readonly IRepository<SkillVacante> skillVacanteRepository;
        private readonly IRepository<IdiomaVacante> idiomaVacanteRepository;
        private readonly IMapper mapper;
        private readonly ManejoRHContext manejoRHContext;

        public VacantService(IRepository<Vacante> vacanteRepository, IRepository<SkillVacante> skillVacanteRepository, IRepository<IdiomaVacante> idiomaVacanteRepository, IMapper mapper, ManejoRHContext manejoRHContext)
        {
            this.vacanteRepository = vacanteRepository;
            this.skillVacanteRepository = skillVacanteRepository;
            this.idiomaVacanteRepository = idiomaVacanteRepository;
            this.mapper = mapper;
            this.manejoRHContext = manejoRHContext;
        }

        public async Task<BaseResponse> Create(VacanteRequest vacanteRequest)
        {
            var outPut = new BaseResponse();
            var strategy = manejoRHContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = manejoRHContext.Database.BeginTransaction())
                {
                    try
                    {
                        var idVacante = await InsertVacante(vacanteRequest);
                        await InsertSkillsVacante(vacanteRequest, idVacante);
                        await InsertIdiomasVacante(vacanteRequest, idVacante);
                        await transaction.CommitAsync();
                        outPut = MapperResponse();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        outPut.StatusCode = HttpStatusCode.InternalServerError;
                        outPut.Message = ex.Message;
                    }
                }
            });

            return outPut;
        }

        private async Task<int> InsertVacante(VacanteRequest vacanteRequest)
        {
            var Vacante = mapper.Map<Vacante>(vacanteRequest);
            Vacante.IdEstadoVacante = TipoEstadoVacante.Activo.GetIdTipoEstado();
            Vacante.IdUserCreated = vacanteRequest.IdUser;
            Vacante.DateCreated = DateTime.Now;
            await vacanteRepository.Insert(Vacante);
            return Vacante.IdVacante;
        }

        private async Task InsertSkillsVacante(VacanteRequest vacanteRequest, int idVacante)
        {
            foreach (var vacante in vacanteRequest.ListSkillsVacante)
            {
                var skillVacante = mapper.Map<SkillVacante>(vacante);
                skillVacante.IdVacante = idVacante;
                await skillVacanteRepository.Insert(skillVacante);
            }
        }

        private async Task InsertIdiomasVacante(VacanteRequest vacanteRequest, int idVacante)
        {
            foreach (var item in vacanteRequest.ListaIdiomas)
            {
                var idiomaVacante = mapper.Map<IdiomaVacante>(item);
                idiomaVacante.IdVacante = idVacante;
                await idiomaVacanteRepository.Insert(idiomaVacante);
            }
        }

        private static BaseResponse MapperResponse()
        {
            return new BaseResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Vacante creada con exito"
            };
        }

        public async Task<BaseResponse> Update(VacanteRequest vacanteRequest)
        {
            var outPut = new BaseResponse();
            var strategy = manejoRHContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = manejoRHContext.Database.BeginTransaction())
                {
                    try
                    {
                        await UpdateVacante(vacanteRequest);
                        await DeleteSkillVacante(vacanteRequest);
                        await InsertSkillsVacante(vacanteRequest, vacanteRequest.IdVacante);
                        await transaction.CommitAsync();
                        outPut = MapperUpdateResponse();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        outPut.StatusCode = HttpStatusCode.InternalServerError;
                        outPut.Message = ex.Message;
                    }
                }
            });
            return outPut;
        }

        private async Task UpdateVacante(VacanteRequest vacanteRequest)
        {
            var vacante = await vacanteRepository.GetById(vacanteRequest.IdVacante);
            if (vacante is not null)
            {
                vacante.IdCliente = vacanteRequest.IdCliente;
                vacante.DescripcionCargo = vacanteRequest.DescripcionCargo;
                vacante.Profesion = vacanteRequest.Profesion;
                vacante.TiempoExperiencia = vacanteRequest.TiempoExperiencia;
                vacante.IdContrato = vacanteRequest.IdContrato;
                vacante.Horario = vacanteRequest.Horario;
                vacante.IdModalidadTrabajo = vacanteRequest.IdModalidadTrabajo;
                vacante.PruebaTecnica = vacanteRequest.PruebaTecnica;
                vacante.DescripcionFunciones = vacanteRequest.DescripcionFunciones;
                vacante.IdEstadoVacante = TipoEstadoVacante.Activo.GetIdTipoEstado();
                vacante.Comentarios = vacanteRequest.Comentarios;
                vacante.Comentarios = vacanteRequest.Comentarios;
                vacante.UserIdModified = vacanteRequest.IdUser;
                vacante.DateModified = DateTime.Now;
                await vacanteRepository.Update(vacante);
            }
        }

        private async Task DeleteSkillVacante(VacanteRequest vacanteRequest)
        {
            var listSkillVacantes = await skillVacanteRepository.GetListByParam(x => x.IdVacante == vacanteRequest.IdVacante);
            if (listSkillVacantes is not null || listSkillVacantes?.Count > 0)
            {
                foreach (var item in listSkillVacantes)
                {
                    await skillVacanteRepository.Delete(item);
                }
            }
        }

        private static BaseResponse MapperUpdateResponse()
        {
            return new BaseResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Vacante actualizada con exito"
            };
        }

        public async Task<BaseResponse> UpdateState(VacanteStateRequest vacanteStateRequest)
        {
            var outPut = new BaseResponse();
            try
            {
                var vacante = await vacanteRepository.GetById(vacanteStateRequest.IdVacante);
                if (vacante is not null)
                {
                    vacante.IdEstadoVacante = vacanteStateRequest.IdEstadoVacante;
                    vacante.UserIdModified = vacanteStateRequest.IdUser;
                    vacante.DateModified = DateTime.Now;
                    await vacanteRepository.Update(vacante);
                }
            }
            catch (Exception ex)
            {
                outPut.StatusCode = HttpStatusCode.InternalServerError;
                outPut.Message = ex.Message;
            }

            return outPut;
        }

        public async Task<List<VacanteDetailResponse>> GetAllVacantes()
        {
            List<VacanteDetailResponse> listVacantes;
            try
            {
                var list = await vacanteRepository.GetAllByParamIncluding(null, 
                    (x => x.ModalidadTrabajo), 
                    (x => x.EstadoVacante), 
                    (x => x.Contrato), 
                    (x => x.TiempoContrato),
                    (x => x.Cliente),
                    (x => x.UserCreated));

                listVacantes = mapper.Map<List<VacanteDetailResponse>>(list);

                foreach (var item in listVacantes)
                {
                    var listSkill = await skillVacanteRepository.GetAllByParamIncluding(p => p.IdVacante == item.IdVacante, (i => i.Categoria));

                    item.ListaSkill = mapper.Map<List<SkillVacanteResponse>>(listSkill);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listVacantes;
        }

        public async Task<VacanteResponse> GetById(int idVacante)
        {
            VacanteResponse vacanteResponse;
            try
            {
                var vacante = await vacanteRepository.GetAllByParamIncluding(f => f.IdVacante == idVacante, 
                    (x => x.TiempoContrato), 
                    (x => x.Contrato), 
                    (x => x.ModalidadTrabajo), 
                    (x => x.RangoEdad),
                    (x => x.Cliente));

                var idiomas = await idiomaVacanteRepository.GetAllByParamIncluding(x => x.IdVacante == idVacante, (x => x.Idioma));

                if (vacante is null)
                {
                    vacanteResponse = new VacanteResponse();
                    vacanteResponse.StatusCode = HttpStatusCode.Unauthorized;
                    vacanteResponse.Message = "Vacante no encontrada";
                    return vacanteResponse;
                }

                vacanteResponse = mapper.Map<VacanteResponse>(vacante.First());
                
                if(vacante is not null)
                    vacanteResponse.ListaIdiomas = mapper.Map<List<IdiomaVacanteResponse>>(idiomas);

                vacanteResponse.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception)
            {
                throw;
            }
            return vacanteResponse;
        }

        public async Task<List<SkillVacanteResponse>> GetSkillsByIdVacante(int idVacante)
        {
            List<SkillVacanteResponse> listSkillVacante;

            try
            {
                var skills = await skillVacanteRepository.GetAllByParamIncluding(x => x.IdVacante == idVacante, (i => i.Categoria));
                listSkillVacante = mapper.Map<List<SkillVacanteResponse>>(skills);
            }
            catch (Exception)
            {
                throw;
            }

            return listSkillVacante;
        }
    }
}