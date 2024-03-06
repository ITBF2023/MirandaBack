using AutoMapper;
using Core.Common;
using Core.Interfaces;
using DataAccess;
using DataAccess.Interface;
using DocumentFormat.OpenXml.Drawing.Charts;
using Domain.Common;
using Domain.Common.Enum;
using Domain.Dto;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Core.Repository
{
    public class CandidatoService : ICandidatoService
    {
        private readonly IRepository<Candidato> candidatoRepository;
        private readonly IRepository<EstudioCandidato> estudioCandidatoRepository;
        private readonly IRepository<ReferenciaLaboralCandidato> referenciaLaboralCandidatoRepository;
        private readonly IRepository<ReferenciaPersonalCandidato> referenciaPersonalCandidatoRepository;
        private readonly IRepository<Configuracion> configuiuracionRepository;
        private readonly IRepository<EstadoCandidato> estadoCandidatoRepository;
        private readonly IRepository<Empleado> empleadoRepository;

        private readonly IMapper mapper;
        private readonly ManejoRHContext manejoRHContext;

        public CandidatoService(IRepository<Candidato> candidatoRepository, IRepository<EstudioCandidato> estudioCandidatoRepository, IRepository<ReferenciaLaboralCandidato> referenciaLaboralCandidatoRepository
            , IRepository<ReferenciaPersonalCandidato> referenciasPersonalesCandidatoRepository, IMapper mapper, ManejoRHContext manejoRHContext, IRepository<Configuracion> configuiuracionRepository,
            IRepository<EstadoCandidato> estadoCandidatoRepository, IRepository<Empleado> empleadoRepository)
        {
            this.candidatoRepository = candidatoRepository;
            this.estudioCandidatoRepository = estudioCandidatoRepository;
            this.referenciaLaboralCandidatoRepository = referenciaLaboralCandidatoRepository;
            this.referenciaPersonalCandidatoRepository = referenciasPersonalesCandidatoRepository;
            this.mapper = mapper;
            this.manejoRHContext = manejoRHContext;
            this.configuiuracionRepository = configuiuracionRepository;
            this.estadoCandidatoRepository = estadoCandidatoRepository;
            this.empleadoRepository = empleadoRepository;
        }

        public async Task<BaseResponse> Create(CandidatoRequest candidatoRequest)
        {
            var outPut = new BaseResponse();
            var strategy = manejoRHContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = manejoRHContext.Database.BeginTransaction())
                {
                    try
                    {
                        var validationResult = await ValidateCreationCandidato(candidatoRequest.Documento);
                        if (validationResult)
                        {
                            var idCandidato = await InsertCandidato(candidatoRequest);
                            await InsertEstudios(candidatoRequest.ListEstudio, idCandidato);
                            await InsertReferenciaLaboral(candidatoRequest.ListReferenciaLaboral, idCandidato);
                            await InsertReferenciaPersonal(candidatoRequest.ListReferenciaPersonal, idCandidato);
                            await transaction.CommitAsync();
                            outPut = MapperResponse();
                        }
                        else
                        {
                            outPut = MapperResponseFail();
                        }
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

        private async Task<bool> ValidateCreationCandidato(string documento)
        {
            var candidato = await candidatoRepository.GetByParam(x => x.Documento.Trim() == documento.Trim());
            return candidato == null;
        }

        private async Task<int> InsertCandidato(CandidatoRequest candidatoRequest)
        {
            var candidato = mapper.Map<Candidato>(candidatoRequest);
            candidato.IdEstado = TipoEstadoCandidato.EnviadoComercial.GetIdEstadoCandidato();
            candidato.IdUserCreated = candidatoRequest.IdUser;
            candidato.DateCreated = DateTime.Now;
            string nameFile = string.Concat("CV", candidatoRequest.Documento, candidatoRequest.PrimerApellido);
            candidato.UrlCV = string.IsNullOrEmpty(candidatoRequest.Base64CV) ? null : await GetPathDocsPdf(candidatoRequest.Base64CV, nameFile);
            candidato.Activo = true;

            await candidatoRepository.Insert(candidato);

            return candidato.IdCandidato;
        }

        private async Task<string> GetPathDocsPdf(string base64File, string clientName)
        {
            var saveFile = new SaveFiles();
            var pathLogos = (await configuiuracionRepository.GetByParam(x => x.Id.Equals(ParamConfig.PathDocsCandidatos.ToString())))?.Value ?? string.Empty;

            var objectFileSave = new ObjectFileSave();
            objectFileSave.FilePath = pathLogos;

            if (base64File.Contains(","))
            {
                string[] data = base64File.Split(',');
                objectFileSave.Base64String = data[1];
            }
            else
            {
                objectFileSave.Base64String = base64File;
            }

            //objectFileSave.Base64String = base64File;
            objectFileSave.FileName = $"{clientName}.pdf";
            var pathFile = saveFile.SaveFileBase64(objectFileSave);
            return objectFileSave.FileName;
        }

        private async Task InsertEstudios(List<EstudioCandidatoRequest>? ListEstudioCandidatoRequest, int idCandidato)
        {
            if (ListEstudioCandidatoRequest is not null)
            {
                foreach (var item in ListEstudioCandidatoRequest)
                {
                    var estudios = mapper.Map<EstudioCandidato>(item);
                    estudios.IdCandidato = idCandidato;
                    estudios.Activo = true;
                    await estudioCandidatoRepository.Insert(estudios);
                }
            }
        }

        private async Task InsertReferenciaLaboral(List<ReferenciaLaboralCandidatoRequest>? ListReferenciasLaboralesCandidatoRequest, int idCandidato)
        {
            if (ListReferenciasLaboralesCandidatoRequest is not null)
            {
                foreach (var item in ListReferenciasLaboralesCandidatoRequest)
                {
                    var referenciaLaboral = mapper.Map<ReferenciaLaboralCandidato>(item);
                    referenciaLaboral.IdCandidato = idCandidato;
                    await referenciaLaboralCandidatoRepository.Insert(referenciaLaboral);
                }
            }
        }

        private async Task InsertReferenciaPersonal(List<ReferenciaPersonalCandidatoRequest>? ListReferenciasLaboralesCandidatoRequest, int idCandidato)
        {
            if (ListReferenciasLaboralesCandidatoRequest is not null)
            {
                foreach (var item in ListReferenciasLaboralesCandidatoRequest)
                {
                    var referenciaPersonal = mapper.Map<ReferenciaPersonalCandidato>(item);
                    referenciaPersonal.IdCandidato = idCandidato;
                    await referenciaPersonalCandidatoRepository.Insert(referenciaPersonal);
                }
            }
        }

        private static BaseResponse MapperResponse()
        {
            return new BaseResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Candidato creado con exito"
            };
        }

        private static BaseResponse MapperResponseFail()
        {
            return new BaseResponse()
            {
                StatusCode = HttpStatusCode.Conflict,
                Message = "El Candidato ya fue creado con el documento digitado"
            };
        }

        public async Task<BaseResponse> Update(CandidatoRequest candidatoRequest)
        {
            var outPut = new BaseResponse();
            var strategy = manejoRHContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = manejoRHContext.Database.BeginTransaction())
                {
                    try
                    {
                        var updateResult = await UpdateCandidato(candidatoRequest);
                        if (updateResult)
                        {
                            await DeleteEstudios(candidatoRequest.IdCandidato);
                            await InsertEstudios(candidatoRequest.ListEstudio, candidatoRequest.IdCandidato);
                            await DeleteReferenciaLaboral(candidatoRequest.IdCandidato);
                            await InsertReferenciaLaboral(candidatoRequest.ListReferenciaLaboral, candidatoRequest.IdCandidato);
                            await DeleteReferenciaPersonal(candidatoRequest.IdCandidato);
                            await InsertReferenciaPersonal(candidatoRequest.ListReferenciaPersonal, candidatoRequest.IdCandidato);
                            await transaction.CommitAsync();
                            outPut = MapperResponseUpdate();
                        }
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

        private async Task<bool> UpdateCandidato(CandidatoRequest candidatoRequest)
        {
            var candidato = await candidatoRepository.GetById(candidatoRequest.IdCandidato);
            if (candidato is not null)
            {
                candidato.IdTipoDocumento = candidatoRequest.IdTipoDocumento;
                candidato.Documento = candidatoRequest.Documento;
                candidato.PrimerNombre = candidatoRequest.PrimerNombre;
                candidato.SegundoNombre = candidatoRequest.SegundoNombre;
                candidato.PrimerApellido = candidatoRequest.PrimerApellido;
                candidato.SegundoApellido = candidatoRequest.SegundoApellido;
                candidato.NumeroTelefonico = candidatoRequest.NumeroTelefonico;
                candidato.Correo = candidatoRequest.Correo;
                string nameFile = candidatoRequest.Documento + candidatoRequest.PrimerApellido;
                candidato.UrlCV = string.IsNullOrEmpty(candidatoRequest.Base64CV) ? candidato.UrlCV : await GetPathDocsPdf(candidatoRequest.Base64CV, nameFile);
                candidato.IdTipoSalario = candidatoRequest.IdTipoSalario;
                candidato.IdVacante = candidato.IdVacante;
                candidato.Comentarios = candidatoRequest.Comentarios;
                candidato.FechaNacimiento = candidatoRequest.FechaNacimiento;
                candidato.UserIdModified = candidatoRequest.IdUser;
                candidato.DateModified = DateTime.Now;
                candidato.IdEstado = candidatoRequest.IdEstado;
                candidato.JustificacionEstado = candidatoRequest.JustificacionEstado;
                candidato.ValorRecurso = candidatoRequest.ValorRecurso;

                await candidatoRepository.Update(candidato);

                bool estadoContratado = estadoCandidatoRepository.GetById(candidatoRequest.IdEstado).Result.Description == "Contratado";
                bool empleadoExistente = empleadoRepository.GetByParam(p => p.IdCandidato == candidatoRequest.IdCandidato).Result != null;

                if (estadoContratado && !empleadoExistente)
                {
                    empleadoRepository.Insert(new Empleado
                    {
                        IdCandidato = candidatoRequest.IdCandidato,
                        Activo = true
                    });
                }

                return true;
            }

            return false;
        }

        private async Task DeleteEstudios(int idVacante)
        {
            var listEstudiosCandidato = await estudioCandidatoRepository.GetListByParam(x => x.IdCandidato == idVacante);
            if (listEstudiosCandidato is not null || listEstudiosCandidato?.Count > 0)
            {
                foreach (var item in listEstudiosCandidato)
                {
                    item.Activo = false;
                    await estudioCandidatoRepository.Update(item);
                }
            }
        }

        private async Task DeleteReferenciaLaboral(int idVacante)
        {
            var listReferencias = await referenciaLaboralCandidatoRepository.GetListByParam(x => x.IdCandidato == idVacante);
            if (listReferencias is not null || listReferencias?.Count > 0)
            {
                foreach (var item in listReferencias)
                {
                    await referenciaLaboralCandidatoRepository.Delete(item);
                }
            }
        }

        private async Task DeleteReferenciaPersonal(int idVacante)
        {
            var listReferencias = await referenciaPersonalCandidatoRepository.GetListByParam(x => x.IdCandidato == idVacante);
            if (listReferencias is not null || listReferencias.Count > 0)
            {
                foreach (var item in listReferencias)
                {
                    await referenciaPersonalCandidatoRepository.Delete(item);
                }
            }
        }

        private static BaseResponse MapperResponseUpdate()
        {
            return new BaseResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Datos actualizados con exito"
            };
        }

        public async Task<BaseResponse> UpdateActiveCandidato(CandidatoActiveRequest candidatoActiveRequest)
        {
            var outPut = new BaseResponse();
            try
            {
                var candidato = await candidatoRepository.GetById(candidatoActiveRequest.IdCandidato);
                if (candidato is not null)
                {
                    candidato.Activo = candidatoActiveRequest.Activo;
                    candidato.UserIdModified = candidatoActiveRequest.IdUser;
                    candidato.DateModified = DateTime.Now;
                    await candidatoRepository.Update(candidato);
                    outPut = MapperResponseUpdate();
                }
                else
                {
                    outPut = MapperResponseUpdateFailed();
                }
            }
            catch (Exception ex)
            {
                outPut.StatusCode = HttpStatusCode.InternalServerError;
                outPut.Message = ex.Message;
            }
            return outPut;
        }

        private static BaseResponse MapperResponseUpdateFailed()
        {
            return new BaseResponse()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "El id del candidato no existe"
            };
        }

        public async Task<BaseResponse> UpdateStateCandidato(CandidatoStateRequest candidatoStateRequest)
        {
            var outPut = new BaseResponse();
            try
            {
                var candidato = await candidatoRepository.GetById(candidatoStateRequest.IdCandidato);
                if (candidato is not null)
                {
                    candidato.IdEstado = candidatoStateRequest.IdEstadoCandidato;
                    candidato.Comentarios = candidatoStateRequest.Comentarios;
                    candidato.UserIdModified = candidatoStateRequest.IdUser;
                    candidato.DateModified = DateTime.Now;
                    await candidatoRepository.Update(candidato);
                    outPut = MapperResponseUpdate();
                }
                else
                {
                    outPut = MapperResponseUpdateFailed();
                }
            }
            catch (Exception ex)
            {
                outPut.StatusCode = HttpStatusCode.InternalServerError;
                outPut.Message = ex.Message;
            }
            return outPut;
        }

        public async Task<BaseResponse> UpdateVerifyRefLaborales(List<ReferenciasLaboralesVerifyRequest> referenciasLaboralesVerifyRequests)
        {
            var outPut = new BaseResponse();
            try
            {
                if (referenciasLaboralesVerifyRequests is not null || referenciasLaboralesVerifyRequests?.Count > 0)
                {
                    await UpdateVerifyRefLaboral(referenciasLaboralesVerifyRequests);
                    outPut = MapperResponseUpdateRef(TipoReferencia.laborales.ToString());
                }
            }
            catch (Exception ex)
            {
                outPut.StatusCode = HttpStatusCode.InternalServerError;
                outPut.Message = ex.Message;
            }
            return outPut;
        }

        private async Task UpdateVerifyRefLaboral(List<ReferenciasLaboralesVerifyRequest> referenciasLaboralesVerifyRequests)
        {
            foreach (var item in referenciasLaboralesVerifyRequests)
            {
                var refLaboral = await referenciaLaboralCandidatoRepository.GetById(item.IdReferenciasLaboralesCandidato);
                if (refLaboral is not null)
                {
                    refLaboral.Verificado = item.Verificado;
                    await referenciaLaboralCandidatoRepository.Update(refLaboral);
                }
            }
        }

        private static BaseResponse MapperResponseUpdateRef(string tipoRef)
        {
            return new BaseResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Message = $"Referencias {tipoRef} actualizadas con exito"
            };
        }

        public async Task<BaseResponse> UpdateVerifyRefPersonales(List<ReferenciasPersonalesVerifyRequest> referenciasPersonalesVerifyRequests)
        {
            var outPut = new BaseResponse();
            try
            {
                if (referenciasPersonalesVerifyRequests is not null || referenciasPersonalesVerifyRequests?.Count > 0)
                {
                    await UpdateVerifyRefPersonal(referenciasPersonalesVerifyRequests);
                    outPut = MapperResponseUpdateRef(TipoReferencia.personales.ToString());
                }
            }
            catch (Exception ex)
            {
                outPut.StatusCode = HttpStatusCode.InternalServerError;
                outPut.Message = ex.Message;
            }
            return outPut;
        }

        private async Task UpdateVerifyRefPersonal(List<ReferenciasPersonalesVerifyRequest> referenciasPersonalesVerifyRequests)
        {
            foreach (var item in referenciasPersonalesVerifyRequests)
            {
                var refLaboral = await referenciaPersonalCandidatoRepository.GetById(item.IdReferenciasPersonalesCandidato);
                if (refLaboral is not null)
                {
                    refLaboral.Verificado = item.Verificado;
                    await referenciaPersonalCandidatoRepository.Update(refLaboral);
                }
            }
        }

        public async Task<List<CandidatoResponse>> GetAllCandidatos()
        {
            List<CandidatoResponse> listCantidatos;
            try
            {
                var list = await candidatoRepository.GetAllByParamIncluding(null, (i => i.Vacante), (i => i.Vacante.Cliente));

                listCantidatos = mapper.Map<List<CandidatoResponse>>(list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listCantidatos;
        }

        public async Task<List<CandidatoResponse>> GetAllContratados()
        {
            List<CandidatoResponse> listCantidatos;
            try
            {
                var list = await candidatoRepository.GetAllByParamIncluding(p => p.Estado.Description == "Contratado",
                    (i => i.Vacante),
                    (i => i.Vacante.Cliente),
                    (i => i.Estado)
                );

                listCantidatos = mapper.Map<List<CandidatoResponse>>(list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listCantidatos;
        }

        public async Task<List<EstudioCandidatoResponse>> GetAllEstudiosCandidato(int idCandidato)
        {
            var listEstudiosResponse = new List<EstudioCandidatoResponse>();
            try
            {
                var listEstudios = await estudioCandidatoRepository.GetListByParam(x => x.IdCandidato == idCandidato && x.Activo);
                listEstudiosResponse = MappeListEstudiosCandidatos(listEstudios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listEstudiosResponse;
        }

        /// <summary>
        /// Obtener candidatos por identificacion
        /// </summary>
        /// <param name="document">Documento de identificacion</param>
        /// <returns></returns>
        public async Task<List<CandidatoResponse>> GetByDocument(string document)
        {
            var listResponse = new List<CandidatoResponse>();

            try
            {
                var listCandidate = await candidatoRepository.GetAllByParamIncluding(x => x.Documento.Contains(document),
                    (i => i.Vacante),
                    (i => i.UserCreated),
                    (i => i.Vacante.Cliente),
                    (i => i.TipoDocumento),
                    (i => i.Estado)
                    );

                listResponse = mapper.Map<List<CandidatoResponse>>(listCandidate);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listResponse;
        }

        /// <summary>
        /// Obtener candidato por el id
        /// </summary>
        /// <param name="id">Id del cantidato</param>
        /// <returns></returns>
        public async Task<CandidatoResponse> GetById(int id)
        {
            try
            {
                var candidate = await candidatoRepository.GetAllByParamIncluding(f => f.IdCandidato == id,
                    (i => i.Vacante),
                    (i => i.UserCreated),
                    (i => i.Vacante.Cliente),
                    (i => i.TipoDocumento));
                var candidateResponse = mapper.Map<CandidatoResponse>(candidate.First());

                return candidateResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtener candidatos por id de vacante
        /// </summary>
        /// <param name="id">Id de vacante</param>
        /// <returns></returns>
        public async Task<List<CandidatoResponse>> GetByIdVacante(int id)
        {
            try
            {
                var candidates = await candidatoRepository.GetAllByParamIncluding(f => f.IdVacante == id, (i => i.Vacante), (i => i.UserCreated), (i => i.Vacante.Cliente));
                var candidatesResponse = mapper.Map<List<CandidatoResponse>>(candidates);

                return candidatesResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<EstudioCandidatoResponse> MappeListEstudiosCandidatos(List<EstudioCandidato> estudioCandidatos)
        {
            var listEstudiosResponse = new List<EstudioCandidatoResponse>();

            foreach (var item in estudioCandidatos)
            {
                var cantidato = mapper.Map<EstudioCandidatoResponse>(item);
                listEstudiosResponse.Add(cantidato);
            }
            return listEstudiosResponse;
        }

        public async Task<List<ReferenciasPersonalesResponse>> GetAllRefPersonalesCandidato(int idCandidato)
        {
            var listRefPersonalesResponse = new List<ReferenciasPersonalesResponse>();
            try
            {
                var listRefPersonales = await referenciaPersonalCandidatoRepository.GetListByParam(x => x.IdCandidato == idCandidato);
                listRefPersonalesResponse = MappeListRefPersonalesCandidatos(listRefPersonales);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listRefPersonalesResponse;
        }

        private List<ReferenciasPersonalesResponse> MappeListRefPersonalesCandidatos(List<ReferenciaPersonalCandidato> lista)
        {
            var listRefPersonalesResponse = new List<ReferenciasPersonalesResponse>();
            foreach (var item in lista)
            {
                var cantidato = mapper.Map<ReferenciasPersonalesResponse>(item);
                listRefPersonalesResponse.Add(cantidato);
            }
            return listRefPersonalesResponse;
        }

        public async Task<List<ReferenciasLaboralesResponse>> GetAllRefLaboralesCandidato(int idCandidato)
        {
            var listRefLaboralesResponse = new List<ReferenciasLaboralesResponse>();
            try
            {
                var listRefLaborales = await referenciaLaboralCandidatoRepository.GetListByParam(x => x.IdCandidato == idCandidato);

                foreach (var item in listRefLaborales)
                {
                    var cantidato = mapper.Map<ReferenciasLaboralesResponse>(item);
                    listRefLaboralesResponse.Add(cantidato);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listRefLaboralesResponse;
        }
    }
}