using ApiManejoRRHH.Helpers;
using Core.Interfaces;
using Domain.Common;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiManejoRRHH.Controllers
{
    /// <summary>
    /// Controlador de proceso Diciplinario
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProcesoDiciplinarioController : ControllerBase
    {
        private readonly IProcesoDiciplinarioService procesoDiciplinarioService;

        /// <summary>
        /// Constructor
        /// </summary>

        public ProcesoDiciplinarioController(IProcesoDiciplinarioService procesoDiciplinarioService)
        {
            this.procesoDiciplinarioService = procesoDiciplinarioService;
        }

        /// <summary>
        /// Metodo de creacion de falla injustificada
        /// </summary>
        ///<param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ProcesoDiciplinarioRequest request)
        {
            try
            {
                var response = await procesoDiciplinarioService.Create(request);
                if (response.StatusCode == HttpStatusCode.OK)
                    return Ok(response);
                else
                    return Problem(response.Message, statusCode: (int)response.StatusCode);
            }
            catch (Exception)
            {
                return Problem();
            }
        }

        /// <summary>
        /// Obtener falla injustificada
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await procesoDiciplinarioService.GetAll();
                return Ok(response);
            }
            catch (Exception)
            {
                return Problem();
            }
        }

        /// <summary>
        /// Obtener falla injustificada por empleado
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdEmpleado(int id)
        {
            try
            {
                var response = await procesoDiciplinarioService.GetByEmpleado(id);
                return Ok(response);
            }
            catch (Exception)
            {
                return Problem();
            }
        }
    }
}