using ApiManejoRRHH.Helpers;
using Core.Interfaces;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiManejoRRHH.Controllers
{
    /// <summary>
    /// Controlador de Rol
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolController : ControllerBase
    {
        private readonly IRolService rolService;

        /// <summary>
        /// Constructor
        /// </summary>
        public RolController(IRolService rolService)
        {
            this.rolService = rolService;
        }

        /// <summary>
        /// Obtener Roles
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
                var roles = await rolService.GetAll();
                return Ok(roles);
            }
            catch (Exception)
            {
                return Problem();
            }
        }
    }
}