using Core.Interfaces;
using Domain.Common.Enum;
using Domain.Common;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ApiManejoRRHH.Helpers;

namespace ApiManejoRRHH.Controllers
{
    /// <summary>
    /// Controlador de Falla Injustificada
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FallaInjustificadaController : ControllerBase
    {
        private readonly IFallaInjustificadaService fallaInjustificadaService;

        /// <summary>
        /// Constructor
        /// </summary>

        public FallaInjustificadaController(IFallaInjustificadaService fallaInjustificadaService)
        {
            this.fallaInjustificadaService = fallaInjustificadaService;
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
        public async Task<IActionResult> Create([FromBody] FallaInjustificadaRequest request)
        {
            try
            {
                var response = await fallaInjustificadaService.Create(request);
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
                var response = await fallaInjustificadaService.GetAll();
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
                var response = await fallaInjustificadaService.GetByEmpleado(id);
                return Ok(response);
            }
            catch (Exception)
            {
                return Problem();
            }
        }
    }
}