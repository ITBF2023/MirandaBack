using ApiManejoRRHH.Helpers;
using Core.Interfaces;
using Domain.Common;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiManejoRRHH.Controllers
{
    /// <summary>
    /// Controlador de llamado de atencion
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LlamadoAtencionController : ControllerBase
    {
        private readonly ILlamadoAtencionService llamadoAtencionService;

        /// <summary>
        /// Constructor
        /// </summary>
        public LlamadoAtencionController(ILlamadoAtencionService llamadoAtencionService)
        {
            this.llamadoAtencionService = llamadoAtencionService;
        }

        /// <summary>
        /// Metodo de creacion de llamado de atencion
        /// </summary>
        ///<param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("[action]")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] LlamadoAtencionRequest request)
        {
            try
            {
                var response = await llamadoAtencionService.Create(request);
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
        /// Obtener llamado de atencion
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
                var response = await llamadoAtencionService.GetAll();
                return Ok(response);
            }
            catch (Exception)
            {
                return Problem();
            }
        }

        /// <summary>
        /// Obtener llamado de atencion por empleado
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
                var response = await llamadoAtencionService.GetByEmpleado(id);
                return Ok(response);
            }
            catch (Exception)
            {
                return Problem();
            }
        }
    }
}