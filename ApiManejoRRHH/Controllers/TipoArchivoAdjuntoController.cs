using ApiManejoRRHH.Helpers;
using Core.Interfaces;
using Domain.Common.Enum;
using Microsoft.AspNetCore.Mvc;

namespace ApiManejoRRHH.Controllers
{
    /// <summary>
    /// Controlador de tipo documento contrato
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TipoArchivoAdjuntoController : ControllerBase
    {
        private readonly ITipoTableService<object> tipoTableService;

        /// <summary>
        /// Constructor
        /// </summary>
        public TipoArchivoAdjuntoController(ITipoTableService<object> tipoTableService)
        {
            this.tipoTableService = tipoTableService;
        }

        /// <summary>
        /// Obtener los tipos de documento de contrato
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
                var rangoEdad = await tipoTableService.GetList(TipoTabla.TipoArchivo);
                return Ok(rangoEdad);
            }
            catch (Exception)
            {
                return Problem();
            }
        }
    }
}