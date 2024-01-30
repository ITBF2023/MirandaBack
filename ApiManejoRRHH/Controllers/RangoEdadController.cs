using ApiManejoRRHH.Helpers;
using Core.Interfaces;
using Domain.Common.Enum;
using Microsoft.AspNetCore.Mvc;

namespace ApiManejoRRHH.Controllers
{
    /// <summary>
    /// Controlador de rangos de edad
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RangoEdadController : ControllerBase
    {
        private readonly ITipoTableService<object> tipoTableService;

        /// <summary>
        /// Constructor
        /// </summary>
        public RangoEdadController(ITipoTableService<object> tipoTableService)
        {
            this.tipoTableService = tipoTableService;
        }

        /// <summary>
        /// Obtener rango edad
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
                var rangoEdad = await tipoTableService.GetList(TipoTabla.RangoEdad);
                return Ok(rangoEdad);
            }
            catch (Exception)
            {
                return Problem();
            }
        }
    }
}