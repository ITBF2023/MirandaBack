using ApiManejoRRHH.Helpers;
using Core.Interfaces;
using Core.Repository;
using Domain.Common;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiManejoRRHH.Controllers
{
    /// <summary>
    /// Controlador de documento adjunto del candidato
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DocumentoAdjuntoController : ControllerBase
    {
        private readonly IDocumentoAdjuntoService documentoAdjuntoService;

        /// <summary>
        /// Constructor
        /// </summary>
        public DocumentoAdjuntoController(IDocumentoAdjuntoService documentoAdjuntoService)
        {
            this.documentoAdjuntoService = documentoAdjuntoService;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="candidatoRequest"></param>
        /// <returns></returns>
        [HttpPost, Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] DocumentoAdjuntoRequest request)
        {
            try
            {
                var documento = await documentoAdjuntoService.Create(request);
                if (documento.StatusCode == HttpStatusCode.OK)
                    return Ok(documento);
                else
                    return Problem(detail: documento.Message, statusCode: (int)documento.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="candidatoRequest"></param>
        /// <returns></returns>
        [HttpPut, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] DocumentoAdjuntoRequest request)
        {
            try
            {
                var documento = await documentoAdjuntoService.Update(request);
                if (documento.StatusCode == HttpStatusCode.OK)
                    return Ok(documento);
                else
                    return Problem(detail: documento.Message, statusCode: (int)documento.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }

        /// <summary>
        /// Obtiene los documentos adjuntos del candidato
        /// </summary>
        /// <param name="idCandidato"></param>
        /// <returns></returns>
        [HttpGet, Route("[action]/{idCandidato}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByCandidato(int idCandidato)
        {
            try
            {
                var documentos = await documentoAdjuntoService.GetByCandidato(idCandidato);
                return Ok(documentos);
            }
            catch (Exception)
            {
                return Problem();
            }
        }

       
        /// <summary>
        /// Obtiene el documento asociado al id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var documento = await documentoAdjuntoService.GetById(id);
                return Ok(documento);
            }
            catch (Exception)
            {
                return Problem();
            }
        }

        /// <summary>
        /// Obtiene 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDocumentosEntregados()
        {
            try
            {
                var documentos = await documentoAdjuntoService.GetDocumentosEntregados();
                return Ok(documentos);
            }
            catch (Exception)
            {
                return Problem();
            }
        }
    }
}