using ApiManejoRRHH.Helpers;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiManejoRRHH.Controllers
{
    /// <summary>
    /// Controlador de competencia empleado
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompetenciaEmpleadoController : ControllerBase
    {
        private readonly ICompetenciaEmpleadoService competenciaEmpleadoService;

        public CompetenciaEmpleadoController(ICompetenciaEmpleadoService competenciaEmpleadoService)
        {
            this.competenciaEmpleadoService = competenciaEmpleadoService;
        }

        [HttpGet, Route("[action]/{idEmpleado}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCompetenciaLaboral(long idEmpleado)
        {
            try
            {
                var employee = await competenciaEmpleadoService.GetCompetenciaLaboral(idEmpleado);
                return Ok(employee);
            }
            catch (Exception)
            {
                return Problem();
            }
        }

        [HttpGet, Route("[action]/{idEmpleado}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCompetenciaPersonal(long idEmpleado)
        {
            try
            {
                var employee = await competenciaEmpleadoService.GetCompetenciaPersonal(idEmpleado);
                return Ok(employee);
            }
            catch (Exception)
            {
                return Problem();
            }
        }
    }
}