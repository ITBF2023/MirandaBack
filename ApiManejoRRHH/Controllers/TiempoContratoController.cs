using ApiManejoRRHH.Helpers;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiManejoRRHH.Controllers
{
    /// <summary>
    /// Controlador de tiempo contrato
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TiempoContratoController : ControllerBase
    {
        private readonly ITiempoContratoService tiempoContratoService;

        /// <summary>
        /// Constructor
        /// </summary>
        public TiempoContratoController(ITiempoContratoService tiempoContratoService)
        {
            this.tiempoContratoService = tiempoContratoService;
        }

        /// <summary>
        /// Obtener tiempo de contratos
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
                var tiempos = await tiempoContratoService.GetAll();
                return Ok(tiempos);
            }
            catch (Exception)
            {
                return Problem();
            }
        }
    }
}