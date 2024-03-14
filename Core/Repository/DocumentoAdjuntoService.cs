using AutoMapper;
using Core.Common;
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
    public class DocumentoAdjuntoService : IDocumentoAdjuntoService
    {
        private readonly IRepository<DocumentoAdjunto> documentoAdjuntoRepository;
        private readonly IRepository<Configuracion> configuracionRepository;
        private readonly IRepository<TipoArchivo> tipoArchivoRepository;

        private readonly IMapper mapper;
        private readonly ManejoRHContext manejoRHContext;

        public DocumentoAdjuntoService(IRepository<DocumentoAdjunto> documentoAdjuntoRepository,
            IRepository<Configuracion> configuracionRepository, IRepository<TipoArchivo> tipoArchivoRepository,
            IMapper mapper, ManejoRHContext manejoRHContext)
        {
            this.documentoAdjuntoRepository = documentoAdjuntoRepository;
            this.tipoArchivoRepository = tipoArchivoRepository;
            this.configuracionRepository = configuracionRepository;
            this.mapper = mapper;
            this.manejoRHContext = manejoRHContext;
        }

        public async Task<BaseResponse> Create(DocumentoAdjuntoRequest request)
        {
            var outPut = new BaseResponse();
            var strategy = manejoRHContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                try
                {
                    var tipoArchivo = tipoArchivoRepository.GetById(request.IdTipoArchivo).Result;

                    var documento = mapper.Map<DocumentoAdjunto>(request);
                    documento.Nombre = string.IsNullOrEmpty(request.Archivo) ? null : await GetPathDocsPdf(request.Archivo, tipoArchivo.CodigoPath);
                    documento.FechaCreacion = DateTime.Now;

                    await documentoAdjuntoRepository.Insert(documento);

                    outPut = new BaseResponse()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Documento adjunto creado con exito"
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

        public async Task<BaseResponse> Update(DocumentoAdjuntoRequest request)
        {
            var outPut = new BaseResponse();
            var strategy = manejoRHContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                try
                {
                    var updateResult = await documentoAdjuntoRepository.GetById(request.Id);
                    if (updateResult != null)
                    {
                        var tipoArchivo = tipoArchivoRepository.GetById(request.IdTipoArchivo).Result;

                        updateResult.Nombre = string.IsNullOrEmpty(request.Archivo) ? null : await GetPathDocsPdf(request.Archivo, tipoArchivo.CodigoPath);
                        updateResult.IdTipoArchivo = request.IdTipoArchivo;

                        outPut = new BaseResponse()
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = "Documento adjunto actualizado con exito"
                        };
                    }
                }
                catch (Exception ex)
                {
                    outPut.StatusCode = HttpStatusCode.InternalServerError;
                    outPut.Message = ex.Message;
                }
            });

            return outPut;
        }

        public async Task<List<DocumentoAdjuntoResponse>> GetByCandidato(int idCandidato)
        {
            var listResponse = new List<DocumentoAdjuntoResponse>();

            try
            {
                var listCandidate = await documentoAdjuntoRepository.GetAllByParamIncluding(x => x.IdCandidato == idCandidato,
                    (i => i.UsuarioCreacion),
                    (i => i.TipoArchivo));

                listResponse = mapper.Map<List<DocumentoAdjuntoResponse>>(listCandidate);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listResponse;
        }

        public async Task<DocumentoAdjuntoResponse> GetById(int id)
        {
            try
            {
                var documentoAdjunto = await documentoAdjuntoRepository.GetAllByParamIncluding(f => f.Id == id,
                    (i => i.UsuarioCreacion),
                    (i => i.TipoArchivo));

                var response = mapper.Map<DocumentoAdjuntoResponse>(documentoAdjunto.First());

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DocumentoEntregadoResponse>> GetDocumentosEntregados()
        {
            try
            {
                List<DocumentoAdjunto> documentoAdjunto = await documentoAdjuntoRepository.GetAllByParamIncluding(null,
                    (i => i.Candidato));

                List<DocumentoEntregadoResponse> response = (from item in documentoAdjunto
                                 group item by item.IdCandidato into g
                                 select new DocumentoEntregadoResponse { 
                                     IdCandidato = g.First().Id,
                                     NombreCandidato = string.Format("{0} {1} {2} {3}", 
                                        g.First().Candidato.PrimerNombre,
                                        g.First().Candidato.SegundoNombre,
                                        g.First().Candidato.PrimerApellido,
                                        g.First().Candidato.SegundoApellido
                                     ),
                                     DocumentosCargados = g.Count()
                                 }).ToList();

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Privados

        private async Task<string> GetPathDocsPdf(string base64File, string path)
        {
            var saveFile = new SaveFiles();
            var pathLogos = (await configuracionRepository.GetByParam(x => x.Id.Equals(path)))?.Value ?? string.Empty;

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

            var nameFile = Guid.NewGuid().ToString();

            objectFileSave.FileName = $"{nameFile}.pdf";
            var pathFile = saveFile.SaveFileBase64(objectFileSave);
            return objectFileSave.FileName;
        }

        #endregion

    }
}